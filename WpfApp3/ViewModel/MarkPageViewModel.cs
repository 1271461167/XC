using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp3.Common;
using WpfApp3.Common.LMC;
using WpfApp3.Model;
using WpfApp3.Model.PageModel;
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp3.ViewModel
{
    public class MarkPageViewModel:NotifyBase
    {
        IMarkController _markController = LMC.GetInstance();
        private MarkPageModel _markModel=new MarkPageModel();
        private static bool ini=false;
        public ObservableCollection<ProductData> Products { get; set; } = new ObservableCollection<ProductData>();
        private static ImageSource _image;
        public ImageSource PriviewImage
        {
            get { return _image; }
            set { _image = value;this.DoNotify(); }
        }
        public CommandBase LoadFileCommand { get; set; }=new CommandBase();
        public CommandBase RefreshCommand { get; set; }= new CommandBase();  
        public CommandBase MarkCommand { get; set; } = new CommandBase();
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
            RefreshCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            RefreshCommand.DoExecute = new Action<object>((obj) =>
            {
                RefreshPreview(obj);
            });
            MarkCommand.DoCanExecute = new Func<object, bool>((obj) => { return true; });
            MarkCommand.DoExecute = new Action<object>((obj) =>
            {
                Products.Add(new ProductData { IsPass = true, Power = 50, ProductionID = "Z-001", ProcessTime = TimeSpan.FromSeconds(2), Time = DateTime.Now.ToString(), Type = "Z", ZHeight = 30 });               
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
        }
        private void RefreshPreview(object obj)
        {
            System.Windows.Controls.Image image = obj as System.Windows.Controls.Image;
            PriviewImage = _markController.GetCurPreviewImage((int)image.Width, (int)image.Height);
        }
    }
}
