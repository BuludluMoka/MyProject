using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Application.ViewModels
{
    public class VMLoan
    {
        public int id { get; set; }
        public string ClientId { get; set; }
        public string Client { get; set; }
        public decimal LoanAmount { get; set; }
        public DateTime  PayoutDate { get; set; }

    }
}
