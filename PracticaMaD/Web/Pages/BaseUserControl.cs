using Es.Udc.DotNet.ModelUtil.Log;
using Es.Udc.DotNet.PracticaMad.Web.HTTP.View.ApplicationObjects;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;

namespace Web.Pages
{
    public class BaseUserControl : UserControl
    {
        protected override void OnLoad(EventArgs e)
        {
            if (SessionManager.IsUserAuthenticated(Context))
            {
                Locale locale = SessionManager.GetLocale(Context);

                String culture = locale.Language + "-" + locale.Country;
                CultureInfo cultureInfo;

                /*
                 * The method CreateSpecificCulture will try to create a
                 * specific culture based on the combination selected by the
                 * user (i.e. <laguageCode2>-<country/regionCode2>). If the
                 * combination is not a valid culture, it will create a
                 * specific culture using 1) the languague and 2) the default
                 * region for that language. For example, if user selects
                 * gl-UK (wich is not a valid culture), an gl-ES specific
                 * culture will be created
                 */
                try
                {
                    cultureInfo = new CultureInfo(culture);

                    LogManager.RecordMessage("Specific culture created: " + cultureInfo.Name, MessageType.Info);
                }
                /*
                 * If any error occurs we will create a default culture
                 * "en-US". This exception should never happen.
                 */
                catch (ArgumentException)
                {
                    cultureInfo = CultureInfo.CreateSpecificCulture("en-US");
                    LogManager.RecordMessage("Default Specific culture created: " + cultureInfo.Name, MessageType.Info);
                }

                Thread.CurrentThread.CurrentCulture = cultureInfo;
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
            }

            base.OnLoad(e);
        }
    }
}