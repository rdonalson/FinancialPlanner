# Financial Planner (MVC 5)
The Original Financial Planner - Source Code 

Demo on Azure: http://rickdonalson-financialplanner.azurewebsites.net<br/>
Login:  Tom.Jones@me.com<br/>
Pswrd:  Abc123*

Setup:
  Connection strings are created dynamically using parameters setup in the Resources folders of the FinancialPlanner "*.Data" and "*.Web" projects.  Utils.cs in "*.Data" and ConnectionHelopers.cs in "*.Web" handle string construction.
  
  By default this application is setup to use the local database (FinancialPlannerLocalDb.mdf) in the App_Data folder in the "*.Web" project.  However, you can set up the FinancialPlanner database in either an instance of SQL Server or on Azure
  
  To do this create a database called FinancialPlanner and run the scripts in the Database Scripts folder.  Then go into the Resources file in the "FinancialPlanner.Web" project and set the "CONN_DIRECTION" to one of these settings: 1  - Azure (your Azure database) or 2  - Server (A network or local server database).  By default the value is 3 for the local db in the App_Data folder.

  The local db in App_Data doesn't require a login or password, but for a Network or Local instance of SQL Server there are properties for the server name, database name (just use FinancialPlanner), login and password.  Same process applies to Azure.
  
  If you want to setup the alternative methods of registering and authentication with Twitter and Facebook then setup accounts with them and add the Logins, Passwords, AppIDs, AppSecrets, etc. to the Resources Parameters in the "*.Web" project.
    
  
