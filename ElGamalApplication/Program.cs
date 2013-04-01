using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ElGamalApplication
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            UserInterface ui = new UserInterface();
            Application.Run(ui);
            /*Key key = new Key(1, 2, 3);
            Key.PrivateKey publicKey = Key.GeneratePrivateKeyFromFile("private.txt");
            Console.WriteLine(publicKey.X + "," + publicKey.P);
            Console.ReadKey();*/
        }
    }
}
