using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoiseTest
{
    public interface IFunction2D
    {
        double Evaluate(double x, double y);
    }

    public interface IFunction3D
    {
        double Evaluate(double x, double y, double z);
    }

    public interface IFunction4D
    {
        double Evaluate(double x, double y, double z, double w);
    }
}
