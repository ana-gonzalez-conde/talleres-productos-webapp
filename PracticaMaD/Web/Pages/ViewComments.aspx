<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewComments.aspx.cs" Inherits="Web.Pages.ViewComments" meta:resourcekey="PageResource1" %>
<%@ Register Src="~/Pages/NavigationBar.ascx" TagPrefix="uc" TagName="NavigationBar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../Css/viewComments.css" rel="stylesheet" type="text/css" />
    <link href="../Css/navigationBar.css" rel="stylesheet" type="text/css" /> <!-- Vinculación al CSS de la barra de navegación -->
    <link href="../Css/modal.css" rel="stylesheet" type="text/css" />
    <link href="../Css/styles.css" rel="stylesheet"type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <!-- Barra de navegación superior usando User Control -->
        <uc:NavigationBar runat="server" id="navBar" />
        
        <br />

        <asp:GridView ID="gvComments" runat="server" class="comments" GridLines="None"
                      AutoGenerateColumns="False" OnRowDataBound="gvComments_RowDataBound" meta:resourcekey="gvCommentsResource1">
            <Columns>
                <asp:TemplateField HeaderText="login" meta:resourcekey="TemplateFieldResource1">
                    <ItemTemplate>
                        <%# GetUserLogin(Convert.ToInt32(Eval("userId"))) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="date" HeaderText="date" meta:resourcekey="BoundFieldResource1" />
                <asp:BoundField DataField="message" HeaderText="message" meta:resourcekey="BoundFieldResource2" />
                <asp:TemplateField HeaderText="tags" meta:resourcekey="TemplateFieldResource2">
                    <ItemTemplate>
                        <%# GetCommentTags(Convert.ToInt32(Eval("commentId"))) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" " meta:resourcekey="TemplateFieldResource3">
                    <ItemTemplate>
                        <asp:HyperLink ID="lnkEdit" runat="server" Text="Edit" NavigateUrl='<%# "EditComment.aspx?CommentId=" + Eval("commentId") %>' meta:resourcekey="lnkEditResource1"></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        
        <div>
            <asp:Button ID="btnAddComment" runat="server" Text="Add Comment" OnClick="BtnAddCommentClick" class="button" meta:resourcekey="btnAddCommentResource1" />
        </div>

    </form>


    <br />
    <!-- "Previous" and "Next" links. -->
    <div class="previousNextLinks">
        <span class="previousLink">
            <asp:HyperLink ID="lnkPrevious" Text="Previous" runat="server"
                       Visible="False" meta:resourcekey="lnkPreviousResource1"></asp:HyperLink>
        </span><span class="nextLink">
            <asp:HyperLink ID="lnkNext" Text="Next" runat="server" Visible="False" meta:resourcekey="lnkNextResource1"></asp:HyperLink>
        </span>
    </div>
</body>
</html>
