<!-- # Financial Planner (MVC 5 Hosted on Azure)
## The Legacy Financial Planner - Source Code 

Demo on Azure: http://rickdonalson-financialplanner.azurewebsites.net<br/>
Login:  Tom.Jones@me.com<br/>
Pswrd:  Abc123*

Setup:
  Connection strings are created dynamically using parameters setup in the Resources folders of the FinancialPlanner "*.Data" and "*.Web" projects.  Utils.cs in "*.Data" and ConnectionHelopers.cs in "*.Web" handle string construction.
  
  By default this application is setup to use the local database (FinancialPlannerLocalDb.mdf) in the App_Data folder in the "*.Web" project.  However, you can set up the FinancialPlanner database in either an instance of SQL Server or on Azure
  
  To do this create a database called FinancialPlanner and run the scripts in the Database Scripts folder.  Then go into the Resources file in the "FinancialPlanner.Web" project and set the "CONN_DIRECTION" to one of these settings: 1  - Azure (your Azure database) or 2  - Server (A network or local server database).  By default the value is 3 for the local db in the App_Data folder.

  The local db in App_Data doesn't require a login or password, but for a Network or Local instance of SQL Server there are properties for the server name, database name (just use FinancialPlanner), login and password.  Same process applies to Azure.
  
  If you want to setup the alternative methods of registering and authentication with Twitter and Facebook then setup accounts with them and add the Logins, Passwords, AppIDs, AppSecrets, etc. to the Resources Parameters in the "*.Web" project.
    
--- -->

# Legacy Financial Planner (MVC & .NET Framework)

The **Legacy Financial Planner** is an earlier version of the Financial Planner Forecast Ledger, built using **ASP.NET MVC**, **.NET Framework**, **Entity Framework (Legacy)**, and a responsive UI powered by **LESS Bootstrap**, **Flot Charts**, and **jQuery plugins**. This version was also hosted on **Azure**, with source code maintained on GitHub.

---

## Overview

This application enables users to project their checking or savings account balance into the future. By entering income sources, recurring expenses, and a starting balance, users can generate a forecast displayed in either a **timeline chart** or a **ledger readout**, with export-to-Excel support.

### How It Works

1. Navigate to **Item Detail** and add your **credits** (paychecks, refunds, etc.) along with their period of occurrence.  
2. Add your **debits** (bills, mortgage, car payments, etc.).  
   - Debit amounts are entered as positive values; the system handles subtraction.  
3. Enter your **starting balance**.  
4. Go to the **Display** section and choose:
   - **Timeline Chart**  
   - **Ledger Display & Export**  
5. Enter a date range and generate your forecast.  
6. Export to Excel if needed.

---

## Demo

**Legacy Financial Planner (MVC)**  
- **Demo:** [Financial Planner](http://rickdonalson-financialplanner.azurewebsites.net/)  
- **Demo Login:** `Tom.Jones@me.com`  
- **Password:** `Abc123*`  
- **GitHub:** [Source Code](https://github.com/rdonalson/FinancialPlanner)  

---

## Project Structure

### **FinancialPlanner.Web**  
**JavaScript Development**
- [`common.js`](https://github.com/rdonalson/FinancialPlanner/blob/master/FinancialPlanner.Web/Scripts/common/common.js)  
- [`Index.js`](https://github.com/rdonalson/FinancialPlanner/blob/master/FinancialPlanner.Web/Scripts/codebehind/views/Timeline/Index.js)  

---

### **FinancialPlanner.Infrastructure**  
**Domains & Repositories**
- [`ItemDetail`](https://github.com/rdonalson/FinancialPlanner/tree/master/FinancialPlanner.Infrastructure/Domain/ItemDetail)  
- [`DebitRepository.cs`](https://github.com/rdonalson/FinancialPlanner/blob/master/FinancialPlanner.Infrastructure/Domain/ItemDetail/Debits/Repository/DebitRepository.cs)  

---

### **Display Section (C# Development)**  
**Timeline Chart**
- [`TimelineController.cs`](https://github.com/rdonalson/FinancialPlanner/blob/master/FinancialPlanner.Web/Areas/Display/Controllers/TimelineController.cs)  
- [`Index.cshtml`](https://github.com/rdonalson/FinancialPlanner/blob/master/FinancialPlanner.Web/Areas/Display/Views/Timeline/Index.cshtml)  
- [`_TimelineChart.cshtml`](https://github.com/rdonalson/FinancialPlanner/blob/master/FinancialPlanner.Web/Areas/Display/Views/Timeline/_TimelineChart.cshtml)  

**Ledger Readout**
- [`LedgerController.cs`](https://github.com/rdonalson/FinancialPlanner/blob/master/FinancialPlanner.Web/Areas/Display/Controllers/TimelineController.cs)  
- [`Index.cshtml`](https://github.com/rdonalson/FinancialPlanner/blob/master/FinancialPlanner.Web/Areas/Display/Views/Timeline/Index.cshtml)  
- [`_LedgerReadout.cshtml`](https://github.com/rdonalson/FinancialPlanner/blob/master/FinancialPlanner.Web/Areas/Display/Views/Timeline/_TimelineChart.cshtml)  

---

### **ItemDetail Section — CRUD**  
Typical implementation for credit and debit editing:
- [`CreditsController.cs`](https://github.com/rdonalson/FinancialPlanner/blob/master/FinancialPlanner.Web/Areas/ItemDetail/Controllers/CreditsController.cs) 
- [`Edit.cshtml`](https://github.com/rdonalson/FinancialPlanner/blob/master/FinancialPlanner.Web/Areas/ItemDetail/Views/Credits/Edit.cshtml)  
- [`_EditForm.cshtml`](https://github.com/rdonalson/FinancialPlanner/blob/master/FinancialPlanner.Web/Areas/ItemDetail/Views/Shared/_EditForm.cshtml)  
- [`_Date.cshtml`](https://github.com/rdonalson/FinancialPlanner/blob/master/FinancialPlanner.Web/Areas/ItemDetail/Views/Shared/EditorTemplates/_Date.cshtml)  

---

### **FinancialPlanner.Data**  
**Entities**
- Legacy Entity Framework models  
- [`Entities`](https://github.com/rdonalson/FinancialPlanner/tree/master/FinancialPlanner.Data/Entity)  

