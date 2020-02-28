using System.Collections.Generic;

namespace LRMS.Models
{
    public class AdaptorStatusRequestModel
    {
        public IList<Adaptor> List { get; set; }
    }

    public class Adaptor
    {
        public string ProcessName { get; set; }
        public string ProcessPath { get; set; }
        public bool? IsRunning { get; set; }
    }
}
