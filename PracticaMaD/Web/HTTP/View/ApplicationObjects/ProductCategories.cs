using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Web.HTTP.View.ApplicationObjects
{
    public class ProductCategories
    {

        private static readonly ArrayList categories_es = new ArrayList();
        private static readonly ArrayList categories_en = new ArrayList();
        private static readonly ArrayList categories_gl = new ArrayList();
        private static readonly ArrayList categoryeCodes = new ArrayList();
        private static readonly Hashtable categories = new Hashtable();
        private static readonly Hashtable dbEquivalences = new Hashtable();


        private ProductCategories() { }

        static ProductCategories()
        {
            #region set the countries

            categories_es.Add(new ListItem("Aceites y lubricantes", "Aceites y lubricantes"));
            categories_es.Add(new ListItem("Filtros", "Filtros"));
            categories_es.Add(new ListItem("Baterías", "Baterías"));
            categories_es.Add(new ListItem("Frenos", "Frenos"));
            categories_es.Add(new ListItem("Neumáticos", "Neumáticos"));
            categories_es.Add(new ListItem("Refrigerantes", "Refrigerantes"));

            categories_en.Add(new ListItem("Oils", "Aceites y lubricantes"));
            categories_en.Add(new ListItem("Filters", "Filtros"));
            categories_en.Add(new ListItem("Batteries", "Baterías"));
            categories_en.Add(new ListItem("Brakes", "Frenos"));
            categories_en.Add(new ListItem("Pneumatics", "Neumáticos"));
            categories_en.Add(new ListItem("Refrigerants", "Refrigerantes"));

            categories_gl.Add(new ListItem("Aceites e lubricantes", "Aceites y lubricantes"));
            categories_gl.Add(new ListItem("Filtros", "Filtros"));
            categories_gl.Add(new ListItem("Baterías", "Baterías"));
            categories_gl.Add(new ListItem("Frenos", "Frenos"));
            categories_gl.Add(new ListItem("Neumáticos", "Neumáticos"));
            categories_gl.Add(new ListItem("Refrixerantes", "Refrigerantes"));

            categoryeCodes.Add("Aceites e lubricantes");
            categoryeCodes.Add("Filtros");
            categoryeCodes.Add("Baterías");
            categoryeCodes.Add("Frenos");
            categoryeCodes.Add("Neumáticos");
            categoryeCodes.Add("Refrixerantes");



            categories.Add("es", categories_es);
            categories.Add("en", categories_en);
            categories.Add("gl", categories_gl);

            dbEquivalences.Add( "Aceites y lubricantes", 1);
            dbEquivalences.Add( "Filtros", 2);
            dbEquivalences.Add( "Baterías", 3);
            dbEquivalences.Add( "Frenos", 4);
            dbEquivalences.Add( "Neumáticos", 5);
            dbEquivalences.Add( "Refrigerantes", 6);


            #endregion

        }

        public static int GetDbCategoryIdByCode(string code)
        {
            return (int)dbEquivalences[code];
       
        }

        public static ArrayList GetCategories(String languageCode)
        {
            ArrayList lang = (ArrayList)categories[languageCode];

            if (lang != null)
            {
                return lang;
            }
            else
            {
                return (ArrayList)categories["en"];
            }
        }
    }
}