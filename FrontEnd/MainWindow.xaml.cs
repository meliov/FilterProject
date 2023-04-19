

using System;
using System.Collections.Generic;
using System.Windows;
using BackEnd;
using BackEnd.db;

namespace FrontEnd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ListContext ListContext = new ListContext();
        public MainWindow()
        {
            InitializeComponent(); 
            DataContext = ListContext;
        }
    }
    
}