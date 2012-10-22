using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using VisaCzech.Properties;

namespace VisaCzech.BL.WordFiller
{
    public class WordFiller
    {
        private static object _missingObj = Missing.Value;
        private static object _trueObj = true;
        private static object _falseObj = false;
        private static string _emptyBox = "□";

        public static void FillTemplate(object templateFileName, List<Person> anketas, string resultPath)
        {
            Microsoft.Office.Interop.Word._Application app = null;
            
            try
            {
                app = new Microsoft.Office.Interop.Word.Application();
                
                foreach (var person in anketas)
                    FillAnketa(app, templateFileName, person, resultPath);
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("{0}{1}", Resources.WordFiller_FillError, e.Message));
            }
            finally
            {
                if (app != null) app.Quit(ref _falseObj);
            }
            MessageBox.Show(Resources.WordFiller_FillComplete);
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
                doc = app.Documents.Add(ref templateFileName, ref _missingObj, ref _missingObj, ref _missingObj);
                var fieldInfos = anketa.GetType().GetFields();

                foreach (var info in fieldInfos)
                {
                    var custAttibs = info.GetCustomAttributes(true);
                    foreach (var oAttr in custAttibs)
                    {
                        if (oAttr is StringAttribute)
                        {
                            FillField(doc, info, oAttr as StringAttribute, anketa);
                        }
                        else if (oAttr is EnumAttribute)
                        {
                            CheckSomeBoxes(doc, info, oAttr as EnumAttribute, anketa);
                        }
                    }
                }
                object newFn = newFileName;
                doc.SaveAs(ref newFn, ref _missingObj, ref _missingObj, ref _missingObj, ref _missingObj, ref _missingObj, ref _missingObj, ref _missingObj, ref _missingObj, ref _missingObj, ref _missingObj, ref _missingObj, ref _missingObj, ref _missingObj, ref _missingObj, ref _missingObj);
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Ошибка '{0}' при заполнении анкеты '{1}' для '{2} {3}'", e.Message + e.StackTrace, templateFileName, anketa.Surname, anketa.Name));
            }
            finally
            {
                if (doc != null) doc.Close(ref _falseObj);
            }
            
        }

        private static void CheckSomeBoxes(_Document doc, FieldInfo info, EnumAttribute attr, Person anketa)
        {
            var templateStr = attr.TemplateString;
            var val = Convert.ToInt32(info.GetValue(anketa)) + 1;
            for (var i = 1; i <= attr.EnumValues; i++)
            {
                object strToFindObj = templateStr + i;
                ReplaceString(doc, i == val ? "X" : _emptyBox, strToFindObj);
            }
        }

        private static void FillField(_Document doc, 
            FieldInfo info, StringAttribute attr, Person anketa)
        {
            object strToFindObj = attr.TemplateString;
            var replaceStrObj = info.GetValue(anketa) ?? "";
            replaceStrObj = replaceStrObj.ToString().ToUpper();
            ReplaceString(doc, replaceStrObj, strToFindObj);
        }

        private static void ReplaceString(_Document doc, object replaceStrObj, object strToFindObj)
        {
            object replaceTypeObj = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;

            for (var i = 1; i <= doc.Sections.Count; i++)
            {
                var wordRange = doc.Sections[i].Range;

                var wordFindObj = wordRange.Find;
                var wordFindParameters = new[]
                                             {
                                                 strToFindObj, _missingObj, _missingObj, _missingObj, _missingObj,
                                                 _missingObj,
                                                 _missingObj, _missingObj, _missingObj, replaceStrObj, replaceTypeObj,
                                                 _missingObj, _missingObj, _missingObj, _missingObj
                                             };

                wordFindObj.GetType().InvokeMember("Execute", BindingFlags.InvokeMethod, null, wordFindObj,
                                                   wordFindParameters);
            }
        }
    }
}
