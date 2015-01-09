// Ported from: https://github.com/bjz/noise-rs/blob/master/src/simplectic.rs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NoiseTest
{
    public class SimpleticNoise : IFunction2D, IFunction3D, IFunction4D
    {
        private const double skew_constant = 0.36602540378; // 0.5 * (sqrt(3.0) - 1.0)
        private const double unskew_constant = 0.2113248654; // (3.0 - sqrt(3.0)) / 6.0

        private const double simplex_size = 0.70710678119;
        private const double inv_simplex_size = 1.41421356235; // 1 / simplex_size()
        private const double layer_offset_x = 0.45534180126; // (2.0 - 3.0 * unskew_constant()) / 3.0
        private const double layer_offset_y = 0.12200846793; // (1.0 - 3.0 * unskew_constant()) / 3.0
        private const double layer_offset_z = 0.35355339059; // (1.0 - 3.0 * unskew_constant()) / 3.0

        private const double norm2_constant = 8.0;
        private const double norm3_constant = 9.0;
        private const double norm4_constant = 10.0;

        private const int TABLE_SIZE = 256;

        private int[] _perm;

        public SimpleticNoise(long seed)
        {
            _perm = new int[256];
            short[] source = new short[256];
            for (short i = 0; i < 256; i++)
                source[i] = i;
            seed = seed * 6364136223846793005L + 1442695040888963407L;
            seed = seed * 6364136223846793005L + 1442695040888963407L;
            seed = seed * 6364136223846793005L + 1442695040888963407L;
            for (int i = 255; i >= 0; i--)
            {
                seed = seed * 6364136223846793005L + 1442695040888963407L;
                int r = (int)((seed + 31) % (i + 1));
                if (r < 0)
                    r += (i + 1);
                _perm[i] = source[r];
                source[r] = source[i];
            }
            _perm = _perm.Concat(_perm).ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private SimplecticPoint2[] simplectic2_points(Vector2 point)
        {
            // Skew the input coordinates into the grid to figure out which grid cell we're in
            var skew_offset = (point.X + point.Y) * skew_constant;
            var x_cell = (long)Math.Floor(point.X + skew_offset);
            var y_cell = (long)Math.Floor(point.Y + skew_offset);

            // Unskew the floored coordinates to find the real coordinates of the cell's origin
            var unskew_offset = (x_cell + y_cell) * unskew_constant;
            var x_origin = x_cell - unskew_offset;
            var y_origin = y_cell - unskew_offset;

            // Compute the delta from the first point, which is the cell origin
            var dx0 = point.X - x_origin;
            var dy0 = point.Y - y_origin;

            // Compute the delta from the second point, which depends on which simplex we're in
            long x1_offset, y1_offset;
            if (dx0 > dy0)
            {
                x1_offset = 1;
                y1_offset = 0;
            }
            else
            {
                x1_offset = 0;
                y1_offset = 1;
            }

            var dx1 = dx0 - x1_offset + unskew_constant;
            var dy1 = dy0 - y1_offset + unskew_constant;

            // Compute the delta from the third point
            var dx2 = dx0 - 1 + 2 * unskew_constant;
            var dy2 = dy0 - 1 + 2 * unskew_constant;

            return new SimplecticPoint2[]
            {
                new SimplecticPoint2
                {
                    x_cell = x_cell,
                    y_cell = y_cell,
                    x_offset = dx0,
                    y_offset = dy0
                },
                new SimplecticPoint2
                {
                    x_cell = x_cell + x1_offset,
                    y_cell = y_cell + y1_offset,
                    x_offset = dx1,
                    y_offset = dy1
                },
                new SimplecticPoint2
                {
                    x_cell = 1 + x_cell,
                    y_cell = 1 + y_cell,
                    x_offset = dx2,
                    y_offset = dy2
                }
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private SimplecticPoint3[] simplectic3_points(Vector3 point)
        {
            var layer = Math.Floor(point.Z * inv_simplex_size);
            var layer_int = (long)layer;

            Vector2 layer1_point, layer2_point;
            if (layer_int % 2 == 0)
            {
                layer1_point = new Vector2(point.X, point.Y);
                layer2_point = new Vector2(point.X + layer_offset_x, point.Y + layer_offset_y);
            }
            else
            {
                layer1_point = new Vector2(point.X + layer_offset_x, point.Y + layer_offset_y);
                layer2_point = new Vector2(point.X, point.Y);

            }

            var s1 = simplectic2_points(layer1_point);
            var s2 = simplectic2_points(layer2_point);

            var z_offset = point.Z - layer * simplex_size;
            return new SimplecticPoint3[]
            {
                new SimplecticPoint3(s1[0], layer_int, z_offset),
                new SimplecticPoint3(s1[1], layer_int, z_offset),
                new SimplecticPoint3(s1[2], layer_int, z_offset),
                new SimplecticPoint3(s2[0], layer_int + 1, z_offset - simplex_size),
                new SimplecticPoint3(s2[1], layer_int + 1, z_offset - simplex_size),
                new SimplecticPoint3(s2[2], layer_int + 1, z_offset - simplex_size)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private SimplecticPoint4[] simplectic4_points(Vector4 point)
        {
            var layer = Math.Floor(point.W * inv_simplex_size);
            var layer_int = (long)layer;

            Vector3 layer1_point, layer2_point;
            if (layer_int % 2 == 0)
            {
                layer1_point = new Vector3(point.X, point.Y, point.Z);
                layer2_point = new Vector3(point.X + layer_offset_x, point.Y + layer_offset_y, point.Z + layer_offset_z);
            }
            else
            {
                layer1_point = new Vector3(point.X + layer_offset_x, point.Y + layer_offset_y, point.Z + layer_offset_z);
                layer2_point = new Vector3(point.X, point.Y, point.Z);
            }

            var s1 = simplectic3_points(layer1_point);
            var s2 = simplectic3_points(layer2_point);

            var w_offset = point.W - layer * simplex_size;
            return new SimplecticPoint4[]
            {
                new SimplecticPoint4(s1[0], layer_int, w_offset),
                new SimplecticPoint4(s1[1], layer_int, w_offset),
                new SimplecticPoint4(s1[2], layer_int, w_offset),
                new SimplecticPoint4(s1[3], layer_int, w_offset),
                new SimplecticPoint4(s1[4], layer_int, w_offset),
                new SimplecticPoint4(s1[5], layer_int, w_offset),
                new SimplecticPoint4(s2[0], layer_int + 1, w_offset - simplex_size),
                new SimplecticPoint4(s2[1], layer_int + 1, w_offset - simplex_size),
                new SimplecticPoint4(s2[2], layer_int + 1, w_offset - simplex_size),
                new SimplecticPoint4(s2[3], layer_int + 1, w_offset - simplex_size),
                new SimplecticPoint4(s2[4], layer_int + 1, w_offset - simplex_size),
                new SimplecticPoint4(s2[5], layer_int + 1, w_offset - simplex_size)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int signed_modulus(long a, long b)
        {
            return (int)(a < 0 ? b - (Math.Abs(a) % b) : a % b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int SeedGet1(long x)
        {
            return _perm[signed_modulus(x, TABLE_SIZE)];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int SeedGet2(long x, long y)
        {
            return _perm[signed_modulus(y, TABLE_SIZE) + SeedGet1(x)];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int SeedGet3(long x, long y, long z)
        {
            return _perm[signed_modulus(z, TABLE_SIZE) + SeedGet2(x, y)];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int SeedGet4(long x, long y, long z, long w)
        {
            return _perm[signed_modulus(w, TABLE_SIZE) + SeedGet3(x, y, z)];
        }

        private const double diag2 = 0.70710678118;
        private const double diag3 = 0.70710678118;
        private const double diag4 = 0.57735026919;
        private const double one = 1.0;
        private const double zero = 0.0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Vector2 GradientGet2(int index)
        {
            switch (index % 8)
            {
                case 0: return new Vector2(diag2, diag2);
                case 1: return new Vector2(diag2, -diag2);
                case 2: return new Vector2(-diag2, diag2);
                case 3: return new Vector2(-diag2, -diag2);
                case 4: return new Vector2(one, zero);
                case 5: return new Vector2(-one, zero);
                case 6: return new Vector2(zero, one);
                case 7: return new Vector2(zero, -one);
                default: throw new InvalidOperationException();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Vector3 GradientGet3(int index)
        {
            switch (index % 12)
            {
                case 0: return new Vector3(diag3, diag3, zero);
                case 1: return new Vector3(diag3, -diag3, zero);
                case 2: return new Vector3(-diag3, diag3, zero);
                case 3: return new Vector3(-diag3, -diag3, zero);
                case 4: return new Vector3(diag3, zero, diag3);
                case 5: return new Vector3(diag3, zero, -diag3);
                case 6: return new Vector3(-diag3, zero, diag3);
                case 7: return new Vector3(-diag3, zero, -diag3);
                case 8: return new Vector3(zero, diag3, diag3);
                case 9: return new Vector3(zero, diag3, -diag3);
                case 10: return new Vector3(zero, -diag3, diag3);
                case 11: return new Vector3(zero, -diag3, -diag3);
                default: throw new InvalidOperationException();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Vector4 GradientGet4(int index)
        {
            switch (index % 32)
            {
                case 0: return new Vector4(diag4, diag4, diag4, zero);
                case 1: return new Vector4(diag4, -diag4, diag4, zero);
                case 2: return new Vector4(-diag4,  diag4,  diag4,  zero);
                case 3: return new Vector4(-diag4, -diag4, diag4, zero);
                case 4: return new Vector4(diag4, diag4, -diag4, zero);
                case 5: return new Vector4(diag4, -diag4, -diag4, zero);
                case 6: return new Vector4(-diag4,  diag4, -diag4,  zero);
                case 7: return new Vector4(-diag4, -diag4, -diag4, zero);
                case 8: return new Vector4(diag4, diag4, zero, diag4);
                case 9: return new Vector4(diag4, -diag4, zero, diag4);
                case 10: return new Vector4(-diag4, diag4, zero, diag4);
                case 11: return new Vector4(-diag4, -diag4, zero, diag4);
                case 12: return new Vector4(diag4, diag4, zero, -diag4);
                case 13: return new Vector4(diag4, -diag4, zero, -diag4);
                case 14: return new Vector4(-diag4, diag4, zero, -diag4);
                case 15: return new Vector4(-diag4, -diag4, zero, -diag4);
                case 16: return new Vector4(diag4, zero, diag4, diag4);
                case 17: return new Vector4(diag4, zero, -diag4, diag4);
                case 18: return new Vector4(-diag4, zero, diag4, diag4);
                case 19: return new Vector4(-diag4, zero, -diag4, diag4);
                case 20: return new Vector4(diag4, zero, diag4, -diag4);
                case 21: return new Vector4(diag4, zero, -diag4, -diag4);
                case 22: return new Vector4(-diag4, zero, diag4, -diag4);
                case 23: return new Vector4(-diag4,  zero, -diag4, -diag4);
                case 24: return new Vector4(zero, diag4, diag4, diag4);
                case 25: return new Vector4(zero, diag4, -diag4, diag4);
                case 26: return new Vector4(zero, -diag4, diag4, diag4);
                case 27: return new Vector4(zero, -diag4, -diag4, diag4);
                case 28: return new Vector4(zero, diag4, diag4, -diag4);
                case 29: return new Vector4(zero, diag4, -diag4, -diag4);
                case 30: return new Vector4(zero, -diag4, diag4, -diag4);
                case 31: return new Vector4(zero, -diag4, -diag4, -diag4);
                default: throw new InvalidOperationException();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double gradient(SimplecticPoint2 p)
        {
            var attn = simplex_size - p.x_offset * p.x_offset - p.y_offset * p.y_offset;
            if (attn > 0.0)
            {
                var g = GradientGet2(SeedGet2(p.x_cell, p.y_cell));
                var attn2 = attn * attn;
                return attn2 * attn2 * (p.x_offset * g.X + p.y_offset * g.Y);
            }
            else
            {
                return 0.0;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double gradient(SimplecticPoint3 p)
        {
            var attn = simplex_size - p.x_offset * p.x_offset - p.y_offset * p.y_offset - p.z_offset * p.z_offset;
            if (attn > 0.0)
            {
                var g = GradientGet3(SeedGet3(p.x_cell, p.y_cell, p.z_cell));
                var attn2 = attn * attn;
                return attn2 * attn2 * (p.x_offset * g.X + p.y_offset * g.Y + p.z_offset * g.Z);
            }
            else
            {
                return 0.0;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double gradient(SimplecticPoint4 p)
        {
            var attn = simplex_size - p.x_offset * p.x_offset - p.y_offset * p.y_offset - p.z_offset * p.z_offset - p.w_offset * p.w_offset;
            if (attn > 0.0)
            {
                var g = GradientGet4(SeedGet4(p.x_cell, p.y_cell, p.z_cell, p.w_cell));
                var attn2 = attn * attn;
                return attn2 * attn2 * (p.x_offset * g.X + p.y_offset * g.Y + p.z_offset * g.Z + p.w_offset * g.W);
            }
            else
            {
                return 0.0;
            }
        }

        public double Evaluate(double x, double y)
        {
            var s = simplectic2_points(new Vector2(x, y));
            return (gradient(s[0]) + gradient(s[1]) + gradient(s[2])) * norm2_constant;
        }

        public double Evaluate(double x, double y, double z)
        {
            var s = simplectic3_points(new Vector3(x, y, z));
            return (gradient(s[0]) + gradient(s[1]) + gradient(s[2]) + gradient(s[3]) + gradient(s[4]) + gradient(s[5])) * norm3_constant;
        }

        public double Evaluate(double x, double y, double z, double w)
        {
            var s = simplectic4_points(new Vector4(x, y, z, w));
            return (gradient(s[0]) + gradient(s[1]) + gradient(s[2]) + gradient(s[3]) + gradient(s[4]) + gradient(s[5]) + gradient(s[6]) + gradient(s[7]) + gradient(s[8]) + gradient(s[9]) + gradient(s[10]) + gradient(s[11])) * norm4_constant;
        }

        public struct Vector2
        {
            public double X;
            public double Y;

            public Vector2(double x, double y)
            {
                X = x;
                Y = y;
            }
        }

        public struct Vector3
        {
            public double X;
            public double Y;
            public double Z;

            public Vector3(double x, double y, double z)
            {
                X = x;
                Y = y;
                Z = z;
            }
        }

        public struct Vector4
        {
            public double X;
            public double Y;
            public double Z;
            public double W;

            public Vector4(double x, double y, double z, double w)
            {
                X = x;
                Y = y;
                Z = z;
                W = w;
            }
        }

        private struct SimplecticPoint2
        {
            public long x_cell;
            public long y_cell;
            public double x_offset;
            public double y_offset;
        }

        private struct SimplecticPoint3
        {
            public long x_cell;
            public long y_cell;
            public long z_cell;
            public double x_offset;
            public double y_offset;
            public double z_offset;

            public SimplecticPoint3(SimplecticPoint2 source, long z_cell, double z_offset)
            {
                x_cell = source.x_cell;
                y_cell = source.y_cell;
                this.z_cell = z_cell;
                x_offset = source.x_offset;
                y_offset = source.y_offset;
                this.z_offset = z_offset;
            }
        }

        private struct SimplecticPoint4
        {
            public long x_cell;
            public long y_cell;
            public long z_cell;
            public long w_cell;
            public double x_offset;
            public double y_offset;
            public double z_offset;
            public double w_offset;

            public SimplecticPoint4(SimplecticPoint3 source, long w_cell, double w_offset)
            {
                x_cell = source.x_cell;
                y_cell = source.y_cell;
                z_cell = source.z_cell;
                this.w_cell = w_cell;
                x_offset = source.x_offset;
                y_offset = source.y_offset;
                z_offset = source.z_offset;
                this.w_offset = w_offset;
            }
        }
    }
}
