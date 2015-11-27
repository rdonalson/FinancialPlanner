using FinancialPlanner.Data.Properties;

namespace FinancialPlanner.Data.Entity
{
    /// =====================================================================
    /// <summary>
    ///     Connection String Utilities
    /// </summary>
    /// =====================================================================
    public static class ConnectionStringManager
    {
        /// ---------------------------------------------------------------------
        /// <summary>
        ///     The conntype value will come from the "CONN_TYPE" resource value in
        ///     FinancialPlanner.Web app and is used to set the application connection to:
        ///     Set the application connection Type to:
        ///     1  - Admin (enAdmin EF Connection)
        ///     2  - Item Detail (enItemDetail EF Connection)
        ///     3  - Default Connection (DefaultConnection Standard Connection)
        ///     Then entity value is the name of the .edmx file in FinancialPlanner.Data
        /// </summary>
        /// <param name="conndirection">int</param>
        /// <param name="conntype">int</param>
        /// <param name="server">string</param>
        /// <param name="database">string</param>
        /// <param name="user">string</param>
        /// <param name="password">string</param>
        /// <returns>Connection String: string</returns>
        /// ---------------------------------------------------------------------
        public static string ConnectionString(
            int conndirection,
            int conntype,
            string server,
            string database,
            string user,
            string password)
        {
            var connstring = Resources.CONNSTRING;
            switch (conndirection)
            {
                // 1 & 2 Are standard sql connection strings
                case 1: // Azure
                case 2: // Local or network server
                    connstring = Resources.CONNSTRING;
                    break;
                // 3 you are connecting to the local database in the
                // App_Data folder
                case 3: // local .mdf file 
                    connstring = Resources.LOCALDBSTRING;
                    break;
            }

            var efmetadatastring = Resources.EFMETADATASTRING;
            string entity;
            var dataSource = string.Format(
                connstring,
                server,
                database,
                user,
                password);
            var result = string.Empty;

            switch (conntype)
            {
                // 1 & 2 are Entity Framework Connections
                case 1: // "enAdmin" 
                    entity = Resources.CONTEXT_ADMIN;
                    result = string.Format(efmetadatastring, entity, dataSource);
                    break;
                case 2: // "enItemDetail"
                    entity = Resources.CONTEXT_ITEMDETAIL;
                    result = string.Format(efmetadatastring, entity, dataSource);
                    break;

                // 3 is a Standard Connection String
                case 3: // "DefaultConnection" 
                    result = dataSource;
                    break;
            }
            // return final connection string
            return result;
        }
    }
}

/* Archive info */
/* ** Azure Server ** */
// metadata=res://*/Entity.enAdmin.csdl|res://*/Entity.enAdmin.ssdl|res://*/Entity.enAdmin.msl;provider=System.Data.SqlClient;
//      provider connection string='Data Source=tcp:pn5745kjcc.database.windows.net,1433;
//      Initial Catalog=FinancialPlanner;User ID=rdonalson@pn5745kjcc;Password=@89UIor#-jk;MultipleActiveResultSets=True;App=EntityFramework'

/* ** Local or Network Server ** */
// Data Source=LENOVO-PC\APPLICATION;Initial Catalog=FinancialPlanner;Persist Security Info=True;User ID=fpadmin;Password=Abc123*

/* ** Local FinancialPlannerLocalDb in App_Data folder ** */
// Data Source=(LocalDb)\v11.0;Initial Catalog=FinancialPlannerLocalDb;Persist Security Info=True;User ID=fpadmin;Password=Abc123*