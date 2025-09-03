using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;
using Model.Services.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Pages.User
{
    public partial class ShowBankCards : SpecificCulturePage
    { 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve bank card details from session and bind them to the repeater
                List<CardDetails> bankCardsDetails = SessionManager.FindBankCardsDetails(Context);
                rptBankCards.DataSource = bankCardsDetails;
                rptBankCards.DataBind();
            }
        }

        protected string GetCardTypeText(object type)
        {
            int cardType = Convert.ToInt32(type);
            return cardType == 0 ? "Credit" : "Debit";
        }

        protected void BtnUpdateCardClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string cardId = btn.CommandArgument;
            Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/User/UpdateBankCard.aspx?cardId=" + cardId));
        }
    }
}
