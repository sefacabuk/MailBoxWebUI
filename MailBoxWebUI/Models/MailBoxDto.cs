using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBoxWebUI.Models
{
    public class MailBoxDto
    {
        public string id { get; set; }
        public string maiL_NAME { get; set; }
        public string subject { get; set; }
        public string content { get; set; }
        public DateTime datE_TIME { get; set; }
        public bool isread { get; set; }
    }
}
