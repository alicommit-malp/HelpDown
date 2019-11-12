using System;
using System.Globalization;
using System.IO;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc.Rendering;
using Westwind.AspNetCore.Markdown;

namespace HelpDown
{
    public static class HelpDown
    {
        /// <summary>
        /// Determine whether the help file for this controller/action exist or not
        /// </summary>
        /// <param name="viewContext"></param>
        /// <param name="cultureInfo"></param>
        /// <returns>True if the help exist</returns>
        public static bool Exists(ViewContext viewContext, CultureInfo cultureInfo)
        {
            var controllerName = viewContext.RouteData.Values["controller"].ToString();
            var actionName = viewContext.RouteData.Values["action"].ToString();
            var lang = cultureInfo.TwoLetterISOLanguageName.ToLower();

            var helpPath = new HelpPath(controllerName, actionName, lang);

            return helpPath.GetPath() != null;
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

            return GenerateHtml(new HelpPath(controllerName, actionName, lang));
        }


        private static string GenerateHtml(HelpPath helpPath)
        {
            var html = new HtmlDocument();

            if (helpPath.GetPath() == null) return null;
            
            var htmlRaw = Markdown.ParseFromFile(helpPath.GetPath());

            if (string.IsNullOrEmpty(htmlRaw)) return null;

            html.LoadHtml(htmlRaw);
            foreach (var node in html.DocumentNode.SelectNodes("//img"))
            {
                var src = node.Attributes["src"].Value;
                node.SetAttributeValue("src", ConvertPathIfNeeded(src, helpPath));
            }

            var stream = new MemoryStream();
            var streamR = new StreamReader(stream);

            html.Save(stream);
            stream.Seek(0, SeekOrigin.Begin);
            return streamR.ReadToEnd();
        }


        private static string ConvertPathIfNeeded(string src, HelpPath helpPath)
        {
            try
            {
                var url = new Uri(src);
                return url.ToString();
            }
            catch (Exception)
            {
                //ignore
            }


            return
                $"/{ApplicationExtension.HelpDownFolderName}/{helpPath.GetControllerName()}/{helpPath.GetActionName()}/{src}";
        }
    }
}