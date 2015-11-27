using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace FinancialPlanner.Web.Helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString RadioButtonList<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            IEnumerable<SelectListItem> listOfValues)
        {
            ModelMetadata metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var sb = new StringBuilder();

            if (listOfValues != null)
            {
                // Create a radio button for each item in the list 
                foreach (SelectListItem item in listOfValues)
                {
                    // Generate an id to be given to the radio button field 
                    string id = string.Format("{0}_{1}", metaData.PropertyName, item.Value);
                    const string @checked = "checked";

                    // Create and populate a radio button using the existing html helpers 
                    MvcHtmlString label = htmlHelper.Label(id, HttpUtility.HtmlEncode(item.Text));
                    string radio;
                    if (item.Selected)
                        radio = htmlHelper.RadioButtonFor(expression, item.Value, new {id, @checked}).ToHtmlString();
                    else
                        radio = htmlHelper.RadioButtonFor(expression, item.Value, new {id}).ToHtmlString();

                    sb.AppendFormat("<li class='list-group-plain-item' >{0}&nbsp;{1}</li>", radio, label);
                }
            }
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}

/*

// Create the html string that will be returned to the client 
// e.g. <input data-val="true" data-val-required="You must select an option" id="TestRadio_1" name="TestRadio" type="radio" value="1" /><label for="TestRadio_1">Line1</label> 
sb.AppendFormat("<li class='list-group-plain-item' >{0}&nbsp;{1}</li>", radio, label);
//sb.AppendFormat("<div class=\"RadioButton\">{0}{1}</div>", radio, label);

*/