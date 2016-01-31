using System;
using FinancialPlanner.Data.Entity;
using FinancialPlanner.Web.Properties;

namespace FinancialPlanner.Web.Helpers
{
    /// =====================================================================
    /// <summary>
    ///     Connection String Helpers
    /// </summary>
    /// =====================================================================
    public static class ConnectionHelpers
    {
        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Set the application connection to:
        ///     1  - Azure (Your Azure datatbase)
        ///     2  - Server (A network or local server database)
        ///     3  - LocalDb (The FinancialPlannerLocalDb.mdf database in App_Data folder)
        /// </summary>
        /// <param name="conndirection">int</param>
        /// <param name="server">out string</param>
        /// <param name="database">out string</param>
        /// <param name="user">out string</param>
        /// <param name="password">out string</param>
        /// ---------------------------------------------------------------------
        public static void ConnStringParameters(
            int conndirection,
            out string server,
            out string database,
            out string user,
            out string password)
        {
            // Initialization
            server = string.Empty;
            database = string.Empty;
            user = string.Empty;
            password = string.Empty;

            switch (conndirection)
            {
                case 1: // Azure (Your Azure datatbase)
                    server = Resources.CONN_AZURE_SERVER;
                    database = Resources.CONN_AZURE_DATABASE;
                    user = Resources.CONN_AZURE_USERID;
                    password = Resources.CONN_AZURE_PASSWORD;
                    break;
                case 2: // Server (A network or local server database)
                    server = Resources.CONN_SRVR_SERVER;
                    database = Resources.CONN_SRVR_DATABASE;
                    user = Resources.CONN_SRVR_USERID;
                    password = Resources.CONN_SRVR_PASSWORD;
                    break;
                case 3: // LocalDb (The FinancialPlannerLocalDb.mdf database in App_Data folder. **Note: uses Integrated Security)
                    server = Resources.CONN_LCLDB_SERVER;
                    database = Resources.CONN_LCLDB_DATABASE;
                    break;
            }
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     Generates the simple DefaultConnection string
        /// </summary>
        /// <returns>Connection String: string</returns>
        /// ---------------------------------------------------------------------
        public static string GetDefaultConnString()
        {
            var conndirection = Convert.ToInt32(Resources.CONN_DIRECTION);
            string server, database, userid, password;
            ConnStringParameters(conndirection, out server, out database, out userid, out password);
            var result = ConnectionStringManager.ConnectionString(
                conndirection,
                3,
                server,
                database,
                userid,
                password);

            return result;
        }
    }
}