using System.Collections.Generic;
using System.ServiceModel;

namespace XtrmAddons.Fotootof.SQLiteService
{
    [ServiceContract()]
    public partial interface ISQLiteSvc
    {
        // Main Service contract
        [OperationContract(Name = "Contract")]
        void Main();
    }
}