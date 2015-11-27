using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FinancialPlanner.Infrastructure.Domain.ItemDetail.Enums;
using FinancialPlanner.Infrastructure.Domain.ItemDetail.Periods.Repository;
using FinancialPlanner.Web.Properties;

namespace FinancialPlanner.Web.Helpers
{
    /// =====================================================================
    /// <summary>
    /// Utility Functions
    /// </summary>
    /// =====================================================================
    public class WebHelpers
    {
        private readonly IPeriodRepository _periodRepository;

        /// ---------------------------------------------------------------------
        /// <summary>
        /// Constructor
        /// </summary>
        /// ---------------------------------------------------------------------
        public WebHelpers()
        {
            var conndirection = Convert.ToInt32(Resources.CONN_DIRECTION);
            string server, database, userid, password;
            ConnectionHelpers.ConnStringParameters(conndirection, out server, out database, out userid, out password);
            _periodRepository = new PeriodRepository(conndirection, server, database, userid, password);
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        /// Gets all of the Periods and then sets the selected value
        /// </summary>
        /// <param name="id">int?</param>
        /// <returns>SelectList</returns>
        /// ---------------------------------------------------------------------
        public SelectList PeriodSelectList(int? id)
        {
            try
            {
                return new SelectList(_periodRepository.GetPeriods(), "PkPeriod", "Name", id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        /// Gets all of the Periods and then sets the selected value
        /// </summary>
        /// <returns>SelectList</returns>
        /// ---------------------------------------------------------------------
        public SelectList PeriodSelectList()
        {
            try
            {
                return new SelectList(_periodRepository.GetPeriods(), "PkPeriod", "Name");
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        /// Generates a list of Weekdays from the Weekday enum 
        /// </summary>
        /// <returns>SelectList</returns>
        /// ---------------------------------------------------------------------
        public SelectList WeekdaySelectList()
        {
            try
            {
                var lsList = new List<WeekDay>();
                for (int i = 0; i <= 6; i++)
                {
                    lsList.Add(new WeekDay
                    {
                        DayVal = (i + 1),
                        DayName = WeekdayList()[i].ToString()
                    });
                }
                return new SelectList(lsList, "DayVal", "DayName");
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        /// Generates a list of Weekdays from the Weekday enum 
        /// and then sets the selected value
        /// </summary>
        /// <param name="value">Weekday</param>
        /// <returns>SelectList</returns>
        /// ---------------------------------------------------------------------
        public SelectList WeekdaySelectList(Weekday? value)
        {
            try
            {
                int? selected = Convert.ToInt32(value);
                var lsList = new List<WeekDay>();
                for (int i = 0; i <= 6; i++)
                {
                    lsList.Add(new WeekDay
                    {
                        DayVal = (i + 1),
                        DayName = WeekdayList()[i].ToString()
                    });
                }
                return new SelectList(lsList, "DayVal", "DayName", selected);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        /// Weekday enum list initializer
        /// </summary>
        /// <returns>List(Weekday)</returns>
        /// ---------------------------------------------------------------------
        protected static List<Weekday> WeekdayList()
        {
            return new List<Weekday>
            {
                Weekday.Sunday,
                Weekday.Monday,
                Weekday.Tuesday,
                Weekday.Wednesday,
                Weekday.Thursday,
                Weekday.Friday,
                Weekday.Saturday,
            };
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        /// Weekday utility class
        /// </summary>
        /// ---------------------------------------------------------------------
        protected class WeekDay
        {
            public int DayVal { get; set; }
            public string DayName { get; set; }
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Remove DBContext
        /// </summary>
        /// ---------------------------------------------------------------------
        public void Dispose()
        {
            _periodRepository.Dispose();
        }
    }
}