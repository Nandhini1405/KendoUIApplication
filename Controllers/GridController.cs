﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using KendoUIGrid.Models;

namespace KendoUIGrid.Controllers
{
    public class GridController : Controller
    {
        private NorthwindEntities db = new NorthwindEntities();
        
        public ActionResult Grid()
        {
            return View();
        }

        public ActionResult Order_Details_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Order_Detail> order_details = db.Order_Details;
            DataSourceResult result = order_details.ToDataSourceResult(request, order_Detail => new
            {
                OrderID = order_Detail.OrderID,
                ProductID = order_Detail.ProductID,
                UnitPrice = order_Detail.UnitPrice,
                Quantity = order_Detail.Quantity,
                Discount = order_Detail.Discount,
            });

            return Json(result);
        }

        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }
    
        [HttpPost]
        public ActionResult Pdf_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
