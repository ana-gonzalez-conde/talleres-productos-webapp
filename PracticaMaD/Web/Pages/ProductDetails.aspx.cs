using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Model.Services.ProductService;
using Model.Services.UserService;
using Model.Services.CommentService;
using System;
using System.Web;
using System.Web.UI;
using Web.HTTP.View.ApplicationObjects;

namespace Web.Pages
{
    public partial class ProductDetails : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                long productId = Convert.ToInt64(Request.QueryString["id"]);

                string url = "~/Pages/ViewComments.aspx?productId=" + productId;

                lnkViewComments.NavigateUrl = url;

                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];

                IProductService productService = iocManager.Resolve<IProductService>();

                ICommentService commentService = iocManager.Resolve<ICommentService>();

                bool hasComments = commentService.FindProductComments(productId, 0, 5).Comments.Count > 0;

                if (!hasComments)
                {
                    lnkViewComments.Visible = false;
                }

                var productDetails = productService.FindProductDetails(productId);
                string category = productService.FindCategory(productDetails.CategoryId);
                LoadProductDetails(category, productDetails);
                updatePanel.Visible = false;

                if (SessionManager.IsUserAuthenticated(Context))
                {
                    UserDetails userProfileDetails =
                       SessionManager.FindUserProfileDetails(Context);

                if (userProfileDetails.IsAdmin)
                {
                    updatePanel.Visible = true;
                    txtName.Text = productDetails.Name;
                    txtDescription.Text = productDetails.Description;
                    txtPrice.Text = productDetails.Price.ToString();
                    txtStock.Text = productDetails.Stock.ToString();

                    UpdateComboCategory(userProfileDetails.Language, category);

                }
                }

            }
        }


        protected void btnUpdateProductClick(object sender, EventArgs e)
        {

            UserSession userSession = SessionManager.GetUserSession(Context);
            var productService = ResolveProductService();

            long productId = Convert.ToInt64(Request.QueryString["id"]);

            UpdateProductDetailsInput input = new UpdateProductDetailsInput(txtName.Text, float.Parse(txtPrice.Text), int.Parse( txtStock.Text ), txtDescription.Text, ProductCategories.GetDbCategoryIdByCode( comboCategory.SelectedValue) );

            productService.UpdateProductDetails(productId, userSession.UserProfileId, input);

            var productDetails = productService.FindProductDetails(productId);
            LoadProductDetails(comboCategory.SelectedValue, productDetails);
        }

        private void LoadProductDetails(string categoryId, Model.Services.ProductService.ProductDetails productDetails)
        {

            // Actualizar controles en la página
            productName.InnerText = productDetails.Name;
            productDate.InnerText = productDetails.AddingDate.ToString("dd/MM/yyyy");
            productDescription.InnerText = productDetails.Description;

            productCategory.InnerText = categoryId;
            productPrice.InnerText = $"{productDetails.Price}€";
            //imagePath.InnerText = productDetails.Image;
            if (productDetails.Image != null)
            {

                imagePath.ImageUrl = productDetails.Image;

            }
            else
            {
                imagePath.ImageUrl = "/Images/default-image.jpg";
            }
        }
        private IProductService ResolveProductService()
        {
            var iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            return iocManager.Resolve<IProductService>();
        }

        protected void ComboCategorySelectedIndexChanged(object sender, EventArgs e)
        {
            UserDetails userProfileDetails =
                      SessionManager.FindUserProfileDetails(Context);

            this.UpdateComboCategory(userProfileDetails.Language,
                comboCategory.SelectedValue); 
        }

        private void UpdateComboCategory(String selectedLanguage, String selectedCategory)
         {
            this.comboCategory.DataSource = ProductCategories.GetCategories(selectedLanguage);
             this.comboCategory.DataTextField = "text";
             this.comboCategory.DataValueField = "value";
             this.comboCategory.DataBind();
             this.comboCategory.SelectedValue = selectedCategory;
         }
    }
}