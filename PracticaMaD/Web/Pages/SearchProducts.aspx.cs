using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Model.Services.OrderService;
using Model.Services.ProductService;

namespace ProductSearchPage
{
    public partial class SearchProducts : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadInitialData();
                PopulateCategories();
            }
            else
            {
                var searchText = Session["LastSearchText"] as string;
                if (!string.IsNullOrEmpty(searchText))
                {
                    LoadProducts(searchText);
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            UpdateCartCountOnNavBar();  // Ensure the cart count is updated before the page is rendered
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text;
            Session["LastSearchText"] = searchText;
            LoadProducts(searchText);
            ltlMessage.Visible = false; // Ocultar mensaje al iniciar búsqueda
        }

        private void LoadInitialData()
        {
            // Implementa la carga de datos iniciales si es necesario.
        }

        private void LoadProducts(string searchText)
        {
            var productService = ResolveProductService();
            string selectedCategoryId = categorySelect.SelectedValue;
            List<ProductDTO> products = !string.IsNullOrEmpty(selectedCategoryId) && selectedCategoryId != "all"
                ? productService.FindProducts(searchText, 0, 10, Convert.ToInt64(selectedCategoryId))
                : productService.FindProducts(searchText, 0, 10);

            UpdateProductGrid(products, productService);
        }

        private IProductService ResolveProductService()
        {
            var iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            return iocManager.Resolve<IProductService>();
        }

        private void PopulateCategories()
        {
            var productService = ResolveProductService();
            var categories = productService.GetAllCategories();
            categorySelect.Items.Clear();
            categorySelect.Items.Add(new ListItem("Todas las categorías", "all"));
            foreach (var category in categories)
            {
                categorySelect.Items.Add(new ListItem(category.name, category.categoryId.ToString()));
            }
        }

        private void UpdateProductGrid(List<ProductDTO> products, IProductService productService)
        {
            productsGrid.Controls.Clear();
            foreach (var product in products)
            {
                Panel productPanel = new Panel { CssClass = "productCard" };
                var titleLink = new HyperLink
                {
                    NavigateUrl = $"ProductDetails.aspx?id={product.ProductId}",
                    Text = product.Name
                };
                titleLink.Controls.Add(new LiteralControl($"<h4>{product.Name}</h4>"));
                var dateLabel = new Label { Text = $"{GetLocalResourceObject("DateAddedText") + ":"} {product.AddingDate.ToShortDateString()}" };
                var priceLabel = new Label { Text = $"{GetLocalResourceObject("PriceLabel") + ":"} {product.Price:C}" };
                var categoryLabel = new Label { Text = $"{GetLocalResourceObject("CategoryLabel") + ":"} {productService.FindCategory(product.CategoryId ?? 0)}" };
                var addToCartButton = new Button
                {
                    Text = $"{GetLocalResourceObject("AddToCartLabel")}",
                    CommandArgument = product.ProductId.ToString(),
                    CssClass = "addToCart",
                    UseSubmitBehavior = false
                };
                addToCartButton.Command += AddToCart_Command;
                productPanel.Controls.Add(titleLink);
                productPanel.Controls.Add(dateLabel);
                productPanel.Controls.Add(new LiteralControl("<br />"));
                productPanel.Controls.Add(categoryLabel);
                productPanel.Controls.Add(new LiteralControl("<br />"));
                productPanel.Controls.Add(priceLabel);
                productPanel.Controls.Add(new LiteralControl("<br />"));
                productPanel.Controls.Add(new LiteralControl("<br />"));
                productPanel.Controls.Add(addToCartButton);
                productsGrid.Controls.Add(productPanel);
            }
        }

        protected void AddToCart_Command(object sender, CommandEventArgs e)
        {
            ShoppingCartActions cart = (ShoppingCartActions)Session["Cart"] ?? new ShoppingCartActions();
            var productService = ResolveProductService();
            // Obtener el ID del producto desde el CommandEventArgs
            long productId = Convert.ToInt64(e.CommandArgument);

            // Recuperar los detalles del producto (implementa la función FindProducts)
            ProductDetails productDetails = productService.FindProductDetails(productId);

            // Crear un CartUnit con cantidad 1
            CartUnit cartUnit = new CartUnit(productId, productDetails, quantity: 1);

            // Agregar el CartUnit al carrito
            var message = cart.AddCartUnit(cartUnit);
            //feedback de añadido
            if (message) DisplayMessage($"{GetLocalResourceObject("AddToCartSuccess")}", true);
            else DisplayMessage($"{GetLocalResourceObject("AddToCartWrong")}", false);
            // Actualizar la sesión con el carrito modificado
            Session["Cart"] = cart;
        }

        public void DisplayMessage(string message, bool isSuccess)
        {
            ltlMessage.Text = message;
            ltlMessage.CssClass = isSuccess ? "message success" : "message error";
            ltlMessage.Visible = true; // Hacer el mensaje visible a nivel de servidor

            // Hacer visible el contenedor del mensaje a través de JavaScript
            string showScript = $@"document.getElementById('{ltlMessage.ClientID}').parentNode.style.display = 'block';";
            ScriptManager.RegisterStartupScript(this, GetType(), "showMessage", showScript, true);

            // Temporizador para ocultar el mensaje y el contenedor
            string hideScript = $@"setTimeout(function() {{
                          var msgContainer = document.getElementById('{ltlMessage.ClientID}').parentNode;
                          if(msgContainer) {{
                              msgContainer.style.display = 'none'; // Oculta todo el contenedor del mensaje
                          }}
                       }}, 1000);"; // Ocultar después de 1 segundo
            ScriptManager.RegisterStartupScript(this, GetType(), "hideMessage", hideScript, true);
        }

        private void UpdateCartCountOnNavBar()
        {
            var navBar = this.FindControl("navBar") as NavigationBar;  // Simplified search
            if (navBar != null)
            {
                navBar.UpdateCartDisplay();
            }
            else
            {
                throw new Exception("NavigationBar control not found directly in SearchProducts.");
            }
        }
    }
}
