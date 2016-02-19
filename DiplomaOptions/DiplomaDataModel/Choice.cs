using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace DiplomaDataModel
{
    public class Choice
    {
        [Key]
        public int ChoiceId { get; set; }

        [ScaffoldColumn(false)]
        [ForeignKey("FK_YearTermId")]
        public int YearTermId { get; set; }
        public YearTerm FK_YearTermId { get; set; }

        [MaxLength(9)]
        [Display(Name ="Student Id")]
        [RegularExpression(@"^A[0-9]{8}$")]
        public String StudentId { get; set; }
        [Display(Name = "First Name")]
        public String StudentFirstName { get; set; }
        [Display(Name = "Last Name")]
        public String StudentLastName { get; set; }

        [ForeignKey("FirstOption")]
        [Display(Name = "First Choice")]
        public int? FirstChoiceOptionId { get; set; }
        [ForeignKey("FirstChoiceOptionId")]
        [Display(Name = "First Choice")]
        public Option FirstOption { get; set; }

        [ForeignKey("SecondOption")]
        public int? SecondChoiceOptionId { get; set; }
        [ForeignKey("SecondChoiceOptionId")]
        [Display(Name = "Second Choice")]
        public Option SecondOption { get; set; }

        [ForeignKey("ThirdOption")]
        public int? ThirdChoiceOptionId { get; set; }
        [ForeignKey("ThirdChoiceOptionId")]
        [DisplayName("Third Choice")]
        public Option ThirdOption { get; set; }

        [ForeignKey("FourthOption")]
        [DisplayName("Fourth Choice: ")]
        public int? FourthChoiceOptionId { get; set; }
        [ForeignKey("FourthChoiceOptionId")]
        [DisplayName("Fourth Choice")]
        public Option FourthOption { get; set; }

        [ScaffoldColumn(false)]
        public DateTime SelectionDate { get; set; }
    }
}
