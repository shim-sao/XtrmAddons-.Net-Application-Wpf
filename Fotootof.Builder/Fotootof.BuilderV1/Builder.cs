using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fotootof.BuilderV1
{
    internal class Builder
    {
        internal Dictionary<string, string> Files
            = new Dictionary<string, string>()
            {
                { "EntityFramework.dll", "Packages\\External\\EntityFramework" },
                { "EntityFramework.SqlServer.dll", "Packages\\External\\EntityFramework" },
                { "Fotootof.AddInsContracts.dll", "Packages\\Internal" },
                { "Fotootof.AddInsContracts.Interfaces", "Packages\\Internal" },
                { "", "Packages\\Internal" },
                { "", "Packages\\Internal" },
                { "", "Packages\\Internal" },
                { "", "Packages\\Internal" },
                { "", "Packages\\Internal" },

            };

        internal List<string> FilesToDelete
            = new List<string>()
            {
                { "Fotootof.AddInsContracts.Interfaces.pdb" },
                { "EntityFramework.SqlServer.dll", "Packages\\External\\EntityFramework" },
                { "Fotootof.AddInsContracts.dll", "Packages\\Internal" },
                { "Fotootof.AddInsContracts.Interfaces", "Packages\\Internal" },
                { "", "Packages\\Internal" },
                { "", "Packages\\Internal" },
                { "", "Packages\\Internal" },
                { "", "Packages\\Internal" },
                { "", "Packages\\Internal" },

            };
    }
}
