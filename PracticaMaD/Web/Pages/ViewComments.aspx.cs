using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.IoC;
using Model.Services;
using Model.Services.CommentService;
using Model.Services.UserService;

namespace Web.Pages
{
    public partial class ViewComments : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            int startIndex, count;

            lnkPrevious.Visible = false;
            lnkNext.Visible = false;

            /* Get ProductId passed as parameter in the request from
             * the previous page
             */
            long productId = Convert.ToInt32(Request.Params.Get("productId"));


            /* Get Start Index */
            try
            {
                startIndex = Int32.Parse(Request.Params.Get("startIndex"));
            }
            catch (ArgumentNullException)
            {
                startIndex = 0;
            }

            /* Get Count */
            try
            {
                count = Int32.Parse(Request.Params.Get("count"));
            }
            catch (ArgumentNullException)
            {
                count = 5;
            }

            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ICommentService commentService = iocManager.Resolve<ICommentService>();

            /* Get Comments Info */
            CommentBlock commentBlock = commentService.FindProductComments(productId, startIndex, count);

            this.gvComments.DataSource = commentBlock.Comments;
            this.gvComments.DataBind();

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                String url =
                    "/Pages/ViewComments.aspx" + "?productId=" + productId +
                    "&startIndex=" + (startIndex - count) + "&count=" +
                    count;

                this.lnkPrevious.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkPrevious.Visible = true;
            }

            /* "Next" link */
            if (commentBlock.ExistMoreComments)
            {
                String url =
                    "/Pages/ViewComments.aspx" + "?productId=" + productId +
                    "&startIndex=" + (startIndex + count) + "&count=" +
                    count;

                this.lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkNext.Visible = true;
            }

        }

        private IUserService ResolveUserService()
        {
            var iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            return iocManager.Resolve<IUserService>();
        }

        private ICommentService ResolveCommentService()
        {
            var iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            return iocManager.Resolve<ICommentService>();
        }



        protected string GetUserLogin(int userId)
        {
            var userService = ResolveUserService();
            string login = userService.UserDetails(userId).Login;
            return login;
        }

        protected string GetCommentTags(int commentId)
        {
            var commentService = ResolveCommentService();
            List<TagDetails> tags = commentService.GetTagsOfComment(commentId);
            var tagsNames = tags.Select(e => e.Name);

            string tagsString = string.Join(", ", tagsNames);
            return tagsString;
        }

        protected void gvComments_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                var user = SessionManager.GetUserSession(Context);
                HyperLink lnkEdit = (HyperLink)e.Row.FindControl("lnkEdit");

                if (user != null)
                {

                    long currentUserId = user.UserProfileId;

                    long commentUseId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "userId"));




                    if (currentUserId != commentUseId)
                    {

                        lnkEdit.Visible = false;
                    }


                }
                else
                {

                    lnkEdit.Visible = false;

                }

               
            }
        }

        protected void BtnAddCommentClick(object sender, EventArgs e)
        {

            long productId = Convert.ToInt32(Request.Params.Get("productId"));

            String url =
                "/Pages/User/AddComment.aspx" + "?productId=" + productId;


            Response.Redirect(Response.
                ApplyAppPathModifier(url));

        }
    }
}