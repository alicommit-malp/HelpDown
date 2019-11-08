using System.IO;

namespace HelpDown
{
    internal class RequestOrigin
    {
        private readonly string _helpDownRootPath;
        private readonly string _controllerName;
        private readonly string _actionName;
        private readonly string _cultureName;

        public RequestOrigin(string controllerName, string actionName, string cultureName)
        {
            _helpDownRootPath = ApplicationExtension.HelpDownRootPath;
            _controllerName = controllerName;
            _actionName = actionName;
            _cultureName = cultureName;
        }
        
        
        public string GetDefaultMdFullPath()
        {
            return Path.Combine(ApplicationExtension.HelpDownRootPath, _controllerName, _actionName,
                $"{ApplicationExtension.DefaultLanguage.ToLower()}.md");
        }
        public string GetMdFullPath()
        {
            return Path.Combine(ApplicationExtension.HelpDownRootPath, _controllerName, _actionName,
                $"{_cultureName}.md");
        }

        
        public string GetDefaultMdPath()
        {
            return Path.Combine(ApplicationExtension.HelpDownFolderName, _controllerName, _actionName,
                $"{ApplicationExtension.DefaultLanguage.ToLower()}.md");
        }
        public string GetMdPath()
        {
            return Path.Combine(ApplicationExtension.HelpDownFolderName, _controllerName, _actionName,
                $"{_cultureName}.md");
        }
    }
}