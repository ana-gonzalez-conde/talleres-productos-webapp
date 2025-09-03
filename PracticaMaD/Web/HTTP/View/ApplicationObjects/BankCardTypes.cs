using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects
{
    public class BankCardTypes
    {
        private static readonly ArrayList bank_card_types_es = new ArrayList();
        private static readonly ArrayList bank_card_types_en = new ArrayList();
        private static readonly ArrayList bank_card_types_gl = new ArrayList();
        private static readonly Hashtable bank_card_types = new Hashtable();

        private BankCardTypes() { }

        static BankCardTypes()
        {

            #region set the types

            bank_card_types_es.Add(new ListItem("Crédito", "cr"));
            bank_card_types_es.Add(new ListItem("Débito", "db"));

            bank_card_types_en.Add(new ListItem("Credit", "cr"));
            bank_card_types_en.Add(new ListItem("Debit", "db"));

            bank_card_types_gl.Add(new ListItem("Crédito", "cr"));
            bank_card_types_gl.Add(new ListItem("Débito", "db"));

            bank_card_types.Add("es", bank_card_types_es);
            bank_card_types.Add("en", bank_card_types_en);
            bank_card_types.Add("gl", bank_card_types_gl);

            #endregion
        }

        public static ICollection GetBankCardTypesCodes()
        {
            return bank_card_types.Keys;
        }

        public static byte GetTypeByCode(String code)
        {
            if (code.Equals("cr"))
            {
                return 0;
            }

            return 1;
        }

        public static string GetCodeByNumber(long number)
        {
            if (number == 0)
            {
                return "cr";
            }

            return "db";
        }

        public static ArrayList GetBankCardTypes(String language)
        {
            ArrayList lang = (ArrayList)bank_card_types[language];

            if (lang != null)
            {
                return lang;
            }
            else
            {
                return (ArrayList)bank_card_types["en"];
            }
        }
    }
}