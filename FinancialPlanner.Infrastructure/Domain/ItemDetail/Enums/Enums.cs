using System.ComponentModel.DataAnnotations;

namespace FinancialPlanner.Infrastructure.Domain.ItemDetail.Enums
{
    /// =====================================================================
    /// <summary>
    ///     Utilities
    /// </summary>
    /// =====================================================================
    public enum Weekday
    {
        [Display(Name = "Sunday")] Sunday = 1,
        [Display(Name = "Monday")] Monday = 2,
        [Display(Name = "Tuesday")] Tuesday = 3,
        [Display(Name = "Wednesday")] Wednesday = 4,
        [Display(Name = "Thursday")] Thursday = 5,
        [Display(Name = "Friday")] Friday = 6,
        [Display(Name = "Saturday")] Saturday = 7
    }

    public enum DayOfMonth
    {
        [Display(Name = "1")] First = 1,
        [Display(Name = "2")] Second = 2,
        [Display(Name = "3")] Third = 3,
        [Display(Name = "4")] Fourth = 4,
        [Display(Name = "5")] Fifth = 5,
        [Display(Name = "6")] Sixth = 6,
        [Display(Name = "7")] Seventh = 7,
        [Display(Name = "8")] Eighth = 8,
        [Display(Name = "9")] Ninth = 9,
        [Display(Name = "10")] Tenth = 10,
        [Display(Name = "11")] Eleventh = 11,
        [Display(Name = "12")] Twelfth = 12,
        [Display(Name = "13")] Thirteenth = 13,
        [Display(Name = "14")] Fourteenth = 14,
        [Display(Name = "15")] Fifteenth = 15,
        [Display(Name = "16")] Sixteenth = 16,
        [Display(Name = "17")] Seventeenth = 17,
        [Display(Name = "18")] Eighteenth = 18,
        [Display(Name = "19")] Nineteenth = 19,
        [Display(Name = "20")] Twentieth = 20,
        [Display(Name = "21")] TwentyFirst = 21,
        [Display(Name = "22")] TwentySecond = 22,
        [Display(Name = "23")] TwentyThird = 23,
        [Display(Name = "24")] TwentyFourth = 24,
        [Display(Name = "25")] TwentyFifth = 25,
        [Display(Name = "26")] TwentySixth = 26,
        [Display(Name = "27")] TwentySeventh = 27,
        [Display(Name = "28")] TwentyEighth = 28
    }

    public enum Month
    {
        [Display(Name = "January")] January = 1,
        [Display(Name = "Feburary")] Feburary = 2,
        [Display(Name = "March")] March = 3,
        [Display(Name = "April")] April = 4,
        [Display(Name = "May")] May = 5,
        [Display(Name = "June")] June = 6,
        [Display(Name = "July")] July = 7,
        [Display(Name = "August")] August = 8,
        [Display(Name = "September")] September = 9,
        [Display(Name = "October")] October = 10,
        [Display(Name = "November")] November = 11,
        [Display(Name = "December")] December = 12
    }
}