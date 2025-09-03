using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Model.Services.OrderService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Pages
{
    public partial class ViewOrderDetails : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadOrderDetails();
            }
        }

        private void LoadOrderDetails()
        {
            if (SessionManager.IsUserAuthenticated(Context))
            {
                long orderId = GetOrderIdFromQuery();

                IOrderService orderService = ResolveOrderService();

                try
                {
                    var orderDetails = orderService.GetLineOrdersByOrderId(orderId);
                    if (orderDetails.Count > 0)
                    {
                        rptOrderDetails.DataSource = orderDetails;
                        rptOrderDetails.DataBind();
                    }
                    else
                    {
                        ShowErrorMessage("No hay detalles para mostrar en este pedido.");
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage("Error al cargar los detalles del pedido: " + ex.Message);
                }
            }
            else
            {
                Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/User/Authentication.aspx"));
            }
        }

        private long GetOrderIdFromQuery()
        {
            long orderId = 0;
            if (!string.IsNullOrEmpty(Request.QueryString["orderId"]))
            {
                long.TryParse(Request.QueryString["orderId"], out orderId);
            }
            return orderId;
        }

        private IOrderService ResolveOrderService()
        {
            var iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            return iocManager.Resolve<IOrderService>();
        }

        private void ShowErrorMessage(string message)
        {
            lblErrorMessage.Text = message;
            lblErrorMessage.Visible = true;
        }

        protected void rptOrderDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                OrderLineDisplayDetails orderLine = (OrderLineDisplayDetails)e.Item.DataItem;
                Image imgProduct = (Image)e.Item.FindControl("imgProduct");

                if (imgProduct != null)
                {
                    // Set the image URL or a default image URL if the actual URL is null or empty
                    string imageUrl = orderLine.ProductImage;
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


    }
}
