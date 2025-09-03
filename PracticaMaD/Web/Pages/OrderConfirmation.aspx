<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderConfirmation.aspx.cs" Inherits="Web.Pages.OrderConfirmation" %>
<%@ Register Src="~/Pages/NavigationBar.ascx" TagPrefix="uc" TagName="NavigationBar" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Confirmación de Pedido</title>
    <link href="../Css/orderConfirmation.css" rel="stylesheet" type="text/css" />
    <link href="../Css/navigationBar.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <uc:NavigationBar runat="server" id="navBar" />
        <div class="confirmation-container">
            <h2><asp:Localize ID="lclOrderConfirmation" runat="server" meta:resourcekey="lclOrderConfirmation" /></h2>
            <div class="order-details">
                <p><asp:Localize ID="lclAddedDate" runat="server" meta:resourcekey="lclAddedDate" /> <asp:Label ID="lblOrderDate" runat="server"></asp:Label></p>
                <p><asp:Localize ID="lclNameOrder" runat="server" meta:resourcekey="lclNameOrder" /> <asp:TextBox ID="txtDescriptiveName" runat="server" CssClass="input-field"></asp:TextBox></p>
                <p><asp:Localize ID="lclShippingAddress" runat="server" meta:resourcekey="lclShippingAddress" /> <asp:TextBox ID="txtShippingAddress" runat="server" CssClass="input-field"></asp:TextBox></p>
                <p><asp:Localize ID="lclBankCard" runat="server" meta:resourcekey="lclBankCard" />  <asp:DropDownList ID="ddlBankCards" runat="server" CssClass="dropdown"></asp:DropDownList></p>
                <p>
                    <asp:CheckBox ID="chkExpressShipping" runat="server" meta:resourcekey="lclExpressAddress" CssClass="checkbox" />
                </p>
            </div>
            <asp:Button ID="btnConfirmOrder" runat="server" meta:resourcekey="lclConfirmationButton" CssClass="button" OnClick="btnConfirmOrder_Click" />
            <asp:Label ID="lblConfirmationMessage" runat="server" Visible="false" CssClass="confirmation-message"></asp:Label>
        </div>
    </form>
</body>
</html>
