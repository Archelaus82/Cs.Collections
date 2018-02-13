using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cs.Collections
{
    public class Vector2
    {
        private double _x;
        private double _y;

        public Vector2() { }

        public Vector2(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public Vector2(Vector2 point)
        {
            _x = point.X;
            _y = point.Y;
        }

        public double Y { get { return _y; } }
        public double X { get { return _x; } }

        public override string ToString()
        {
            return String.Format("[x.{0}, y.{1}]", _x, _y);
        }
    }
}
