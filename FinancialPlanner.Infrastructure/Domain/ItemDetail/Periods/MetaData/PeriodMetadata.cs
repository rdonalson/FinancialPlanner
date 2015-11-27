using System.ComponentModel.DataAnnotations;

namespace FinancialPlanner.Infrastructure.Domain.ItemDetail.Periods.MetaData
{
    public class PeriodMetadata
    {
        [Required(ErrorMessage = "Please enter : PkPeriod")]
        [Display(Name = "Period ID")]
        public int PkPeriod { get; set; }

        [Required(ErrorMessage = "Please enter : Period")]
        [Display(Name = "Period")]
        public string Name { get; set; }
    }
}