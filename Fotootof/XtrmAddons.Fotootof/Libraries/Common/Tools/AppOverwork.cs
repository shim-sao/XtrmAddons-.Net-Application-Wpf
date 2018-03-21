namespace XtrmAddons.Fotootof.Libraries.Common.Tools
{
    /// <summary>
    /// 
    /// </summary>
    public static class AppOverwork
    {
        /// <summary>
        /// 
        /// </summary>
        public static bool IsBusy
        {
            get => AppNavigator.MainWindow.XCTKBusyIndicator.IsBusy;
            set => AppNavigator.MainWindow.XCTKBusyIndicator.IsBusy = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public static object BusyContent
        {
            get => AppNavigator.MainWindow.XCTKBusyIndicator.BusyContent;
            set => AppNavigator.MainWindow.XCTKBusyIndicator.BusyContent = value;
        }
    }
}
