<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="Web.Pages.Cart" %>
<%@ Register Src="~/Pages/NavigationBar.ascx" TagPrefix="uc" TagName="NavigationBar" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mi cesta</title>
    <link href="../Css/cart.css" rel="stylesheet" type="text/css" />
    <link href="../Css/navigationBar.css" rel="stylesheet" type="text/css" />
    <link href="../../Css/styles.css" rel="stylesheet"type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <uc:NavigationBar runat="server" id="navBar" />
        <div class="cart-container">
            <asp:Repeater ID="rptCartItems" runat="server" OnItemCommand="rptCartItems_ItemCommand" OnItemDataBound="rptCartItems_ItemDataBound">
                <ItemTemplate>
                    <div class="cart-item">
                             <asp:Image ID="imgProduct" runat="server" CssClass="product-image" Height="200" Width="200" />
                        <div class="item-details">
                            <span class="product-name"><%# Eval("ProductDetails.Name") %></span>
                            <span class="product-category">
                                <asp:Localize ID="lclCategory" runat="server" meta:resourcekey="lclCategory" /> <asp:Label ID="lblCategoryName" runat="server"></asp:Label>
                            </span>
                            <span class="product-date-added">
                                <asp:Localize ID="lclAddedDate" runat="server" meta:resourcekey="lclAddedDate" /> <asp:Label ID="lblDateAdded" runat="server"></asp:Label>
                            </span>
                        </div>
                        <div class="item-actions">
                            <div class="item-quantity-remove">
                                <div class="item-quantity">
                                    <asp:LinkButton CommandName="Decrease" CommandArgument='<%# Eval("ProductID") %>' Text="-" CssClass="quantity-decrease" runat="server" />
                                    <span class="quantity-count"><%# Eval("Quantity") %></span>
                                    <asp:LinkButton CommandName="Increase" CommandArgument='<%# Eval("ProductID") %>' Text="+" CssClass="quantity-increase" runat="server" />
                                </div>
                                <asp:LinkButton CommandName="Remove" CommandArgument='<%# Eval("ProductID") %>' meta:resourcekey="lclDelete" CssClass="remove-item" runat="server" />
                            </div>
                            <span class="product-price" style="font-weight:bold;"><%# Eval("ProductDetails.Price") %>€</span>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            
            <div class="checkout-container">
                <div class="order-summary">
                    <span class="total-price-label">Total:</span>
                    <asp:Label ID="lblTotalPrice" runat="server" CssClass="total-price"></asp:Label>
                    <asp:Button ID="btnCheckout" runat="server" meta:resourcekey="lclOrderButton" CssClass="button" OnClick="btnCheckout_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
