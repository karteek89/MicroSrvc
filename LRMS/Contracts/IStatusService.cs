using LRMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRMS.Contracts
{
    public interface IStatusService
    {
        Task<bool> GetSftpStatus(SftpRequestModel model);
        Task<IList<Adaptor>> GetAdaptorStatus(IList<Adaptor> list);
    }
}
