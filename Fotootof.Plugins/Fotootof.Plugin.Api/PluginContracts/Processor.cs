using Fotootof.AddInsContracts.Interfaces;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace Fotootof.Plugin.Api.PluginContracts
{
    /// <summary>
    /// Class XtrmAddons Fotootof Plugin Api Plugin Contracts Processor.
    /// </summary>
    [Export("ApiProcess", typeof(IProcess))]
    public class Processor : IProcess
    {
        #region Properties

        /// <summary>
        /// Property to check if the processor is enable.
        /// </summary>
        public bool IsEnable { get => true; set => throw new System.NotImplementedException(); }

        #endregion



        #region Methods

        /// <summary>
        /// Method to run a process.
        /// </summary>
        public void Run()
        {
            Trace.WriteLine($"Running Module process : {Assembly.GetAssembly(GetType()).FullName}.");
            MessageBox.Show($"Assembly : {Assembly.GetAssembly(GetType()).FullName}");
        }

        #endregion
    }
}