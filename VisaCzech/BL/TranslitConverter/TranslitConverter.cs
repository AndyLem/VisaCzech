﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Word;

namespace VisaCzech.BL.TranslitConverter
{
    public enum TransliterationType
    {
        Gost,
        ISO
    }
    public static class TranslitConverter
    {
        private static readonly Dictionary<string, string> Gost = new Dictionary<string, string>(); //ГОСТ 16876-71
        private static readonly Dictionary<string, string> Iso = new Dictionary<string, string>(); //ISO 9-95

        /// <summary>
        /// Перевод русского в транслит
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Front(string text)
        {
            return Front(text, TransliterationType.ISO);
        }

        /// <summary>
        /// Перевод русского в транслит
        /// </summary>
        /// <param name="text"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string Front(string text, TransliterationType type)
        {
            var output = text;
            var tdict = GetDictionaryByType(type);

            return tdict.Aggregate(output, (current, key) => current.Replace(key.Key, key.Value));
        }

        /// <summary>
        /// Перевод транслита в русский
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Back(string text)
        {
            return Back(text, TransliterationType.ISO);
        }

        /// <summary>
        /// Перевод транслита в русский
        /// </summary>
        /// <param name="text"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string Back(string text, TransliterationType type)
        {
            var output = text;
            var tdict = GetDictionaryByType(type);

            return tdict.Aggregate(output, (current, key) => current.Replace(key.Value, key.Key));
        }

        private static Dictionary<string, string> GetDictionaryByType(TransliterationType type)
        {
            return (type == TransliterationType.Gost) ? Gost : Iso;
        }

        static TranslitConverter()
        {
            Gost.Add("Є", "Eh");
            Gost.Add("І", "I");
            Gost.Add("і", "i");
            Gost.Add("№", "#");
            Gost.Add("є", "eh");
            Gost.Add("А", "A");
            Gost.Add("Б", "B");
            Gost.Add("В", "V");
            Gost.Add("Г", "G");
            Gost.Add("Д", "D");
            Gost.Add("Е", "E");
            Gost.Add("Ё", "Jo");
            Gost.Add("Ж", "Zh");
            Gost.Add("З", "Z");
            Gost.Add("И", "I");
            Gost.Add("Й", "Jj");
            Gost.Add("К", "K");
            Gost.Add("Л", "L");
            Gost.Add("М", "M");
            Gost.Add("Н", "N");
            Gost.Add("О", "O");
            Gost.Add("П", "P");
            Gost.Add("Р", "R");
            Gost.Add("С", "S");
            Gost.Add("Т", "T");
            Gost.Add("У", "U");
            Gost.Add("Ф", "F");
            Gost.Add("Х", "Kh");
            Gost.Add("Ц", "C");
            Gost.Add("Ч", "CH");
            Gost.Add("Ш", "Sh");
            Gost.Add("Щ", "Shh");
            Gost.Add("Ъ", "'");
            Gost.Add("Ы", "Y");
            Gost.Add("Ь", "");
            Gost.Add("Э", "Eh");
            Gost.Add("Ю", "Yu");
            Gost.Add("Я", "Ya");
            Gost.Add("а", "a");
            Gost.Add("б", "b");
            Gost.Add("в", "v");
            Gost.Add("г", "g");
            Gost.Add("д", "d");
            Gost.Add("е", "e");
            Gost.Add("ё", "jo");
            Gost.Add("ж", "zh");
            Gost.Add("з", "z");
            Gost.Add("и", "i");
            Gost.Add("й", "jj");
            Gost.Add("к", "k");
            Gost.Add("л", "l");
            Gost.Add("м", "m");
            Gost.Add("н", "n");
            Gost.Add("о", "o");
            Gost.Add("п", "p");
            Gost.Add("р", "r");
            Gost.Add("с", "s");
            Gost.Add("т", "t");
            Gost.Add("у", "u");
            Gost.Add("ф", "f");
            Gost.Add("х", "kh");
            Gost.Add("ц", "c");
            Gost.Add("ч", "ch");
            Gost.Add("ш", "sh");
            Gost.Add("щ", "shh");
            Gost.Add("ъ", "");
            Gost.Add("ы", "y");
            Gost.Add("ь", "");
            Gost.Add("э", "eh");
            Gost.Add("ю", "yu");
            Gost.Add("я", "ya");
            Gost.Add("«", "");
            Gost.Add("»", "");
            Gost.Add("—", "-");

            Iso.Add("Є", "Ye");
            Iso.Add("І", "I");
            Iso.Add("Ѓ", "G");
            Iso.Add("і", "i");
            Iso.Add("№", "#");
            Iso.Add("є", "ye");
            Iso.Add("ѓ", "g");
            Iso.Add("А", "A");
            Iso.Add("Б", "B");
            Iso.Add("В", "V");
            Iso.Add("Г", "G");
            Iso.Add("Д", "D");
            Iso.Add("Е", "E");
            Iso.Add("Ё", "Yo");
            Iso.Add("Ж", "Zh");
            Iso.Add("З", "Z");
            Iso.Add("И", "I");
            Iso.Add("Й", "J");
            Iso.Add("К", "K");
            Iso.Add("Л", "L");
            Iso.Add("М", "M");
            Iso.Add("Н", "N");
            Iso.Add("О", "O");
            Iso.Add("П", "P");
            Iso.Add("Р", "R");
            Iso.Add("С", "S");
            Iso.Add("Т", "T");
            Iso.Add("У", "U");
            Iso.Add("Ф", "F");
            Iso.Add("Х", "X");
            Iso.Add("Ц", "C");
            Iso.Add("Ч", "Ch");
            Iso.Add("Ш", "Sh");
            Iso.Add("Щ", "Shh");
            Iso.Add("Ъ", "'");
            Iso.Add("Ы", "Y");
            Iso.Add("Ь", "");
            Iso.Add("Э", "E");
            Iso.Add("Ю", "Yu");
            Iso.Add("Я", "Ya");
            Iso.Add("а", "a");
            Iso.Add("б", "b");
            Iso.Add("в", "v");
            Iso.Add("г", "g");
            Iso.Add("д", "d");
            Iso.Add("е", "e");
            Iso.Add("ё", "yo");
            Iso.Add("ж", "zh");
            Iso.Add("з", "z");
            Iso.Add("и", "i");
            Iso.Add("й", "j");
            Iso.Add("к", "k");
            Iso.Add("л", "l");
            Iso.Add("м", "m");
            Iso.Add("н", "n");
            Iso.Add("о", "o");
            Iso.Add("п", "p");
            Iso.Add("р", "r");
            Iso.Add("с", "s");
            Iso.Add("т", "t");
            Iso.Add("у", "u");
            Iso.Add("ф", "f");
            Iso.Add("х", "x");
            Iso.Add("ц", "c");
            Iso.Add("ч", "ch");
            Iso.Add("ш", "sh");
            Iso.Add("щ", "shh");
            Iso.Add("ъ", "");
            Iso.Add("ы", "y");
            Iso.Add("ь", "");
            Iso.Add("э", "e");
            Iso.Add("ю", "yu");
            Iso.Add("я", "ya");
            Iso.Add("«", "");
            Iso.Add("»", "");
            Iso.Add("—", "-");
        }
    }
}
