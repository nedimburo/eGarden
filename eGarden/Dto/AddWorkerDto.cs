﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGarden.Dto
{
    public class AddWorkerDto
    {
        public string description { get; set; }
        public string phoneNumber { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string username { get; set; }
    }
}
