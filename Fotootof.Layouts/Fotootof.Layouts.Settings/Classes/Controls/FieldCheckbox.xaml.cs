using Fotootof.Libraries.Controls;
using System.Windows;
using System.Windows.Controls;

namespace Fotootof.Layouts.Settings.Controls
{
    /// <summary>
    /// Class Fotootof Layouts Settings Controls Checkbox Field.
    /// </summary>
    public partial class FieldCheckBox : ControlLayout
    {
        #region Variables
        
        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
        	log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion


        #region Properties : FieldLabelValue

        /// <summary>
        /// <see cref="DependencyProperty"/> to manage the field label content value property.
        /// </summary>
        public static readonly DependencyProperty FieldLabelValueProperty =
            DependencyProperty.Register(
                "FieldLabelValue",
                typeof(string),
                typeof(FieldCheckBox),
                new FrameworkPropertyMetadata(
                    "FieldCheckbox Label",
                    new PropertyChangedCallback(FieldLabelValue_Changed)
                )
            );

        /// <summary>
        /// Property string to access to the field label content value.
        /// </summary>
        public string FieldLabelValue
        {
            get { return (string)GetValue(FieldLabelValueProperty); }
            set { SetValue(FieldLabelValueProperty, value); }
        }

        #endregion


        #region Properties : FieldLabelTooltip

        /// <summary>
        /// <see cref="DependencyProperty"/> to manage the field label tooltip content value property.
        /// </summary>
        public static readonly DependencyProperty FieldLabelTooltipProperty =
            DependencyProperty.Register(
                "FieldLabelTooltip",
                typeof(string),
                typeof(FieldCheckBox),
                new FrameworkPropertyMetadata(
                    null,
                    new PropertyChangedCallback(FieldLabelTooltip_Changed)
                )
            );

        /// <summary>
        /// Property string to access to the field label content value.
        /// </summary>
        public string FieldLabelTooltip
        {
            get { return (string)GetValue(FieldLabelTooltipProperty); }
            set { SetValue(FieldLabelTooltipProperty, value); }
        }        

        #endregion


        #region Properties


        /// <summary>
        /// <see cref="DependencyProperty"/> to manage the field checkbox is checked value property.
        /// </summary>
        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register(
                "IsChecked",
                typeof(bool),
                typeof(FieldCheckBox),
                new FrameworkPropertyMetadata(
                    false,
                    new PropertyChangedCallback(IsChecked_Changed)
                )
            );

        /// <summary>
        /// Property boolean to access to the field checkbox is checked value.
        /// </summary>
        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }


        /// <summary>
        /// <see cref="DependencyProperty"/> to manage the field checkbox is checked default value property.
        /// </summary>
        public static readonly DependencyProperty IsCheckedDefaultProperty =
            DependencyProperty.Register(
                "IsCheckedDefault",
                typeof(bool?),
                typeof(FieldCheckBox),
                new FrameworkPropertyMetadata(
                    null,
                    new PropertyChangedCallback(IsCheckedDefault_Changed)
                )
            );

        /// <summary>
        /// Property boolean to access to the field checkbox is checked default value.
        /// </summary>
        public bool? IsCheckedDefault
        {
            get { return (bool?)GetValue(IsCheckedDefaultProperty); }
            set { SetValue(IsCheckedDefaultProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty InnerContentProperty =
            DependencyProperty.Register(
                "InnerContent",
                typeof(object),
                typeof(FieldCheckBox),
                new PropertyMetadata(
                    null,
                    new PropertyChangedCallback(InnerContent_Changed)
                )
            );

        /// <summary>
        /// Gets or sets additional content for the UserControl
        /// </summary>
        public object InnerContent
        {
            get { return (object)GetValue(InnerContentProperty); }
            set { SetValue(InnerContentProperty, value); }
        }

        #endregion


        #region Constructors

        /// <summary>
        /// Class Fotootof Layouts Settings Controls Checkbox Field constructor.
        /// </summary>
        public FieldCheckBox()
        {
            InitializeComponent();
        }

        #endregion


        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void FieldLabelValue_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Label lbl = FindLabel(d);
            if (lbl.Content != e.NewValue)
            {
                lbl.Content = e.NewValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void FieldLabelTooltip_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Label lbl = FindLabel(d);
            if (e.NewValue != null && lbl.ToolTip != e.NewValue)
            {
                lbl.ToolTip = e.NewValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private static Label FindLabel(DependencyObject d)
        {
            return ((FieldCheckBox)d).FindName<Label>("FieldLabel");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void IsChecked_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var cb = ((FieldCheckBox)d).FindName<CheckBox>("FieldChkBox");

            if (cb.IsChecked != (bool)e.NewValue)
            {
                cb.IsChecked = (bool)e.NewValue;
            }

            if (((FieldCheckBox)d).IsCheckedDefault == null)
            {
                ((FieldCheckBox)d).IsCheckedDefault = cb.IsChecked;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void IsCheckedDefault_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void InnerContent_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FieldCheckBox)d).FindName<ContentPresenter>("FieldContentPresenter").Content = e.NewValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FieldCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            IsChecked = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FieldCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            IsChecked = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FieldReset_Click(object sender, RoutedEventArgs e)
        {
            IsChecked = (bool)IsCheckedDefault;
        }

        #endregion


        #region Methods ControlLayout : Layout Size Changed

        /// <summary>
        /// Method called on layout control size changed event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">Size changed event arguments <see cref="SizeChangedEventArgs"/>.</param>
        public override void Layout_SizeChanged(object sender, SizeChangedEventArgs e) { }
        
        #endregion
    }
}
