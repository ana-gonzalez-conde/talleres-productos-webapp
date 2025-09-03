<%@ Page Language="C#" AutoEventWireup="true"
    Codebehind="UpdateUserProfile.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.UpdateUserProfile"
    meta:resourcekey="Page" %>

<head runat="server">
    <title>Register</title>
    <link href="../../Css/modal.css" rel="stylesheet" type="text/css" />
    <link href="../../Css/styles.css" rel="stylesheet"type="text/css" />
</head>

    <div id="form">
        <div class ="modal-container">
        <form id="UpdateUserProfileForm" method="POST" runat="server" class="form-container">
            <h2 class="title"><asp:Localize ID="lclTitle" runat="server" meta:resourcekey="lclTitle" /></h2>

            <asp:HyperLink ID="lnkChangePassword" runat="server" 
                NavigateUrl="~/Pages/User/ChangePassword.aspx"
                meta:resourcekey="lnkChangePassword" class="change-password-link"/>
            <asp:HyperLink ID="lnkChangeCard" runat="server" 
                NavigateUrl="~/Pages/User/ShowBankCards.aspx"
                meta:resourcekey="lnkChangeCard" class="change-password-link"/>
            <div class="fields-container">

            <div class="field">
                <span class="label"><asp:Localize ID="lclFirstName" runat="server" meta:resourcekey="lclFirstName" /></span><span class="entry">
                    <asp:TextBox ID="txtFirstName" runat="server" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server"
                        ControlToValidate="txtFirstName" Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"/></span>
            </div>
            <div class="field">
                <span class="label"><asp:Localize ID="lclSurname" runat="server" meta:resourcekey="lclSurname" /></span><span class="entry">
                    <asp:TextBox ID="txtSurname" runat="server" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvSurname" runat="server"
                        ControlToValidate="txtSurname" Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"/></span>
            </div>
            <div class="field">
                <span class="label"><asp:Localize ID="lclEmail" runat="server" meta:resourcekey="lclEmail" /></span><span class="entry">
                    <asp:TextBox ID="txtEmail" runat="server" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                        ControlToValidate="txtEmail" Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"/>
                    <asp:RegularExpressionValidator ID="revEmail" runat="server"
                        ControlToValidate="txtEmail" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" meta:resourcekey="revEmail"></asp:RegularExpressionValidator></span>
            </div>
            <div class="field">
                <span class="label"><asp:Localize ID="lclAddress" runat="server" meta:resourcekey="lclAddress" /></span><span class="entry">
                    <asp:TextBox ID="txtAddress" runat="server" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="txtAddress" Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"/></span>
            </div>
            <div class="field">
                <span class="label"><asp:Localize ID="lclLanguage" runat="server" meta:resourcekey="lclLanguage" /></span><span class="entry">
                    <asp:DropDownList ID="comboLanguage" runat="server" AutoPostBack="True" 
                     onselectedindexchanged="ComboLanguageSelectedIndexChanged">
                    </asp:DropDownList></span>
            </div>
            <div class="field">
                <span class="label"><asp:Localize ID="lclCountry" runat="server" meta:resourcekey="lclCountry" /></span><span class="entry">
                    <asp:DropDownList ID="comboCountry" runat="server" >
                    </asp:DropDownList></span>
            </div>
            <div>
                <asp:Button ID="btnUpdate" runat="server" OnClick="BtnUpdateClick" meta:resourcekey="btnUpdate" class="button"/>
            </div>
            </div>
            </div>

        </form>
    </div>
