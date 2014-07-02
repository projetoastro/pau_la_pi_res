using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public static class MyHtmlHelper
{
    public static String NavActive(this HtmlHelper htmlHelper,
                      string actionName,
                      string controllerName)
    {
        var controller = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");
        var action = htmlHelper.ViewContext.RouteData.GetRequiredString("action");

        if (controllerName == controller && action == actionName)
            return "active";
        else
            return String.Empty;
    }
}