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
using System.Windows.Shapes;

namespace XtrmAddons.Fotootof.Layouts.Windows.About
{
    /// <summary>
    /// Class XtrmAddons Fotootof Layouts Windows About.
    /// </summary>
    public partial class WindowAbout : Window
    {
        #region Properties

        /// <summary>
        /// Property to access to the Window model.
        /// </summary>
        public WindowAboutModel Model { get; private set; }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Layouts Windows About Constructor.
        /// </summary>
        public WindowAbout()
        {
            InitializeComponent();
            InitializeModel();
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method called on Window loaded event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        protected void Window_Load(object sender, RoutedEventArgs e)
        {
            // Add data model to the window data context for binding.
            DataContext = Model;
        }

        /// <summary>
        /// Method to initialize window data model.
        /// </summary>
        protected void InitializeModel()
        {
            Model = new WindowAboutModel(this);
        }

        #endregion
    }
}
