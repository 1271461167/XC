using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace _2023_12_11XiChun.Control
{
    public class IconButton : Button
    {
        public string IconData
        {
            get { return (string)GetValue(IconDataProperty); }
            set { SetValue(IconDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconDataProperty =
            DependencyProperty.Register(nameof(IconData), typeof(string), typeof(IconButton), new PropertyMetadata(default(string)));


        public double IconSize
        {
            get { return (double)GetValue(IconSizeProperty); }
            set { SetValue(IconSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconSizeProperty =
            DependencyProperty.Register(nameof(IconSize), typeof(double), typeof(IconButton), new PropertyMetadata(default(double)));


    }
}
