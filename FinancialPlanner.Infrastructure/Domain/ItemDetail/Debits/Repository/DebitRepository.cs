using System;
using System.Data.Entity;
using System.Linq;
using FinancialPlanner.Data.Entity;
using FinancialPlanner.Infrastructure.Domain.ItemDetail.Enums;
using FinancialPlanner.Infrastructure.Domain.ItemDetail.ViewModels;

namespace FinancialPlanner.Infrastructure.Domain.ItemDetail.Debits.Repository
{
    /// =====================================================================
    /// <summary>
    ///     Debit Repository
    /// </summary>
    /// =====================================================================
    public class DebitRepository : IDebitRepository
    {
        private readonly ItemDetailEntities _db;

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Base Constructor
        /// </summary>
        /// ---------------------------------------------------------------------
        public DebitRepository(
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
        ///     Return List of all Debits
        /// </summary>
        /// <param name="userName">userName</param>
        /// <returns>IQueryable(Debit)</returns>
        /// ---------------------------------------------------------------------
        public IQueryable<Debit> GetDebits(string userName)
        {
            try
            {
                return _db.Debits.Where(d => d.UserName == userName).Include(d => d.Period);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Get Debit View
        /// </summary>
        /// <param name="id">int?</param>
        /// <param name="userName">string</param>
        /// <returns>DetailItemView</returns>
        /// ---------------------------------------------------------------------
        public DetailItemView GetDebitView(int? id, string userName)
        {
            try
            {
                var debit = GetDebit(id, userName);
                if (debit != null)
                {
                    //int? x = 6; // Diagnostic
                    var debitView = new DetailItemView
                    {
                        PkID = debit.PkDebit,
                        UserName = debit.UserName,
                        Name = debit.Name,
                        StrAmount = debit.Amount.ToString(),
                        FkPeriod = debit.FkPeriod,
                        BeginDate = debit.BeginDate,
                        EndDate = debit.EndDate,
                        WeeklyDOW = (Weekday?) debit.WeeklyDOW,
                        EverOtherWeekDOW = (Weekday?) debit.EverOtherWeekDOW,
                        BiMonthlyDay1 = (DayOfMonth?) debit.BiMonthlyDay1,
                        BiMonthlyDay2 = (DayOfMonth?) debit.BiMonthlyDay2,
                        MonthlyDOM = (DayOfMonth?) debit.MonthlyDOM,
                        Quarterly1Month = (Month?) debit.Quarterly1Month,
                        Quarterly1Day = (DayOfMonth?) debit.Quarterly1Day,
                        Quarterly2Month = (Month?) debit.Quarterly2Month,
                        Quarterly2Day = (DayOfMonth?) debit.Quarterly2Day,
                        Quarterly3Month = (Month?) debit.Quarterly3Month,
                        Quarterly3Day = (DayOfMonth?) debit.Quarterly3Day,
                        Quarterly4Month = (Month?) debit.Quarterly4Month,
                        Quarterly4Day = (DayOfMonth?) debit.Quarterly4Day,
                        SemiAnnual1Month = (Month?) debit.SemiAnnual1Month,
                        SemiAnnual1Day = (DayOfMonth?) debit.SemiAnnual1Day,
                        SemiAnnual2Month = (Month?) debit.SemiAnnual2Month,
                        SemiAnnual2Day = (DayOfMonth?) debit.SemiAnnual2Day,
                        AnnualMOY = (Month?) debit.AnnualMOY,
                        AnnualDOM = (DayOfMonth?) debit.AnnualDOM,
                        DateRangeReq = debit.DateRangeReq,
                        Period = debit.Period
                    };
                    return debitView;
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
        ///     Add New Debit
        /// </summary>
        /// <param name="detailItemView">DetailItemView</param>
        /// <returns>bool</returns>
        /// ---------------------------------------------------------------------
        public bool Add(DetailItemView detailItemView)
        {
            try
            {
                var debit = new Debit
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

                _db.Debits.Add(debit);
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
        ///     Save Debit View
        /// </summary>
        /// <param name="detailItemView">DetailItemView</param>
        /// <returns>bool</returns>
        /// ---------------------------------------------------------------------
        public bool Save(DetailItemView detailItemView)
        {
            try
            {
                var debit = GetDebit(detailItemView.PkID, detailItemView.UserName);
                if (debit != null)
                {
                    debit.Name = detailItemView.Name;
                    debit.Amount = Convert.ToDecimal(detailItemView.StrAmount.Replace("$", ""));
                    debit.FkPeriod = detailItemView.FkPeriod;
                    debit.BeginDate = detailItemView.BeginDate;
                    debit.EndDate = detailItemView.EndDate;
                    debit.WeeklyDOW = (int?) detailItemView.WeeklyDOW;
                    debit.EverOtherWeekDOW = (int?) detailItemView.EverOtherWeekDOW;
                    debit.BiMonthlyDay1 = (int?) detailItemView.BiMonthlyDay1;
                    debit.BiMonthlyDay2 = (int?) detailItemView.BiMonthlyDay2;
                    debit.MonthlyDOM = (int?) detailItemView.MonthlyDOM;
                    debit.Quarterly1Month = (int?) detailItemView.Quarterly1Month;
                    debit.Quarterly1Day = (int?) detailItemView.Quarterly1Day;
                    debit.Quarterly2Month = (int?) detailItemView.Quarterly2Month;
                    debit.Quarterly2Day = (int?) detailItemView.Quarterly2Day;
                    debit.Quarterly3Month = (int?) detailItemView.Quarterly3Month;
                    debit.Quarterly3Day = (int?) detailItemView.Quarterly3Day;
                    debit.Quarterly4Month = (int?) detailItemView.Quarterly4Month;
                    debit.Quarterly4Day = (int?) detailItemView.Quarterly4Day;
                    debit.SemiAnnual1Month = (int?) detailItemView.SemiAnnual1Month;
                    debit.SemiAnnual1Day = (int?) detailItemView.SemiAnnual1Day;
                    debit.SemiAnnual2Month = (int?) detailItemView.SemiAnnual2Month;
                    debit.SemiAnnual2Day = (int?) detailItemView.SemiAnnual2Day;
                    debit.AnnualMOY = (int?) detailItemView.AnnualMOY;
                    debit.AnnualDOM = (int?) detailItemView.AnnualDOM;
                    debit.DateRangeReq = detailItemView.DateRangeReq;

                    debit.Period = detailItemView.Period;

                    _db.Entry(debit).State = EntityState.Modified;
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
        ///     Delete Debit
        /// </summary>
        /// <param name="id">int?</param>
        /// <param name="userName">string</param>
        /// <returns>bool</returns>
        /// ---------------------------------------------------------------------
        public bool Delete(int? id, string userName)
        {
            try
            {
                var debit = GetDebit(id, userName);
                _db.Debits.Remove(debit);
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
        ///     Get a specific Debit
        /// </summary>
        /// <param name="id">int?</param>
        /// <param name="userName">string</param>
        /// <returns>Debit</returns>
        /// ---------------------------------------------------------------------
        private Debit GetDebit(int? id, string userName)
        {
            try
            {
                return _db.Debits.SingleOrDefault(d => d.PkDebit == id && d.UserName == userName);
            }
            catch (Exception ex)
            {
                return null;
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
    }
}