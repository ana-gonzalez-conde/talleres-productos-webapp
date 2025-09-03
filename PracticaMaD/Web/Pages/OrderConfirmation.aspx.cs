using System;
using System.Collections.Generic;
using System.Management.Instrumentation;
using System.Web;
using System.Web.UI;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Model.Services;
using Model.Services.Exceptions;
using Model.Services.OrderService;
using Model.Services.UserService;

namespace Web.Pages
{
    public partial class OrderConfirmation : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDefaultPaymentAndAddress();
            }
        }

        private void LoadDefaultPaymentAndAddress()
        {
            var userService = ResolveUserService();
            var user = SessionManager.GetUserSession(Context);
            var userInfo = userService.DefaultInfo(user.UserProfileId);
            txtShippingAddress.Text = userInfo.ShippingAddress;
            ddlBankCards.DataSource = userInfo.BankCards;
            ddlBankCards.DataTextField = "Number"; 
            ddlBankCards.DataValueField = "CardId";
            ddlBankCards.DataBind();

            DateTime orderDate = DateTime.Now; // Obtiene la fecha y hora actual
            lblOrderDate.Text = orderDate.ToString("dd/MM/yyyy"); // Formatea y muestra la fecha actual en la etiqueta
            var defaultCard = userInfo.DefaultCard();
            // Si existe una tarjeta predeterminada, la selecciona en el DropDownList
            if (defaultCard != null)
            {
                ddlBankCards.SelectedValue = defaultCard.CardId.ToString();
            }
        }

        protected void btnConfirmOrder_Click(object sender, EventArgs e)
        {
            ShoppingCartActions cart = Session["Cart"] as ShoppingCartActions;
            long userId = SessionManager.GetUserSession(Context).UserProfileId;  // recuperamos UserProfileId en la sesión
            string address = txtShippingAddress.Text;
            string descriptiveName = txtDescriptiveName.Text;
            if (string.IsNullOrEmpty(ddlBankCards.SelectedValue))
            {
                lblConfirmationMessage.Text = "Por favor, seleccione una tarjeta bancaria.";
                lblConfirmationMessage.CssClass = "confirmation-message error";
                lblConfirmationMessage.Visible = true;
                return;
            }
            long cardId = Convert.ToInt64(ddlBankCards.SelectedValue);
            bool expressShipping = chkExpressShipping.Checked; 

            try
            {
                var orderService = ResolveOrderService(); // Obtiene la instancia de OrderService usando inyección de dependencias
                List<OrderLinePurchaseDetails> orderLinesDetails = cart.RetrieveOrderLinesFromCart();

                long orderId = orderService.CreateOrder(userId, orderLinesDetails, cardId, address, descriptiveName, expressShipping);

                lblConfirmationMessage.Text = "Pedido confirmado con éxito. ID del pedido: " + orderId;
                lblConfirmationMessage.Visible = true;
                lblConfirmationMessage.CssClass = "confirmation-message success";
                btnConfirmOrder.Enabled = false;
                Session["Cart"] = null; // Limpia el carrito después de realizar el pedido
                ScriptManager.RegisterStartupScript(this, GetType(), "redirect",
    "setTimeout(function(){ window.location.href = 'searchProducts.aspx'; }, 2000);", true);

            }
            catch (UserNotAuthenticatedException ex)
            {
                lblConfirmationMessage.Text = "Usuario no autenticado: " + ex.Message;
                lblConfirmationMessage.CssClass = "confirmation-message error";
            }
            catch (IncorrectBankCardException ex)
            {
                lblConfirmationMessage.Text = "Error con la tarjeta bancaria: " + ex.Message;
                lblConfirmationMessage.CssClass = "confirmation-message error";
            }
            catch (ArgumentException ex)
            {
                lblConfirmationMessage.Text = "Información de pedido incorrecta: " + ex.Message;
                lblConfirmationMessage.CssClass = "confirmation-message error";
            }
            catch (InstanceNotFoundException ex)
            {
                lblConfirmationMessage.Text = "Error: " + ex.Message;
                lblConfirmationMessage.CssClass = "confirmation-message error";
            }
            catch (Exception ex)
            {
                lblConfirmationMessage.Text = "Error al procesar el pedido: " + ex.Message;
                lblConfirmationMessage.CssClass = "confirmation-message error";
            }

            lblConfirmationMessage.Visible = true;
        }

        private IOrderService ResolveOrderService()
        {
            var iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            return iocManager.Resolve<IOrderService>();
        }

        private IUserService ResolveUserService()
        {
            var iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            return iocManager.Resolve<IUserService>();
        }
    }
}
