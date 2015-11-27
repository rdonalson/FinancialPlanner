using System;
using System.Data.Entity;
using System.Linq;
using FinancialPlanner.Data.Entity;
using FinancialPlanner.Infrastructure.Domain.ItemDetail.ViewModels;

namespace FinancialPlanner.Infrastructure.Domain.ItemDetail.Initial.Repository
{
    /// =====================================================================
    /// <summary>
    ///     Initial Amount Repository
    /// </summary>
    /// =====================================================================
    public class InitialAmountRepository : IInitialAmountRepository
    {
        private readonly ItemDetailEntities _db;

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Base Constructor
        /// </summary>
        /// ---------------------------------------------------------------------
        public InitialAmountRepository(
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
        ///     Return the List of Initial Amounts which will always be just one
        /// </summary>
        /// <param name="userName">userName</param>
        /// <returns>IQueryable(InitialAmount)</returns>
        /// ---------------------------------------------------------------------
        public IQueryable<InitialAmount> GetInitialAmounts(string userName)
        {
            try
            {
                return _db.InitialAmounts.Where(d => d.UserName == userName);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Get Initial Amount View
        /// </summary>
        /// <param name="id">int?</param>
        /// <param name="userName">string</param>
        /// <returns>DetailItemView</returns>
        /// ---------------------------------------------------------------------
        public InitialAmountView GetInitialAmountView(int? id, string userName)
        {
            try
            {
                var initialAmount = GetInitialAmount(id, userName);
                if (initialAmount != null)
                {
                    var initialAmountView = new InitialAmountView
                    {
                        PkID = initialAmount.PkID,
                        UserName = initialAmount.UserName,
                        StrAmount = initialAmount.Amount.ToString(),
                        BeginDate = initialAmount.BeginDate
                    };
                    return initialAmountView;
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
        /// <param name="initialAmountView">InitialAmountView</param>
        /// <returns>bool</returns>
        /// ---------------------------------------------------------------------
        public bool Save(InitialAmountView initialAmountView)
        {
            try
            {
                var initialAmount = GetInitialAmount(initialAmountView.PkID, initialAmountView.UserName);
                if (initialAmount != null)
                {
                    initialAmount.Amount = Convert.ToDecimal(initialAmountView.StrAmount.Replace("$", ""));
                    initialAmount.BeginDate = initialAmountView.BeginDate;

                    _db.Entry(initialAmount).State = EntityState.Modified;
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
        /// <param name="initialAmountView">InitialAmountView</param>
        /// <returns>bool</returns>
        /// ---------------------------------------------------------------------
        public bool Add(InitialAmountView initialAmountView)
        {
            try
            {
                var initialAmount = new InitialAmount
                {
                    UserName = initialAmountView.UserName,
                    Amount = Convert.ToDecimal(initialAmountView.StrAmount.Replace("$", "")),
                    BeginDate = initialAmountView.BeginDate
                };

                _db.InitialAmounts.Add(initialAmount);
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
        ///     Delete Initial Amount
        /// </summary>
        /// <param name="id">int?</param>
        /// <param name="userName">string</param>
        /// <returns>bool</returns>
        /// ---------------------------------------------------------------------
        public bool Delete(int? id, string userName)
        {
            try
            {
                var initialAmount = GetInitialAmount(id, userName);
                _db.InitialAmounts.Remove(initialAmount);
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
        ///     Get a specific Initial Amount for the User
        /// </summary>
        /// <param name="id">int?</param>
        /// <param name="userName">string</param>
        /// <returns>InitialAmount</returns>
        /// ---------------------------------------------------------------------
        private InitialAmount GetInitialAmount(int? id, string userName)
        {
            try
            {
                return _db.InitialAmounts.SingleOrDefault(i => i.PkID == id && i.UserName == userName);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}