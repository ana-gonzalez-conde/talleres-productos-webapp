<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddComment.aspx.cs" Inherits="Web.Pages.User.AddComment" meta:resourcekey="PageResource1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="../../Css/modal.css" rel="stylesheet" type="text/css" />
    <link href="../../Css/styles.css" rel="stylesheet"type="text/css" />
</head>
<body>
    <div id="form">
        <div class ="modal-container">
            <form id="form1" runat="server">
                <h2 class="title"><asp:Localize ID="lclTitle" runat="server" Text="Add Comment" meta:resourcekey="lclTitleResource1" />
</h2>

            <div class="fields-container">

            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclMessage" runat="server" Text="Message" meta:resourcekey="lclMessageResource1" />
                </span><span
                        class="entry">
                        <asp:TextBox ID="txtMessage" runat="server" Columns="16" meta:resourcekey="txtMessageResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvMessage" runat="server" ControlToValidate="txtMessage"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>" meta:resourcekey="rfvMessageResource1"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Message must be under 4000 characters" ControlToValidate="txtMessage" 
                                         OnServerValidate="CustomValidator_ServerValidate" meta:resourcekey="CustomValidator1Resource1"></asp:CustomValidator>
                        <asp:Label ID="lblLoginError" runat="server" ForeColor="Red" Style="position: relative"
                            Visible="False" meta:resourcekey="lblLoginErrorResource1"></asp:Label></span>
      
            </div>
                
                <div class="field">
                    <span class="label">
                        <asp:Localize ID="lclTags" runat="server" Text="Tags" meta:resourcekey="lclTagsResource1" />
                    </span><span
                               class="entry">
                        <asp:TextBox ID="txtLabels" runat="server" Columns="16" meta:resourcekey="txtLabelsResource1"></asp:TextBox>
                        </span>
      
                </div>

            <div>
                <asp:Button ID="btnPublish" runat="server" OnClick="BtnPublishClick" Text="Publish" class="button" meta:resourcekey="btnPublishResource1" />
            </div>
            </div>
            </form>
        </div>
    </div>
</body>
</html>
