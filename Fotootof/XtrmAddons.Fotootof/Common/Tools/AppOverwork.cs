using System;
using XtrmAddons.Net.Application;

namespace XtrmAddons.Fotootof.Common.Tools
{
    /// <summary>
    /// Method XtrmAddons Fotootof Common Tools Application Overwork.
    /// </summary>
    [Obsolete("Use MessageBase", true)]
    public static class AppOverwork
    {
        /// <summary>
        /// 
        /// </summary>
        public static bool IsBusy
        {
            get => AppNavigator.MainWindow.XCTKBusyIndicator.IsBusy;
            set => ApplicationBase.BeginInvokeIfRequired(() => { AppNavigator.MainWindow.XCTKBusyIndicator.IsBusy = value; });
        }

        /// <summary>
        /// 
        /// </summary>
        public static object BusyContent
        {
            get => AppNavigator.MainWindow.XCTKBusyIndicator.BusyContent;
            set => ApplicationBase.BeginInvokeIfRequired(() => { AppNavigator.MainWindow.XCTKBusyIndicator.BusyContent = value; });
        }
    }
}
