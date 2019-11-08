using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using Westwind.AspNetCore.Markdown;

namespace HelpDown
{
    public static class ApplicationExtension
    {
        public static string HelpDownRootPath;
        public static string HelpDownFolderName;
        public static string DefaultLanguage;

        public static IApplicationBuilder UseHelpDown(
            this IApplicationBuilder app,
            string folderName = "helpDown",
            string wwwrootName = "wwwroot",
            string defaultLanguage="en"
            )
        {
            HelpDownFolderName = folderName;
            DefaultLanguage = defaultLanguage;
            
            app.UseMarkdown();

            CreateBaseFolders(wwwrootName,folderName);

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(HelpDownRootPath),
                RequestPath = $"/{folderName}"
            });

            return app;
        }


        /// <summary>
        /// Create base folders 
        /// </summary>
        /// <param name="wwwrootName">Name of the wwwroot folder</param>
        /// <param name="folderName"></param>
        /// <returns>The base folder's root folder path </returns>
        private static void CreateBaseFolders(string wwwrootName, string folderName)
        {
            HelpDownRootPath=Path.Combine(Environment.CurrentDirectory, wwwrootName, folderName);
            Directory.CreateDirectory(HelpDownRootPath);
        }
    }
}