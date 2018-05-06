using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUD.Models
{
    public class ClientViewModel
    {
        
        public int ClientId { get; set; }

        public String Document { get; set; }

        public String ComertialName { get; set; }

        public String FirstNameContact { get; set; }

        public String LastNameContact { get; set; }

        public String Address { get; set; }

        public int Telephone1 { get; set; }

        public int Telephone2 { get; set; }
       
        public String Mail { get; set; }

        public String Note { get; set; }

        public int DocumentTypeId { get; set; }
    }
}