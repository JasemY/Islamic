using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using System.Reflection;

namespace Ia.Islamic.Cl.Model.Data
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Koran Reference Network Class Library support functions: Data model
    /// </summary>
    /// <value>
    /// https://msdn.microsoft.com/en-us/library/z1hkazw7(v=vs.100).aspx
    /// </value>
    /// <remarks> 
    /// Copyright © 2001-2015 Jasem Y. Al-Shamlan (info@ia.com.kw), Internet Applications - Kuwait. All Rights Reserved.
    ///
    /// This library is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by
    /// the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
    ///
    /// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
    /// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
    /// 
    /// You should have received a copy of the GNU General Public License along with this library. If not, see http://www.gnu.org/licenses.
    /// 
    /// Copyright notice: This notice may not be removed or altered from any source distribution.
    /// </remarks> 
    public class Default
    {
        private static XDocument faithXDocument;
        private static XDocument kashAlShubuhatFiAlTawheed;

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Default() { }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static XDocument KashAlShubuhatFiAlTawheed
        {
            get 
            {
                kashAlShubuhatFiAlTawheed = Ia.Cl.Model.Xml.Load(@"app_data\\tawheed.xml");

                return kashAlShubuhatFiAlTawheed; 
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static string[] ListOfUsedKoranLanguageSymbols
        {
            get
            {
                string[] p = { "en", "es", "fr", "de", "nl", "ja", "ko", "ar", "zh", "ru" };

                return p;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        public static Hashtable FaithHashtable()
        {
            int id;
            string name;

            Hashtable ht;

            ht = new Hashtable();

            foreach (XElement xe in FaithXDocument.Elements("faithTopic").Elements("topic"))
            {
                id = int.Parse(xe.Attribute("id").Value);
                name = xe.Attribute("name").Value;

                ht[id] = name;
            }

            return ht;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        public static void CreateVerseTopic(VerseTopic verseTopic, string verseId, out int op, out string result)
        {
            op = 0;
            result = "";

            using (var db = new Ia.Islamic.Cl.Model.Context.Koran())
            {
                if (db.VerseTopics.Find(verseTopic.Id) == null)
                {
                    db.VerseTopics.Add(verseTopic);

                    op = 1;
                    result = "Success: record added. ";

                    db.SaveChanges();
                }
                else
                {
                    op = -1;
                    result = "Error: record aready exists. ";
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        public static List<Chapter> ReturnChapterListUsingLanguageSymbol(string languageSymbol, out int op, out string result)
        {
            List<Chapter> chapterList;

            op = 0;
            result = "";
            chapterList = null;

            try
            {
                using (var db = new Ia.Islamic.Cl.Model.Context.Koran())
                {
                    chapterList = (from q in db.Chapters
                                   where q.Koran.LanguageIso == languageSymbol
                                   select q).OrderBy(p => p.Id).ToList();

                    if (chapterList.Count() > 0)
                    {
                        op = 1;
                    }
                    else
                    {
                        op = -1;
                        result = "Error: ReturnChapterListUsingLanguage()";
                    }
                }
            }
            catch (Exception ex)
            {
                op = -1;
                result = "Exception: " + ex.Message + ". ";
                chapterList = null;
            }

            return chapterList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        public static List<Ia.Islamic.Cl.Model.Ui.VerseUi> ReturnVerseListUsingLanguageSymbolAndChapter(string languageSymbol, int chapterNumber, out int op, out string result)
        {
            List<Ia.Islamic.Cl.Model.Ui.VerseUi> verseList;

            op = 0;
            result = "";
            verseList = null;

            try
            {
                using (var db = new Ia.Islamic.Cl.Model.Context.Koran())
                {
                    verseList = (from q in db.Verses
                                 where q.Chapter.Koran.LanguageIso == languageSymbol && q.Chapter.Number == chapterNumber
                                 select new Ia.Islamic.Cl.Model.Ui.VerseUi
                                 {
                                     Id = q.Id,
                                     Number = q.Number,
                                     ChapterNumber = q.Chapter.Number,
                                     Content = q.Content
                                 }).OrderBy(p => p.Id).ToList();

                    if (verseList.Count() > 0)
                    {
                        op = 1;
                    }
                    else
                    {
                        op = -1;
                        result = "Error: ReturnVerseListUsingLanguageSymbolAndChapter()";
                    }
                }
            }
            catch (Exception ex)
            {
                op = -1;
                result = "Exception: " + ex.Message + ". ";
                verseList = null;
            }

            return verseList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// How to embed and access resources by using Visual C# http://support.microsoft.com/kb/319292/en-us
        /// 
        /// 1. Change the "Build Action" property of your XML file from "Content" to "Embedded Resource".
        /// 2. Add "using System.Reflection".
        /// 3. See sample below.
        /// 
        /// </summary>

        public static XDocument FaithXDocument
        {
            get
            {
                Assembly _assembly;
                StreamReader streamReader;

                faithXDocument = null;
                _assembly = Assembly.GetExecutingAssembly();
                streamReader = new StreamReader(_assembly.GetManifestResourceStream("Ia.Islamic.Cl.model.faith-topic.xml"));

                try
                {
                    if (streamReader.Peek() != -1)
                    {
                        faithXDocument = System.Xml.Linq.XDocument.Load(streamReader);
                    }
                }
                catch (Exception)
                {
                }
                finally
                {
                }

                return faithXDocument;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        public static List<Ia.Islamic.Cl.Model.Ui.VerseUi> ReturnVersesUsingWordVariantsAndLanguage(Ia.Cl.Model.Language language, Hashtable wordVariants, int maximumLengthOfVerseList, out int op, out string result)
        {
            Hashtable id_ht;
            List<Ia.Islamic.Cl.Model.Ui.VerseUi> verseList;

            var wordTest = new string[10];

            op = 0;
            result = "";

            id_ht = new Hashtable(2 * wordVariants.Count);

            if (wordVariants.Count > 0)
            {
                int i = 0;
                foreach (string s in wordVariants.Values) wordTest[i++] = s;

                // below: add variants
                foreach (string u in wordVariants.Keys) id_ht[Ia.Cl.Model.Default.ByteToHex(Encoding.UTF8.GetBytes(language.Id + u))] = 1;

                try
                {
                    using (var db = new Ia.Islamic.Cl.Model.Context.Koran())
                    {
                        verseList = (from q in db.Verses
                                     from word in db.Words
                                     where /*wordTest.Any(word.Content.Contains)*/  wordTest.Contains(word.Content) //if(stringArray.Any(stringToCheck.Contains))
                                     && word.Verses.Contains(q)
                                     && q.Chapter.Koran.LanguageIso == language.Id
                                     select new Ia.Islamic.Cl.Model.Ui.VerseUi
                                     {
                                         Id = q.Id,
                                         Number = q.Number,
                                         ChapterNumber = q.Chapter.Number,
                                         Content = q.Content
                                     }).OrderBy(p => p.Id).Take(maximumLengthOfVerseList).ToList();

                        if (verseList.Count() > 0)
                        {
                        }
                        else
                        {
                            op = -1;
                            result = "Error: ReturnVersesWithAllWordVariantsAndLanguage()";
                        }

                        return verseList.ToList();
                    }
                }
                catch (Exception ex)
                {
                    op = -1;
                    result = "Exception: " + ex.Message + ". ";
                }
            }
            else
            {
                op = -1;
                result = "No word variants. ";
            }

            return null;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        public static Ia.Islamic.Cl.Model.Ui.VerseUi ReturnSingleVerseUsingLanguageAndChapterNumberAndVerseNumber(Ia.Cl.Model.Language language, int chapterNumber, int verseNumber, out int op, out string result)
        {
            op = 0;
            result = "";
            Ia.Islamic.Cl.Model.Ui.VerseUi verse;

            try
            {
                using (var db = new Ia.Islamic.Cl.Model.Context.Koran())
                {
                    verse = (from q in db.Verses
                             where q.Chapter.Koran.LanguageIso == language.Id
                             && q.Chapter.Number == chapterNumber
                             && q.Number == verseNumber
                             select new Ia.Islamic.Cl.Model.Ui.VerseUi
                             {
                                 Id = q.Id,
                                 Number = q.Number,
                                 ChapterNumber = q.Chapter.Number,
                                 Content = q.Content
                             }).FirstOrDefault();

                    if (verse != null)
                    {
                    }
                    else
                    {
                        op = -1;
                        result = "Error: ReturnSingleVerseUsingLanguageAndChapterNumberAndVerseNumber()";
                    }

                    return verse;
                }
            }
            catch (Exception ex)
            {
                op = -1;
                result = "Exception: " + ex.Message + ". ";
            }

            return null;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }
}
