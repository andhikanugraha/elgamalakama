using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ElGamalApplication
{
    class Key
    {
        long p, g, x;
        
        public Key(long _p, long _g, long _x) 
        {
            p = _p;
            g = _g;
            x = _x;
        }

        public PublicKey GeneratePublicKey()
        {
            PublicKey key = new PublicKey();
            key.G = g;
            key.P = p;
            key.Y = modular_pow(g, x, p);
            return key;
        }

        public PrivateKey GeneratePrivateKey()
        {
            PrivateKey key = new PrivateKey();
            key.X = x;
            key.P = p;
            return key;
        }

        public static void SaveToFile(string fileName, PublicKey key)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            
            byte[] byte_toWrite = Encoding.ASCII.GetBytes(key.Y.ToString()).ToArray();

            fs.Write(byte_toWrite, 0, byte_toWrite.Count());
            fs.WriteByte((byte)',');

            byte_toWrite = Encoding.ASCII.GetBytes(key.G.ToString()).ToArray();

            fs.Write(byte_toWrite, 0, byte_toWrite.Count());
            fs.WriteByte((byte)',');

            byte_toWrite = Encoding.ASCII.GetBytes(key.P.ToString()).ToArray();

            fs.Write(byte_toWrite, 0, byte_toWrite.Count());
            fs.WriteByte((byte)'\n');
            fs.Close();
        }

        public static PublicKey GeneratePublicKeyFromFile(string fileName)
        {
            PublicKey key = new PublicKey();
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            key.Y = GetElement(fs);
            key.G = GetElement(fs);
            key.P = GetElement(fs);

            return key;
        }

        public static void SaveToFile(string fileName, PrivateKey key)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);

            Console.WriteLine(key.X);
            byte[] byte_toWrite = Encoding.ASCII.GetBytes(key.X.ToString()).ToArray();

            fs.Write(byte_toWrite, 0, byte_toWrite.Count());
            fs.WriteByte((byte)',');

            byte_toWrite = Encoding.ASCII.GetBytes(key.P.ToString()).ToArray();

            fs.Write(byte_toWrite, 0, byte_toWrite.Count());
            fs.WriteByte((byte)'\n');
            fs.Close();
        }

        public void saveToFile(string fileName)
        {
            
            Key.SaveToFile(fileName + ".pub", this.GeneratePublicKey());
            Key.SaveToFile(fileName + ".pri", this.GeneratePrivateKey());
        }

        public static PrivateKey GeneratePrivateKeyFromFile(string fileName)
        {
            PrivateKey key = new PrivateKey();
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            key.X = GetElement(fs);
            key.P = GetElement(fs);

            return key;
        }

        private static long GetElement(FileStream fs)
        {
            List<byte> bytes_toRead = new List<byte>();

            int read = fs.ReadByte();
            while (read != -1 && read != (int)',' && read != (int)'\n')
            {
                bytes_toRead.Add((byte)read);
                read = fs.ReadByte();
            }
            string number_string = Encoding.ASCII.GetString(bytes_toRead.ToArray());

            return long.Parse(number_string); 
        }

        private long modular_pow(long _base, long exp, long modulus)
        {
            long c = 1;
            for (int i = 1; i < exp + 1; i++)
            {
                c = (c * _base) % modulus;
            }

            return c;
        }

        #region public key
        public class PublicKey
        {
            private long g, y,p;

            public PublicKey()
            {
                
            }

            public long Y
            {
                get { return y; }
                set { y = value; }
            }

            public long G
            {
                get { return g; }
                set { g = value;  }
            }

            public long P
            {
                set { p = value; }
                get { return p; }
            }
        }
        #endregion

        #region private key
        public class PrivateKey
        {
            long x, p;

            public PrivateKey()
            {

            }

            public long X
            {
                set { x = value; }
                get { return x; }
            }

            public long P
            {
                set { p = value; }
                get { return p; }
            }
        }
        #endregion
    }
}
