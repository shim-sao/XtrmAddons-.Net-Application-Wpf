using System.ServiceModel;

namespace Fotootof.SQLite.Services
{
    /// <summary>
    /// Interface XtrmAddons Fotootof SQLite Service.
    /// </summary>
    [ServiceContract()]
    public partial interface ISQLiteSvc
    {
        /// <summary>
        /// Method Main Service contract.
        /// </summary>
        [OperationContract(Name = "GetVersion")]
        string GetVersion();
    }
}