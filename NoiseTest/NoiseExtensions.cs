using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NoiseTest
{
    public static class NoiseExtensions
    {
        public static IFunction2D Create2D(this INoiseProvider provider, Random rng, double scale)
        {
            return new Noise2DFunction(provider.Create2D(rng), scale * provider.BaseScale);
        }

        public static IFunction2D Create2D(this INoiseProvider provider, Random rng, double scale, double xPeriod, double yPeriod)
        {
            return new SeamlessNoise2DFunction(provider.Create4D(rng), scale * provider.BaseScale, xPeriod, yPeriod);
        }

        public static IFunction3D Create3D(this INoiseProvider provider, Random rng, double scale)
        {
            return new Noise3DFunction(provider.Create3D(rng), scale * provider.BaseScale);
        }

        public static IFunction2D Slice3D(this INoiseProvider provider, Random rng, double scale)
        {
            return new Slice3DFunction(provider.Create3D(rng), scale * provider.BaseScale);
        }

        public static IFunction2D Slice4D(this INoiseProvider provider, Random rng, double scale)
        {
            return new Slice4DFunction(provider.Create4D(rng), scale * provider.BaseScale);
        }

        public static IFunction2D FractionalBrownianMotion(this INoiseProvider provider, Random rng, double scale, int octaves, double persistence = 0.5, double lacunarity = 2.0, Func<double, double> octaveModifier = null, double? xPeriod = null, double? yPeriod = null, SampleMode mode = SampleMode.Sample2D)
        {
            return new FractionalBrownianMotionFunction(provider, rng, scale, octaves, persistence, lacunarity, octaveModifier, xPeriod, yPeriod, mode);
        }

        public static void FillHeightMap(this IFunction2D function, double[,] heightMap)
        {
            var width = heightMap.GetLength(0);
            var height = heightMap.GetLength(1);
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    heightMap[x, y] = function.Evaluate(x, y);
                }
            }
        }

        public static Bitmap RenderBitmap(this double[,] heightMap)
        {
            var width = heightMap.GetLength(0);
            var height = heightMap.GetLength(1);
            var bitmap = new Bitmap(width, height);
            var data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            try
            {
                var lockedBuffer = new byte[data.Stride * height];
                for (var y = 0; y < height; y++)
                {
                    var byteIndex = data.Stride * y;
                    for (var x = 0; x < width; x++)
                    {
                        var color = heightMap[x, y];
                        color = (color + 1.0) / 2.0;
                        byte value = (byte)(int)(color * 255);
                        lockedBuffer[byteIndex++] = value;
                        lockedBuffer[byteIndex++] = value;
                        lockedBuffer[byteIndex++] = value;
                        lockedBuffer[byteIndex++] = 255;
                    }
                }
                Marshal.Copy(lockedBuffer, 0, data.Scan0, lockedBuffer.Length);
            }
            finally
            {
                bitmap.UnlockBits(data);
            }
            return bitmap;
        }

        private class Noise2DFunction : IFunction2D
        {
            private IFunction2D _baseNoise;
            private double _scale;

            public Noise2DFunction(IFunction2D baseNoise, double scale)
            {
                _baseNoise = baseNoise;
                _scale = scale;
            }

            public double Evaluate(double x, double y)
            {
                return _baseNoise.Evaluate(x * _scale, y * _scale);
            }
        }

        public class SeamlessNoise2DFunction : IFunction2D
        {
            private IFunction4D _baseNoise;
            private double _xFactor;
            private double _yFactor;
            private double _xScale;
            private double _yScale;

            public SeamlessNoise2DFunction(IFunction4D baseNoise, double scale, double xPeriod, double yPeriod)
            {
                _baseNoise = baseNoise;
                _xFactor = (2.0 * Math.PI) / xPeriod;
                _yFactor = (2.0 * Math.PI) / yPeriod;
                _xScale = (xPeriod * scale) / (2.0 * Math.PI);
                _yScale = (yPeriod * scale) / (2.0 * Math.PI);
            }

            public double Evaluate(double x, double y)
            {
                x *= _xFactor;
                y *= _yFactor;
                var nx = Math.Cos(x) * _xScale;
                var ny = Math.Cos(y) * _yScale;
                var nz = Math.Sin(x) * _xScale;
                var nw = Math.Sin(y) * _yScale;
                return _baseNoise.Evaluate(nx, ny, nz, nw);
            }
        }

        public class Noise3DFunction : IFunction3D
        {
            private IFunction3D _baseNoise;
            private double _scale;

            public Noise3DFunction(IFunction3D baseNoise, double scale)
            {
                _baseNoise = baseNoise;
                _scale = scale;
            }

            public double Evaluate(double x, double y, double z)
            {
                return _baseNoise.Evaluate(x * _scale, y * _scale, z * _scale);
            }
        }

        public class Slice3DFunction : IFunction2D
        {
            private IFunction3D _baseNoise;
            private double _scale;

            public Slice3DFunction(IFunction3D baseNoise, double scale)
            {
                _baseNoise = baseNoise;
                _scale = scale;
            }

            public double Evaluate(double x, double y)
            {
                return _baseNoise.Evaluate(x * _scale, y * _scale, 0.0);
            }
        }

        public class Slice4DFunction : IFunction2D
        {
            private IFunction4D _baseNoise;
            private double _scale;

            public Slice4DFunction(IFunction4D baseNoise, double scale)
            {
                _baseNoise = baseNoise;
                _scale = scale;
            }

            public double Evaluate(double x, double y)
            {
                return _baseNoise.Evaluate(x * _scale, y * _scale, 0.0, 0.0);
            }
        }

        public class FractionalBrownianMotionFunction : IFunction2D
        {
            private Tuple<double, IFunction2D>[] _octaves;
            private double _maxAmplitude;
            private Func<double, double> _octaveModifier;

            public FractionalBrownianMotionFunction(INoiseProvider provider, Random rng, double scale, int octaves, double persistence = 0.5, double lacunarity = 2.0, Func<double, double> octaveModifier = null, double? xPeriod = null, double? yPeriod = null, SampleMode mode = SampleMode.Sample2D)
            {
                _octaveModifier = octaveModifier;
                _octaves = new Tuple<double, IFunction2D>[octaves];

                bool tiling = xPeriod != null || yPeriod != null;
                if (tiling && xPeriod == null)
                {
                    xPeriod = yPeriod;
                }
                if (tiling && yPeriod == null)
                {
                    yPeriod = xPeriod;
                }

                _maxAmplitude = 0.0;
                var amplitude = 1.0;
                var frequency = scale;

                for (var i = 0; i < octaves; i++)
                {
                    IFunction2D noise;
                    switch (mode)
                    {
                        case SampleMode.Sample2D:
                            noise = provider.Create2D(rng, frequency);
                            break;
                        case SampleMode.Slice3D:
                            noise = provider.Slice3D(rng, frequency);
                            break;
                        case SampleMode.Slice4D:
                            noise = provider.Slice4D(rng, frequency);
                            break;
                        case SampleMode.Tileable2D:
                            noise = provider.Create2D(rng, frequency, xPeriod.Value, yPeriod.Value);
                            break;
                        default:
                            throw new InvalidOperationException();
                    }
                    _octaves[i] = Tuple.Create(amplitude, noise);

                    _maxAmplitude += amplitude;
                    amplitude *= persistence;
                    frequency *= lacunarity;
                }
            }

            public double Evaluate(double x, double y)
            {
                var noise = 0.0;
                foreach (var octave in _octaves)
                {
                    var value = octave.Item2.Evaluate(x, y);
                    if (_octaveModifier != null)
                    {
                        value = _octaveModifier(value);
                    }
                    noise += value * octave.Item1;
                }

                return noise / _maxAmplitude;
            }
        }
    }
}
