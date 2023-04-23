using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace loginRegister.Modal
{
    public class EmailSetting
    {
        public string PrimaryDomain { get; set; }
        public int  PrimaryPort { get; set; }
        public string SecondaryDomain { get; set; }
        public int SecondaryPort { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string FromEmail { get; set; }
        public string  ToEmail { get; set; }

        public string CcEmail { get; set; }

    }
}
