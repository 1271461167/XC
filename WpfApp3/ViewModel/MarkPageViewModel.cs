using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp3.Common;
using WpfApp3.Common.LMC;
using WpfApp3.Model;

namespace WpfApp3.ViewModel
{
    public class MarkPageViewModel:NotifyBase
    {
        IMarkController _markController = LMC.GetInstance();
        private static bool ini=false;
        private ImageSource _image;
        public ImageSource PriviewImage
        {
            set { _image = value;this.DoNotify(); }
        }
        public CommandBase LoadFileCommand { get; set; }=new CommandBase();
        public MarkPageViewModel() 
        {
            if (!ini)
            {
                _markController.Initialize(System.Environment.CurrentDirectory, true);
                ini = true;
            }
            LoadFileCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            LoadFileCommand.DoExecute = new Action<object>((obj) =>
            {
                LoadEdzFile(obj);
            });

        }

        private void LoadEdzFile(object obj)
        {
            System.Windows.Controls.Image image = obj as System.Windows.Controls.Image;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "EZD|*.ezd";
            Nullable<bool> result=dlg.ShowDialog();
            if (result==true)
            {
                string filename = dlg.FileName;
                _markController.LoadEzdFile(filename);
                PriviewImage = _markController.GetCurPreviewImage((int)image.Width, (int)image.Height);
            }
            //PriviewImage = _markController.GetCurPreviewImage(image.Width, image.Height);
        }
    }
}
