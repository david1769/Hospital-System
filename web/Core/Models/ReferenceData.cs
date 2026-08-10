using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class ReferenceData
    {

        public int Id {get;set; }
        public int? ReferenceDataCategoryId { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
        public ReferenceDataCategory? ReferenceDataCategory { get; set; }
    }
}
