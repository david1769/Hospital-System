using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class PatientResponse
    {
        public List<Patient>? Data { get; set; }
        public int TotalCount { get; set; }
    }
}
