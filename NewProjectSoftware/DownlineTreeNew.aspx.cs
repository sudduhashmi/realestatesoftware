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
            try {
                if (!IsPostBack)
                {
                    if (Session != null && Session["associateloginid"] != null)
                    {
                        DataSet ds = obj.getAssociateId(Session["associateloginid"].ToString());
                        string associateid = "0";

                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            string strcontent = @"Sponser id : " + ds.Tables[0].Rows[0]["sponserloginid"].ToString() + "<br/> Sponser Name : " + ds.Tables[0].Rows[0]["sponsername"].ToString() + "<br/>" + "Percentage : " + ds.Tables[0].Rows[0]["percentage"].ToString();

                            if (ltanchor != null) ltanchor.Text = @"<a href='#'  data-html=""true"" class='showpopover' style=""color:black;"" data-content=""" + strcontent + @""" rel=""popover"" data-placement=""bottom"" data-original-title=""User Details"" data-trigger=""hover"">";
                            if (ltanchorend != null) ltanchorend.Text = @"</a>";
                            associateid = ds.Tables[0].Rows[0]["pk_userid"].ToString();
                        }

                        Session["prevuserid"] = associateid;
                        GetDirectData(associateid);
                    }
                }
            } catch { }
        }

        public void GetDirectData(string parentid)
        {
            try
            {
                if (string.IsNullOrEmpty(parentid)) return;
                obj.LoginId = parentid;
                dsResult = obj.getDownlineTree();

                if (dsResult != null && dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (lbluserid1 != null) lbluserid1.Text = dsResult.Tables[0].Rows[0]["Loginid"].ToString();
                    if (lblusername1 != null) lblusername1.Text = dsResult.Tables[0].Rows[0]["firstname"].ToString() + " " + dsResult.Tables[0].Rows[0]["lastname"].ToString();

                    if (dsResult.Tables.Count > 1 && trpDOwnline != null)
                    {
                        trpDOwnline.DataSource = dsResult.Tables[1];
                        trpDOwnline.DataBind();
                    }
                }
            }
            catch { }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = sender as LinkButton;
                if (btn != null)
                {
                    RepeaterItem item = btn.NamingContainer as RepeaterItem;
                    if (item != null)
                    {
                        Label lbluserid = item.FindControl("lbluserid") as Label;
                        if (lbluserid != null)
                        {
                            string userId = lbluserid.Text;
                            string prev = (Session != null && Session["prevuserid"] != null) ? Session["prevuserid"].ToString() : "";
                            if (Session != null)
                            {
                                Session["prevuserid"] = prev + "," + userId;
                            }
                            GetDirectData(userId);
                        }
                    }
                }
            }
            catch { }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session != null && Session["prevuserid"] != null)
                {
                    string strprevuserid = Session["prevuserid"].ToString();
                    if (!string.IsNullOrEmpty(strprevuserid) && strprevuserid.Contains(","))
                    {
                        int lastComma = strprevuserid.LastIndexOf(',');
                        if (lastComma >= 0)
                        {
                            string newHistory = strprevuserid.Substring(0, lastComma);
                            Session["prevuserid"] = newHistory;
                            string[] parts = newHistory.Split(',');
                            if (parts.Length > 0)
                            {
                                string lastId = parts[parts.Length - 1];
                                if (!string.IsNullOrEmpty(lastId))
                                {
                                    GetDirectData(lastId);
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }

        protected void trpDOwnline_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    LinkButton Linkbutton1 = e.Item.FindControl("Linkbutton1") as LinkButton;
                    Label lblsponsername = e.Item.FindControl("lblsponsername") as Label;
                    Label lblpercentage = e.Item.FindControl("lblpercentage") as Label;
                    Label lblsponserid = e.Item.FindControl("lblsponserid") as Label;

                    if (Linkbutton1 != null && lblsponserid != null && lblsponsername != null && lblpercentage != null)
                    {
                        string strcontent = @"Sponser id : " + lblsponserid.Text + "<br/> Sponser Name : " + lblsponsername.Text + "<br/>" + "Percentage : " + lblpercentage.Text;
                        Linkbutton1.Attributes.Add("data-content", strcontent);
                    }
                }
            } catch { }
        }
    }
}
