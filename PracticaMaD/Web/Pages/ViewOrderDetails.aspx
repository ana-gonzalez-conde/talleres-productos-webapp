<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewOrderDetails.aspx.cs" Inherits="Web.Pages.ViewOrderDetails" %>
<%@ Register Src="~/Pages/NavigationBar.ascx" TagPrefix="uc" TagName="NavigationBar" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detalles del Pedido</title>
    <link href="../Css/viewOrder.css" rel="stylesheet" type="text/css" />
    <link href="../Css/navigationBar.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <uc:NavigationBar runat="server" id="navBar" />
        <div class="order-details-container">
            <h1><asp:Localize ID="lclOrderDetails" runat="server" meta:resourcekey="lclOrderDetails" /></h1>
            <asp:Label ID="lblErrorMessage" runat="server" Visible="false" CssClass="error-message"></asp:Label>
            <asp:Repeater ID="rptOrderDetails" runat="server" OnItemDataBound="rptOrderDetails_ItemDataBound">
                <ItemTemplate>
                    <div class="order-line">
                        <asp:Image ID="imgProduct" runat="server" CssClass="product-image" Height="200" Width="200" />
                        <div class="product-info">
                            <div class="product-name"><%# Eval("ProductName") %></div>
                            <div class="product-description"><%# Eval("ProductDescription") %></div>
                            <div class="product-quantity"><asp:Localize ID="lclQuantity" runat="server" meta:resourcekey="lclQuantity" /> <%# Eval("Units") %></div>
                            <div class="product-price"><asp:Localize ID="lclPrice" runat="server" meta:resourcekey="lclPrice" /> <%# Eval("Price", "{0:C}") %></div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
