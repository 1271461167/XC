﻿using _2023_12_11XiChun.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace _2023_12_11XiChun.View
{
    /// <summary>
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : Page
    {       
        public MainPage()
        {
            InitializeComponent();
            this.DataContext = new MainPageViewModel();
            MarkPageViewModel.ch += ChangeIndex;
        }
        public void ChangeIndex(int index)
        {
            this.IDCombox.SelectedIndex = index;
        }
    }
}
