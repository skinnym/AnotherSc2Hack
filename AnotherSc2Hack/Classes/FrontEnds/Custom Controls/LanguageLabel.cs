﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using AnotherSc2Hack.Classes.BackEnds;

namespace AnotherSc2Hack.Classes.FrontEnds
{
    public class LanguageLabel : Label
    {
        private static readonly List<LanguageLabel> Instances = new List<LanguageLabel>();

        public LanguageLabel()
        {
            Instances.Add(this);
        }

        ~LanguageLabel()
        {
            var index = Instances.FindIndex(x => x.GetHashCode().Equals(GetHashCode()));
            Instances.RemoveAt(index);
        }


        public static void OutputPath()
        {
            foreach (var languageLabel in Instances)
            {
                Console.WriteLine(HelpFunctions.GetParent(languageLabel) + Constants.ChrLanguageSplitSign + " ");
            }
        }

        public String LanguageFile
        {
            get { return _languageFile; }
            set
            {
                _languageFile = value;
                ChangeLanguage();
            }
        }

        private string _languageFile = String.Empty;

        public void ChangeLanguage()
        {
            ReadLanguagefile();
        }



        private void ReadLanguagefile()
        {
            var tmpFileAvailable = File.Exists(_languageFile);

            if (!tmpFileAvailable)
                return;

            using (var sr = new StreamReader(_languageFile, System.Text.Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    var strLine = sr.ReadLine();

                    if (strLine == null ||
                            strLine.Length <= 0)
                        continue;

                    if (strLine.StartsWith(";"))
                        continue;

                    /* If it is contained there.. (PARTLY) */
                    if (strLine.Contains(Name))
                    {
                        /* Now the actual compare */
                        //var tmp = strLine.Replace(" ", "");
                        var tmp = strLine;

                        var mappedStuff = tmp.Split('=');

                        if (mappedStuff.Length > 1)
                        {
                            if (mappedStuff[0].Trim().Equals(Name))
                            {
                                Text = mappedStuff[1].TrimStart();
                            }
                        }
                    }
                }
            }
        }
    }
 
}
