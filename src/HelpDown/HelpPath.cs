using System.IO;

namespace HelpDown
{
    internal class HelpPath
    {
        private readonly string _controllerName;
        private readonly string _actionName;
        private readonly string _cultureName;

        public HelpPath(string controllerName, string actionName, string cultureName)
        {
            _controllerName = controllerName;
            _actionName = actionName;
            _cultureName = cultureName;
        }


        /// <summary>
        /// Try to get the MD file path , if the related culture path does not exist
        /// therefore it will try to get the default language's MD file
        /// if the default md file does not exits also , then it will return null
        /// </summary>
        /// <returns></returns>
        public string GetPath()
        {
            if (File.Exists(GetAbsolutePath()))
                return Path.Combine(ApplicationExtension.HelpDownFolderName, _controllerName, _actionName,
                    $"{_cultureName}.md");


            if (File.Exists(GetAbsolutePath(true)))
                return Path.Combine(ApplicationExtension.HelpDownFolderName, _controllerName, _actionName,
                    $"{ApplicationExtension.DefaultLanguage.ToLower()}.md");

            return null;
        }

        private string GetAbsolutePath(bool defaultMdFile = false)
        {
            return Path.Combine(ApplicationExtension.HelpDownRootPath, _controllerName, _actionName,
                !defaultMdFile ? $"{_cultureName}.md" : $"{ApplicationExtension.DefaultLanguage.ToLower()}.md");
        }

        public string GetControllerName()
        {
            return _controllerName;
        }

        public string GetActionName()
        {
            return _actionName;
        }
    }
}