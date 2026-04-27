using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RealEstateRegalSpace.Models;

namespace RealEstateRegalSpace.Controllers
{
    public class PrintController : Controller
    {
        [HttpGet]
        public ActionResult CustomerLedger(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return Content("Error: Plot ID is missing.");
                }

                Reports obj = new Reports();
                obj.id = id;
                DataSet ds = obj.CustomerLedger();

                if (ds != null && ds.Tables.Count >= 3 && ds.Tables[0].Rows.Count > 0)
                {
                    ViewBag.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                    ViewBag.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                    ViewBag.SiteName = ds.Tables[0].Rows[0]["SiteName"].ToString();
                    ViewBag.PlotNumber = ds.Tables[0].Rows[0]["PlotNumber"].ToString();
                    ViewBag.TotalArea = ds.Tables[0].Rows[0]["TotalArea"].ToString();
                    ViewBag.PlotRate = ds.Tables[0].Rows[0]["PlotRate"].ToString();
                    ViewBag.PLCPerc = ds.Tables[0].Rows[0]["PLCPerc"].ToString();
                    ViewBag.PlotAmount = ds.Tables[0].Rows[0]["PlotAmount"].ToString();
                    ViewBag.NetPlotAmount = ds.Tables[0].Rows[0]["NetPlotAmount"].ToString();
                    
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        ViewBag.TotalPlotAmount = ds.Tables[2].Rows[0]["TotalPlotAmount"].ToString();
                        ViewBag.TotalPaidAmount = ds.Tables[2].Rows[0]["TotalPaidAmount"].ToString();
                        ViewBag.TotalBalance = (Convert.ToDecimal(ViewBag.TotalPlotAmount) - Convert.ToDecimal(ViewBag.TotalPaidAmount)).ToString("0.00");
                    }
                    else
                    {
                        ViewBag.TotalPlotAmount = "0";
                        ViewBag.TotalPaidAmount = "0";
                        ViewBag.TotalBalance = "0";
                    }

                    obj.paymentDetails = ds.Tables[1];
                    return View("~/Views/Admin/PrintCustomerLedger.cshtml", obj);
                }
                else
                {
                    return Content("Error: No data found for this plot.");
                }
            }
            catch (Exception ex)
            {
                return Content("Print Error: " + ex.Message);
            }
        }
    }
}
