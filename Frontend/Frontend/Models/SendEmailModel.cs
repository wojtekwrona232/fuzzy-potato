using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Frontend.Models
{
    public class SendEmailModel
    {
        public ICollection<string> Recipients { get; set; }
        public ICollection<string> Ccs { get; set; } // carbon copy
        public ICollection<string> Bccs { get; set; } // blind carbon copy
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Signature { get; set; }
    }
}