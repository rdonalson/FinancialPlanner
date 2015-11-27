using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FinancialPlanner.Data.Entity;
using FinancialPlanner.Web.Models;
using EntityState = System.Data.Entity.EntityState;

namespace FinancialPlanner.Web.Controllers
{
    [System.Web.Mvc.Authorize]
    public class ChartTestsController : ApiController
    {
        //private readonly ItemDetailEntities _db = new ItemDetailEntities();

        // GET: api/ChartTests
        //public IQueryable<ChartTest> GetChartTests()
        //{
        //    return _db.ChartTests;
        //}

        //public List<ChartTestView> GetChartTests()
        //{

        //    var result = _db.ChartTests.Select(item => new ChartTestView
        //    {
        //        Labels = item.Labels.ToString(),
        //        Values = item.Values
        //    }).ToList();

        //    return result;
        //    //return _db.ChartTests.Select(item => new ChartTestView
        //    //{

        //    //    Labels = String.Format("{0:d}",item.Labels),
        //    //    Values = item.Values
        //    //}).ToList();
        //}


        //// GET: api/ChartTests/5
        //[ResponseType(typeof(ChartTest))]
        //public IHttpActionResult GetChartTest(int id)
        //{
        //    ChartTest chartTest = _db.ChartTests.Find(id);
        //    if (chartTest == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(chartTest);
        //}

        //// PUT: api/ChartTests/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutChartTest(int id, ChartTest chartTest)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != chartTest.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _db.Entry(chartTest).State = EntityState.Modified;

        //    try
        //    {
        //        _db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ChartTestExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/ChartTests
        //[ResponseType(typeof(ChartTest))]
        //public IHttpActionResult PostChartTest(ChartTest chartTest)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _db.ChartTests.Add(chartTest);
        //    _db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = chartTest.Id }, chartTest);
        //}

        //// DELETE: api/ChartTests/5
        //[ResponseType(typeof(ChartTest))]
        //public IHttpActionResult DeleteChartTest(int id)
        //{
        //    ChartTest chartTest = _db.ChartTests.Find(id);
        //    if (chartTest == null)
        //    {
        //        return NotFound();
        //    }

        //    _db.ChartTests.Remove(chartTest);
        //    _db.SaveChanges();

        //    return Ok(chartTest);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        _db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool ChartTestExists(int id)
        //{
        //    return _db.ChartTests.Count(e => e.Id == id) > 0;
        //}
    }
}