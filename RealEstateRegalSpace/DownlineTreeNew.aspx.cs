using RealEstateRegalSpace.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealEstateRegalSpace
{
    public partial class DownlineTreeNew : System.Web.UI.Page
    {
        public DataSet dsResult = new DataSet();
        Tree obj = new Tree();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (Session["Pk_userId"] == null)
                //{
                //    Response.Redirect("/Home/Login");
                //}
                if (Session["associateloginid"] != null)
                {

                    DataSet ds = new DataSet();
                    ds = obj.getAssociateId(Session["associateloginid"].ToString());
                    string associateid = "0";

                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        string strcontent = @"Sponser id : "+ds.Tables[0].Rows[0]["sponserloginid"].ToString()+"<br/> Sponser Name : "+ ds.Tables[0].Rows[0]["sponsername"].ToString()+"<br/>"+"Percentage : "+ ds.Tables[0].Rows[0]["percentage"].ToString();

                        ltanchor.Text = @"<a href='#'  data-html=""true"" class='showpopover' style=""color:black;"" data-content="""+ strcontent + @""" rel=""popover"" data-placement=""bottom"" data-original-title=""User Details"" data-trigger=""hover"">";
                        ltanchorend.Text = @"</a>";
                        associateid = ds.Tables[0].Rows[0]["pk_userid"].ToString();
                    }

                    Session["prevuserid"] = associateid;
                    GetDirectData(associateid);
                }
            }
        }

        public void GetDirectData(string parentid)
        {

            try
            {



                obj.LoginId = parentid;

                dsResult = obj.getDownlineTree();

                if (dsResult != null && dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {

                    lbluserid1.Text = dsResult.Tables[0].Rows[0]["Loginid"].ToString();
                    lblusername1.Text = dsResult.Tables[0].Rows[0]["firstname"].ToString() + " " + dsResult.Tables[0].Rows[0]["lastname"].ToString();

                    trpDOwnline.DataSource = dsResult.Tables[1];
                    trpDOwnline.DataBind();

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;
            Label lbluserid = (Label)item.FindControl("lbluserid");
            string str_previds = Session["prevuserid"].ToString() + "," + lbluserid.Text;
            Session["prevuserid"] = str_previds;


            GetDirectData(lbluserid.Text);
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            string strprevuserid = Session["prevuserid"].ToString();
            if (strprevuserid.Contains(','))
            {
                if (strprevuserid.Length > 0)
                {

                    strprevuserid = strprevuserid.Substring(0, strprevuserid.LastIndexOf(','));
                    Session["prevuserid"] = strprevuserid;
                    string[] arr = strprevuserid.Split(',');

                    GetDirectData(arr[arr.Length - 1]);
                }
            }


            //   Message.Show(strprevuserid);
        }

        protected void trpDOwnline_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton Linkbutton1 = e.Item.FindControl("Linkbutton1") as LinkButton;
                Label lblsponsername = e.Item.FindControl("lblsponsername") as Label;
                Label lblpercentage = e.Item.FindControl("lblpercentage") as Label;
                Label lblsponserid = e.Item.FindControl("lblsponserid") as Label;

                string strcontent = @"Sponser id : " + lblsponserid.Text + "<br/> Sponser Name : " + lblsponsername .Text+ "<br/>" + "Percentage : " + lblpercentage.Text;


                Linkbutton1.Attributes.Add("data-content", strcontent);

            }

                
        }
    }
}