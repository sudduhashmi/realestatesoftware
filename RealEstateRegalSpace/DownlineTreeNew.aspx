<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DownlineTreeNew.aspx.cs" Inherits="RealEstateRegalSpace.DownlineTreeNew" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <!-- Responsive datatable examples -->
    <%--<link href="plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="assets/css/bootstrap.min.css" rel="stylesheet" type="text/css">--%>
    
</head>
<body>
    <form id="form1" runat="server">
        <div>
               <table style="width:100%;">
                                <tr>
                                       <td style="text-align:right;">
                                      
                                           <asp:Button ID="btnBack" runat="server" CssClass="btn btn-info" Text="Back" OnClick="btnBack_Click" />
                              
                                            </td>
                                </tr>
                            </table>
                 <table style="width:100%;" class="table-responsive">
                                <tr>
                                       <td style="text-align:center;" class="text-center">

                                             <%--<a href='#'  data-html="true" class='showpopover' style="color:black;" data-content="data" rel="popover" data-placement="bottom" data-original-title="User Details" data-trigger="hover">--%>
                                           <asp:Literal ID="ltanchor" runat="server"></asp:Literal>

                                         <img src="assets/img/1.png" style="height:45px;width:45px;border-radius:22px;" />
                    <br />
                    <asp:Label ID="lbluserid1" runat="server" Text=""></asp:Label><br/><asp:Label ID="lblusername1" runat="server" Text=""></asp:Label>
                             <asp:Literal ID="ltanchorend" runat="server"></asp:Literal>
                                            </td>
                                </tr>
                            </table>

                            <table style="width:100%;" class="table-responsive">

                                <tr>
                            <asp:Repeater ID="trpDOwnline"  runat="server" OnItemDataBound="trpDOwnline_ItemDataBound" >
                                <ItemTemplate>
                                    <td style="text-align:center;">



                                        <asp:LinkButton ID="LinkButton1" OnClick="LinkButton1_Click" runat="server"  data-html="true" class='showpopover' style="color:black;"  rel="popover" data-placement="bottom" data-original-title="User Details" data-trigger="hover">
                                            <asp:Label ID="lblsponsername" Visible="false" runat="server" Text='<%#Eval("sponsername") %>'></asp:Label>
                                            <asp:Label ID="lblpercentage" Visible="false" runat="server" Text='<%#Eval("percentage") %>'></asp:Label>
                                            <asp:Label ID="lblsponserid" Visible="false" runat="server" Text='<%#Eval("sponserloginid") %>'></asp:Label>
                                            


                                         <%--<asp:Literal ID="ltuser1" runat="server"></asp:Literal>--%>
                                            <img src="assets/img/1.png" style="height:45px;width:45px;border-radius:22px;" />
                    <br />
                    <asp:Label ID="lbluserid" Visible="false" runat="server" Text='<%#Eval("pk_userid") %>'></asp:Label>
                                                <asp:Label ID="Label2"  runat="server" Text='<%#Eval("loginid") %>'></asp:Label><br/><asp:Label ID="lblusername" runat="server" Text='<%#Eval("firstname") %>'></asp:Label> <asp:Label ID="Label1" runat="server" Text='<%#Eval("lastname") %>'></asp:Label>
                       
                                                 </asp:LinkButton>
                                            </td>
                                </ItemTemplate>
                            </asp:Repeater>
                                      </tr>
                            </table>
        </div>

        
<script src="assets/popover/jquery-1.8.2.js"></script>
    <script src="assets/popover/jquery.tooltip.min.js" type="text/javascript"></script>
    <script src="assets/popover/bootstrap.min.js"></script>
<script>
    $('.showpopover').popover();
    //$(function () {
    //    $('.showpopover').popover({
    //        container: 'body'
    //    })
    //})
</script>


<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css">
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js">
</script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js">
</script>

    </form>
</body>
</html>