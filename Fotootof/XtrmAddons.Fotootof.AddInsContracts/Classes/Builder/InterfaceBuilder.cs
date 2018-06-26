using System.Collections.Generic;
using System.ComponentModel.Composition;
using XtrmAddons.Fotootof.AddInsContracts.Interfaces;

namespace XtrmAddons.Fotootof.AddInsContracts
{
    /// <summary>
    /// Class XtrmAddons Fotootof AddIns Contracts.
    /// </summary>
    public class InterfaceBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        [ImportMany(typeof(IExtension))]
        public IEnumerable<IExtension> Extensions { get; set; }
    }
}