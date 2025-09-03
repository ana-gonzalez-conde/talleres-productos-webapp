using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Model.Services.OrderService;
using Model.Services.ProductService;
using ProductSearchPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Pages
{
    public partial class Cart : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShoppingCartActions cart = Session["Cart"] as ShoppingCartActions;
                //Redirige a la pagina principal si eliminaste todos los elementos
                if (cart == null || !cart.cartUnits.Any())
                {
                    Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/searchProducts.aspx"));
                }
                else
                {
                    LoadCartItems();
                }
            }
        }

        private void LoadCartItems()
        {
            ShoppingCartActions cart = Session["Cart"] as ShoppingCartActions;
            if (cart != null)
            {
                rptCartItems.DataSource = cart.cartUnits;
                rptCartItems.DataBind();
                UpdateTotalPrice(cart);
            }
        }

        private void UpdateTotalPrice(ShoppingCartActions cart)
        {
            lblTotalPrice.Text = cart.GetTotalPrice().ToString("C");
        }

        private IProductService ResolveProductService()
        {
            var iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            return iocManager.Resolve<IProductService>();
        }

        protected void BtnCheckout_Click(object sender, EventArgs e)
        {
            // Lógica para procesar la compra o redirigir a la página de pago
        }

        protected void rptCartItems_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var cartUnit = (CartUnit)e.Item.DataItem;
                Label lblCategoryName = (Label)e.Item.FindControl("lblCategoryName");
                Label lblDateAdded = (Label)e.Item.FindControl("lblDateAdded");
                Image imgProduct = (Image)e.Item.FindControl("imgProduct");

                if (lblCategoryName != null)
                {
                    IProductService productService = ResolveProductService();
                    lblCategoryName.Text = productService.FindCategory(cartUnit.ProductDetails.CategoryId);
                }
                if (lblDateAdded != null)
                {
                    // Format the date to display only the date without the time
                    lblDateAdded.Text = cartUnit.ProductDetails.AddingDate.ToString("dd/MM/yyyy");
                }
                if (imgProduct != null)
                {
                    // Set the image URL or a default image URL if the actual URL is null or empty
                    string imageUrl = cartUnit.ProductDetails.Image;
                    if (!string.IsNullOrEmpty(imageUrl))
                    {
                        imgProduct.ImageUrl = imageUrl;
                    }
                    else
                    {
                        // Set a default image or show a placeholder if image URL is empty
                        imgProduct.ImageUrl = "/Images/default-image.jpg"; // Change this to the path of your default image
                    }
                }
            }
        }


        protected void rptCartItems_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            long productId = Convert.ToInt64(e.CommandArgument);
            ShoppingCartActions cart = Session["Cart"] as ShoppingCartActions;

            var cartUnit = cart.cartUnits.Find(c => c.ProductId == productId);
            if (cartUnit == null) return;
            switch (e.CommandName)
            {
                case "Remove":
                    cart.RemoveCartUnit(productId);
                    break;
                case "Increase":
                    if (cartUnit.EnoughStock(1)) // Asegura que hay stock suficiente antes de incrementar
                    {
                        cart.SetQuantity(productId, cartUnit.Quantity + 1);
                    }
                    break;
                case "Decrease":
                    if (cartUnit.Quantity > 1)
                    {
                        cart.SetQuantity(productId, cartUnit.Quantity - 1);
                    }
                    break;
            }

            Session["Cart"] = cart;
            LoadCartItems();

            if (!cart.cartUnits.Any())
            {
                Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/searchProducts.aspx"));
            }
            UpdateCartCountOnNavBar();
        }
        private void UpdateCartCountOnNavBar()
        {
            var navBar = (NavigationBar) FindControl("navBar");
            if (navBar != null)
            {
                navBar.UpdateCartDisplay();
            }
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            // Aquí puedes agregar cualquier lógica necesaria antes de finalizar el pedido,
            // como verificar que el carrito no esté vacío o realizar alguna validación final.

            ShoppingCartActions cart = Session["Cart"] as ShoppingCartActions;
            if (cart != null && cart.cartUnits.Any())
            {
                // Lógica para procesar la compra o hacer cualquier otro procesamiento necesario

                // Finalmente, redirige a la página de confirmación de la orden
                Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/OrderConfirmation.aspx"));
            }
            else
            {
                // Opcionalmente, maneja el caso donde el carrito está vacío (aunque no deberia estarlo)
                Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/searchProducts.aspx"));
            }
        }
    }
}