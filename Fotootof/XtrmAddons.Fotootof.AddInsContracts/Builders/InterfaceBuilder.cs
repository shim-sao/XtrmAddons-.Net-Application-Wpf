using System.Collections.Generic;
using System.ComponentModel.Composition;
using XtrmAddons.Fotootof.Interfaces.AddInsContracts;

namespace XtrmAddons.Fotootof.Builders.AddInsContracts
{
    public class InterfaceBuilder
    {
        [ImportMany(typeof(IModule))]
        public IEnumerable<IModule> Modules { get; set; }

        //[ImportMany(typeof(IModule))]
        //public IEnumerable<IModule> Attachers { get; set; }
    }
}
