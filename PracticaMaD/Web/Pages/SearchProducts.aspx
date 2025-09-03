<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchProducts.aspx.cs" Inherits="ProductSearchPage.SearchProducts" meta:resourcekey="PageResource2" %>
<%@ Register Src="~/Pages/NavigationBar.ascx" TagPrefix="uc" TagName="NavigationBar" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search Products</title>
    <link href="../Css/searchProducts.css" rel="stylesheet" type="text/css" />
    <link href="../Css/navigationBar.css" rel="stylesheet" type="text/css" /> <!-- Vinculación al CSS de la barra de navegación -->
    <link href="../Css/styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <!-- Barra de navegación superior con carrito y login usando User Control -->
        <uc:NavigationBar runat="server" id="navBar" />

        <!-- Contenedor de búsqueda con desplegable de categorías -->
        <div class="search-container">
            <div class="search-bar">
                <asp:DropDownList ID="categorySelect" runat="server" CssClass="category-select">

                </asp:DropDownList>
                <asp:TextBox ID="txtSearch" runat="server" CssClass="searchBox" meta:resourcekey="lclSearchBox"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" meta:resourcekey="lclSearch" class="button" OnClick="btnSearch_Click"/>
            </div>
        </div>
        <div class="message-container" style="display: none;">
            <asp:Label ID="ltlMessage" runat="server" Visible="false" CssClass="message" />
        </div>
        <!-- Grid de productos -->
        <div id="productsGrid" runat="server" class="productsGrid">
            <!-- Los productos se mostrarán aquí -->
        </div>
    </form>
</body>
</html>
