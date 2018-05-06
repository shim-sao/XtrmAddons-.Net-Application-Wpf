using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using XtrmAddons.Fotootof.Lib.Base.Classes.Controls.Converters;
using XtrmAddons.Fotootof.Lib.Base.Classes.Windows;
using XtrmAddons.Fotootof.Lib.Base.Interfaces;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;
using XtrmAddons.Fotootof.Libraries.Common.Collections;
using XtrmAddons.Fotootof.Libraries.Common.Tools;
using XtrmAddons.Net.Common.Extensions;
using XtrmAddons.Net.Windows.Controls.Extensions;

namespace XtrmAddons.Fotootof.Libraries.Common.Windows.Forms.SectionForm
{
    /// <summary>
    /// <para>Class XtrmAddons Fotootof Libraries Common Windows Form Section.</para>
    /// <para>Provides form to add or edit a Section.</para>
    /// </summary>
    public partial class WindowFormSectionTest : WindowBaseForm, IWindowForm<SectionEntity>
    {
        #region Variables

        /// <summary>
        /// Variable Window AclGroup Form model of the view.
        /// </summary>
        private WindowFormSectionModel<WindowFormSectionTest> model;

        /// <summary>
        /// Variable preview TextBox Name text for Alias autofill compared.
        /// </summary>
        private string previewAlias = "";

        #endregion



        #region Properties

        /// <summary>
        /// Property old Section informations backup.
        /// </summary>
        public SectionEntity OldForm { get; set; }

        /// <summary>
        /// Property current or new Section informations.
        /// </summary>
        public SectionEntity NewForm
        {
            get => model?.Section;
            set => model.Section = value;
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Common Windows Form Section Constructor.
        /// </summary>
        /// <param name="entity">A Section entity.</param>
        public WindowFormSectionTest(SectionEntity entity = default(SectionEntity)) : base()
        {
            InitializeComponent();
            InitializeModel(entity);
            Loaded += Window_Load;
            Closing += Window_Closing;
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method called on Window loaded event.
        /// </summary>
        /// <param name="entity">A Section entity.</param>
        protected void InitializeModel(SectionEntity entity = default(SectionEntity))
        {
            // 1 - Initialize view model.
            model = new WindowFormSectionModel<WindowFormSectionTest>(this);

            // 2 - Make sure entity is not null.
            entity = entity ?? new SectionEntity();

            // 3 - Initialize new entity if required.
            if (entity.PrimaryKey > 0)
            {
                entity.Initialize();
            }

            // 4 - Store current entity data in a new entity.
            OldForm = entity.Clone();

            // 5 - Assign entity to the model.
            NewForm = entity;

            // 6 - Set model entity to dependencies converters.
            IsAclGroupInSection.Entity = model.Section;
            IsAlbumInSection.Entity = model.Section;

            // 7.1 - Assign list of AclGroup to the model.
            model.AclGroups = new AclGroupEntityCollection(true);

            // 7.2 - Assign list of Album to the model.
            model.Albums = new AlbumEntityCollection(true);
        }

        /// <summary>
        /// Method called on Window loaded event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event atguments.</param>
        protected void Window_Load(object sender, RoutedEventArgs e)
        {
            DataContext = model;
            Button_Cancel.Focus();
        }

        /// <summary>
        /// <para>Method to send validation error to a TextBox.</para>
        /// <para>Disable Form Save Button to prevent unwanted save.</para>
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void Input_Error(object sender, ValidationErrorEventArgs e)
        {
            TextBox tb = sender as TextBox;
            

            Trace.WriteLine("-------------------------------------------------------------");
            Trace.WriteLine("Input_Error Name => " + tb.Name);
            Trace.WriteLine("Input_Error Text => " + tb.Text);
            Trace.WriteLine("Button_Save.IsEnabled = " + Button_Save.IsEnabled);
            Trace.WriteLine("-------------------------------------------------------------");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void InputName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            TextBox originalSource = e.OriginalSource as TextBox;

            Trace.WriteLine("-------------------------------------------------------------");
            Trace.WriteLine("Input_TextChanged Name => " + tb.Name);
            Trace.WriteLine("Input_TextChanged Text => " + tb.Text);
            Trace.WriteLine("InputName.Text = " + InputName.Text);
            Trace.WriteLine("InputAlias.Text = " + InputAlias.Text);
            Trace.WriteLine("NewForm.HandledName = " + NewForm.Name);
            Trace.WriteLine("NewForm.Name = " + NewForm.Name);
            Trace.WriteLine("NewForm.Alias = " + NewForm.Alias);
            Trace.WriteLine("-----------------------------");

            //if (NewForm.Alias.IsNullOrWhiteSpace() || NewForm.Alias == previewAlias)
            if (InputAlias.Text.IsNullOrWhiteSpace() || InputAlias.Text == previewAlias)
            {
                InputAlias.Text = tb.Text.Sanitize().RemoveDiacritics().ToLower();
                
                if (InputAlias.Text.IsNotNullOrWhiteSpace())
                {
                    InputAlias.ValidationClear();
                }
            }

            if (tb.Text.IsNullOrWhiteSpace())
            {
                Button_Save.IsEnabled = false;
            }
            else
            {
                Button_Save.IsEnabled = IsSaveEnabled;
            }
            
            previewAlias = tb.Text.Sanitize().RemoveDiacritics().ToLower();

            Trace.WriteLine("InputName.Text = " + InputName.Text);
            Trace.WriteLine("InputAlias.Text = " + InputAlias.Text);
            Trace.WriteLine("NewForm.HandledName = " + NewForm.Name);
            Trace.WriteLine("NewForm.Name = " + NewForm.Name);
            Trace.WriteLine("NewForm.Alias = " + NewForm.Alias);

            Trace.WriteLine("Button_Save.IsEnabled = " + Button_Save.IsEnabled);
            Trace.WriteLine("-------------------------------------------------------------");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void InputName_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            TextBox tb = sender as TextBox;

            Trace.WriteLine("-------------------------------------------------------------");
            Trace.WriteLine("InputName_SourceUpdated Name => " + tb.Name);
            Trace.WriteLine("InputName_SourceUpdated Text => " + tb.Text);

            Trace.WriteLine("InputName.Text = " + InputName.Text);
            Trace.WriteLine("InputAlias.Text = " + InputAlias.Text);
            Trace.WriteLine("NewForm.Name = " + NewForm.Name);
            Trace.WriteLine("NewForm.Alias = " + NewForm.Alias);
            Trace.WriteLine("-----------------------------");

            if (InputAlias.Text.IsNullOrWhiteSpace() && !tb.Text.IsNullOrWhiteSpace())
            {
                InputAlias.Text = InputName.Text.Sanitize().RemoveDiacritics().ToLower();

                if (InputAlias.Text.IsNotNullOrWhiteSpace())
                {
                    InputAlias.ValidationClear();
                }
            }

            if (tb.Text.IsNullOrWhiteSpace())
            {
                Button_Save.IsEnabled = false;
            }
            else
            {
                Button_Save.IsEnabled = IsSaveEnabled;
            }

            Trace.WriteLine("InputName.Text = " + InputName.Text);
            Trace.WriteLine("InputAlias.Text = " + InputAlias.Text);
            Trace.WriteLine("NewForm.Name = " + NewForm.Name);
            Trace.WriteLine("NewForm.Alias = " + NewForm.Alias);

            Trace.WriteLine("Button_Save.IsEnabled = " + IsSaveEnabled);
            Trace.WriteLine("-------------------------------------------------------------");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void InputName_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (tb.Text.IsNullOrWhiteSpace())
            {
                Button_Save.IsEnabled = false;
            }
            else
            {
                Button_Save.IsEnabled = IsSaveEnabled;
            }

            Trace.WriteLine("-------------------------------------------------------------");
            Trace.WriteLine("InputName_TargetUpdated Name => " + tb.Name);
            Trace.WriteLine("InputName_TargetUpdated Text => " + tb.Text);

            Trace.WriteLine("InputName.Text = " + InputName.Text);
            Trace.WriteLine("InputAlias.Text = " + InputAlias.Text);
            Trace.WriteLine("NewForm.Name = " + NewForm.Name);
            Trace.WriteLine("NewForm.Alias = " + NewForm.Alias);

            Trace.WriteLine("Button_Save.IsEnabled = " + IsSaveEnabled);
            Trace.WriteLine("-------------------------------------------------------------");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void InputAlias_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;

            Trace.WriteLine("-------------------------------------------------------------");
            Trace.WriteLine("InputAlias_TextChanged Name => " + tb.Name);
            Trace.WriteLine("InputAlias_TextChanged Text => " + tb.Text);

            Trace.WriteLine("InputName.Text = " + InputName.Text);
            Trace.WriteLine("InputAlias.Text = " + InputAlias.Text);
            Trace.WriteLine("NewForm.Name = " + NewForm.Name);
            Trace.WriteLine("NewForm.Alias = " + NewForm.Alias);
            Trace.WriteLine("-----------------------------");

            tb.Text = tb.Text.Sanitize().RemoveDiacritics().ToLower();

            if(tb.Text.IsNullOrWhiteSpace())
            {
                Button_Save.IsEnabled = false;
            }
            else
            {
                Button_Save.IsEnabled = IsSaveEnabled;
            }

            Trace.WriteLine("InputName.Text = " + InputName.Text);
            Trace.WriteLine("InputAlias.Text = " + InputAlias.Text);
            Trace.WriteLine("NewForm.Name = " + NewForm.Name);
            Trace.WriteLine("NewForm.Alias = " + NewForm.Alias);

            Trace.WriteLine("Button_Save.IsEnabled = " + Button_Save.IsEnabled);
            Trace.WriteLine("-------------------------------------------------------------");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void InputAlias_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (tb.Text.IsNullOrWhiteSpace())
            {
                Button_Save.IsEnabled = false;
            }
            else
            {
                Button_Save.IsEnabled = IsSaveEnabled;
            }

            Trace.WriteLine("-------------------------------------------------------------");
            Trace.WriteLine("InputAlias_SourceUpdated Name => " + tb.Name);
            Trace.WriteLine("InputAlias_SourceUpdated Text => " + tb.Text);

            Trace.WriteLine("InputName.Text = " + InputName.Text);
            Trace.WriteLine("InputAlias.Text = " + InputAlias.Text);
            Trace.WriteLine("NewForm.Name = " + NewForm.Name);
            Trace.WriteLine("NewForm.Alias = " + NewForm.Alias);

            Trace.WriteLine("Button_Save.IsEnabled = " + IsSaveEnabled);
            Trace.WriteLine("-------------------------------------------------------------");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void InputAlias_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (tb.Text.IsNullOrWhiteSpace())
            {
                Button_Save.IsEnabled = false;
            }
            else
            {
                Button_Save.IsEnabled = IsSaveEnabled;
            }

            Trace.WriteLine("-------------------------------------------------------------");
            Trace.WriteLine("InputAlias_TargetUpdated Name => " + tb.Name);
            Trace.WriteLine("InputAlias_TargetUpdated Text => " + tb.Text);

            Trace.WriteLine("InputName.Text = " + InputName.Text);
            Trace.WriteLine("InputAlias.Text = " + InputAlias.Text);
            Trace.WriteLine("NewForm.Name = " + NewForm.Name);
            Trace.WriteLine("NewForm.Alias = " + NewForm.Alias);

            Trace.WriteLine("Button_Save.IsEnabled = " + IsSaveEnabled);
            Trace.WriteLine("-------------------------------------------------------------");
        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void DataGridCollectionAlbum_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        private void DataGridCollectionAclGroup_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">The text changed event arguments.</param>
        protected override bool IsSaveEnabled => ValidateForm();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private new bool ValidateForm()
        {
            bool save = false;

            if (NewForm != null)
            {
                save = true;

                if (NewForm.Name.IsNullOrWhiteSpace())
                {
                    save = false;
                }

                NewForm.Alias = NewForm.Alias.Sanitize().RemoveDiacritics().ToLower();
                if (NewForm.Alias.IsNullOrWhiteSpace())
                {
                    save = false;
                }
            }

            return save;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event atguments.</param>
        private void CheckBoxAclGroup_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                model.Section.LinkAclGroup(Tag2Object<AclGroupEntity>(sender).PrimaryKey);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                AppLogger.Error(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event atguments.</param>
        private void CheckBoxAclGroup_UnChecked(object sender, RoutedEventArgs e)
        {
            try
            {
                model.Section.UnLinkAclGroup(Tag2Object<AclGroupEntity>(sender).PrimaryKey);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                AppLogger.Error(ex);
            }
        }

        /// <summary>
        /// Method called to uncheck Album on the Albums list of the Section.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void CheckBoxAlbum_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                model.Section.LinkAlbum(Tag2Object<AlbumEntity>(sender).PrimaryKey);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                AppLogger.Error(ex);
            }
        }

        /// <summary>
        /// Method called to uncheck Album on the Albums list of the Section.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void CheckBoxAlbum_UnChecked(object sender, RoutedEventArgs e)
        {
            try
            {
                model.Section.UnlinkAlbum(Tag2Object<AlbumEntity>(sender).PrimaryKey);
            }
            catch(Exception ex)
            {
                log.Error(ex);
                AppLogger.Error(ex);
            }
        }

        #endregion
    }
}