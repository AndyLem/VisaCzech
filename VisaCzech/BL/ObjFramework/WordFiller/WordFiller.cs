using System;
using System.Collections.Generic;
using System.Reflection;

namespace VisaCzech.BL.ObjFramework.WordFiller
{
    public class WordFiller
    {
        private Microsoft.Office.Interop.Word._Application app;
        
        private static object missingObj = System.Reflection.Missing.Value;
        private static object trueObj = true;
        private static object falseObj = false;

        public static void FillTemplate(object templateFileName, List<Person> anketas, string resultPath)
        {
            Microsoft.Office.Interop.Word._Application app = null;
            
            try
            {
                app = new Microsoft.Office.Interop.Word.Application();
                
                foreach (var person in anketas)
                    FillAnketa(app, templateFileName, person, resultPath);
            }
            catch
            {
                    
            }
            finally
            {
                if (app != null) app.Quit(ref falseObj);
                app = null;
            }
        }

        private static void FillAnketa(Microsoft.Office.Interop.Word._Application app, object templateFileName, Person anketa, string resultPath)
        {
            Microsoft.Office.Interop.Word._Document doc = null;
            var newFileName = resultPath;
            if (!newFileName.EndsWith("\\"))
                newFileName += "\\";
            newFileName += anketa.Surname;
            try
            {
                doc = app.Documents.Add(ref templateFileName, ref missingObj, ref missingObj, ref missingObj);
                var fieldInfos = anketa.GetType().GetFields();

                foreach (var info in fieldInfos)
                {
                    var custAttibs = info.GetCustomAttributes(true);
                    foreach (var oAttr in custAttibs)
                    {
                        if (oAttr is StringAttribute)
                        {
                            ReplaceString(doc, info, oAttr as StringAttribute, anketa);
                        }
                        else if (oAttr is EnumAttribute)
                        {
                            
                        }
                    }
                }
                object newFn = newFileName;
                doc.SaveAs(ref newFn, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj);
            }
            finally
            {
                if (doc != null) doc.Close(ref falseObj);
                doc = null;
            }
            
        }

        private static void ReplaceString(Microsoft.Office.Interop.Word._Document doc, 
            System.Reflection.FieldInfo info, StringAttribute attr, Person anketa)
        {
            object strToFindObj = attr.TemplateString;
            var replaceStrObj = info.GetValue(anketa) ?? "";
            object replaceTypeObj = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;

            for (var i = 1; i <= doc.Sections.Count; i++)
            {
                var wordRange = doc.Sections[i].Range;

                var wordFindObj = wordRange.Find;
                var wordFindParameters = new object[15]
                                                  {
                                                      strToFindObj, missingObj, missingObj, missingObj, missingObj,
                                                      missingObj,
                                                      missingObj, missingObj, missingObj, replaceStrObj, replaceTypeObj,
                                                      missingObj, missingObj, missingObj, missingObj
                                                  };

                wordFindObj.GetType().InvokeMember("Execute", BindingFlags.InvokeMethod, null, wordFindObj,
                                                   wordFindParameters);

            }
        }
    }
}
