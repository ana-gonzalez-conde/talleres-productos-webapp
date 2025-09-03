<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewOrders.aspx.cs" Inherits="Web.Pages.ViewOrders" %>
<%@ Register Src="~/Pages/NavigationBar.ascx" TagPrefix="uc" TagName="NavigationBar" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mis Pedidos</title>
    <link href="../Css/orders.css" rel="stylesheet" type="text/css" />
    <link href="../Css/navigationBar.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <uc:NavigationBar runat="server" id="navBar" />
        <div class="orders-header">
            <h1><asp:Localize ID="lclViewOrder" runat="server" meta:resourcekey="lclViewOrder" /></h1>
        </div>
        <div class="orders-container">
            <asp:Label ID="lblErrorMessage" runat="server" Visible="false" CssClass="error-message"></asp:Label>
            <table class="orders-table">
                <thead>
                    <tr>
                        <th><asp:Localize ID="lclPurchaseDate" runat="server" meta:resourcekey="lclPurchaseDate" /></th>
                        <th><asp:Localize ID="lclNameOrder" runat="server" meta:resourcekey="lclNameOrder" /></th>
                        <th><asp:Localize ID="lclTotalPrice" runat="server" meta:resourcekey="lclTotalPrice" /></th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptOrders" runat="server">
                        <ItemTemplate>
                            <tr class="order-item">
                                <td class="order-date"><%# Eval("PurchasingDate", "{0:dd/MM/yyyy}") %></td>
                                <td class="order-name"><asp:HyperLink ID="lnkOrderDetails" runat="server" NavigateUrl='<%# "ViewOrderDetails.aspx?orderId=" + Eval("OrderId") %>' Text='<%# Eval("DescriptiveName") %>' /></td>
                                <td class="order-total"><%# Eval("TotalPrice", "{0:C}") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <asp:Label ID="lblNoOrdersMessage" runat="server" Visible="false" CssClass="no-orders-message"><asp:Localize ID="lclNoOrders" runat="server" meta:resourcekey="lclNoOrders" /></asp:Label>
        </div>
    </form>
</body>
</html>
