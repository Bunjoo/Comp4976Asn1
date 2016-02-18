using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomaDataModel
{
    public class Choice
    {
        [Key]
        public int ChoiceId { get; set; }

        [ForeignKey("FK_YearTermId")]
        public int YearTermId { get; set; }
        public YearTerm FK_YearTermId { get; set; }

        [MaxLength(9)]
        [RegularExpression(@"^A[0-9]{8}$")]
        public String StudentId { get; set; }
        public String StudentFirstName { get; set; }
        public String StudentLastName { get; set; }

        [Display(Name = "First Choice: ")]
        [ForeignKey("FirstOption")]
        public int? FirstChoiceOptionId { get; set; }
        [ForeignKey("FirstChoiceOptionId")]
        public Option FirstOption { get; set; }

        [Display(Name = "Second Choice: ")]
        [ForeignKey("SecondOption")]
        public int? SecondChoiceOptionId { get; set; }
        [ForeignKey("SecondChoiceOptionId")]
        public Option SecondOption { get; set; }

        [Display(Name = "Third Choice: ")]
        [ForeignKey("ThirdOption")]
        public int? ThirdChoiceOptionId { get; set; }
        [ForeignKey("ThirdChoiceOptionId")]
        public Option ThirdOption { get; set; }

        [Display(Name = "Fourth Choice: ")]
        [ForeignKey("FourthOption")]
        public int? FourthChoiceOptionId { get; set; }
        [ForeignKey("FourthChoiceOptionId")]
        public Option FourthOption { get; set; }

        [ScaffoldColumn(false)]
        public DateTime SelectionDate { get; set; }
    }
}
