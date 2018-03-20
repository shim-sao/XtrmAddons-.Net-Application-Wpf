using System;
using System.Collections.Generic;
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

namespace XtrmAddons.Fotootof.Libraries.Common.Controls.Tooltips
{
    /// <summary>
    /// Logique d'interaction pour UCToolTipPicture.xaml
    /// </summary>
    public partial class UCToolTipPicture : UserControl
    {
        public UCToolTipPicture()
        {
            InitializeComponent();
            Loaded += (s,e) => test();
        }

        private void test()
        {
            var a = DataContext;
            var b = a;
        }
    }
}
