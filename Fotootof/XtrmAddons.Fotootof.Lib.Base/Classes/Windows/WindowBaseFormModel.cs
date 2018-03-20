namespace XtrmAddons.Fotootof.Lib.Base.Classes.Windows
{
    /// <summary>
    /// Class XtrmAddons Fotootof Server Libraries Base Model.
    /// </summary>
    public class WindowBaseFormModel<WindowBase> : WindowBaseModel<WindowBase>
    {
        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Server Libraries Window Base Model Constructor.
        /// </summary>
        /// <param name="pageBase">The page associated to the model.</param>
        /*public WindowBaseFormModel(WindowBaseForm window = null)
                {
                    windowBase = window;
                }*/

        public WindowBaseFormModel(WindowBase owner) : base(owner)
        {
        }

        #endregion


        #region Methods
        #endregion
    }
}
