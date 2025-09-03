<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="Web.Pages.ProductDetails" %>
<%@ Register Src="~/Pages/NavigationBar.ascx" TagPrefix="uc" TagName="NavigationBar" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detalles del Producto</title>
    <link href="../Css/productDetails.css" rel="stylesheet" type="text/css" />
    <link href="../Css/navigationBar.css" rel="stylesheet" type="text/css" /> <!-- Vinculación al CSS de la barra de navegación -->
    <link href="../Css/modal.css" rel="stylesheet" type="text/css" />
    <link href="../Css/styles.css" rel="stylesheet"type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <!-- Barra de navegación superior usando User Control -->
        <uc:NavigationBar runat="server" id="navBar" />

        <!-- Título del producto -->
        <h2 id="productName" runat="server" class="product-name"></h2>

        <!-- Contenedor Principal -->
        <div class="details-container">
            <!-- Imagen del Producto -->
            <div class="product-image">
                <asp:Image ID="imagePath" Width="200" Height="200" runat="server" />
            </div>

            <!-- Tabla de Detalles del Producto -->
            <div class="product-info">
                <table>
                    <tr><th><asp:Localize ID="lclDateLabel" runat="server" meta:resourcekey="lclDateLabel" />:</th><td id="productDate" runat="server"></td></tr>
                    <tr><th><asp:Localize ID="lclDescriptionInfo" runat="server" meta:resourcekey="lclDescription" />: </th><td id="productDescription" runat="server"></td></tr>
                    <tr><th><asp:Localize ID="lclCategoryInfo" runat="server" meta:resourcekey="lclCategory" />: </th><td id="productCategory" runat="server"></td></tr>
                    <tr><th><asp:Localize ID="lclPriceInfo" runat="server" meta:resourcekey="lclPrice" />: </th><td id="productPrice" runat="server"></td></tr>
                </table>
            </div>
        </div>
        <div class ="link-container">
            <asp:HyperLink ID="lnkViewComments" runat="server" meta:resourcekey="lnkViewComments" ></asp:HyperLink>
        </div>
        <asp:Panel runat="server" ID="updatePanel" Visible="true">
        <div id="form">
         <div class ="modal-container">
           <div id="RegisterForm" method="post" runat="server" class="form-container">
            <div class="fields-container">
             <div class="field">
                <span class="label"><asp:Localize ID="lclName" runat="server" meta:resourcekey="lclName" /></span><span class="entry">
                    <asp:TextBox ID="txtName" runat="server" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvName" runat="server"
                        ControlToValidate="txtName" Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"/></span>
            </div>
            <div class="field">
                <span class="label"><asp:Localize ID="lclPrice" runat="server" meta:resourcekey="lclPrice" /></span><span class="entry">
                    <asp:TextBox ID="txtPrice" runat="server" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPrice" runat="server"
                        ControlToValidate="txtPrice" Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"/>
                    <asp:RegularExpressionValidator ID="revPrice" runat="server" ControlToValidate="txtPrice"
                            Display="Dynamic" ValidationExpression="^[0-9]+[.][0-9]{2}$"
                            meta:resourcekey="revPrice"></asp:RegularExpressionValidator>
                                                                                                                    </span>
            </div>
            <div class="field">
                <span class="label"><asp:Localize ID="lclStock" runat="server" meta:resourcekey="lclStock" /></span><span class="entry">
                    <asp:TextBox ID="txtStock" runat="server" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvStock" runat="server"
                        ControlToValidate="txtStock" Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"/></span>
            </div>
            <div class="field">
                <span class="label"><asp:Localize ID="lclDescription" runat="server" meta:resourcekey="lclDescription" /></span><span class="entry">
                    <asp:TextBox ID="txtDescription" runat="server" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvDescription" runat="server"
                        ControlToValidate="txtDescription" Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"/></span>
            </div>
            <div class="field">
                <span class="label"><asp:Localize ID="lclCategory" runat="server" meta:resourcekey="lclCategory" /></span><span class="entry">
                     <asp:DropDownList ID="comboCategory" runat="server" AutoPostBack="True"
                            meta:resourcekey="comboCategoryResource"
                            OnSelectedIndexChanged="ComboCategorySelectedIndexChanged">
                        </asp:DropDownList></span>
            </div>
                 <div>
                <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdateProductClick" meta:resourcekey="btnUpdate" class="button" />
            </div>
                </div>
               </div>
               </div>
            </div>
            
        </asp:Panel>
    </form>
</body>
</html>