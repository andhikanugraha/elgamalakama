using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace ElGamalApplication
{

   
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ElGamal elgamal = new ElGamal();


        public MainWindow()
        {
            InitializeComponent();
        }
        
        // --- GENERIC UI ---
        private void ShowMessageBox(string message,
            MessageBoxButton button = MessageBoxButton.OK,
            MessageBoxImage icon = MessageBoxImage.Error)
        {
            string messageBoxText = message;
            string caption = "ElGamal";
            MessageBox.Show(messageBoxText, caption, button, icon);
        }

        // --- OPEN FILE UI ---

        private void ShowOpenDialog(TextBox target)
        {
            // Instantiate open file dialog
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "";
            dlg.DefaultExt = "";
            dlg.Filter = "All files (*.*)|*.*";

            // Show dialog
            Nullable<bool> result = dlg.ShowDialog();

            // Process choice
            if (result == true)
            {
                string filename = dlg.FileName;
                target.Text = filename;
            }
        }

        private string ShowOpenDialog(string filter = "All files (*.*)|*.*" )
        {
            string s = null;
            // Instantiate open file dialog
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "";
            dlg.DefaultExt = "";
            dlg.Filter = filter;

            // Show dialog
            Nullable<bool> result = dlg.ShowDialog();

            // Process choice
            if (result == true)
            {
                string filename = dlg.FileName;
                s = filename;
            }
            return s;
        }

        private void browseUnencryptedFileButton_Click(object sender, RoutedEventArgs e)
        {
            ShowOpenDialog(unencryptedFileName);
            encryptionDestinationFileName.Text = unencryptedFileName.Text + ".bin";
        }

        private void browseEncryptedFileButton_Click(object sender, RoutedEventArgs e)
        {
            ShowOpenDialog(encryptedFileName);
        }

        // --- SAVE FILE UI ---

        private void ShowSaveDialog(TextBox target, TextBox source = null)
        {   
            // Instantiate save file dialog
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "All files (*.*)|*.*";

            // Source file name
            if (source != null && source.Text.Length > 0)
            {
                String sourceDirectoryName = System.IO.Path.GetDirectoryName(source.Text);
                String sourceFileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(source.Text);
                String sourceExtension = System.IO.Path.GetExtension(source.Text);
                dlg.InitialDirectory = sourceDirectoryName;
                dlg.FileName = "";
                dlg.DefaultExt = sourceExtension;
            }

            // Show dialog
            Nullable<bool> result = dlg.ShowDialog();

            // Process choice
            if (result == true)
            {
                string filename = dlg.FileName;
                target.Text = filename;
            }
        }

        private void browseEncryptionDestinationFileButton_Click(object sender, RoutedEventArgs e)
        {
            ShowSaveDialog(encryptionDestinationFileName, unencryptedFileName);
        }

        private void browseDecryptionDestinationFileButton_Click(object sender, RoutedEventArgs e)
        {
            ShowSaveDialog(decryptionDestinationFileName, encryptedFileName);
        }

        private void encryptButton_Click(object sender, RoutedEventArgs e)
        {
            Encrypt();
        }

        private void Encrypt()
        {
            try
            {
                string sourceFile = unencryptedFileName.Text;
                string destinationFile = encryptionDestinationFileName.Text;
                long y = long.Parse(encrypt_key_y.Text);
                long g = long.Parse(encrypt_key_g.Text);
                long p = long.Parse(encrypt_key_p.Text);

                Key.PublicKey key = new Key.PublicKey();

                key.Y = y;
                key.G = g;
                key.P = p;

                elgamal.Encrypt(sourceFile, key);
                elgamal.SaveEncryptToFile(destinationFile);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            
        }

        private void decryptButton_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    string sourceFileName = encryptedFileName.Text;
            //    string destinationFileName = decryptionDestinationFileName.Text;
            //    string key = decryptionKey.Text;

            //    Engine.StartDecryption(sourceFileName, destinationFileName, key);

            //    ShowMessageBox("Decryption completed.", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine("\nMessage ---\n{0}", ex.Message);
            //    Debug.WriteLine("\nHelpLink ---\n{0}", ex.HelpLink);
            //    Debug.WriteLine("\nSource ---\n{0}", ex.Source);
            //    Debug.WriteLine("\nStackTrace ---\n{0}", ex.StackTrace);
            //    ShowMessageBox(ex.Message);
            //}
        }

        // --- READ FILE UI ---

        private void ShowReaderWindow(TextBox target)
        {
            string FileName = target.Text;
            ReaderWindow rw = new ReaderWindow();

            try
            {
                rw.LoadFile(FileName);
                rw.Show();
            }
            catch (Exception e)
            {
                rw.Close();
                ShowMessageBox(e.Message);
            }
        }

        private void openUnencryptedFileButton_Click(object sender, RoutedEventArgs e)
        {
            ShowReaderWindow(unencryptedFileName);
        }

        private void openEncryptionDestinationFileButton_Click(object sender, RoutedEventArgs e)
        {
            ShowReaderWindow(encryptionDestinationFileName);
        }

        private void openEncryptedFileButton_Click(object sender, RoutedEventArgs e)
        {
            ShowReaderWindow(encryptedFileName);
        }

        private void openDecryptionDestinationFileButton_Click(object sender, RoutedEventArgs e)
        {
            ShowReaderWindow(decryptionDestinationFileName);
        }

        private void generatePrivateKeyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Fetch ingredients for private key
                long p = Convert.ToInt64(pTextBox.Text);
                long g = Convert.ToInt64(gTextBox.Text);
                long x = Convert.ToInt64(xTextBox.Text);

                // Generate key
                Key k = new Key(p, g, x);
                ElGamalApplication.Key.PrivateKey pri = k.GeneratePrivateKey();
                ElGamalApplication.Key.PublicKey pub = k.GeneratePublicKey();

                xTextBox.Text = pri.X.ToString();
                yTextBox.Text = pub.Y.ToString();
            }
            catch (Exception ex)
            {
                ShowMessageBox("Key generation failed.");
            }
        }

        private void saveKeysButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Fetch ingredients for private key
                long p = Convert.ToInt64(pTextBox.Text);
                long g = Convert.ToInt64(gTextBox.Text);
                long x = Convert.ToInt64(xTextBox.Text);

                // Generate key
                Key k = new Key(p, g, x);
                ElGamalApplication.Key.PrivateKey pri = k.GeneratePrivateKey();
                ElGamalApplication.Key.PublicKey pub = k.GeneratePublicKey();

                SaveFileDialog fileBrowser = new SaveFileDialog();

                Nullable<bool> result = fileBrowser.ShowDialog();

                if (result == true)
                {
                    string fileName = fileBrowser.FileName;
                    k.saveToFile(fileName);
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox("Key generation failed.");
            }
        }

        private void encrypt_Load_Key_Click(object sender, RoutedEventArgs e)
        {
            string fileKey = ShowOpenDialog("Public Key (*.pub)|*.pub");
            if (fileKey == null) return;

            Key.PublicKey key = Key.GeneratePublicKeyFromFile(fileKey);

            encrypt_key_g.Text = key.G.ToString();
            encrypt_key_p.Text = key.P.ToString();
            encrypt_key_y.Text = key.Y.ToString();

        }
    }
}
