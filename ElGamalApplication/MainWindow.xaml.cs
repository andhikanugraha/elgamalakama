﻿using System;
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

            // TODO Multihreading
            //try
            //{
            //    string sourceFileName = unencryptedFileName.Text;
            //    string destinationFileName = encryptionDestinationFileName.Text;
            //    string key = encryptionKey.Text;
            //    string mode = encryptionMode.Text;
                
            //    byte cipherMode;

            //    switch (mode)
            //    {
            //        case "ECB":
            //            cipherMode = Engine.ECB;
            //            break;
            //        case "CBC":
            //            cipherMode = Engine.CBC;
            //            break;
            //        case "CFB":
            //            cipherMode = Engine.CFB;
            //            break;
            //        case "OFB":
            //            cipherMode = Engine.OFB;
            //            break;
            //        default:
            //            throw new Exception("Invalid mode of operation.");
            //    }

            //    Engine.StartEncryption(sourceFileName, destinationFileName, key, cipherMode);

            //    ShowMessageBox("Encryption completed.", MessageBoxButton.OK, MessageBoxImage.Information);

            //    decryptionKey.Text = key;
            //    encryptedFileName.Text = destinationFileName;
            //    if (decryptionDestinationFileName.Text == "")
            //        decryptionDestinationFileName.Text = sourceFileName;
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

        private void Encrypt()
        {
            string sourceFile = unencryptedFileName.Text;
            string destinationFile = encryptionDestinationFileName.Text;
            long p = long.Parse(encrypt_key_p.Text);
            long q = long.Parse(encrypt_key_q.Text);
            long x = long.Parse(encrypt_key_x.Text);

            Key.PublicKey key = new Key.PublicKey();
            
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
    }
}
