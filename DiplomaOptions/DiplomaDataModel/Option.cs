using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DiplomaDataModel
{
    public class Option
    {
        [Key]
        public int OptionId { get; set; }
        [MaxLength(50)]
        public String Title { get; set; }
        public Boolean isActive { get; set; }
    }
}
