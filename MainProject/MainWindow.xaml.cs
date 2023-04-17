using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;

namespace MainProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        //App.Current.Windows[0].Close();
        public MainWindow()
        {
            InitializeComponent();

            // Hide the WPF window
            this.Visibility = Visibility.Collapsed;

            // Create a new console window
            AllocConsole();

            // Redirect the standard input, output, and error to the console window
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
            Console.SetError(new StreamWriter(Console.OpenStandardError()) { AutoFlush = true });

            // Write to the console window
            Console.Out.WriteLine("Please enter your name:");

            // Read input from the console window
            ConsoleKeyInfo keyInfo;
            string name = "";
            do
            {
                keyInfo = Console.ReadKey();
                if (keyInfo.Key != ConsoleKey.Enter)
                {
                    name += keyInfo.KeyChar;
                }
            } while (keyInfo.Key != ConsoleKey.Enter);

            // Write a message to the console window
            Console.Out.WriteLine($"Hello, {name}!");

            // Dispose of the console window
            FreeConsole();

            // Close the application
            Application.Current.Shutdown();
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FreeConsole();
    }
}