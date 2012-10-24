using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VisaCzech.DL
{
    public static class TemplateStorage
    {
        public static IEnumerable<string> LoadTemplates()
        {
            IEnumerable<string> files;
            try
            {
                files = Directory.EnumerateFiles(DefaultPath, "*.docx", SearchOption.TopDirectoryOnly);
            }
            catch
            {
                yield break;
            }
            foreach (var file in files.Where(file => !file.StartsWith("~")))
                yield return Path.GetFileName(file);
        }

        private static string DefaultPath
        {
            get { return AppDomain.CurrentDomain.BaseDirectory + "Templates\\"; }
        }

        public static string GetFullTemplateName(string template)
        {
            return DefaultPath + template;
        }
    }
}
