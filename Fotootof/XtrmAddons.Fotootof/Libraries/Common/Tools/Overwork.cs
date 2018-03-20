namespace XtrmAddons.Fotootof.Libraries.Common.Tools
{
    /// <summary>
    /// 
    /// </summary>
    public static class Overwork
    {
        /// <summary>
        /// 
        /// </summary>
        public static bool IsBusy
        {
            get => Navigator.MainWindow.XCTKBusyIndicator.IsBusy;
            set => Navigator.MainWindow.XCTKBusyIndicator.IsBusy = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public static object BusyContent
        {
            get => Navigator.MainWindow.XCTKBusyIndicator.BusyContent;
            set => Navigator.MainWindow.XCTKBusyIndicator.BusyContent = value;
        }
    }
}
