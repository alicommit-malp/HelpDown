using System.Globalization;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Westwind.AspNetCore.Markdown;

namespace HelpDown
{
    public static class Utils
    {
        /// <summary>
        /// Determine whether the help file for this controller/action exist or not
        /// </summary>
        /// <param name="viewContext"></param>
        /// <param name="cultureInfo"></param>
        /// <returns>True if the help exist</returns>
        public static bool HelpExist(ViewContext viewContext, CultureInfo cultureInfo)
        {
            var controllerName = viewContext.RouteData.Values["controller"].ToString();
            var actionName = viewContext.RouteData.Values["action"].ToString();
            var lang = cultureInfo.TwoLetterISOLanguageName.ToLower();

            var requestOrigin = new RequestOrigin(controllerName, actionName, lang);

            return File.Exists(requestOrigin.GetMdFullPath())
                   || File.Exists(requestOrigin.GetDefaultMdFullPath());
        }


        /// <summary>
        /// Find the md file by using the controller,action,language of the requester page
        /// </summary>
        /// <param name="viewContext"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public static string GenerateHtml(ViewContext viewContext, CultureInfo cultureInfo)
        {
            var controllerName = viewContext.RouteData.Values["controller"].ToString();
            var actionName = viewContext.RouteData.Values["action"].ToString();
            var lang = cultureInfo.TwoLetterISOLanguageName.ToLower();

            var requestOrigin = new RequestOrigin(controllerName, actionName, lang);
            

            if (File.Exists(requestOrigin.GetMdFullPath()))
                return Markdown.ParseFromFile(requestOrigin.GetMdPath());

            if (File.Exists(requestOrigin.GetDefaultMdFullPath()))
                return Markdown.ParseFromFile(requestOrigin.GetDefaultMdPath());

            return "";
        }
    }
}