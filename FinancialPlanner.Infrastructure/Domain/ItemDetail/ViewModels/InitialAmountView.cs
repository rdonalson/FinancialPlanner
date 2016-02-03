using System;
using System.ComponentModel.DataAnnotations;

namespace FinancialPlanner.Infrastructure.Domain.ItemDetail.ViewModels
{
    /// =====================================================================
    /// <summary>
    ///     This View Model is used for Insert and Update duties for the
    ///     Initial Amount
    /// </summary>
    /// =====================================================================
    public class InitialAmountView
    {
        /// ---------------------------------------------------------------------
        /// <summary>
        ///     General Properties
        /// </summary>
        /// ---------------------------------------------------------------------
        public int PkID { get; set; }

        public string UserName { get; set; }

        [Display(Name = "Initialization Amount")]
        [Required(ErrorMessage = "Please enter: The initial amount")]
        [RegularExpression(@"\$\040{0,1}(\d{1,3}(\,\d{3})*|(\d+))(\.\d{2})?", ErrorMessage = "Invalid Currency Value")]
        public string StrAmount { get; set; }

		// Archive
        //[Display(Name = "Initialization Date:")]
        //[Required(ErrorMessage = "Please select: A date")]
        //public DateTime? BeginDate { get; set; }
    }
}