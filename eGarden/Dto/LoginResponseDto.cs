using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGarden.Dto
{
    public class LoginResponseDto
    {
        public string message { get; set; }
        public string username { get; set; }
        public string role { get; set; }
        public bool hasCard { get; set; }
    }
}
