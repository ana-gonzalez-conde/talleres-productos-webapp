using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Model.Services.OrderService;
using Model.Services.ProductService;
using System;
using System.Web;
using System.Web.UI.WebControls;
using Web.Pages;

namespace ProductSearchPage
{
    public partial class NavigationBar : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializeCart();
                UpdateCartDisplay();
                if(SessionManager.IsUserAuthenticated(Context))
                {
                    lnkAuthentication.Visible = false;
                    lnkLogOut.Visible = true;
                    lnkUpdateUserInfo.Visible = true;
                    lnkAddBankCard.Visible = true;
                    lnkMyOrders.Visible = true;
                }
                else
                {
                    lnkAuthentication.Visible = true;
                    lnkLogOut.Visible = false;
                    lnkUpdateUserInfo.Visible = false;
                    lnkAddBankCard.Visible = false;
                    lnkMyOrders.Visible = false;
                }
            }
        }

        private void InitializeCart()
        {
            if (Session["Cart"] == null)
            {
                Session["Cart"] = new ShoppingCartActions(); // Inicializa el carrito como una lista simple
            }
        }

        public void UpdateCartDisplay()
        {
            Literal cartCount = (Literal)FindControl("cartCount");
            if (cartCount != null)
            {
                ShoppingCartActions cart = Session["Cart"] as ShoppingCartActions;
                cartCount.Text = (cart != null) ? cart.GetTotalItemCount().ToString() : "0";
            }
            else
            {
                // Considera manejar el error si no se encuentra cartCount
                throw new Exception("Cart count literal not found.");
            }
        }

        private IProductService ResolveProductService()
        {
            var iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            return iocManager.Resolve<IProductService>();
        }

        protected void AddToCart_Command(object sender, CommandEventArgs e)
        {
            ShoppingCartActions cart = (ShoppingCartActions)Session["Cart"] ?? new ShoppingCartActions();
            var productService = ResolveProductService();
            // Obtener el ID del producto desde el CommandEventArgs
            long productId = Convert.ToInt64(e.CommandArgument);

            // Recuperar los detalles del producto (implementa la función FindProducts)
            Model.Services.ProductService.ProductDetails productDetails = productService.FindProductDetails(productId);

            // Crear un CartUnit con cantidad 1
            CartUnit cartUnit = new CartUnit(productId, productDetails, quantity: 1);

            // Agregar el CartUnit al carrito
            cart.AddCartUnit(cartUnit);

            // Actualizar la sesión con el carrito modificado
            Session["Cart"] = cart;
            UpdateCartDisplay(); // Actualiza el visual del contador del carrito
        }
    }
}
