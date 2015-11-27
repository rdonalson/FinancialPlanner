using System;
using System.ComponentModel.DataAnnotations;
using FinancialPlanner.Data.Entity;
using FinancialPlanner.Infrastructure.Domain.ItemDetail.Enums;

namespace FinancialPlanner.Infrastructure.Domain.ItemDetail.ViewModels
{
    /// <summary>
    ///     This model is used for Intsert and Update duties for both
    ///     Credits and Debits, since they have same properties
    /// </summary>
    public class DetailItemView
    {
        /// <summary>
        ///     General Properties
        /// </summary>
        public int PkID { get; set; }

        public string UserName { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please enter: A representative name")]
        public string Name { get; set; }

        [Display(Name = "Amount")]
        [Required(ErrorMessage = "Please enter: The amount")]
        [RegularExpression(@"\$\040{0,1}(\d{1,3}(\,\d{3})*|(\d+))(\.\d{2})?", ErrorMessage = "Invalid Currency Value")]
        public string StrAmount { get; set; }

        [Required(ErrorMessage = "Please enter: Period")]
        [Display(Name = "Period:")]
        public int? FkPeriod { get; set; }

        [Display(Name = "Start Date:")]
        [Required(ErrorMessage = "Please select: A date")]
        public DateTime? BeginDate { get; set; }

        [Display(Name = "End Date:")]
        [Required(ErrorMessage = "Please select: A date")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Timeframe?")]
        public bool DateRangeReq { get; set; }

        [Display(Name = "Period")]
        public Period Period { get; set; }

        /// <summary>
        ///     Weekly
        /// </summary>
        [Display(Name = "Every Week:")]
        [Required(ErrorMessage = "Please select: A weekday")]
        public Weekday? WeeklyDOW { get; set; }

        /// <summary>
        ///     Every Other Week ** Also uses "BeginDate" as an Initalization Date
        /// </summary>
        [Display(Name = "Every Other Week:")]
        [Range(1, 7)]
        [Required(ErrorMessage = "Please select: A weekday")]
        public Weekday? EverOtherWeekDOW { get; set; }

        /// <summary>
        ///     Bi-Monthly
        /// </summary>
        [Display(Name = "Bi-Monthly:")]
        [Required(ErrorMessage = "Please select: The 1st day of occurrence")]
        [Range(1, 28)]
        public DayOfMonth? BiMonthlyDay1 { get; set; }

        [Required(ErrorMessage = "Please select: The 2nd day of occurrence")]
        [Range(1, 28)]
        public DayOfMonth? BiMonthlyDay2 { get; set; }

        /// <summary>
        ///     Monthly
        /// </summary>
        [Display(Name = "Day of the Month:")]
        [Required(ErrorMessage = "Please select: The day of occurrence")]
        [Range(1, 28)]
        public DayOfMonth? MonthlyDOM { get; set; }

        /// <summary>
        ///     Quarterly
        /// </summary>
        [Display(Name = "1st Quarter:")]
        [Required(ErrorMessage = "Please enter: The 1st month of occurrence")]
        [Range(1, 12)]
        public Month? Quarterly1Month { get; set; }

        [Required(ErrorMessage = "Please enter: The day of occurrence")]
        [Range(1, 28)]
        public DayOfMonth? Quarterly1Day { get; set; }

        [Display(Name = "2nd Quarter:")]
        [Required(ErrorMessage = "Please enter: The 2nd month of occurrence")]
        [Range(1, 12)]
        public Month? Quarterly2Month { get; set; }

        [Required(ErrorMessage = "Please enter: The day of occurrence")]
        [Range(1, 28)]
        public DayOfMonth? Quarterly2Day { get; set; }

        [Display(Name = "3rd Quarter:")]
        [Required(ErrorMessage = "Please enter: The 3rd month of occurrence")]
        [Range(1, 12)]
        public Month? Quarterly3Month { get; set; }

        [Required(ErrorMessage = "Please enter: The day of occurrence")]
        [Range(1, 28)]
        public DayOfMonth? Quarterly3Day { get; set; }

        [Display(Name = "4th Quarter:")]
        [Required(ErrorMessage = "Please enter: The 4th month of occurrence")]
        [Range(1, 12)]
        public Month? Quarterly4Month { get; set; }

        [Required(ErrorMessage = "Please enter: The day of occurrence")]
        [Range(1, 28)]
        public DayOfMonth? Quarterly4Day { get; set; }

        /// <summary>
        ///     Semi-Annual
        /// </summary>
        [Display(Name = "1st Semi-Annual:")]
        [Required(ErrorMessage = "Please enter: The 1st month of occurrence")]
        [Range(1, 12)]
        public Month? SemiAnnual1Month { get; set; }

        [Required(ErrorMessage = "Please enter: The day of occurrence")]
        [Range(1, 28)]
        public DayOfMonth? SemiAnnual1Day { get; set; }

        [Display(Name = "2nd Semi-Annual:")]
        [Required(ErrorMessage = "Please enter: The 2nd month of occurrence")]
        [Range(1, 12)]
        public Month? SemiAnnual2Month { get; set; }

        [Required(ErrorMessage = "Please enter: The day of occurrence")]
        [Range(1, 28)]
        public DayOfMonth? SemiAnnual2Day { get; set; }

        /// <summary>
        ///     Annual
        /// </summary>
        [Display(Name = "Annual:")]
        [Required(ErrorMessage = "Please enter: The month of occurrence")]
        [Range(1, 12)]
        public Month? AnnualMOY { get; set; }

        [Required(ErrorMessage = "Please enter: The day of occurrence")]
        [Range(1, 28)]
        public DayOfMonth? AnnualDOM { get; set; }
    }
}

// Archive
//[DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]