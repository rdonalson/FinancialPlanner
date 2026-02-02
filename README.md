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

