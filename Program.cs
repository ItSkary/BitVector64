using Connect4.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    static public class Program
    {
        // Main Method with int return type 
        static int Main(String[] args)
        {
            Console.WriteLine("-------------------------------------\r\n\r\n");
            Console.WriteLine("Test 0 - Constructor & Representation");
            {
                BitVector64 vector1 = new BitVector64();
                Console.WriteLine(vector1);

                BitVector64 vector2 = new BitVector64(0x3000000000000003);
                Console.WriteLine(vector2);

                BitVector64 vector3 = new BitVector64(vector2);
                vector2.Negate();
                Console.WriteLine(vector3);

                BitVector64 vector4 = new BitVector64(new bool[]{ true, true, true});
                Console.WriteLine(vector4);

                Console.WriteLine("\r\nPrinting 0xB9000C80008C00AB\r\n");
                BitVector64 vector5 = new BitVector64(0xB9000C80008C00AB);
                Console.WriteLine(vector5.StringFormatted());//test and internal use only
                Console.WriteLine(vector5);
            }

            Console.WriteLine("-------------------------------------\r\n\r\n");
            Console.WriteLine("Test 1 - Set/Unset");
            {
                BitVector64 vector = new BitVector64();

                for (int i = 0; i < 64; i++)
                {
                    vector.Set(i);
                    Console.WriteLine(vector);
                }

                for (int i = 0; i < 64; i++)
                {
                    vector.Unset(63 - i);
                    Console.WriteLine(vector);
                }

                for (int i = 0; i < 64; i++)
                {
                    vector.Apply(i,true);
                    Console.WriteLine(vector);
                }

                for (int i = 0; i < 64; i++)
                {
                    vector.Apply(63 - i, false);
                    Console.WriteLine(vector);
                }
            }

            Console.WriteLine("-------------------------------------\r\n\r\n");
            Console.WriteLine("Test 2 - Read");
            {
                BitVector64 vector = new BitVector64(0xD110010012B0404B);
                Console.WriteLine(vector);

                string queryResult = "";
                for (int i = 0; i < 64; i++)
                {
                    queryResult += ( vector.Get(i) ? "T" : "F" );
                }

                Console.WriteLine(queryResult);
            }

            Console.WriteLine("-------------------------------------\r\n\r\n");
            Console.WriteLine("Test 3 - Rotation");
            {
                for (int i = -256; i < 256; i++)
                {
                    BitVector64 vector = new BitVector64(13);
                    Console.WriteLine(vector.Rotate(i));
                }
            }

            Console.WriteLine("-------------------------------------\r\n\r\n");
            Console.WriteLine("Test 4 - Shift");
            {
                for (int i = 0; i < 64; i++)
                {
                    BitVector64 vector = new BitVector64(0xB00000000000000D);
                    Console.WriteLine(i.ToString() + "]\t" + vector.Shift(i));
                }

                Console.WriteLine("\r\n");

                for (int i = 0; i > -64; i--)
                {
                    BitVector64 vector = new BitVector64(0xB00000000000000D);
                    Console.WriteLine(i.ToString() + "]\t" + vector.Shift(i));
                }
            }

            Console.WriteLine("-------------------------------------\r\n\r\n");
            Console.WriteLine("Test 5 - Intersect & Union");
            {
                BitVector64 vector1 = new BitVector64(0xB000000000000000);
                BitVector64 vector2 = new BitVector64(0x800000000000000B);
                Console.WriteLine(vector1);
                Console.WriteLine(vector2);
                Console.WriteLine("Union     " + vector1.Union(vector2));
                vector1 = new BitVector64(0xB000000000000000);
                Console.WriteLine("Intersect " + vector1.Intersect(vector2));
            }

            Console.WriteLine("-------------------------------------\r\n\r\n");
            Console.WriteLine("Test 6 - Negate");
            {
                BitVector64 vector = new BitVector64(0xB000000000000000);
                Console.WriteLine(vector);
                Console.WriteLine(vector.Negate());
                Console.WriteLine(vector.Negate());
            }

            Console.WriteLine("-------------------------------------\r\n\r\n");
            Console.WriteLine("Test 7 - ExclusiveUnion");
            {
                BitVector64 vector1 = new BitVector64(0xB000000000000000);
                BitVector64 vector2 = new BitVector64(0x800000000000000B);
                Console.WriteLine(vector1);
                Console.WriteLine(vector2);
                Console.WriteLine(vector1.ExclusiveUnion(vector2));
            }

            Console.WriteLine("-------------------------------------\r\n\r\n");
            Console.WriteLine("Test 8 - Left&Right");
            {
                BitVector64 vector1 = new BitVector64(0xB0000003C0000000);
                BitVector64 vector2 = new BitVector64(0x3C000000D);

                Console.WriteLine("Left");
                Console.WriteLine(vector1);
                Console.WriteLine(vector2);
                Console.WriteLine(vector1.Left(vector2));

                vector1 = new BitVector64(0xB0000003C0000000);
                vector2 = new BitVector64(0x3C000000D);

                Console.WriteLine("Right");
                Console.WriteLine(vector1);
                Console.WriteLine(vector2);
                Console.WriteLine(vector1.Right(vector2));
            }

            Console.WriteLine("-------------------------------------\r\n\r\n");
            Console.WriteLine("Test 9 - Reverse");
            {
                BitVector64 vector1 = new BitVector64(0xA0442251EA451603);

                Console.WriteLine(string.Format("0x{0:X}", vector1.Data));
                Console.WriteLine(vector1);
                Console.WriteLine("-------------------------------------");
                vector1.Reverse();
                Console.WriteLine(string.Format("0x{0:X}", vector1.Data));
                Console.WriteLine(vector1);
                Console.WriteLine("-------------------------------------");
                vector1.Reverse();
                Console.WriteLine(string.Format("0x{0:X}", vector1.Data));
                Console.WriteLine(vector1);
            }

            Console.WriteLine("-------------------------------------\r\n\r\n");
            Console.WriteLine("Test 10 - Subset limit & length");
            {
                BitVector64 vector1 = new BitVector64(0xA0442251EA451603);

                Console.WriteLine(string.Format("0x{0:X}", vector1.Data));
                Console.WriteLine(vector1);
                Console.WriteLine(vector1.StringFormatted());
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("Limit");
                Console.WriteLine("0-3\r\n"   + vector1.SubsetAtoB(0,3));
                Console.WriteLine("4-7\r\n"   + vector1.SubsetAtoB(4,7));
                Console.WriteLine("8-11\r\n"  + vector1.SubsetAtoB(8,11));
                Console.WriteLine("12-15\r\n" + vector1.SubsetAtoB(12,15));
                Console.WriteLine("16-19\r\n" + vector1.SubsetAtoB(16,19));
                Console.WriteLine("20-23\r\n" + vector1.SubsetAtoB(20,23));
                Console.WriteLine("24-27\r\n" + vector1.SubsetAtoB(24,27));
                Console.WriteLine("28-31\r\n" + vector1.SubsetAtoB(28,31));
                Console.WriteLine("32-35\r\n" + vector1.SubsetAtoB(32,35));
                Console.WriteLine("36-39\r\n" + vector1.SubsetAtoB(36,39));
                Console.WriteLine("40-43\r\n" + vector1.SubsetAtoB(40,43));
                Console.WriteLine("44-47\r\n" + vector1.SubsetAtoB(44,47));
                Console.WriteLine("48-51\r\n" + vector1.SubsetAtoB(48,51));
                Console.WriteLine("52-55\r\n" + vector1.SubsetAtoB(52,55));
                Console.WriteLine("56-59\r\n" + vector1.SubsetAtoB(56,59));
                Console.WriteLine("60-63\r\n" + vector1.SubsetAtoB(60,63));

                
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("Length");
                Console.WriteLine(vector1);
                Console.WriteLine("1..\r\n" + vector1.SubsetATillLength(1));
                Console.WriteLine("2..\r\n" + vector1.SubsetATillLength(2));
                Console.WriteLine("32,L9\r\n" + vector1.SubsetATillLength(32, 9));
                Console.WriteLine("32,L-9\r\n" + vector1.SubsetATillLength(32, -9));
            }

            Console.WriteLine("-------------------------------------\r\n\r\n");
            Console.WriteLine("Test 11 - Insert Bits");
            {
                //101 Test
                {
                    BitVector64 vector1 = new BitVector64(0);
                    UInt64 bits = 5;

                    Console.WriteLine(vector1);
                    Console.WriteLine("Inserting '101'");
                    Console.WriteLine("-------------------------------------");

                    int entryPoint = 0;
                    int len = 3;//'101'.length = 3
                    while (entryPoint <= 63)
                    {
                        Console.WriteLine($"{entryPoint},{len}]\t" + vector1.InsertBits(bits, entryPoint, len));
                        entryPoint += len;
                    }
                }

                Console.WriteLine();
                //1001 Test
                {
                    BitVector64 vector1 = new BitVector64(0);
                    UInt64 bits = 9;

                    Console.WriteLine(vector1);
                    Console.WriteLine("Inserting '1001'");
                    Console.WriteLine("-------------------------------------");

                    int entryPoint = 0;
                    int len = 4;//'1001'.length = 4
                    while (entryPoint <= 63)
                    {
                        Console.WriteLine($"{entryPoint},{len}]\t" + vector1.InsertBits(bits, entryPoint, len));
                        entryPoint += len;
                    }
                }
            }


            Console.ReadKey();
            // for successful execution of code 
            return 0;
        }
    }
}
