using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp3.Control
{
    public class LED:UserControl
    {
        public bool IsTrue
        {
            get { return (bool)GetValue(IsTrueProperty); }
            set { SetValue(IsTrueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsTrue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsTrueProperty =
            DependencyProperty.Register(nameof(IsTrue), typeof(bool), typeof(LED), new PropertyMetadata(default(bool)));
    }
}
