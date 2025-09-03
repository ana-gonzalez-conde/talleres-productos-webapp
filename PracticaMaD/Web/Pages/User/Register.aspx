<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Register.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.Register"
    meta:resourcekey="Page" %>

<head runat="server">
    <title>Register</title>
    <link href="../../Css/modal.css" rel="stylesheet" type="text/css" />
    <link href="../../Css/styles.css" rel="stylesheet"type="text/css" />

</head>

    <div id="form">
        <div class ="modal-container">
        <form id="RegisterForm" method="post" runat="server" class="form-container">
            <h2 class="title"><asp:Localize ID="lclTitle" runat="server" meta:resourcekey="lclTitle" />
</h2>

            <div class="fields-container">


            <div class="field">
                <asp:HyperLink ID="lnkUserExists" runat="server" meta:resourcekey="lnkUserExists" NavigateUrl="~/Pages/UserExists.aspx"></asp:HyperLink>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclUserName" runat="server" meta:resourcekey="lclUserName" />
                </span><span
                        class="entry">
                        <asp:TextBox ID="txtLogin" runat="server" Columns="16"
                            meta:resourcekey="txtLoginResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtLogin"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvUserNameResource1"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblLoginError" runat="server" ForeColor="Red" Style="position: relative"
                            Visible="False" meta:resourcekey="lblLoginError"></asp:Label></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclPassword" runat="server" meta:resourcekey="lclPassword" /></span><span
                        class="entry">
                        <asp:TextBox TextMode="Password" ID="txtPassword" runat="server"
                            Columns="16" meta:resourcekey="txtPasswordResource1"></asp:TextBox>
                       <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvPasswordResource1"></asp:RequiredFieldValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclRetypePassword" runat="server" meta:resourcekey="lclRetypePassword" /></span><span
                        class="entry">
                        <asp:TextBox TextMode="Password" ID="txtRetypePassword" runat="server"
                            Columns="16" meta:resourcekey="txtRetypePasswordResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvRetypePassword" runat="server" ControlToValidate="txtRetypePassword"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvRetypePasswordResource1"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cvPasswordCheck" runat="server" ControlToCompare="txtPassword"
                            ControlToValidate="txtRetypePassword" meta:resourcekey="cvPasswordCheck"></asp:CompareValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclFirstName" runat="server" meta:resourcekey="lclFirstName" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtFirstName" runat="server" 
                            Columns="16" meta:resourcekey="txtFirstNameResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvFirstNameResource1"></asp:RequiredFieldValidator>
                        </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclSurname" runat="server" meta:resourcekey="lclSurname" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtSurname" runat="server" Columns="16"
                            meta:resourcekey="txtSurnameResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvSurname" runat="server" ControlToValidate="txtSurname"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvSurnameResource1"></asp:RequiredFieldValidator>
                       </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclEmail" runat="server" meta:resourcekey="lclEmail" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtEmail" runat="server" Columns="16"
                            meta:resourcekey="txtEmailResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvEmailResource1"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                            Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            meta:resourcekey="revEmail"></asp:RegularExpressionValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclAddress" runat="server" meta:resourcekey="lclAddress" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtAddress" runat="server" Columns="16"
                            meta:resourcekey="txtAddressResource1"></asp:TextBox>
                       </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclLanguage" runat="server" meta:resourcekey="lclLanguage" /></span><span
                        class="entry">
                        <asp:DropDownList ID="comboLanguage" runat="server" AutoPostBack="True"
                            meta:resourcekey="comboLanguageResource1"
                            OnSelectedIndexChanged="ComboLanguageSelectedIndexChanged">
                        </asp:DropDownList></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclCountry" runat="server" meta:resourcekey="lclCountry" /></span><span
                        class="entry">
                        <asp:DropDownList ID="comboCountry" runat="server"
                            meta:resourcekey="comboCountryResource1">
                        </asp:DropDownList></span>
            </div>
            <div>
                <asp:Button ID="btnRegister" runat="server" OnClick="BtnRegisterClick" meta:resourcekey="btnRegister" class="button" />
            </div>
            </div>

        </form>
        </div>
    </div>
