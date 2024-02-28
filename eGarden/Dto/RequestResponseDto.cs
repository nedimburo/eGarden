using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGarden.Dto
{
    public class RequestResponseDto
    {
        public int id { get; set; }
        public string chosenMaintenance { get; set; }
        public string chosenDecoration { get; set; }
        public string chosenLayout { get; set; }
        public string paymentMethod { get; set; }
        public string allowAgency { get; set; }
        public float plannedBudget { get; set; }
        public float price { get; set; }
        public string status { get; set; }
        public string creationDate { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public string country { get; set; }
    }
}
