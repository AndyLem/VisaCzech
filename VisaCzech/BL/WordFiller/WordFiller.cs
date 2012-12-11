using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using VisaCzech.Properties;
using VisaCzech.UI;
using System.ComponentModel;
using VisaCzech.BL.WordFiller.FillerStatus;

namespace VisaCzech.BL.WordFiller
{
    public class WordFiller
    {
        private static object _missingObj = Missing.Value;
// ReSharper disable UnusedMember.Local
        private static object _trueObj = true;
// ReSharper restore UnusedMember.Local
        private static object _falseObj = false;
        private const string EmptyBox = "□";
        private static ValidationFunctionFactory _validationFunctionFactory;
        private static IFillerStatusStrategy _fillerStatusStrategy;


        public static void FillTemplate(ICollection<Person> persons, WordFillerOptions options)
        {
            _fillerStatusStrategy = StrategyFactory.CreateStrategy(options);
            _fillerStatusStrategy.Init(persons, options);
            _fillerStatusStrategy.Worker.DoWork += (o, eventArgs) => FillTemplate(options.TemplateName, persons, options.SavePath);
            _fillerStatusStrategy.Run();
        }

        private static void FillTemplate(object templateFileName, ICollection<Person> anketas, string resultPath)
        {
            _Application app = null;
            InitValidationFunction();
            Directory.CreateDirectory(resultPath);

            try
            {
                app = new Microsoft.Office.Interop.Word.Application();

                var progress = 0;
                var progressStep = (int)(100/anketas.Count);
                _fillerStatusStrategy.Worker.ReportProgress(0, "Идет формирование анкет");
                foreach (var person in anketas)
                {
                    var newFileName = FillAnketa(app, templateFileName, person, resultPath);
                    progress += progressStep;
                    if (progress > 100) progress = 100;
                    _fillerStatusStrategy.Worker.ReportProgress(progress, string.Format("Анкета для {0} {1} сформирована в файле {2}", person.Surname, person.Name, newFileName));
                    if (_fillerStatusStrategy.ShouldStop) break;
                }
                _fillerStatusStrategy.Worker.ReportProgress(100, "Формирование анкет завершено");
            }
            catch (Exception e)
            {
                _fillerStatusStrategy.Worker.ReportProgress(100, e.Message);
                _fillerStatusStrategy.WasError = true;
            }
            finally
            {
                if (app != null) app.Quit(ref _falseObj);
            }
        }

        private static void InitValidationFunction()
        {
            _validationFunctionFactory = new ValidationFunctionFactory();
            _validationFunctionFactory.RegisterFunction("IsVisa1Checked", o =>
            {
                var person = (Person)o;
                return person.Visa1Enabled;
            });
            _validationFunctionFactory.RegisterFunction("IsVisa2Checked", o =>
            {
                var person = (Person)o;
                return person.Visa2Enabled;
            });
            _validationFunctionFactory.RegisterFunction("IsVisa3Checked", o =>
            {
                var person = (Person)o;
                return person.Visa3Enabled;
            });
            _validationFunctionFactory.RegisterFunction("CheckSurname2", o =>
            {
                var person = (Person)o;
                return person.Surname.ToUpper() != person.SurnameAtBirth.ToUpper();
            });
            _validationFunctionFactory.RegisterFunction("CheckCitizenship", o =>
            {
                var person = (Person)o;
                return person.Citizenship.ToUpper() != person.BirthCitizenship.ToUpper();
            });
        }

        private static string FillAnketa(_Application app, object templateFileName, Person anketa, string resultPath)
        {
            _Document doc = null;
            var newFileName = resultPath;
            if (!newFileName.EndsWith("\\"))
                newFileName += "\\";
            newFileName += anketa.Surname+" "+anketa.Name;
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
                        else if (oAttr is BoolAttribute)
                        {
                            CheckSomeBoxesFromBool(doc, info, oAttr as BoolAttribute, anketa);
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
            return newFileName;
        }

        private static void CheckSomeBoxes(_Document doc, FieldInfo info, EnumAttribute attr, Person anketa)
        {
            var templateStr = attr.TemplateString;
            var val = Convert.ToInt32(info.GetValue(anketa)) + 1;
            for (var i = 1; i <= attr.EnumValues; i++)
            {
                object strToFindObj = templateStr + i;
                ReplaceString(doc, i == val ? "X" : EmptyBox, strToFindObj);
            }
        }

        private static void CheckSomeBoxesFromBool(_Document doc, FieldInfo info, BoolAttribute attr, Person anketa)
        {
            var templateStr = attr.TemplateString;
            var val = Convert.ToBoolean(info.GetValue(anketa));
            for (var i = 0; i <= 1; i++)
            {
                object strToFindObj = templateStr + i;
                ReplaceString(doc, ((val && i == 1) || (!val && i == 0)) ? "X" : EmptyBox, strToFindObj);
            }
        }

        private static void FillField(_Document doc, 
            FieldInfo info, StringAttribute attr, Person anketa)
        {
            var cond = true;
            if (!string.IsNullOrEmpty(attr.ValidationFuncName))
            {
                if (_validationFunctionFactory != null)
                {
                    var func = _validationFunctionFactory[attr.ValidationFuncName];
                    cond = func(anketa);
                }
            }
                            
            object strToFindObj = attr.TemplateString;
            var replaceStrObj = cond ? (info.GetValue(anketa) ?? "") : attr.InvalidValue;
            replaceStrObj = replaceStrObj.ToString().ToUpper();
            ReplaceString(doc, replaceStrObj, strToFindObj);
        }

        private static void ReplaceString(_Document doc, object replaceStrObj, object strToFindObj)
        {
            object replaceTypeObj = WdReplace.wdReplaceAll;

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
