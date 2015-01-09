using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoiseTest
{
    public interface INoiseProvider
    {
        double BaseScale { get; }

        IFunction2D Create2D(Random rng);
        IFunction3D Create3D(Random rng);
        IFunction4D Create4D(Random rng);
    }
}
