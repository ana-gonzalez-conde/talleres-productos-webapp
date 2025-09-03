using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Model.Services.OrderService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Pages
{
    public partial class ViewOrders : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadOrders();
            }
        }

        private void LoadOrders()
        {
            if (SessionManager.IsUserAuthenticated(Context))
            {
                long userId = SessionManager.GetUserSession(Context).UserProfileId;

                IOrderService orderService = ResolveOrderService();

                try
                {
                    List<OrderDetails> orders = orderService.GetOrdersByUserId(userId);
                    if (orders.Count > 0)
                    {
                        rptOrders.DataSource = orders;
                        rptOrders.DataBind();
                        lblNoOrdersMessage.Visible = false;  // Oculta el mensaje si hay pedidos
                    }
                    else
                    {
                        lblNoOrdersMessage.Visible = true;  // Muestra el mensaje si no hay pedidos
                    }
                }
                catch (Exception ex)
                {
                    lblErrorMessage.Text = "Error loading orders: " + ex.Message;
                    lblErrorMessage.Visible = true;
                }
            }
            else
            {
                Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/User/Authentication.aspx"));
            }
        }

        private IOrderService ResolveOrderService()
        {
            var iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            return iocManager.Resolve<IOrderService>();
        }
    }
}
