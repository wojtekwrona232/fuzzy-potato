using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Emails.Dtos
{
    public class EmailDto
    {
        [Required] public ICollection<string> Recipients { get; set; }
        public ICollection<string> Ccs { get; set; }    // carbon copy
        public ICollection<string> Bccs { get; set; }   // blind carbon copy
        [Required] [MinLength(1)] [MaxLength(128)] public string Subject { get; set; }
        [Required] [MinLength(1)] [MaxLength(32768)] public string Body { get; set; }
        [MinLength(1)] [MaxLength(256)] public string Signature { get; set; }
    }
}
