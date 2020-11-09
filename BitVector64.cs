using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Diagnostics;
using System.ComponentModel;

namespace Connect4.Type
{
    /// <devdoc>
    ///    <para>Provides a simple light bit vector with easy integer or Boolean access to
    ///       a 64 bit storage.</para>
    /// </devdoc>
    public struct BitVector64 : ICloneable
    {
        private readonly static UInt64[] _MASK = new UInt64[]
        {
            0x01,
            0x02,
            0x04,
            0x08,
            0x10,
            0x20,
            0x40,
            0x80,
            0x100,
            0x200,
            0x400,
            0x800,
            0x1000,
            0x2000,
            0x4000,
            0x8000,
            0x10000,
            0x20000,
            0x40000,
            0x80000,
            0x100000,
            0x200000,
            0x400000,
            0x800000,
            0x1000000,
            0x2000000,
            0x4000000,
            0x8000000,
            0x10000000,
            0x20000000,
            0x40000000,
            0x80000000,
            0x100000000,
            0x200000000,
            0x400000000,
            0x800000000,
            0x1000000000,
            0x2000000000,
            0x4000000000,
            0x8000000000,
            0x10000000000,
            0x20000000000,
            0x40000000000,
            0x80000000000,
            0x100000000000,
            0x200000000000,
            0x400000000000,
            0x800000000000,
            0x1000000000000,
            0x2000000000000,
            0x4000000000000,
            0x8000000000000,
            0x10000000000000,
            0x20000000000000,
            0x40000000000000,
            0x80000000000000,
            0x100000000000000,
            0x200000000000000,
            0x400000000000000,
            0x800000000000000,
            0x1000000000000000,
            0x2000000000000000,
            0x4000000000000000,
            0x8000000000000000,
        };

        private readonly static Byte[] _REVERSE = new Byte[]
        {
            0   ,
            128 ,
            64  ,
            192 ,
            32  ,
            160 ,
            96  ,
            224 ,
            16  ,
            144 ,
            80  ,
            208 ,
            48  ,
            176 ,
            112 ,
            240 ,
            8   ,
            136 ,
            72  ,
            200 ,
            40  ,
            168 ,
            104 ,
            232 ,
            24  ,
            152 ,
            88  ,
            216 ,
            56  ,
            184 ,
            120 ,
            248 ,
            4   ,
            132 ,
            68  ,
            196 ,
            36  ,
            164 ,
            100 ,
            228 ,
            20  ,
            148 ,
            84  ,
            212 ,
            52  ,
            180 ,
            116 ,
            244 ,
            12  ,
            140 ,
            76  ,
            204 ,
            44  ,
            172 ,
            108 ,
            236 ,
            28  ,
            156 ,
            92  ,
            220 ,
            60  ,
            188 ,
            124 ,
            252 ,
            2   ,
            130 ,
            66  ,
            194 ,
            34  ,
            162 ,
            98  ,
            226 ,
            18  ,
            146 ,
            82  ,
            210 ,
            50  ,
            178 ,
            114 ,
            242 ,
            10  ,
            138 ,
            74  ,
            202 ,
            42  ,
            170 ,
            106 ,
            234 ,
            26  ,
            154 ,
            90  ,
            218 ,
            58  ,
            186 ,
            122 ,
            250 ,
            6   ,
            134 ,
            70  ,
            198 ,
            38  ,
            166 ,
            102 ,
            230 ,
            22  ,
            150 ,
            86  ,
            214 ,
            54  ,
            182 ,
            118 ,
            246 ,
            14  ,
            142 ,
            78  ,
            206 ,
            46  ,
            174 ,
            110 ,
            238 ,
            30  ,
            158 ,
            94  ,
            222 ,
            62  ,
            190 ,
            126 ,
            254 ,
            1   ,
            129 ,
            65  ,
            193 ,
            33  ,
            161 ,
            97  ,
            225 ,
            17  ,
            145 ,
            81  ,
            209 ,
            49  ,
            177 ,
            113 ,
            241 ,
            9   ,
            137 ,
            73  ,
            201 ,
            41  ,
            169 ,
            105 ,
            233 ,
            25  ,
            153 ,
            89  ,
            217 ,
            57  ,
            185 ,
            121 ,
            249 ,
            5   ,
            133 ,
            69  ,
            197 ,
            37  ,
            165 ,
            101 ,
            229 ,
            21  ,
            149 ,
            85  ,
            213 ,
            53  ,
            181 ,
            117 ,
            245 ,
            13  ,
            141 ,
            77  ,
            205 ,
            45  ,
            173 ,
            109 ,
            237 ,
            29  ,
            157 ,
            93  ,
            221 ,
            61  ,
            189 ,
            125 ,
            253 ,
            3   ,
            131 ,
            67  ,
            195 ,
            35  ,
            163 ,
            99  ,
            227 ,
            19  ,
            147 ,
            83  ,
            211 ,
            51  ,
            179 ,
            115 ,
            243 ,
            11  ,
            139 ,
            75  ,
            203 ,
            43  ,
            171 ,
            107 ,
            235 ,
            27  ,
            155 ,
            91  ,
            219 ,
            59  ,
            187 ,
            123 ,
            251 ,
            7   ,
            135 ,
            71  ,
            199 ,
            39  ,
            167 ,
            103 ,
            231 ,
            23  ,
            151 ,
            87  ,
            215 ,
            55  ,
            183 ,
            119 ,
            247 ,
            15  ,
            143 ,
            79  ,
            207 ,
            47  ,
            175 ,
            111 ,
            239 ,
            31  ,
            159 ,
            95  ,
            223 ,
            63  ,
            191 ,
            127 ,
            255 ,
        };

        private UInt64 _data;

        /// <devdoc>
        /// <para>Initializes a new instance of the BitVector64 structure with the specified internal data.</para>
        /// </devdoc>
        public BitVector64(UInt64 data = 0)
        {
            _data = (UInt64)data;
        }

        /// <devdoc>
        /// <para>Initializes a new instance of the BitVector64 structure with the information in the specified 
        ///    value.</para>
        /// </devdoc>
        public BitVector64(BitVector64 value)
        {
            _data = value.Data;
        }

        /// <devdoc>
        /// <para>Initializes a new instance of the BitVector64 structure with the information in the specified 
        ///    value.
        ///  Note : not particulary efficient
        /// </para>
        /// </devdoc>
        public BitVector64(bool[] value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            if (value.Length <= 0 || value.Length > 64)
                throw new IndexOutOfRangeException("The array provided sould be bound to the lenght between [1,64].");

            _data = 0;
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i])
                    this.Set(i);
            }
        }

        /// <devdoc>
        ///    returns the raw data stored in this bit vector...
        /// </devdoc>
        public UInt64 Data
        {
            get
            {
                return (UInt64)_data;
            }
        }

        public override bool Equals(object o)
        {
            if (!(o is BitVector64))
            {
                return false;
            }

            return _data == ((BitVector64)o).Data;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Return the current status of a flag in the BitVector
        /// </summary>
        /// <param name="index">>ndex of the bit to get</param>
        /// <returns>State of selected bit</returns>
        public bool Get(int index)
        {
            if (index < 0 || index > 63)
                throw new IndexOutOfRangeException($"Index should be bount to [0-63] values, actual Index :{index}");

            return (_data & _MASK[index]) != 0;
        }

        /// <summary>
        /// Mark as On a bit in the vector
        /// </summary>
        /// <param name="index">Index of the bit to mark (left to right from 0 to 63)</param>
        public BitVector64 Set(int index)
        {
            if (index < 0 || index > 63)
                throw new IndexOutOfRangeException($"Index should be bount to [0-63] values, actual Index :{index}");

            _data |= _MASK[index];

            return this;
        }

        /// <summary>
        /// Mark as Off a bit in the vector
        /// </summary>
        /// <param name="index">Index of the bit to mark (left to right from 0 to 63)</param>
        public BitVector64 Unset(int index)
        {
            if (index < 0 || index > 63)
                throw new IndexOutOfRangeException($"Index should be bount to [0-63] values, actual Index :{index}");

            _data &= ~_MASK[index];

            return this;
        }

        /// <summary>
        /// Change the bit status
        /// </summary>
        /// <param name="index">Index of bit to change</param>
        /// <param name="status">The new bit status (true = 1, false = 0)</param>
        public BitVector64 Apply(int index, bool status)
        {
            return status ? this.Set(index) : this.Unset(index);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(64);
            UInt64 locdata = _data;

            for (int i = 0; i < 64; i++)
            {
                if ((locdata & 0x01) != 0)
                {
                    sb.Append("1");
                }
                else
                {
                    sb.Append("0");
                }
                locdata >>= 1;
            }

            return sb.ToString();
        }

        /// <summary>
        /// Debug only, print a formatted bitString
        /// </summary>
        /// <returns></returns>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string StringFormatted()
        {
            char[] bits = this.ToString().ToCharArray();
            List<char> temp = bits.ToList();
            temp.Reverse();
            bits = temp.ToArray();

            string[] trunk = new string[8];
            for (int i = 7; i >= 0; i--)
                trunk[7-i] = string.Join(" ", bits.Skip(i * 8).Take(8));
            string result = $@"Actual Layout :
 ===============================================================================================================================================
║        7        |        6        |        5        |        4        |        3        |        2        |        1        |        0        ║
║ 7 6 5 4 3 2 1 0 | 7 6 5 4 3 2 1 0 | 7 6 5 4 3 2 1 0 | 7 6 5 4 3 2 1 0 | 7 6 5 4 3 2 1 0 | 7 6 5 4 3 2 1 0 | 7 6 5 4 3 2 1 0 | 7 6 5 4 3 2 1 0 ║
║_________________|_________________|_________________|_________________|_________________|_________________|_________________|_________________║
║                 |                 |                 |                 |                 |                 |                 |                 ║
║ {trunk[7]} | {trunk[6]} | {trunk[5]} | {trunk[4]} | {trunk[3]} | {trunk[2]} | {trunk[1]} | {trunk[0]} ║
 ===============================================================================================================================================
        ";
            return result;
        }

        /// <summary>
        /// Apply the bits starting form certain bit of the bitvector, till a certain length of the supplied bits.
        /// </summary>
        /// <param name="bits">UInt64 that retain the interesting bits</param>
        /// <param name="bitVectorApplyPoint">Starting point in the current bitvector [0,63]</param>
        /// <param name="bitsLength">Define how many of the supplied bits have to be taken into account [1,64].If there is not enought space after the apply point, bits are discarded.</param>
        public BitVector64 InsertBits (UInt64 bits, int bitVectorApplyPoint, int bitsLength)
        {
            if (bitVectorApplyPoint < 0 || bitVectorApplyPoint > 63)
                throw new IndexOutOfRangeException($"BitVectorApplyPoint should be bount to [0,63] values, actual Value :{bitVectorApplyPoint}");

            if (bitsLength < 1 || bitsLength > 64)
                throw new IndexOutOfRangeException($"BitsLength should be bount to [1,64] values, actual Value :{bitsLength}");

            UInt64 temporary = 0;
            int firstStageBlankShift  = 0;
            int secondStageBlankShift = 64 - bitsLength;
            int thirdStageBlankShift = 0;

            //first stage of original data, if any
            if (bitVectorApplyPoint > 0)
            {
                firstStageBlankShift = 64 - bitVectorApplyPoint;
                temporary = (_data << firstStageBlankShift) >> firstStageBlankShift;
            }

            //second stage of inserted data (mandatory)
            {
                secondStageBlankShift = 64 - bitsLength;
                temporary |= ((bits << secondStageBlankShift) >> secondStageBlankShift) << bitVectorApplyPoint;
            }

            //third stage of original data, if any
            thirdStageBlankShift = bitVectorApplyPoint + secondStageBlankShift;
            if (thirdStageBlankShift < 64)
            {
                temporary |= (_data >> thirdStageBlankShift) << thirdStageBlankShift;
            }

            _data = temporary;
            return this;
        }

        /// <summary>
        /// Reverse the bits of the current BitVector64
        /// </summary>
        /// <returns>Return the current BitVector64</returns>
        public BitVector64 Reverse()
        {
            UInt64 b0 = _data & 0xFF;
            UInt64 b1 = _data & 0xFF00;
            UInt64 b2 = _data & 0xFF0000;
            UInt64 b3 = _data & 0xFF000000;
            UInt64 b4 = _data & 0xFF00000000;
            UInt64 b5 = _data & 0xFF0000000000;
            UInt64 b6 = _data & 0xFF000000000000;
            UInt64 b7 = _data & 0xFF00000000000000;

            b0 = _REVERSE[b0];
            b1 = _REVERSE[b1>>8 ];
            b2 = _REVERSE[b2>>16];
            b3 = _REVERSE[b3>>24];
            b4 = _REVERSE[b4>>32];
            b5 = _REVERSE[b5>>40];
            b6 = _REVERSE[b6>>48];
            b7 = _REVERSE[b7>>56];

            _data = 0;
            _data |= (b0 << 56) | (b1 << 48) | (b2 << 40) | (b3 << 32) | (b4 << 24) | (b5 << 16) | (b6 << 8) | b7 ;
            return this;
        }

        /// <summary>
        /// Rotate the current array by a given numer of bits.
        /// </summary>
        /// <param name="bits">Numbe of bits that need to be rotate [-256,256]</param>
        /// <returns></returns>
        public BitVector64 Rotate(int bits)
        {
            if (bits < -256 || bits > 256)
                throw new IndexOutOfRangeException($"Bits should be bount to [-256,256] values, actual Value :{bits}");

            _data = (_data << bits) | (_data >> (64 - bits));

            return this;
        }

        /// <summary>
        /// Shift the current array by a given numer of bits.
        /// </summary>
        /// <param name="bits">Numbe of bits that need to be rotate [-64,64]</param>
        public BitVector64 Shift(int bits)
        {
            if (bits < -63 || bits > 63)
                throw new IndexOutOfRangeException($"Bits should be bount to [-63,63] values, actual Value :{bits}");

            _data = (bits > 0) ? (_data << bits) : (_data >> -bits);

            return this;
        }

        /// <summary>
        /// Join the data of two vector
        /// </summary>
        /// <param name="vector">Vector to join with current vector</param>
        /// <returns>The current vector update</returns>
        public BitVector64 Union(BitVector64 vector)
        {
            _data |= vector.Data;
            return this;
        }

        /// <summary>
        /// Intersect the data of two vector
        /// </summary>
        /// <param name="vector">Vector to intersect with current vector</param>
        /// <returns>The current vector update</returns>
        public BitVector64 Intersect(BitVector64 vector)
        {
            _data &= vector.Data;
            return this;
        }

        /// <summary>
        /// Flip all bits in the current BitVector64
        /// </summary>
        /// <returns>Return the updated BitVector64</returns>
        public BitVector64 Negate()
        {
            _data = ~_data;
            return this;
        }

        /// <summary>
        /// Join the data of two vector, excluding the intersection 
        /// </summary>
        /// <param name="vector">Vector to exclusevelly unite with current vector</param>
        /// <returns></returns>
        public BitVector64 ExclusiveUnion(BitVector64 vector)
        {
            _data ^= vector.Data;
            return this;
        }

        /// <summary>
        /// Get the bit that are in current BitVector64 but not in the given one.
        /// </summary>
        /// <param name="vector">Provided BitVector64</param>
        /// <returns></returns>
        public BitVector64 Left(BitVector64 vector)
        {
            _data |= vector.Data;
            _data &= (~vector.Data);
            
            return this;
        }

        /// <summary>
        /// Get the bit that are in the given BitVector64 but not in the current one
        /// </summary>
        /// <param name="vector">Provided BitVector64</param>
        /// <returns></returns>
        public BitVector64 Right (BitVector64 vector)
        {
            UInt64 original = _data;

            _data |= vector.Data;
            _data &= (~original);

            return this;
        }

        /// <summary>
        /// Return a new BitVector64 built arround the given subset
        /// </summary>
        /// <param name="subsetStart">Start of selection [0,63]</param>
        /// <param name="subsetEnd">End of selection [0,63]</param>
        /// <returns></returns>
        public BitVector64 SubsetAtoB(ushort subsetStart, ushort subsetEnd)
        {
            if (subsetStart < 0 || subsetStart > 63 || subsetEnd < 0 || subsetEnd > 63)
                throw new IndexOutOfRangeException($"Boundaries must be in the range [0,63] , actual : [{subsetStart},[{subsetEnd}]");

            UInt64 internalState = _data;
            internalState <<= 63-subsetEnd;
            internalState >>= subsetStart + (63 - subsetEnd);
            return new BitVector64(internalState);
        }

        /// <summary>
        /// Return a new BitVector64 built arround the given subset
        /// </summary>
        /// <param name="subsetStart">Start of selection [0,63]</param>
        /// <param name="subsetLength">Number of bits to select (could be negative if you would take bits before start)</param>
        /// <returns></returns>
        public BitVector64 SubsetATillLength(ushort subsetStart, short subsetLength = 0)
        {
            if (subsetStart < 0 || subsetStart > 63)
                throw new IndexOutOfRangeException($"SubsetStart must be in the range [0,63] , actual : {subsetStart}");

            if (subsetLength == 0)
                subsetLength = (short)(63 - subsetStart);

            int firstBoundary = subsetStart;
            int secondBoundary = subsetStart + subsetLength + (subsetLength > 0 ? -1 : 1);

            if (firstBoundary > secondBoundary)
            {
                int tempBoundary = secondBoundary;
                secondBoundary = firstBoundary;
                firstBoundary = tempBoundary;
            }

            if (firstBoundary < 0 || firstBoundary > 63 || secondBoundary < 0 || secondBoundary > 63)
                throw new IndexOutOfRangeException($"Boundaries must be in the range [0,63] , actual : [{firstBoundary},[{secondBoundary}]");

            return this.SubsetAtoB((ushort)firstBoundary, (ushort)secondBoundary);
        }

        /// <summary>
        /// Generate a deepcopy of current element
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new BitVector64(_data);
        }
    }
}
