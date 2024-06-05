using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VernamCipher
{
    internal class Program
    {
        private static Random rng = new Random();
        static void Main(string[] args)
        {
            string subtext = Console.ReadLine();

            string[] BinaryArr = ConvertToBinary(GetASCII(ConvertToCharArr(subtext)));
            
            string[] CipherBinaryArr = MakeCipher(BinaryArr.Length);

            string[] Ciphertext = GetCipherText(subtext.Length, BinaryArr, CipherBinaryArr);

            int[] AsciiValues = new int[Ciphertext.Length]; 
            
            for (int i = 0; i < Ciphertext.Length; i++)
            {
                AsciiValues[i] = GetAsciiFromBinary(Ciphertext[i]); //Converting binary to Ascii
            }

            char[] Ciphertxt = new char[AsciiValues.Length];
            for (int i = 0; i < AsciiValues.Length; i++)
            {
                Ciphertxt[i] = Convert.ToChar(AsciiValues[i]);
            }

            string returnvalue = "";
            for (int i = 0; i < Ciphertxt.Length; i++)
            {
                returnvalue = returnvalue + Ciphertxt.ToString();
            }
            
            Console.WriteLine($"The ciphertext is: {returnvalue}");
            Console.ReadKey();
        }

        static int GetAsciiFromBinary(string Ciphertxt)
        {
            int Ascii = Convert.ToInt32(Ciphertxt,2);
            return Ascii;
        }
        
        static string[] GetCipherText(int subtextLength, string[] Subtext, string[] Cipher)
        {
            string[] Ciphertextarr = new string[subtextLength];

            for (int i = 0; i < subtextLength; i++)
            {
                Ciphertextarr[i] = XorBinaryStrings(Subtext[i], Cipher[i]);
            }

            return Ciphertextarr;
        }
        
        static string XorBinaryStrings(string bin1, string bin2)
        {
            if (bin1.Length != bin2.Length)
            {
                throw new ArgumentException("Binary strings must be of equal length.");
            }

            char[] result = new char[bin1.Length];

            for (int i = 0; i < bin1.Length; i++)
            {
                // Convert chars to ints, perform XOR, then convert back to char
                int bit1 = bin1[i] - '0';
                int bit2 = bin2[i] - '0';
                result[i] = (bit1 ^ bit2).ToString()[0];
            }

            return new string(result);
        }
        
        static string[] MakeCipher(int LengthOfArr)
        {
            string Cipher = "";
            string[] Alphabet = "A B C D E F G H I J K L M N O P Q R S T U V W X Y Z".Split(' ');

            for (int i = 0; i < LengthOfArr; i++)
            {
                Cipher = Cipher + Alphabet[rng.Next(0,25)];
            }
            
            Console.WriteLine($"The Cypher used is: {Cipher}");

            string[] CipherBinary = ConvertToBinary(GetASCII(ConvertToCharArr(Cipher)));

            return CipherBinary;
        }
        static int[] GetASCII(char[] arrSubtext)
        {
            int[] Ascii = new int[arrSubtext.Length];

            for (int i = 0; i < arrSubtext.Length; i++)
            {
                Ascii[i] = (int)arrSubtext[i];
            }

            return Ascii;
        }
        
        static string[] ConvertToBinary(int[] AsciiArr)
        {
            string[] Binary = new string[AsciiArr.Length];
            
            for (int i = 0; i < AsciiArr.Length; i++)
            {
                Binary[i] = Convert.ToString(AsciiArr[i], 2);
            }

            return Binary;
        }
        
        static char[] ConvertToCharArr(string subtext)
        {
            char[] arrChar = new char[subtext.Length];
            
            for (int i = 0; i < subtext.Length; i++)
            {
                arrChar[i] = subtext[i];
            }

            return arrChar;
        }
    }
}
