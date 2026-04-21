using RealEstateRegalSpace.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static RealEstateRegalSpace.Models.Common;

namespace RealEstateRegalSpace.Controllers
{
    public class AssociateController : AssociateBaseController
    {
        // GET: Associate
        public ActionResult DashBoard()
        {
            Dashboard obj = new Dashboard();
            obj.Fk_UserId = Session["Pk_UserId"].ToString();
            DataSet ds = obj.GetAssociateDashboard();
            if (ds != null)
            {
                obj.associateDetails = ds.Tables[0];
                if (ds.Tables.Count > 1)
                {
                    obj.investmentDetails = ds.Tables[1];
                }
                if (ds.Tables.Count > 2)
                {
                    obj.kycDetails = ds.Tables[2];
                }
                if (ds.Tables.Count > 3)
                {
                    if (string.IsNullOrEmpty(ds.Tables[3].Rows[0]["FathersName"].ToString()) || string.IsNullOrEmpty(ds.Tables[3].Rows[0]["GaurdianRelation"].ToString()) || string.IsNullOrEmpty(ds.Tables[3].Rows[0]["Address"].ToString()))
                    {
                        ViewBag.NoteStyle = "display:block";
                    }
                    else
                    {
                        ViewBag.NoteStyle = "display:none";
                    }
                }

                //ViewBag.PaidBusinessLeft = ds.Tables[2].Rows[0]["PaidBusinessLeft"].ToString();
                //ViewBag.PaidBusinessRight = ds.Tables[2].Rows[0]["PaidBusinessRight"].ToString();
                //ViewBag.TotalBusinessLeft = ds.Tables[2].Rows[0]["TotalBusinessLeft"].ToString();
                //ViewBag.TotalBusinessRight = ds.Tables[2].Rows[0]["TotalBusinessRight"].ToString();
                //ViewBag.CarryLeft = ds.Tables[2].Rows[0]["CarryLeft"].ToString();
                //ViewBag.CarryRight = ds.Tables[2].Rows[0]["CarryRight"].ToString();
            }
            return View(obj);
        }
        public ActionResult DirectDashBoard()
        {
            Dashboard obj = new Dashboard();
            obj.Fk_UserId = Session["Pk_UserId"].ToString();
            DataSet ds = obj.GetDirectAssociateDashboard();
            if (ds != null)
            {
                obj.associateDetails = ds.Tables[0];

            }
            return View(obj);
        }
        public ActionResult Tree()
        {
            ViewBag.Fk_UserId = Session["Pk_UserId"].ToString();
            return View();
        }
        public ActionResult GetUserList()
        {
            Reports obj = new Reports();
            List<Reports> lst = new List<Reports>();
            obj.LoginId = Session["LoginId"].ToString();
            DataSet ds = obj.GettingUserProfile();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Reports objList = new Reports();
                    objList.UserName = dr["Fullname"].ToString();
                    objList.LoginIDD = dr["LoginId"].ToString();
                    lst.Add(objList);
                }
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BinaryTree()
        {
            return View();
        }

        public ActionResult Direct()
        {
            Reports obj = new Reports();
            obj.LoginId = Session["LoginId"].ToString();
            DataSet ds = obj.GetDirectList();
            obj.directList = ds.Tables[0];
            return View(obj);
        }
        [HttpPost]
        public ActionResult Direct(Reports obj)
        {
            obj.LoginId = Session["LoginId"].ToString();
            DataSet ds = obj.GetDirectList();
            obj.directList = ds.Tables[0];
            return View(obj);
        }
        public ActionResult Downline()
        {
            Reports obj = new Reports();
            obj.LoginId = Session["LoginId"].ToString();
            DataSet ds = obj.GetDownline();
            obj.directList = ds.Tables[0];
            return View(obj);
        }
        [HttpPost]
        public ActionResult Downline(Reports obj)
        {
            obj.LoginId = Session["LoginId"].ToString();
            DataSet ds = obj.GetDownline();
            obj.directList = ds.Tables[0];
            return View(obj);
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordClass obj)
        {
            try
            {
                obj.OldPassword = Crypto.Encrypt(obj.OldPassword);
                obj.NewPassword = Crypto.Encrypt(obj.NewPassword);
                obj.LoginId = Session["LoginId"].ToString();
                DataSet ds = obj.UpdatePassword();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        TempData["Login"] = "Password Changed Successfully";
                        return RedirectToAction("Login", "Home");
                    }
                    else
                    {
                        TempData["Login"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(obj);
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["Login"] = ex.Message;
                return View(obj);
            }
            return RedirectToAction("ChangePassword");
        }

        public ActionResult UpdateProfile()
        {
            UpdateProfile obj = new UpdateProfile();
            obj.LoginId = Session["LoginId"].ToString();
            DataSet ds = obj.GetProfile();
            obj.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
            obj.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
            obj.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
            obj.Email = ds.Tables[0].Rows[0]["Email"].ToString();
            obj.AccountNo = ds.Tables[0].Rows[0]["AccountNo"].ToString();
            obj.BankName = ds.Tables[0].Rows[0]["BankName"].ToString();
            obj.BankBranch = ds.Tables[0].Rows[0]["BankBranch"].ToString();
            obj.IFSC = ds.Tables[0].Rows[0]["IFSC"].ToString();
            obj.ProfilePic = ds.Tables[0].Rows[0]["ProfilePic"].ToString();

            return View(obj);
        }
        [HttpPost]
        public ActionResult UpdateProfile(UpdateProfile model, IEnumerable<HttpPostedFileBase> postedFile)
        {
            try
            {
                foreach (var file in postedFile)
                {
                    if (file != null && file.ContentLength > 0)
                    {

                        model.ProfilePic = "../UserImage/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                        file.SaveAs(Path.Combine(Server.MapPath(model.ProfilePic)));
                    }

                }
                model.LoginId = Session["LoginId"].ToString();
                DataSet ds = model.UpdateAssociateProfile();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        Session["ProfilePic"] = ds.Tables[0].Rows[0]["UserImage"].ToString();
                        TempData["Profile"] = "Profile Updated Successfully";
                        return RedirectToAction("UpdateProfile");
                    }
                    else
                    {
                        TempData["Profile"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(model);
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["Profile"] = ex.Message;
                return View(model);
            }
            return RedirectToAction("UpdateProfile");
        }

        public ActionResult Registration()
        {
            Registration obj = new Registration();
            #region ForQueryString
            if (Request.QueryString["Pid"] != null)
            {
                obj.sponserId = Request.QueryString["Pid"].ToString();
            }
            if (Request.QueryString["lg"] != null)
            {
                obj.leg = Request.QueryString["lg"].ToString();
                if (obj.leg == "Right")
                {
                    ViewBag.RightChecked = "checked";
                    ViewBag.LeftChecked = "";
                }
                else
                {
                    ViewBag.RightChecked = "";
                    ViewBag.LeftChecked = "checked";
                }
            }
            if (Request.QueryString["Pid"] != null)
            {
                Common objcomm = new Common();
                objcomm.sponserId = obj.sponserId;
                DataSet ds = objcomm.GetMemberDetails();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    obj.sponsorName = ds.Tables[0].Rows[0]["FullName"].ToString();
                }
            }
            else
            {
                ViewBag.RightChecked = "";
                ViewBag.LeftChecked = "checked";
            }
            #endregion ForQueryString

            #region BindGender
            List<SelectListItem> lstgender = Common.BindGender();
            ViewBag.ddlGender = lstgender;
            #endregion BindGenders
            return View(obj);
        }
        [HttpPost]
        public ActionResult Registration(Registration obj)
        {
            #region BindGender
            List<SelectListItem> lstgender = Common.BindGender();
            ViewBag.ddlGender = lstgender;
            #endregion BindGenders
            string password = Common.GenerateRandom();
            obj.password = Crypto.Encrypt(password);
            DataSet ds = obj.SaveRegistration();
            if (ds != null)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    Session["AssociateLoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                    Session["AssociateName"] = ds.Tables[0].Rows[0]["Name"].ToString();
                    Session["Password"] = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                    string msg = "Dear " + Session["AssociateName"].ToString() + " Welcome to Regal Space Infratech Pvt. Ltd..Your Login Id " + Session["AssociateLoginId"].ToString() + " and Password is " + Session["Password"].ToString() + ". Kindly login www.gayatrinfra.com Thankyou";
                    BLSMS.SendSMSNew(obj.mobileNo, msg);
                    return RedirectToAction("ConfirmationPage");
                }
                else
                {
                    return View(obj);
                }
            }
            return RedirectToAction("ConfirmationPage");
        }
        public ActionResult ConfirmationPage()
        {
            return View();
        }

        public ActionResult PayoutRequest()
        {
            Wallet model = new Wallet();
            model.Fk_UserId = Session["Pk_userId"].ToString();
            DataSet Ds = model.GetPayoutBalance();
            ViewBag.Balance = Ds.Tables[0].Rows[0]["Balance"].ToString();

            return View(model);
        }
        [HttpPost]
        public ActionResult PayoutRequest(Wallet obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {

                obj.AddedBy = Session["Pk_userId"].ToString();
                obj.LoginId = Session["LoginId"].ToString();
                DataSet ds = obj.SavePayoutRequest();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        TempData["PayoutReq"] = " Payout requested successfully !";

                    }
                    else
                    {
                        TempData["PayoutReq"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["PayoutReq"] = ex.Message;
            }
            FormName = "PayoutRequest";
            Controller = "Associate";

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult PayoutLedger()
        {
            Reports obj = new Reports();
            obj.LoginId = Session["LoginId"].ToString();
            DataSet ds = obj.GetAssociateLedger();

            obj.lstDetails = ds.Tables[0];
            obj.customerLedger = ds.Tables[1];
            ViewBag.AdvancePayment = ds.Tables[2].Rows[0]["Advance"].ToString();
            return View(obj);
        }

        public ActionResult BookingList()
        {
            Plot model = new Plot();
            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = model.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;
            return View(model);
        }
        [HttpPost]
        public ActionResult BookingList(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.AssociateID = Session["LoginId"].ToString();
            DataSet ds = model.GetBookingDetailsList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {
                    TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
                else
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Plot obj = new Plot();
                        obj.PK_BookingId = r["PK_BookingId"].ToString();

                        obj.CustomerID = r["CustomerID"].ToString();
                        obj.CustomerLoginID = r["CustomerLoginID"].ToString();
                        obj.CustomerName = r["CustomerName"].ToString();
                        obj.AssociateID = r["AssociateID"].ToString();
                        obj.AssociateLoginID = r["AssociateLoginID"].ToString();
                        obj.AssociateName = r["AssociateName"].ToString();
                        obj.PlotNumber = r["PlotInfo"].ToString();
                        obj.BookingDate = r["BookingDate"].ToString();
                        obj.BookingAmount = r["BookingAmt"].ToString();

                        obj.BookingNumber = r["BookingNo"].ToString();
                        obj.PK_BookingDetailsId = r["PK_BookingDetailsId"].ToString();
                        obj.EncryptKey = Crypto.Encrypt(r["PK_BookingDetailsId"].ToString());
                        lst.Add(obj);
                    }
                    model.lstPlot = lst;
                }
            }
            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = model.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;
            return View(model);
        }



        public ActionResult AssociateUplineDetail(Reports model)
        {

            model.LoginId = Session["LoginId"].ToString();

            List<Reports> lst = new List<Reports>();

            DataSet ds = model.GetUplineDetails();
            model.lstDetails = ds.Tables[0];

            return View(model);
        }
        public ActionResult AssociateDownlineDetail(Reports model)
        {

            model.LoginId = Session["LoginId"].ToString();

            List<Reports> lst = new List<Reports>();

            DataSet ds = model.GetDownlineDetails();
            model.lstDetails = ds.Tables[0];

            return View(model);
        }
        public ActionResult PayoutReport()
        {

            Reports payoutDetail = new Reports();
            payoutDetail.LoginId = Session["LoginID"].ToString();
            DataSet ds11 = payoutDetail.GetPayoutReport();
            if (ds11 != null && ds11.Tables.Count > 0)
            {
                if (ds11.Tables[0].Rows.Count > 0)
                {

                    if (ds11.Tables[0].Rows[0][0].ToString() == "1")
                    {

                        if (Session["CommissionType"].ToString() == "Direct")
                        {
                            ds11 = payoutDetail.GetDircetPayout();
                        }
                        else
                        {
                            ds11 = payoutDetail.GetPayoutReport();
                        }
                        payoutDetail.payoutData = ds11.Tables[0];
                        return View(payoutDetail);
                    }
                    else
                    {
                        TempData["PayoutReport"] = ds11.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    }

                }
            }

            return View();

        }
        [HttpPost]
        public ActionResult PayoutReport(Reports payoutDetail)
        {

            payoutDetail.FromDate = string.IsNullOrEmpty(payoutDetail.FromDate) ? null : Common.ConvertToSystemDate(payoutDetail.FromDate, "dd/MM/yyyy");
            payoutDetail.Todate = string.IsNullOrEmpty(payoutDetail.Todate) ? null : Common.ConvertToSystemDate(payoutDetail.Todate, "dd/MM/yyyy");
            payoutDetail.LoginId = Session["LoginID"].ToString();
            DataSet ds11 = payoutDetail.GetPayoutReport();
            payoutDetail.payoutData = ds11.Tables[0];
            if (ds11 != null && ds11.Tables.Count > 0)
            {
                if (ds11.Tables[0].Rows[0][0].ToString() == "1")
                {
                    payoutDetail.LoginId = Session["LoginID"].ToString();
                    if (Session["CommissionType"].ToString() == "Direct")
                    {
                        ds11 = payoutDetail.GetDircetPayout();
                    }
                    else
                    {
                        ds11 = payoutDetail.GetPayoutReport();
                    }
                    payoutDetail.payoutData = ds11.Tables[0];
                    return View(payoutDetail);
                }
                else
                {
                    TempData["PayoutReport"] = ds11.Tables[0].Rows[0]["ErrorMessage"].ToString();

                }
            }

            return View();

        }

        public ActionResult GetPayoutDetails(string LoginId, string Payout)
        {
            Reports obj = new Reports();
            List<Reports> lst = new List<Reports>();
            obj.LoginId = LoginId;
            obj.PayoutNo = Payout;
            DataSet ds = obj.GetPayoutDetails();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Reports objList = new Reports();
                    objList.LoginId = dr["LoginId"].ToString();
                    objList.Name = dr["Name"].ToString();
                    objList.IncomeType = dr["IncomeType"].ToString();
                    objList.BusinessAMount = dr["BusinessAMount"].ToString();
                    objList.Amount = dr["Amount"].ToString();
                    objList.PlotNumber = dr["PlotNumber"].ToString();
                    lst.Add(objList);
                }
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public ActionResult KYCDocuments()
        {
            KYCDocuments objKYC = new KYCDocuments();

            //List<Profile> lstprofile = new List<Profile>();
            objKYC.PKUserID = Session["Pk_userId"].ToString();
            KYCDocuments obj = new KYCDocuments();
            DataSet ds = objKYC.GetKYCDocuments();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                obj.AdharNumber = ds.Tables[0].Rows[0]["AdharNumber"].ToString();
                obj.AdharImage = ds.Tables[0].Rows[0]["AdharImage"].ToString();
                obj.AdharStatus = ds.Tables[0].Rows[0]["AdharStatus"].ToString();
                obj.PanNumber = ds.Tables[0].Rows[0]["PanNumber"].ToString();
                obj.PanImage = ds.Tables[0].Rows[0]["PanImage"].ToString();
                obj.PanStatus = ds.Tables[0].Rows[0]["PanStatus"].ToString();
                obj.DocumentNumber = ds.Tables[0].Rows[0]["DocumentNumber"].ToString();
                obj.DocumentImage = ds.Tables[0].Rows[0]["DocumentImage"].ToString();
                obj.Signature = ds.Tables[0].Rows[0]["Signature"].ToString();
                obj.DocumentStatus = ds.Tables[0].Rows[0]["DocumentStatus"].ToString();
                obj.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                obj.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                obj.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                obj.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                obj.AccountNo = ds.Tables[0].Rows[0]["AccountNo"].ToString();
                obj.BankName = ds.Tables[0].Rows[0]["BankName"].ToString();
                obj.BankBranch = ds.Tables[0].Rows[0]["BankBranch"].ToString();
                obj.IFSC = ds.Tables[0].Rows[0]["IFSC"].ToString();
                obj.ProfilePic = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
                obj.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                obj.Relation = ds.Tables[0].Rows[0]["GaurdianRelation"].ToString();
                obj.FatherName = ds.Tables[0].Rows[0]["FathersName"].ToString();

            }
            #region BindRelation
            List<SelectListItem> ddlRelation = Common.BindRelation();
            ViewBag.ddlRelation = ddlRelation;
            #endregion BindRelation
            return View(obj);
        }

        [HttpPost]
        public ActionResult KYCDocuments(HttpPostedFileBase postedFileaadhar, HttpPostedFileBase postedFilePan, HttpPostedFileBase postedFileAadharBack, HttpPostedFileBase postedFileProfile, HttpPostedFileBase postedFileSignature, KYCDocuments obj)
        {
            string FormName = "";
            string Controller = "";
            #region BindRelation
            List<SelectListItem> ddlRelation = Common.BindRelation();
            ViewBag.ddlRelation = ddlRelation;
            #endregion BindRelation
            try
            {
                if (postedFileaadhar != null)
                {
                    obj.AdharImage = "/UserImage/" + Guid.NewGuid() + Path.GetExtension(postedFileaadhar.FileName);
                    postedFileaadhar.SaveAs(Path.Combine(Server.MapPath(obj.AdharImage)));
                }
                if (postedFilePan != null)
                {
                    obj.PanImage = "/UserImage/" + Guid.NewGuid() + Path.GetExtension(postedFilePan.FileName);
                    postedFilePan.SaveAs(Path.Combine(Server.MapPath(obj.PanImage)));
                }
                if (postedFileAadharBack != null)
                {
                    obj.DocumentImage = "/UserImage/" + Guid.NewGuid() + Path.GetExtension(postedFileAadharBack.FileName);
                    postedFileAadharBack.SaveAs(Path.Combine(Server.MapPath(obj.DocumentImage)));
                }
                if (postedFileProfile != null)
                {
                    obj.ProfilePic = "/UserImage/" + Guid.NewGuid() + Path.GetExtension(postedFileProfile.FileName);
                    postedFileProfile.SaveAs(Path.Combine(Server.MapPath(obj.ProfilePic)));
                }
                if (postedFileSignature != null)
                {
                    obj.Signature = "/UserImage/" + Guid.NewGuid() + Path.GetExtension(postedFileSignature.FileName);
                    postedFileSignature.SaveAs(Path.Combine(Server.MapPath(obj.Signature)));
                }
                obj.PKUserID = Session["Pk_userId"].ToString();

                DataSet ds = obj.UploadKYCDocuments();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["DocumentUpload"] = "Document uploaded successfully..";
                        FormName = "KYCDocuments";
                        Controller = "Associate";
                    }
                    else
                    {
                        TempData["DocumentUpload"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "KYCDocuments";
                        Controller = "Associate";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["DocumentUpload"] = ex.Message;
                FormName = "KYCDocuments";
                Controller = "Associate";
            }
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult PlotAvailbilty()
        {
            return View();
        }

        public ActionResult BusinessReport(string loginid)
        {
            #region bindmonth

            Common objmonth = new Common();
            List<SelectListItem> ddlMonth = new List<SelectListItem>();
            ddlMonth.Add(new SelectListItem { Text = "Select Month", Value = "0" });
            DataSet dsBank = objmonth.GetMonth();
            if (dsBank != null && dsBank.Tables.Count > 0 && dsBank.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBank.Tables[0].Rows)
                {

                    ddlMonth.Add(new SelectListItem { Text = r["MonthName"].ToString(), Value = r["MonthId"].ToString() });

                }
            }

            ViewBag.ddlMonth = ddlMonth;

            #endregion bindmonth
            Reports obj = new Reports();

            obj.LoginId = string.IsNullOrEmpty(loginid) ? Session["LoginId"].ToString() : loginid;
            obj.Month = "0";
            DataSet ds11 = obj.GetBusinessReport();
            obj.lstDetails = ds11.Tables[0];
            return View(obj);
        }
        [HttpPost]
        public ActionResult BusinessReport(Reports obj)
        {
            #region bindmonth

            Common objmonth = new Common();
            List<SelectListItem> ddlMonth = new List<SelectListItem>();
            ddlMonth.Add(new SelectListItem { Text = "Select Month", Value = "0" });
            DataSet dsBank = objmonth.GetMonth();
            if (dsBank != null && dsBank.Tables.Count > 0 && dsBank.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBank.Tables[0].Rows)
                {

                    ddlMonth.Add(new SelectListItem { Text = r["MonthName"].ToString(), Value = r["MonthId"].ToString() });

                }
            }

            ViewBag.ddlMonth = ddlMonth;

            #endregion bindmonth
            Session["Month"] = obj.Month;
            obj.LoginId = Session["LoginId"].ToString();
            DataSet ds11 = obj.GetBusinessReport();
            obj.lstDetails = ds11.Tables[0];
            return View(obj);

        }


        public ActionResult AssociateRegistration(string UserID)
        {


            TraditionalAssociate model = new TraditionalAssociate();
            try
            {

                model.sponserId = Session["Loginid"].ToString();


                TraditionalAssociate obj = new TraditionalAssociate();


                #region ddlDesignation

                int desgnationCount = 0;
                List<SelectListItem> ddlDesignation = new List<SelectListItem>();
                DataSet dsdesignation = obj.GetDesignationList2();
                if (dsdesignation != null && dsdesignation.Tables.Count > 0 && dsdesignation.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsdesignation.Tables[0].Rows)
                    {
                        if (desgnationCount == 0)
                        {
                            ddlDesignation.Add(new SelectListItem { Text = "Select Designation", Value = "0" });
                        }
                        ddlDesignation.Add(new SelectListItem { Text = r["DesignationName"].ToString(), Value = r["PK_DesignationID"].ToString() });
                        desgnationCount = desgnationCount + 1;
                    }
                }

                ViewBag.ddlDesignation = ddlDesignation;

                #endregion

                #region BindCommissionType
                List<SelectListItem> lstcommmissionType = Common.BindCommissionType();
                ViewBag.lstcommmissionType = lstcommmissionType;
                #endregion BindCommissionType
                return View(model);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        [HttpPost]
        public ActionResult AssociateRegistration(TraditionalAssociate model, string btnSave)
        {

            #region ddlDesignation

            int desgnationCount = 0;
            List<SelectListItem> ddlDesignation = new List<SelectListItem>();
            DataSet dsdesignation = model.GetDesignationList2();
            if (dsdesignation != null && dsdesignation.Tables.Count > 0 && dsdesignation.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsdesignation.Tables[0].Rows)
                {
                    if (desgnationCount == 0)
                    {
                        ddlDesignation.Add(new SelectListItem { Text = "Select Designation", Value = "0" });
                    }
                    ddlDesignation.Add(new SelectListItem { Text = r["DesignationName"].ToString(), Value = r["PK_DesignationID"].ToString() });
                    desgnationCount = desgnationCount + 1;
                }
            }

            ViewBag.ddlDesignation = ddlDesignation;

            #endregion
            string FormName = "";
            string Controller = "";
            try
            {
                Random rnd = new Random();
                int ctrPasword = rnd.Next(111111, 999999);
                model.Password = Crypto.Encrypt(ctrPasword.ToString());

                DataSet dsRegistration = new DataSet();

                model.AddedBy = Session["Pk_userId"].ToString();
                model.CommissionType = "Level";
                dsRegistration = model.AssociateRegistration();


                if (dsRegistration != null && dsRegistration.Tables.Count > 0)
                {
                    if (dsRegistration.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        if (!string.IsNullOrEmpty(btnSave))
                        {
                            Session["DisplayNameConfirm"] = dsRegistration.Tables[0].Rows[0]["Name"].ToString();
                            Session["LoginIDConfirm"] = dsRegistration.Tables[0].Rows[0]["LoginId"].ToString();
                            Session["PasswordConfirm"] = Crypto.Decrypt(dsRegistration.Tables[0].Rows[0]["Password"].ToString());
                            //Session["PKUserID"] = Crypto.Encrypt(dsRegistration.Tables[0].Rows[0]["PKUserID"].ToString());

                            string name = dsRegistration.Tables[0].Rows[0]["Name"].ToString();
                            string id = dsRegistration.Tables[0].Rows[0]["LoginId"].ToString();
                            string pass = Crypto.Decrypt(dsRegistration.Tables[0].Rows[0]["Password"].ToString());
                            string mob = model.Contact;
                            try
                            {
                                string msg = "Dear " + name + " Welcome to Regal Space Infratech Pvt. Ltd..Your Login Id " + id + " and Password is " + pass + ". Kindly login" + SoftwareDetails.Website + " Thankyou";
                                //BLSMS.SendSMSNew(model.Contact, msg);
                            }
                            catch { }
                            try
                            {
                                string msg = "Dear " + name + " Welcome to Regal Space Infratech Pvt. Ltd..Your Login Id " + id + " and Password is " + pass + ". Kindly login" + SoftwareDetails.Website + " Thankyou";
                                //BLSMS.SendSMSNew("0000000000", msg);
                            }
                            catch { }
                        }
                        else
                        {
                            TempData["Registration"] = "Associate Updated Successfully";
                            return RedirectToAction("AssociateRegistration");
                        }

                    }
                    else
                    {
                        TempData["Registration"] = dsRegistration.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Registration"] = ex.Message;
            }
            FormName = "ConfirmRegistrationAssociate";
            Controller = "Associate";

            return RedirectToAction(FormName, Controller);

        }

        public ActionResult ConfirmRegistrationAssociate()
        {
            return View();
        }

        public ActionResult GetSponsorName(string SponsorID)
        {
            try
            {
                TraditionalAssociate model = new TraditionalAssociate();
                model.LoginID = SponsorID;
                model.CommissionType = "Level";

                DataSet dsSponsorName = model.GetAssociateList();
                if (dsSponsorName != null && dsSponsorName.Tables[0].Rows.Count > 0)
                {
                    model.sponsorName = dsSponsorName.Tables[0].Rows[0]["Name"].ToString();
                    model.UserID = dsSponsorName.Tables[0].Rows[0]["PK_UserID"].ToString();
                    model.SponsorDesignationID = dsSponsorName.Tables[0].Rows[0]["FK_DesignationID"].ToString();
                    model.Percentage = dsSponsorName.Tables[0].Rows[0]["Percentage"].ToString();
                    int desgnationCount = 0;

                    DataSet dsdesignation = model.GetDesignationList2();
                    List<SelectListItem> ddlDesignation = new List<SelectListItem>();
                    if (dsdesignation != null && dsdesignation.Tables.Count > 0 && dsdesignation.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in dsdesignation.Tables[0].Rows)
                        {
                            if (desgnationCount == 0)
                            {
                                ddlDesignation.Add(new SelectListItem { Text = "Select Designation", Value = "0" });
                            }
                            ddlDesignation.Add(new SelectListItem { Text = r["DesignationName"].ToString(), Value = r["PK_DesignationID"].ToString() });
                            desgnationCount = desgnationCount + 1;
                        }
                    }

                    // ViewBag.ddlDesignation = ddlDesignation;

                    model.ddlDesignation = ddlDesignation;
                    model.Result = "yes";
                }
                else
                {
                    model.sponsorName = "";
                    model.Result = "no";
                }


                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public ActionResult AdvancePaymentList(Wallet obj)
        {
            obj.FromDate = string.IsNullOrEmpty(obj.FromDate) ? null : Common.ConvertToSystemDate(obj.FromDate, "dd/MM/yyyy");
            obj.ToDate = string.IsNullOrEmpty(obj.ToDate) ? null : Common.ConvertToSystemDate(obj.ToDate, "dd/MM/yyyy");
            obj.LoginId = Session["LoginId"].ToString();
            DataSet ds = obj.AdvancePaymentReport();
            obj.advancePayment = ds.Tables[0];
            return View(obj);

        }

    }

}