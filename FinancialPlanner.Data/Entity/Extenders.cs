namespace FinancialPlanner.Data.Entity
{
    /// =====================================================================
    /// <summary>
    /// Entity Framework Extenders 
    /// </summary>
    /// =====================================================================
    public partial class AdminEntities
    {
        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Custom Constructor
        /// </summary>
        /// ---------------------------------------------------------------------
        public AdminEntities(string connectionString)
            : base(connectionString)
        {
        }
    }
    /// ---------------------------------------------------------------------
    /// <summary>
    ///     Custom Constructor
    /// </summary>
    /// ---------------------------------------------------------------------
    public partial class ItemDetailEntities
    {
        public ItemDetailEntities(string connectionString)
            : base(connectionString)
        {
        }
    }
}