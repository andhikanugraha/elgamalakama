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
            int blockLength = getBitLength(key.P) - 1;
            int n_blockLength = blockLength;

            // byte to read from file
            int n_byte = 8;
            int byte_read = fs.ReadByte();
            
            // Message that want to be encrypt
            long message = 0;

            // Process Message
            while (byte_read >= 0)
            {
                
                int temp_bit = -1;

                while (byte_read >= 0 && message < key.P && n_blockLength > 0)
                {
                    if (n_byte == 0)
                    {
                        // Read next byte of file
                        byte_read = fs.ReadByte();
                        n_byte = 8;
                        if (byte_read < 0) break;
                    }

                    int bit = (byte_read >> (n_byte - 1)) % 2 == 0 ? 0 : 1;
                    message = (message << 1 )+ bit;
                
                    n_byte--;
                    n_blockLength--;
                
                }

                // if Message more than block or P
                if (message >= key.P || n_blockLength < 0)
                {
                    temp_bit = (byte_read >> (n_byte - 1)) % 2 == 0 ? 0 : 1;
                    message = message >> 1;
                }

                long k = key.P-2; // Must be random
                long a = modular_pow(key.G, k, key.P);
                long b = modular_pow(message, key.Y, k, key.P);
                
                Tuple t = new Tuple();
                t.a = a;
                t.b = b;

                tuples.Add(t);

                message = 0;
                n_blockLength = blockLength;

                // if only the temp_bit have any value
                if (temp_bit >= 0)
                {
                    message = message << 1 + temp_bit;
                    n_blockLength--;
                }

            }

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
        private int getBitLength(long p)
        {
            int count = 0;
            while (p / 2 != 0)
            {
                count++;
                p = p / 2;
            }
            return count;
        }
    }
}
