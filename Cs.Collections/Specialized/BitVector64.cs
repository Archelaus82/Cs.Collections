using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections.Specialized
{
    public class BitVector64Exception : Exception
    {
        public BitVector64Exception()
        {
        }

        public BitVector64Exception(string message)
            : base(message)
        {
        }

        public BitVector64Exception(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class BitVector64
    {
        private UInt64 _data;

        public BitVector64() { }

        public BitVector64(UInt64 data)
        {
            _data = data;
        }

        public BitVector64(BitVector64 value)
        {
            _data = value._data;
        }

        public UInt64 Data { get { return _data; } }

        public override string ToString()
        {
            UInt64 temp = _data;
            string binary = (temp & 0x1).ToString();

            while ((temp >>= 1) != 0)
                binary = (temp & 0x1).ToString() + binary;

            return "0b" + binary.PadLeft(64, '0');
        }

        public static string ToString(BitVector64 value)
        {
            return value.ToString();
        }

        public UInt16 this[Section section]
        {
            get { return (UInt16)((_data >> section.Offset) & section.Mask); }
            set
            {
                UInt64 mask = (UInt64)(section.Mask << section.Offset);
                _data &= ~mask;
                mask = (UInt64)(value << section.Offset);
                _data |= mask;
            }
        }

        public bool this[int bit]
        {
            get { return Convert.ToBoolean((_data >> bit) & 0x1); }
            set
            {
                UInt64 mask = (UInt64)(1 << bit);
                _data &= ~mask;
                mask = Convert.ToUInt64(value) << bit;
                _data |= mask;
            }
        }

        public static Section CreateSection(UInt16 maxValue, Section previous)
        {
            int bits = 1;
            while ((maxValue >>= 1) != 0)
                bits++;

            int offset = 1;
            while ((previous.Mask >>= 1) != 0)
                offset++;

            Section section = new Section();
            section.Offset = (byte)(offset + previous.Offset);
            section.Mask = (UInt16)(Math.Pow(2, bits) - 1);

            return section;
        }

        public static Section CreateSection(UInt16 maxValue)
        {
            int bits = 1;
            while ((maxValue >>= 1) != 0)
                bits++;

            Section section = new Section();
            section.Offset = 0;
            section.Mask = (UInt16)(Math.Pow(2, bits) - 1);

            return section;
        }

        public static int CreateMask(int previous)
        {
            return previous + 1;
        }

        public static int CreateMask()
        {
            return 0;
        }


        public struct Section
        {
            public UInt16 Mask;
            public byte Offset;

            #region [ Public Methods ]

            public static bool operator !=(Section a, Section b)
            {
                if (a.Mask == b.Mask && a.Offset == b.Offset)
                    return false;
                else
                    return true;
            }

            public static bool operator ==(Section a, Section b)
            {
                if (a.Mask == b.Mask && a.Offset == b.Offset)
                    return true;
                else
                    return false;
            }

            public override bool Equals(object obj)
            {
                if (!(obj is Section))
                    return false;

                Section section = (Section)obj;

                return this.Mask.Equals(section.Mask) && this.Offset.Equals(section.Offset);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    int hash = 17;

                    hash = hash * 23 + Mask.GetHashCode();
                    hash = hash * 23 + Offset.GetHashCode();

                    return hash;
                }
            }

            public override string ToString()
            {
                return String.Format("Mask [{0}] Offset[{1}]", Mask, Offset);
            }

            public static string ToString(Section section)
            {
                return section.ToString();
            }

            #endregion
        }
    }
}
