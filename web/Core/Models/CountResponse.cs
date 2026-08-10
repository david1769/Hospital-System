using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class CountResponse 
    {
        public int Count { get; set; }
        public int StatusCode { get; set; }
        public List<string> Messages { get; set; } = new();
    }
}
