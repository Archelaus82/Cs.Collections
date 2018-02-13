using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cs.Collections
{
    public class Range<t> where t : IComparable<t>
    {
        private t _min;
        private t _max;

        public Range(t min, t max)
        {
            if (!(min.CompareTo(max) <= 0))
                throw new Exception("Range min > max");
            _min = min;
            _max = max;
        }

        public Range(Range<t> range)
        {
            _min = range._min;
            _max = range._max;
        }

        public t Min
        {
            get { return _min; }
            set { _min = value; }
        }

        public t Max
        {
            get { return _max; }
            set { _max = value; }
        }

        public override string ToString()
        {
            return String.Format("[{0} - {1}]", _min, _max);
        }

        public bool Contains(t val)
        {
            return Min.CompareTo(val) <= 0 && val.CompareTo(Max) <= 0;
        }

        public Range<t> this[t min, t max] //specifies indexers
        {
            get { return new Range<t>(min, max); }
        }
    }
}
