using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cs.Collections
{
    public class Line2
    {
        private double _m;
        private double _b;

        private Range<double> _xRange;
        private Range<double> _yRange;

        private Vector2 _point1;
        private Vector2 _point2;

        public Line2(Vector2 point1, Vector2 point2)
        {
            _point1 = point1;
            _point2 = point2;

            _Calculate(_point1, _point2);
        }

        public Line2(Line2 line)
        {
            _point1 = line.Point1;
            _point2 = line.Point2;
            _m = line.M;
            _b = line.B;
        }

        public double B { get { return _b; } }
        public double M { get { return _m; } }
        public Range<double> XRange { get { return _xRange; } }
        public Range<double> YRange { get { return _yRange; } }
        public Vector2 Point2 { get { return _point2; } }
        public Vector2 Point1 { get { return _point1; }  }

        private void _Calculate(Vector2 point1, Vector2 point2)
        {
            _m = (point2.Y - point1.Y) / (point2.X - point1.X);
            _b = point1.Y - (_m * point1.X);
            _xRange = new Range<double>(Math.Min(point1.X, point2.X), Math.Max(point1.X, point2.X));
            _yRange = new Range<double>(Math.Min(point1.Y, point2.Y), Math.Max(point1.Y, point2.Y));
        }

        public override string ToString()
        {
            return String.Format("[Y = {0} * X + {1}]", _m, _b);
        }

        public double GetY(double x)
        {
            double f = 1 / (((_xRange.Max - x) / (x - _xRange.Min)) + 1);
            return (f * _yRange.Max) + ((1 - f) * _yRange.Min);
        }

        public double GetX(double y)
        {
            double f = 1 / (((_yRange.Max - y) / (y - _yRange.Min)) + 1);
            return (f * _xRange.Max) + ((1 - f) * _xRange.Min);
        }

        public bool InYRange(double y)
        {
            if (_yRange.Contains(y)) return true;
            return false;
        }

        public bool InXRange(double x)
        {
            if (_xRange.Contains(x)) return true;
            return false;
        }
    }
}
