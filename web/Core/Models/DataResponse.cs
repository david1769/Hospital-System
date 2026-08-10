using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class DataResponse<T>
    {
        public List<T>? Data { get; set; }
        public int StatusCode { get; set; }
        public List<string> Messages { get; set; } = new(); 
    
    }
}
