using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;
using Model.Services.UserService;
using System;
using System.Web.UI;

namespace Web.Pages.User
{
    public partial class UpdateBankCard : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["cardId"] != null)
                {
                    UserDetails userProfileDetails =
                        SessionManager.FindUserProfileDetails(Context);

                    long cardId = Convert.ToInt64( Request.QueryString["cardId"]);
                    // Use cardId to retrieve card details and populate the form for updating
                    CardDetails card = SessionManager.FindBankCardDetails(cardId);

                    txtNumber.Text = card.Number;
                    txtCvv.Text = card.Cvv;
                    UpdateComboType(userProfileDetails.Language, BankCardTypes.GetCodeByNumber(card.Type));
                    txtExpirationDate.Text = card.ExpirationData.ToShortDateString();
                    checkIsDefault.Checked = card.IsDefault;
                    Calendar1.SelectedDate = card.ExpirationData;
                    if(card.IsDefault == true)
                    {
                        checkIsDefault.Enabled = false;
                    }
                }
            }
        }

        private void UpdateComboType(String selectedLanguage, String selectedType)
        {
            this.comboType.DataSource = BankCardTypes.GetBankCardTypes(selectedLanguage);
            this.comboType.DataTextField = "text";
            this.comboType.DataValueField = "value";
            this.comboType.DataBind();
            this.comboType.SelectedValue = selectedType;
        }

        protected void BtnUpdateCardClick(object sender, EventArgs e)
        {
            
            if (Page.IsValid)
            {
                long cardId = Convert.ToInt64(Request.QueryString["cardId"]);

                CardDetails card =
                    new CardDetails(BankCardTypes.GetTypeByCode(comboType.SelectedValue), txtNumber.Text,
                        txtCvv.Text, DateTime.ParseExact(txtExpirationDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), checkIsDefault.Checked);

                SessionManager.UpdateBankCard(Context, cardId, card);

                Response.Redirect(
                    Response.ApplyAppPathModifier("~/Pages/SearchProducts.aspx"));
            }
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
                // Check if the selected date is equal to or after today's date
                if (Calendar1.SelectedDate >= DateTime.Today)
                {
                // Set the selected date from the calendar to the TextBox
                txtExpirationDate.Text = Calendar1.SelectedDate.ToString("dd/MM/yyyy");
                }
                else
                {
                // Clear the selection and inform the user
                Calendar1.SelectedDate = DateTime.Today;
                    txtExpirationDate.Text = "";
                
                    // You can show a message to the user indicating that past dates are not allowed.
                    // For example:
                     ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select a date on or after today.');", true);
                }

        }
    }
}