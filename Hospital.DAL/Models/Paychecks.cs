using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL.Models
{
    public class Paycheck
    {
        public int ChequeNumber { get; set; }
        public int DoctorId { get; set; }
        public float Salary { get; set; }
        public float Bonus { get; set; }
        public DateTime PayDate { get; set; }
    }
}
