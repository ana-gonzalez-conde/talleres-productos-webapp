<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddBankCard.aspx.cs" Inherits="Web.Pages.User.AddBankCard" %>



<head runat="server">
    <title>Register</title>
    <link href="../../Css/modal.css" rel="stylesheet" type="text/css" />
    <link href="../../Css/styles.css" rel="stylesheet"type="text/css" />
    <link href="../../Css/addBankCard.css" rel="stylesheet"type="text/css" />

</head>
<body>
    <div id="form">
        <div class ="modal-container">
        <form id="UpdateBankCardForm" method="post" runat="server" class="form-container">
            <h2 class="title"><asp:Localize ID="lclTitle" runat="server" meta:resourcekey="lclTitle" /></h2>
            <div class="fields-container">

            <div class="field">
                <span class="label"><asp:Localize ID="lclNumber" runat="server" meta:resourcekey="lclNumber" /></span><span class="entry">
                    <asp:TextBox ID="txtNumber" runat="server" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvSurname" runat="server" ControlToValidate="txtNumber"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvSurnameResource1"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revNumber" runat="server" ControlToValidate="txtNumber"
                            Display="Dynamic" ValidationExpression="\d{16}"
                            meta:resourcekey="revNumber"></asp:RegularExpressionValidator>
            </div>
            <div class="field">
                <span class="label"><asp:Localize ID="lclCvv" runat="server" meta:resourcekey="lclCvv" /></span><span class="entry">
                    <asp:TextBox ID="txtCvv" runat="server" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCvv"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvSurnameResource1"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revCvv" runat="server" ControlToValidate="txtCvv"
                            Display="Dynamic" ValidationExpression="\d{3}"
                            meta:resourcekey="revCvv"></asp:RegularExpressionValidator>
            </div>
            
           
            <div class="field">
                <span class="label"><asp:Localize ID="lclType" runat="server" meta:resourcekey="lclType" /></span><span class="entry">
                    <asp:DropDownList ID="comboType" runat="server" AutoPostBack="True" meta:resourcekey="comboTypeResource1">
                    </asp:DropDownList></span>
            </div>
            <div class="field">
                <span class="label"><asp:Localize ID="lclExpirationDate" runat="server" meta:resourcekey="lclExpirationDate" /></span><span class="entry">
                    <asp:TextBox ID="txtExpirationDate" runat="server" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtExpirationDate"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvSurnameResource1"></asp:RequiredFieldValidator>
                <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged" MinDate="<%# DateTime.Today %>"></asp:Calendar>
            </div>
                <div class="checkbox">
                  <asp:CheckBox ID="checkIsDefault" runat="server" TextAlign="Left" meta:resourcekey="lclIsDefault" class="checkboxfield"/>
            </div>
            <div >
                 <asp:Button ID="btnAddCard" runat="server" OnClick="BtnAddCardClick" class="button" meta:resourcekey="btnAddCard" />
            </div>
            </div>

        </form>
    </div>
    </div>
</body>