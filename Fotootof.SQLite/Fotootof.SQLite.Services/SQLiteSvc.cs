using System.Collections.Generic;
using System.ServiceModel;

namespace Fotootof.SQLite.Services
{
    [ServiceContract()]
    public partial interface ISQLiteSvc
    {
        // Main Service contract
        [OperationContract(Name = "Contract")]
        void Main();

        // Main Service contract
        //[OperationContract(Name = "GetInstance")]
        // SQLiteSvc GetInstance();
    }
}