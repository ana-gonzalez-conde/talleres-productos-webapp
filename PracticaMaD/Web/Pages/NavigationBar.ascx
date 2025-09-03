<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavigationBar.ascx.cs" Inherits="ProductSearchPage.NavigationBar" %>
<div class="navbar">
    <div class="navbar-center">
        <asp:HyperLink ID="homeLink" NavigateUrl="~/Pages/searchProducts.aspx" runat="server" CssClass="navbar-brand">TALLER</asp:HyperLink>
    </div>
    <div class="navbar-right">
        <asp:HyperLink ID="lnkMyOrders" runat="server" NavigateUrl="~/Pages/ViewOrders.aspx" ><asp:Localize ID="lclMyOrders" runat="server" meta:resourcekey="lclMyOrders" /></asp:HyperLink>
        <asp:HyperLink ID="lnkAddBankCard" runat="server" NavigateUrl="~/Pages/User/AddBankCard.aspx" > <asp:Localize ID="lclAddBankCard" runat="server" meta:resourcekey="lclAddBankCard" /> </asp:HyperLink>
        <asp:HyperLink ID="lnkUpdateUserInfo" runat="server" NavigateUrl="~/Pages/User/UpdateUserProfile.aspx" >  <asp:Localize ID="lclUpdateUser" runat="server" meta:resourcekey="lclUpdateUser" /> </asp:HyperLink>
        <asp:HyperLink ID="lnkAuthentication" runat="server" NavigateUrl="~/Pages/User/Authentication.aspx" >  <asp:Localize ID="lclLogin" runat="server" meta:resourcekey="lclLogin" /> </asp:HyperLink>
        <asp:HyperLink ID="lnkLogOut" runat="server" NavigateUrl="~/Pages/User/Logout.aspx" >  <asp:Localize ID="lclLogout" runat="server" meta:resourcekey="lclLogout" /> </asp:HyperLink>
        <div class="cart-icon">
            <a href="Cart.aspx" style="color: white; text-decoration: none; position: relative;">
                Carrito
                <span class="cart-count">
                    <asp:Literal ID="cartCount" runat="server">0</asp:Literal>
                </span>
            </a>
        </div>
    </div>
</div>
