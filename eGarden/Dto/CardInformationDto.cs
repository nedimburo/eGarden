using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGarden.Dto
{
    public class CardInformationDto
    {
        public string cardNumber { get; set; }
        public string pinCode { get; set; }
        public string threeDigitNumber { get; set; }
        public string expirationDate { get; set; }
        public string username { get; set; }
    }
}
