using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL.DTO
{
    public class PatientDto
    {
        public string PatientSsn { get; set; }
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
    }
}
