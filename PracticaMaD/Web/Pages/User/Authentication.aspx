<%@ Page Language="C#" AutoEventWireup="true"
    Codebehind="Authentication.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.Authentication"
    meta:resourcekey="Page" %>

<head runat="server">
    <title>Authentication</title>
    <link href="../../Css/modal.css" rel="stylesheet" type="text/css" />
    <link href="../../Css/styles.css" rel="stylesheet"type="text/css" />
    <link href="../../Css/authentication.css" rel="stylesheet"type="text/css" />


</head>

<body runat="server">
    <div id="form">
        <div class ="modal-container">
            <form id="AuthenticationForm" method="POST" runat="server" class="form-container">
                <h2 class="title"><asp:Localize ID="lclTitle" runat="server" meta:resourcekey="lclTitle" /></h2>
                <div class="fields-container">
                    <div class="field">
                        <span class="label">
                            <asp:Localize ID="lclLogin" runat="server" meta:resourcekey="lclLogin" /></span>
                            <span
                                class="entry">
                                <asp:TextBox ID="txtLogin" runat="server" Columns="16"></asp:TextBox>
                                <asp:Label ID="lblLoginError" runat="server" ForeColor="Red" Style="position: relative"
                                    Visible="False" meta:resourcekey="lblLoginError">                        
                                </asp:Label>
                            </span>
                    </div>
                    <div class="field">
                        <span class="label">
                            <asp:Localize ID="lclPassword" runat="server" meta:resourcekey="lclPassword" /></span><span
                                class="entry">
                                <asp:TextBox TextMode="Password" ID="txtPassword" runat="server" Columns="16"></asp:TextBox>
                                <asp:Label ID="lblPasswordError" runat="server" ForeColor="Red" Style="position: relative"
                                    Visible="False" meta:resourcekey="lblPasswordError">       
                                </asp:Label>
                            </span>
                    </div>
                    <div class="checkbox">
                        <asp:CheckBox ID="checkRememberPassword" runat="server" TextAlign="Left" meta:resourcekey="checkRememberPassword" />
                    </div>

                    <div >
                        <asp:Button ID="btnLogin" runat="server" OnClick="BtnLoginClick" CssClass="button" meta:resourcekey="btnLogin" />
                    </div>
                    <asp:HyperLink ID="lnkRegister" runat="server" NavigateUrl="~/Pages/User/Register.aspx" class="register-link" meta:resourcekey="lnkRegister" />

                </div>

            </form>
        </div>
    </div>
</body>
