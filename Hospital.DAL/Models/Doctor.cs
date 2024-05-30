using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }   
        public string DoctorName { get; set; }

        public string DoctorSSN { get; set; }
        public long DoctorPno { get; set; }
    }
}
