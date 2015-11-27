using System;
using System.Data.Entity;
using System.Linq;
using FinancialPlanner.Data.Entity;
using FinancialPlanner.Infrastructure.Domain.ItemDetail.Enums;
using FinancialPlanner.Infrastructure.Domain.ItemDetail.ViewModels;

namespace FinancialPlanner.Infrastructure.Domain.ItemDetail.Credits.Repository
{
    /// =====================================================================
    /// <summary>
    ///     Credit Repository
    /// </summary>
    /// =====================================================================
    public class CreditRepository : ICreditRepository
    {
        private readonly ItemDetailEntities _db;

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Base Constructor
        /// </summary>
        /// ---------------------------------------------------------------------
        public CreditRepository(
            int conndirection,
            string server,
            string database,
            string userid,
            string password)
        {
            _db = new ItemDetailEntities(ConnectionStringManager.ConnectionString(
                    conndirection,
                    2,
                    server,
                    database,
                    userid,
                    password));
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Return List of all Credits
        /// </summary>
        /// <param name="userName">userName</param>
        /// <returns>IQueryable(Credit)</returns>
        /// ---------------------------------------------------------------------
        public IQueryable<Credit> GetCredits(string userName)
        {
            try
            {
                return _db.Credits.Where(d => d.UserName == userName).Include(d => d.Period);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Get Credit View
        /// </summary>
        /// <param name="id">int?</param>
        /// <param name="userName">string</param>
        /// <returns>DetailItemView</returns>
        /// ---------------------------------------------------------------------
        public DetailItemView GetCreditView(int? id, string userName)
        {
            try
            {
                var credit = GetCredit(id, userName);
                if (credit != null)
                {
                    var creditView = new DetailItemView
                    {
                        PkID = credit.PkCredit,
                        UserName = credit.UserName,
                        Name = credit.Name,
                        StrAmount = credit.Amount.ToString(),
                        FkPeriod = credit.FkPeriod,
                        BeginDate = credit.BeginDate,
                        EndDate = credit.EndDate,
                        WeeklyDOW = (Weekday?) credit.WeeklyDOW,
                        EverOtherWeekDOW = (Weekday?) credit.EverOtherWeekDOW,
                        BiMonthlyDay1 = (DayOfMonth?) credit.BiMonthlyDay1,
                        BiMonthlyDay2 = (DayOfMonth?) credit.BiMonthlyDay2,
                        MonthlyDOM = (DayOfMonth?) credit.MonthlyDOM,
                        Quarterly1Month = (Month?) credit.Quarterly1Month,
                        Quarterly1Day = (DayOfMonth?) credit.Quarterly1Day,
                        Quarterly2Month = (Month?) credit.Quarterly2Month,
                        Quarterly2Day = (DayOfMonth?) credit.Quarterly2Day,
                        Quarterly3Month = (Month?) credit.Quarterly3Month,
                        Quarterly3Day = (DayOfMonth?) credit.Quarterly3Day,
                        Quarterly4Month = (Month?) credit.Quarterly4Month,
                        Quarterly4Day = (DayOfMonth?) credit.Quarterly4Day,
                        SemiAnnual1Month = (Month?) credit.SemiAnnual1Month,
                        SemiAnnual1Day = (DayOfMonth?) credit.SemiAnnual1Day,
                        SemiAnnual2Month = (Month?) credit.SemiAnnual2Month,
                        SemiAnnual2Day = (DayOfMonth?) credit.SemiAnnual2Day,
                        AnnualMOY = (Month?) credit.AnnualMOY,
                        AnnualDOM = (DayOfMonth?) credit.AnnualDOM,
                        DateRangeReq = credit.DateRangeReq,
                        Period = credit.Period
                    };
                    return creditView;
                }
                throw new ArgumentNullException();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Save Credit View
        /// </summary>
        /// <param name="detailItemView">DetailItemView</param>
        /// <returns>bool</returns>
        /// ---------------------------------------------------------------------
        public bool Save(DetailItemView detailItemView)
        {
            try
            {
                var credit = GetCredit(detailItemView.PkID, detailItemView.UserName);
                if (credit != null)
                {
                    credit.Name = detailItemView.Name;
                    credit.Amount = Convert.ToDecimal(detailItemView.StrAmount.Replace("$", ""));
                    credit.FkPeriod = detailItemView.FkPeriod;
                    credit.BeginDate = detailItemView.BeginDate;
                    credit.EndDate = detailItemView.EndDate;
                    credit.WeeklyDOW = (int?) detailItemView.WeeklyDOW;
                    credit.EverOtherWeekDOW = (int?) detailItemView.EverOtherWeekDOW;
                    credit.BiMonthlyDay1 = (int?) detailItemView.BiMonthlyDay1;
                    credit.BiMonthlyDay2 = (int?) detailItemView.BiMonthlyDay2;
                    credit.MonthlyDOM = (int?) detailItemView.MonthlyDOM;
                    credit.Quarterly1Month = (int?) detailItemView.Quarterly1Month;
                    credit.Quarterly1Day = (int?) detailItemView.Quarterly1Day;
                    credit.Quarterly2Month = (int?) detailItemView.Quarterly2Month;
                    credit.Quarterly2Day = (int?) detailItemView.Quarterly2Day;
                    credit.Quarterly3Month = (int?) detailItemView.Quarterly3Month;
                    credit.Quarterly3Day = (int?) detailItemView.Quarterly3Day;
                    credit.Quarterly4Month = (int?) detailItemView.Quarterly4Month;
                    credit.Quarterly4Day = (int?) detailItemView.Quarterly4Day;
                    credit.SemiAnnual1Month = (int?) detailItemView.SemiAnnual1Month;
                    credit.SemiAnnual1Day = (int?) detailItemView.SemiAnnual1Day;
                    credit.SemiAnnual2Month = (int?) detailItemView.SemiAnnual2Month;
                    credit.SemiAnnual2Day = (int?) detailItemView.SemiAnnual2Day;
                    credit.AnnualMOY = (int?) detailItemView.AnnualMOY;
                    credit.AnnualDOM = (int?) detailItemView.AnnualDOM;
                    credit.DateRangeReq = detailItemView.DateRangeReq;

                    credit.Period = detailItemView.Period;

                    _db.Entry(credit).State = EntityState.Modified;
                    _db.SaveChanges();
                }
                else
                {
                    throw new ArgumentNullException();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Add New Credit
        /// </summary>
        /// <param name="detailItemView">DetailItemView</param>
        /// <returns>bool</returns>
        /// ---------------------------------------------------------------------
        public bool Add(DetailItemView detailItemView)
        {
            try
            {
                var credit = new Credit
                {
                    UserName = detailItemView.UserName,
                    Name = detailItemView.Name,
                    Amount = Convert.ToDecimal(detailItemView.StrAmount.Replace("$", "")),
                    FkPeriod = detailItemView.FkPeriod,
                    BeginDate = detailItemView.BeginDate,
                    EndDate = detailItemView.EndDate,
                    WeeklyDOW = (int?) detailItemView.WeeklyDOW,
                    EverOtherWeekDOW = (int?) detailItemView.EverOtherWeekDOW,
                    BiMonthlyDay1 = (int?) detailItemView.BiMonthlyDay1,
                    BiMonthlyDay2 = (int?) detailItemView.BiMonthlyDay2,
                    MonthlyDOM = (int?) detailItemView.MonthlyDOM,
                    Quarterly1Month = (int?) detailItemView.Quarterly1Month,
                    Quarterly1Day = (int?) detailItemView.Quarterly1Day,
                    Quarterly2Month = (int?) detailItemView.Quarterly2Month,
                    Quarterly2Day = (int?) detailItemView.Quarterly2Day,
                    Quarterly3Month = (int?) detailItemView.Quarterly3Month,
                    Quarterly3Day = (int?) detailItemView.Quarterly3Day,
                    Quarterly4Month = (int?) detailItemView.Quarterly4Month,
                    Quarterly4Day = (int?) detailItemView.Quarterly4Day,
                    SemiAnnual1Month = (int?) detailItemView.SemiAnnual1Month,
                    SemiAnnual1Day = (int?) detailItemView.SemiAnnual1Day,
                    SemiAnnual2Month = (int?) detailItemView.SemiAnnual2Month,
                    SemiAnnual2Day = (int?) detailItemView.SemiAnnual2Day,
                    AnnualMOY = (int?) detailItemView.AnnualMOY,
                    AnnualDOM = (int?) detailItemView.AnnualDOM,
                    DateRangeReq = detailItemView.DateRangeReq,
                    Period = detailItemView.Period
                };

                _db.Credits.Add(credit);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Delete Credit
        /// </summary>
        /// <param name="id">int?</param>
        /// <param name="userName">string</param>
        /// <returns>bool</returns>
        /// ---------------------------------------------------------------------
        public bool Delete(int? id, string userName)
        {
            try
            {
                var credit = GetCredit(id, userName);
                _db.Credits.Remove(credit);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Remove DBContext
        /// </summary>
        /// ---------------------------------------------------------------------
        public void Dispose()
        {
            _db.Dispose();
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Get a specific Credit
        /// </summary>
        /// <param name="id">int?</param>
        /// <param name="userName">string</param>
        /// <returns>Credit</returns>
        /// ---------------------------------------------------------------------
        private Credit GetCredit(int? id, string userName)
        {
            try
            {
                return _db.Credits.SingleOrDefault(d => d.PkCredit == id && d.UserName == userName);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}