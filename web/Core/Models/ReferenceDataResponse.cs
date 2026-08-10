using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class ReferenceDataResponse
    {
        public int Id { get; set; }
        public List<ReferenceData>? Data { get; set; }
        public int StatusCode { get; set; }
        public List<string> Messages { get; set; } = new();
    }
}
