﻿

using System;
using System.Collections.Generic;
using BackEnd;
using BackEnd.db;

namespace FrontEnd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        
        public MainWindow()
        {
            InitializeComponent(); 
            DataContext = new ListContext();
        }
    }
}