using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomaDataModel
{
    public class YearTerm
    {
        [Key]
        public int YearTermId { get; set; }
        public int Year { get; set; }
        public int Term { get; set; }
        public Boolean isDefault { get; set; }
    }
}
