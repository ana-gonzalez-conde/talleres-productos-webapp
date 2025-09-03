using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.IoC;
using Model.Services;
using Model.Services.UserService;
using Model.Services.CommentService;
using Model.Services.CommentService.Exceptions;

namespace Web.Pages.User
{
    public partial class AddComment : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int maxLength = 4000; 

            args.IsValid = txtMessage.Text.Length < maxLength;
        }

        private ICommentService ResolveCommentService()
        {
            var iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            return iocManager.Resolve<ICommentService>();
        }

        protected void BtnPublishClick(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {
                try
                {

                    long productId = Convert.ToInt32(Request.Params.Get("productId"));

                    var user = SessionManager.GetUserSession(Context);
                    long currentUserId = user.UserProfileId;

                    var commentService = ResolveCommentService();

                    long commentId = commentService.AddComment(currentUserId, productId, txtMessage.Text);

                    if (txtLabels.Text != null)
                    {

                        string[] tagsString = txtLabels.Text.Split(',');
                        List<string> tags = new List<string>(tagsString);

                        commentService.AddTagsToComment(commentId, tags);

                    }


                    String url =
                        "/Pages/ViewComments.aspx" + "?productId=" + productId +
                        "&startIndex=" + 0 + "&count=" + 5;


                    Response.Redirect(Response.
                        ApplyAppPathModifier(url));
                }
                catch (MaxAllowedCharactersExceedException)
                {
                    lblLoginError.Visible = true;
                }
            }

        }
    }
}