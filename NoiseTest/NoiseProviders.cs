using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoiseTest
{
    public class NoiseProviders
    {
        public static readonly INoiseProvider Simplex = new SimplexProvider();
        public static readonly INoiseProvider Simpletic = new SimpleticProvider();
        public static readonly INoiseProvider OpenSimplex = new OpenSimplexProvider();

        private class SimplexProvider : INoiseProvider
        {
            public double BaseScale { get { return 1.0; } }

            public IFunction2D Create2D(Random rng)
            {
                return new SimplexNoise(rng);
            }

            public IFunction3D Create3D(Random rng)
            {
                return new SimplexNoise(rng);
            }

            public IFunction4D Create4D(Random rng)
            {
                return new SimplexNoise(rng);
            }
        }

        private class SimpleticProvider : INoiseProvider
        {
            public double BaseScale { get { return 1.0; } }

            public IFunction2D Create2D(Random rng)
            {
                return new SimpleticNoise(rng.Next());
            }

            public IFunction3D Create3D(Random rng)
            {
                return new SimpleticNoise(rng.Next());
            }

            public IFunction4D Create4D(Random rng)
            {
                return new SimpleticNoise(rng.Next());
            }
        }

        private class OpenSimplexProvider : INoiseProvider
        {
            public double BaseScale { get { return 2.0; } }

            public IFunction2D Create2D(Random rng)
            {
                return new OpenSimplexNoise(rng.Next());
            }

            public IFunction3D Create3D(Random rng)
            {
                return new OpenSimplexNoise(rng.Next());
            }

            public IFunction4D Create4D(Random rng)
            {
                return new OpenSimplexNoise(rng.Next());
            }
        }
    }
}
