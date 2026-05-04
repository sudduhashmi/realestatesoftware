using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RealEstateRegalSpace.Models
{
    public class InvestorModelNew : Common
    {
        public List<InvestorModelNew> lstInvestor { get; set; }

        public string InvestorCode { get; set; }
        public string Pk_InvestorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string InvestorType { get; set; }
        public string InterestType { get; set; }
        public string CompanyName { get; set; }
        public string MobileNo { get; set; }
        public string EmailAddress { get; set; }
        public string InvestorPerc { get; set; }
        public string AddedBy { get; set; }
        
        public string Address { get; set; }
        public DataTable InvestorList { get; set; }

        public List<InvestorModelNew> InvestorTypes { get; set; }


        public List<InvestorDetailViewModel> InvestorDetails { get; set; }
        


        public DataSet InvestorRegistration()
        {
            SqlParameter[] para =
                            {
                                        new SqlParameter("@InvestorCode",InvestorCode),
                                        new SqlParameter("@InvestorType",InvestorType),
                                        new SqlParameter("@FirstName", FirstName),
                                        new SqlParameter("@LastName", LastName),
                                        new SqlParameter("@CompanyName", CompanyName),
                                        new SqlParameter("@MobileNo", MobileNo),
                                        new SqlParameter("@EmailAddress", EmailAddress),
                                        new SqlParameter("@Address", Address),
                                        new SqlParameter("@InvestorPerc", InvestorPerc),
                                        new SqlParameter("@AddedBy", AddedBy),
                                        new SqlParameter("@InterestType", InterestType),


                            };
            DataSet ds = Connection.ExecuteQuery("InvestorRegistration", para);
            return ds;
        }

        public DataSet getInvestorList()
        {
            SqlParameter[] para =
                           {
                                        //new SqlParameter("@FK_UserId ",Fk_UserId),

                            };
            DataSet ds = Connection.ExecuteQuery("sp_getInvestorList", para);
            return ds;
        }

        //public string Address { get; set; }

        //public DataSet GetLedger()
        //{
        //    SqlParameter[] para = {

        //                                new SqlParameter("@FromDate", FromDate),
        //                                new SqlParameter("@ToDate", ToDate),
        //                                new SqlParameter("@Pk_LedgerId", Pk_LedgerId)

        //    };
        //    DataSet ds = Connection.ExecuteQuery("GetLedger", para);
        //    return ds;
        //}

        //public DataSet GetLedgerById()
        //{
        //    SqlParameter[] para = {

        //                                new SqlParameter("@FromDate", FromDate),
        //                                new SqlParameter("@ToDate", ToDate),
        //                                new SqlParameter("@Pk_LedgerId", Pk_LedgerId)

        //    };
        //    DataSet ds = Connection.ExecuteQuery("GetDayBookById", para);
        //    return ds;
        //}

        //public DataSet SaveCreateHead()
        //{
        //    SqlParameter[] param = {
        //                                new SqlParameter("@SubAccountHead", AccountHead),
        //                                new SqlParameter("@AddedBy", AddedBy),
        //                                new SqlParameter("@HeadType", HeadType),
        //    };
        //    DataSet ds = Connection.ExecuteQuery("sp_CreateHead", param);
        //    return ds;
        //}

        //public DataSet GetUserDetailsForExp()
        //{
        //    SqlParameter[] param = {
        //                                new SqlParameter("@LoginId", LoginId),

        //    };
        //    DataSet ds = Connection.ExecuteQuery("GetUserDetailsForExp", param);
        //    return ds;
        //}
        //public DataSet GetNameForLevelExpense()
        //{
        //    SqlParameter[] param = {
        //                                new SqlParameter("@LoginId", LoginId),

        //    };
        //    DataSet ds = Connection.ExecuteQuery("GetNameForLevelExpense", param);
        //    return ds;
        //}
        //public DataSet GetDayBook()
        //{
        //    SqlParameter[] param = {
        //                                new SqlParameter("@FromDate", FromDate),
        //                                new SqlParameter("@ToDate", ToDate),
        //                              new SqlParameter("@AddedBy", AddedBy),
        //    };
        //    DataSet ds = Connection.ExecuteQuery("GetDayBook", param);
        //    return ds;
        //}
        //public DataSet ExpenseList()
        //{
        //    SqlParameter[] param = {
        //                                new SqlParameter("@FromDate", FromDate),
        //                                new SqlParameter("@ToDate", ToDate),
        //                                new SqlParameter("@AddedBy", AddedBy),

        //    };
        //    DataSet ds = Connection.ExecuteQuery("GetExpenseList", param);
        //    return ds;
        //}
    }

    public class InvestorDetailViewModel
    {
        public string InvestorCode { get; set; }
        public string Pk_InvestorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string InvestorType { get; set; }
        public string CompanyName { get; set; }
        public string MobileNo { get; set; }
        public string EmailAddress { get; set; }
        public string InvestorPerc { get; set; }
        public string AddedBy { get; set; }

        public string Address { get; set; }
    }
}
