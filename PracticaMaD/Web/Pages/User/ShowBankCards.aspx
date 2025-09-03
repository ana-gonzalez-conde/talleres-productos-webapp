<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowBankCards.aspx.cs" Inherits="Web.Pages.User.ShowBankCards" %>

<head runat="server">
    <title>Register</title>
    <link href="../../Css/modal.css" rel="stylesheet" type="text/css" />
    <link href="../../Css/styles.css" rel="stylesheet" type="text/css" />
    <link href="../../Css/UpdateBankCard.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div id="form">
        <div class="modal-container">
            <form id="UpdateBankCardForm" method="post" runat="server" class="form-scroll">
                <h2 class="title"><asp:Localize ID="lclTitle" runat="server" meta:resourcekey="lclTitle" /></h2>
                <asp:Repeater ID="rptBankCards" runat="server">
                    <ItemTemplate>
                        <div class="fields-container">
                            <div class="field">
                                <span class="label"><asp:Localize ID="lclNumber" runat="server" meta:resourcekey="lclNumber" /></span>
                                <span class="entry">
                                    <asp:Label ID="lblNumber" runat="server" Text='<%# Eval("Number") %>'></asp:Label>
                                </span>
                            </div>
                            <div class="field">
                                <span class="label"><asp:Localize ID="lclCvv" runat="server" meta:resourcekey="lclCvv" /></span>
                                <span class="entry">
                                    <asp:Label ID="lblCVV" runat="server" Text='<%# Eval("CVV") %>'></asp:Label>
                                </span>
                            </div>
                            <div class="field">
                                <span class="label"><asp:Localize ID="lclType" runat="server" meta:resourcekey="lclType" /></span>
                                <span class="entry">
                                    <asp:Label ID="lblType" runat="server" Text='<%# GetCardTypeText(Eval("Type")) %>'></asp:Label>
                                </span>
                            </div>
                            <div class="field">
                                <span class="label"><asp:Localize ID="lclExpirationDate" runat="server" meta:resourcekey="lclExpirationDate" /></span>
                                <span class="entry">
                                    <asp:Label ID="lblExpirationDate" runat="server" Text='<%# Eval("ExpirationData") %>'></asp:Label>
                                </span>
                            </div>
                            <div class="field">
                                <span class="label"><asp:Localize ID="lclIsDefault" runat="server" meta:resourcekey="lclIsDefault" /></span>
                                <span class="entry">
                                    <asp:CheckBox ID="chkIsDefault" runat="server" Checked='<%# Convert.ToBoolean(Eval("IsDefault")) %>' Enabled="false" />
                                </span>
                            </div>
                            <div >
                                 <asp:Button ID="btnUpdateCard" runat="server" OnClick="BtnUpdateCardClick" class="button" CommandArgument='<%# Eval("CardId") %>' meta:resourcekey="btnUpdateCard" />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </form>
        </div>
    </div>
</body>
