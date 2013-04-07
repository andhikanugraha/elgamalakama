using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ElGamalApplication
{

    class Tuple
    {
        public long a;
        public long b;
    }

    class ElGamal
    {
        List<Tuple> tuples;

        public ElGamal() {
            tuples = new List<Tuple>();
        }


        public void Encrypt(string fileName, Key.PublicKey key)
        {
            tuples.Clear();

            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            // Block Message Length based on value of P in key
            int blockLength = getByteLength(key.P);
            int n_blockLength = blockLength;
            System.Diagnostics.Debug.WriteLine(blockLength);

            // byte to read from file
            int byte_read = 0;
            
            // Message that want to be encrypt
            long message = 0;
            

            // Process Message
            while (byte_read >= 0)
            {

                int temp_byte = -1;

                while (byte_read >= 0 && message < key.P && n_blockLength > 0)
                {
                    byte_read = fs.ReadByte();
                    if (byte_read < 0) break;
                    message = 256 * message + byte_read;
                    n_blockLength--;
                    System.Diagnostics.Debug.WriteLine((char)byte_read);
                }
                

                // if Message more than block or P
                if (message >= key.P )
                {
                    temp_byte = byte_read;
                    message = message/256;
                }

                long k = 1520; // Must be random
                long a = modular_pow(key.G, k, key.P);
                long b = modular_pow(message, key.Y, k, key.P);
                
                Tuple t = new Tuple();
                t.a = a;
                t.b = b;

                tuples.Add(t);
                System.Diagnostics.Debug.WriteLine("process :" + (char)message + "," + message);
                //Console.WriteLine(message);

                message = 0;
                n_blockLength = blockLength;

                // if only the temp_bit have any value
                if (temp_byte >= 0)
                {
                    message = message*256 + temp_byte;
                    n_blockLength--;
                }
            }
            System.Diagnostics.Debug.WriteLine("Count Encrypt : " + tuples.Count());
            fs.Close();
        }

        public void SaveEncryptToFile(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);

            foreach (Tuple t in tuples)
            {
                fs.WriteByte((byte)'(');
                byte[] bytes_toWrite = Encoding.ASCII.GetBytes(t.a.ToString("X"));
                fs.Write(bytes_toWrite, 0, bytes_toWrite.Count());
                fs.WriteByte((byte)',');
                bytes_toWrite = Encoding.ASCII.GetBytes(t.b.ToString("X"));
                fs.Write(bytes_toWrite, 0, bytes_toWrite.Count());
                fs.WriteByte((byte)')');
            }
            fs.Close();
        }

        public List<Tuple> ReadTuplesFromFile(string fileName, Key.PrivateKey key)
        {
            var tuples = new List<Tuple>();

            // Read text - init
            BinaryReader br = new BinaryReader(File.OpenRead(fileName));

            // Get block length
            int blockLength = getByteLength(key.P);

            int pos = 0;
            char currentChar;
            string hexString;
            while (pos < br.BaseStream.Length)
            {
                br.ReadChar(); // (
                ++pos;

                currentChar = br.ReadChar();
                ++pos;

                hexString = "";
                while (currentChar != ',')
                {
                    hexString += currentChar;
                    currentChar = br.ReadChar();
                    ++pos;
                }
                
                long a = long.Parse(hexString, System.Globalization.NumberStyles.HexNumber);

                currentChar = br.ReadChar();
                ++pos;

                hexString = "";
                while (currentChar != ')')
                {
                    hexString += currentChar;
                    currentChar = br.ReadChar();
                    ++pos;
                }
                long b = long.Parse(hexString, System.Globalization.NumberStyles.HexNumber);

                Tuple t = new Tuple();
                t.a = a;
                t.b = b;
                tuples.Add(t);
            }

            return tuples;
        }

        public long Decrypt(Tuple tuple, Key.PrivateKey key)
        {
            var a = tuple.a;
            var b = tuple.b;
            var p = key.P;
            var x = key.X;

            // q = (a^x)^-1
            var q = modular_pow(a, p - 1 - x, p);
            var m = (b * q) % p;

            return m;
        }

        public void Decrypt(string sourceFileName, string targetFileName, Key.PrivateKey key)
        {
            // Fetch tuples
            tuples = ReadTuplesFromFile(sourceFileName, key);
            System.Diagnostics.Debug.WriteLine("Count Decrypt : " + tuples.Count());
            // Calculate byte length
            // byteLength is used to determine how to write to the target file
            var byteLength = getByteLength(key.P);

            // Open filewrite handle
            var bw = new BinaryWriter(File.Open(targetFileName, FileMode.Create));

            // Loop through each tuple, decrypt, and write
            long m;
            foreach (var t in tuples)
            {
                // Decrypt
                m = Decrypt(t, key);

                // Write
                // TODO if number of bytes is not power of 2
                switch (getByteLength(m))
                {
                    case 1:
                        bw.Write((byte)m);
                        break;
                    case 2:
                        bw.Write((short)m);
                        break;
                    case 4:
                        bw.Write((int)m);
                        break;
                    case 8:
                        bw.Write((long)m);
                        break;
                    case 16:
                        bw.Write((decimal)m);
                        break;
                }

                // Written.
            }

            bw.Close();
        }

        #region modular pow function
        private long modular_pow(long _base, long exp, long modulus)
        {
            long c = 1;
            for (int i = 1; i < exp + 1; i++)
            {
                c = (c * _base) % modulus;
            }

            return c;
        }

        private long modular_pow(long start_c, long _base, long exp, long modulus)
        {
            long c = start_c;
            for (int i = 1; i < exp + 1; i++)
            {
                c = (c * _base) % modulus;
            }

            return c;
        }
        #endregion

        // Get Bit Length based on p
        private int getByteLength(long p)
        {
            int count = 1;
            while (p / 256 != 0)
            {
                count++;
                p = p / 256;
            }
            return count;
        }
    }
}
