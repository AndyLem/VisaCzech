﻿using System;
using System.Collections.Generic;
using System.IO;

namespace VisaCzech.DL
{
    public static class TemplateStorage
    {
        public static IEnumerable<string> LoadTemplates()
        {
            IEnumerable<string> files;
            try
            {
                files = Directory.EnumerateFiles(DefaultPath, "*.dotx", SearchOption.TopDirectoryOnly);
            }
            catch
            {
                yield break;
            }
            foreach (var file in files)
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
