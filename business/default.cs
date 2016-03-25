using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Data;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Ia.Islamic.Cl.Model.Business
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Koran Reference Network Class Library support functions: Business model
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
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Default()
        {
        }

        /*
        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public DataTable Search(string line, Ia.Cl.Model.Language language, out int op, out string result)
        {
            string s, id, word, verse_li;
            string[] sp;
            DataTable dt;
            StringBuilder sb;
            Hashtable ht, word_ht, variant_ht, verse_ht, variant_verse_ht, word_verse_ht;
            MatchCollection mc;

            dt = null;

            if (line != "")
            {
                // below: replace all consecutive space characters with a single ' '
                line = Regex.Replace(line, @"\s+", " ");

                // below: spaces at the start and end of string
                line = Regex.Replace(line, @"^\s+", "");
                line = Regex.Replace(line, @"\s+$", "");

                // below: remove any possible consecutive * wildcards.
                //line = Regex.Replace(line, @"\*{2,}", @"\*");

                word_ht = new Hashtable(10);
                variant_ht = new Hashtable(word_ht.Count);
                variant_verse_ht = new Hashtable(word_ht.Count);
                word_verse_ht = new Hashtable(word_ht.Count);

                s = "[" + Ia.Cl.Model.Language.Word(language.Symbol) + "]+|[" + Ia.Cl.Model.Language.Ideograph(language.Symbol) + "]{1}";
                s = s.Replace("|[]{1}", "");

                mc = Regex.Matches(line, @"(" + s + ")", RegexOptions.Compiled);
                foreach (Match m in mc)
                {
                    word = m.Groups[1].Captures[0].Value;

                    // below: words regular
                    word_ht[word] = null;
                    variant_ht[word] = word;

                    // below: unaccented:
                    variant_ht[this.Unaccent(word)] = word;

                    // below: basic form: lower case, no accents
                    variant_ht[this.Basic(word)] = word;
                }

                word_ht.Remove(" ");
                word_ht.Remove("");

                variant_ht.Remove(" ");
                variant_ht.Remove("");

                // below: collect info from hashtable to sql

                if (word_ht.Count > 0)
                {
                    dt = data.ReturnAllWordVerseForLanguageAndVariants(language, variant_ht);

                    try
                    {
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                verse_li = "";

                                foreach (DataRow ra in dt.Rows)
                                {
                                    id = ra["id"].ToString();
                                    word = ra["word"].ToString();

                                    verse_li = ra["ia_verse_lid"].ToString();
                                    verse_li = Regex.Replace(verse_li, @"\s+$", "");

                                    if (verse_li.Length > 0)
                                    {
                                        sp = verse_li.Split(' ');

                                        if (sp.Length > 0)
                                        {
                                            // below: the Hashtable below is to make sure we do not copy similar positions into variant_verse_ht

                                            ht = new Hashtable(sp.Length);

                                            foreach (string t in sp)
                                            {
                                                if (t.Length > 0 && !ht.ContainsKey(t))
                                                {
                                                    if (variant_verse_ht[word] == null) variant_verse_ht[word] = t;
                                                    else variant_verse_ht[word] = variant_verse_ht[word] + " " + t;

                                                    ht[t] = 1;
                                                }
                                            }
                                        }
                                    }
                                }

                                variant_verse_ht.Remove(" ");
                                variant_verse_ht.Remove("");

                                // below: I will loop through original words, loop through their variants, collect all positions of
                                // variants, and the positions of words
                                foreach (string u in word_ht.Keys)
                                {
                                    foreach (string v in variant_ht.Keys)
                                    {
                                        word = variant_ht[v].ToString();

                                        // below: check if variant belongs to word
                                        if (u == word)
                                        {
                                            // below: add positions of variant to word
                                            if (variant_verse_ht[v] != null)
                                            {
                                                if (word_verse_ht[u] == null) word_verse_ht[u] = variant_verse_ht[v].ToString();
                                                else if (word_verse_ht[u].ToString().IndexOf(variant_verse_ht[v].ToString()) >= 0) { }
                                                else word_verse_ht[u] = word_verse_ht[u] + " " + variant_verse_ht[v].ToString();
                                            }
                                        }
                                    }
                                }

                                // below: remove duplicate positions from word_verse
                                foreach (string u in word_ht.Keys)
                                {
                                    sp = word_verse_ht[u].ToString().Split(' ');

                                    ht = new Hashtable(sp.Length);

                                    // below: collect unique positions
                                    foreach (string t in sp) if (t.Length > 0) ht[t] = 1;

                                    sb = new StringBuilder(ht.Count * 8);

                                    // below: build string and assign it again
                                    foreach (string v in ht.Keys) sb.Append(v + " ");

                                    if (sb.Length > 0)
                                    {
                                        s = sb.ToString();
                                        s = s.Remove(s.Length - 1, 1);
                                    }

                                    word_verse_ht[u] = s;
                                }

                                if (word_verse_ht.Count > 0)
                                {
                                    // below: collect all locations from all words in one hashtable. Increment the value for every time the location is entered
                                    // then delete all entries that do not have a value equal to the number of words

                                    verse_ht = new Hashtable(1000);

                                    foreach (string t in word_verse_ht.Keys)
                                    {
                                        verse_li = word_verse_ht[t].ToString();

                                        if (verse_li.Length > 0)
                                        {
                                            sp = verse_li.Split(' ');

                                            if (sp.Length > 0)
                                            {
                                                foreach (string u in sp)
                                                {
                                                    // below: this will increment the value of key every time it appears
                                                    if (verse_ht[u] == null) verse_ht[u] = 1;
                                                    else verse_ht[u] = (int)verse_ht[u] + 1;
                                                }
                                            }
                                        }
                                    }

                                    verse_ht.Remove(" ");
                                    verse_ht.Remove("");

                                    ht = new Hashtable(verse_ht.Count);

                                    // below: now I will remove all verse_ht values that are less than the number of words
                                    foreach (string t in verse_ht.Keys)
                                    {
                                        if ((int)verse_ht[t] == word_ht.Count) ht[t] = 1;

                                        if (ht.Count >= 1000) break; // TEMP break at max of results
                                    }

                                    ht.Remove(" ");
                                    ht.Remove("");

                                    if (ht.Count > 0)
                                    {
                                        dt = data.ReturnAllVersesForLanguageAndId(language, ht);

                                        try
                                        {
                                            if (dt != null)
                                            {
                                                if (dt.Rows.Count > 0)
                                                {
                                                    // below: result text
                                                    op = 1;

                                                    if (word_ht.Count == 1)
                                                    {
                                                        result = Text("search_for_word");
                                                        //result = (from u in xd.Descendants("search_for_word") select u.Value).Single();
                                                    }
                                                    else if (word_ht.Count > 1)
                                                    {
                                                        result = Text("search_for_words");
                                                    }

                                                    result = @": """ + line + @""" " + Text("number_of_appear_words") + " " + ht.Count;
                                                }
                                                else
                                                {
                                                    op = -1;
                                                    result = "Verse query returned no results";
                                                }
                                            }
                                            else
                                            {
                                                op = -1;
                                                result = "SQL verse query returned null DataTable";
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            op = -1;
                                            result = "Exception: " + ex.Message;
                                        }
                                    }
                                    else
                                    {
                                        op = -1;
                                        result = Text("the_words");
                                        result = @" """ + line + @""" ";
                                        result = Text("not_all_together");
                                    }
                                }
                                else
                                {
                                    op = -1;
                                    result = "Word verse Hashtable length zero";
                                }
                            }
                            else
                            {
                                op = -1;

                                if (word_ht.Count == 1)
                                {
                                    result = Text("the_word");
                                }
                                else if (word_ht.Count > 1)
                                {
                                    result = Text("the_words");
                                }

                                result = @" """ + line + @""" ";

                                if (word_ht.Count == 1)
                                {
                                    result = Text("was_not_found");
                                }
                                else if (word_ht.Count > 1)
                                {
                                    result = Text("were_not_found");
                                }
                            }
                        }
                        else
                        {
                            op = -1;
                            result = "SQL query returned null DataTable";
                        }
                    }
                    catch (Exception ex)
                    {
                        op = -1;
                        result = "Exception: " + ex.Message;
                    }
                }
                else
                {
                    op = -1;
                    result = "Could not parse words";
                }
            }
            else
            {
                op = -1;
                result = Text("empty_field");
            }

            return dt;
        }
         */

        /*
        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Return text according to language
        /// </summary>
        public string Text(string text)
        {
            /*
        language = "de";
        translation_xd = Ia.Cl.Model.Xml.ReturnXmlNode(@"app_data\translation.xml");
        xd = translation_xd.SelectSingleNode("language/lang[@symbol='" + language + "']/search/result"); 
             * /

            return "";
        }
         */

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////





        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public List<Ia.Islamic.Cl.Model.Ui.VerseUi> Search(string line, string language_, out int op, out string result)
        {
            const int maximumLengthOfVerseList = 100;
            string regularExpression, word, basicWord;
            Hashtable word_ht, variant_ht, variant_verse_ht, word_verse_ht;
            MatchCollection mc;
            Translation translation;
            List<Ia.Islamic.Cl.Model.Ui.VerseUi> verseList;
            Ia.Cl.Model.Language language;

            language = new Ia.Cl.Model.Language(language_);

            translation = new Translation(language.Id);

            verseList = null;

            if (line != "")
            {
                // below: replace all consecutive space characters with a single ' '
                line = Regex.Replace(line, @"\s+", " ");

                // below: spaces at the start and end of string
                line = line.Trim();

                // below: remove any possible consecutive * wildcards.
                //line = Regex.Replace(line, @"\*{2,}", @"\*");

                word_ht = new Hashtable(10);
                variant_ht = new Hashtable(word_ht.Count);
                variant_verse_ht = new Hashtable(word_ht.Count);
                word_verse_ht = new Hashtable(word_ht.Count);

                if (language.Id == "ja" || language.Id == "ko")
                {
                    regularExpression = Ia.Cl.Model.Language.BasicWordsRegularExpression(language.Symbol) + "|[" + Ia.Cl.Model.Language.Ideograph(language.Symbol) + "]{1}";
                    regularExpression = regularExpression.Replace("|[]{1}", "");
                }
                else
                {
                    regularExpression = Ia.Cl.Model.Language.BasicWordsRegularExpression(language.Symbol);
                }

                mc = Regex.Matches(Ia.Cl.Model.Language.BasicForm(line), @"(" + regularExpression + ")", RegexOptions.Compiled);

                foreach (Match m in mc)
                {
                    word = m.Groups[1].Captures[0].Value;

                    basicWord = Ia.Cl.Model.Language.BasicForm(word);

                    // below: words regular
                    word_ht[word] = null;
                    variant_ht[word] = word;

                    // below: unaccented:
                    //variant_ht[Ia.Cl.Model.Language.RemoveAccent(word)] = word;

                    // below: basic form: lower case, no accents
                    variant_ht[word] = basicWord;

                    if (language.Id == "ar")
                    {
                        ArrayList al;
                        al = Ia.Cl.Model.Language.ProduceSimilarArabicWords(word);

                        foreach (string u in al) variant_ht[u] = basicWord;
                    }
                }

                word_ht.Remove(" ");
                word_ht.Remove("");

                variant_ht.Remove(" ");
                variant_ht.Remove("");

                // below: collect info from hashtable to sql

                if (word_ht.Count > 0)
                {
                    verseList = Ia.Islamic.Cl.Model.Data.Default.ReturnVersesUsingWordVariantsAndLanguage(language, variant_ht, maximumLengthOfVerseList, out op, out result);

                    if (verseList != null)
                    {
                        if (verseList.Count() > 0)
                        {
                            //    verse_li = "";

                            //    foreach (Verse ra in verseList)
                            //    {
                            //        id = ra.Id.ToString();
                            //        word = ""; // ra["word"].ToString();

                            //        verse_li = ""; // ra["ia_verse_lid"].ToString();
                            //        verse_li = Regex.Replace(verse_li, @"\s+$", "");

                            //        if (verse_li.Length > 0)
                            //        {
                            //            sp = verse_li.Split(' ');

                            //            if (sp.Length > 0)
                            //            {
                            //                // below: the Hashtable below is to make sure we do not copy similar positions into variant_verse_ht

                            //                ht = new Hashtable(sp.Length);

                            //                foreach (string t in sp)
                            //                {
                            //                    if (t.Length > 0 && !ht.ContainsKey(t))
                            //                    {
                            //                        if (variant_verse_ht[word] == null) variant_verse_ht[word] = t;
                            //                        else variant_verse_ht[word] = variant_verse_ht[word] + " " + t;

                            //                        ht[t] = 1;
                            //                    }
                            //                }
                            //            }
                            //        }
                            //    }

                            //    variant_verse_ht.Remove(" ");
                            //    variant_verse_ht.Remove("");

                            //    // below: I will loop through original words, loop through their variants, collect all positions of
                            //    // variants, and the positions of words
                            //    foreach (string u in word_ht.Keys)
                            //    {
                            //        foreach (string v in variant_ht.Keys)
                            //        {
                            //            word = variant_ht[v].ToString();

                            //            // below: check if variant belongs to word
                            //            if (u == word)
                            //            {
                            //                // below: add positions of variant to word
                            //                if (variant_verse_ht[v] != null)
                            //                {
                            //                    if (word_verse_ht[u] == null) word_verse_ht[u] = variant_verse_ht[v].ToString();
                            //                    else if (word_verse_ht[u].ToString().IndexOf(variant_verse_ht[v].ToString()) >= 0) { }
                            //                    else word_verse_ht[u] = word_verse_ht[u] + " " + variant_verse_ht[v].ToString();
                            //                }
                            //            }
                            //        }
                            //    }

                            //    // below: remove duplicate positions from word_verse
                            //    foreach (string u in word_ht.Keys)
                            //    {
                            //        sp = word_verse_ht[u].ToString().Split(' ');

                            //        ht = new Hashtable(sp.Length);

                            //        // below: collect unique positions
                            //        foreach (string t in sp) if (t.Length > 0) ht[t] = 1;

                            //        sb = new StringBuilder(ht.Count * 8);

                            //        // below: build string and assign it again
                            //        foreach (string v in ht.Keys) sb.Append(v + " ");

                            //        if (sb.Length > 0)
                            //        {
                            //            s = sb.ToString();
                            //            s = s.Remove(s.Length - 1, 1);
                            //        }

                            //        word_verse_ht[u] = s;
                            //    }

                            //    if (word_verse_ht.Count > 0)
                            //    {
                            //        // below: collect all locations from all words in one hashtable. Increment the value for every time the location is entered
                            //        // then delete all entries that do not have a value equal to the number of words

                            //        verse_ht = new Hashtable(1000);

                            //        foreach (string t in word_verse_ht.Keys)
                            //        {
                            //            verse_li = word_verse_ht[t].ToString();

                            //            if (verse_li.Length > 0)
                            //            {
                            //                sp = verse_li.Split(' ');

                            //                if (sp.Length > 0)
                            //                {
                            //                    foreach (string u in sp)
                            //                    {
                            //                        // below: this will increment the value of key every time it appears
                            //                        if (verse_ht[u] == null) verse_ht[u] = 1;
                            //                        else verse_ht[u] = (int)verse_ht[u] + 1;
                            //                    }
                            //                }
                            //            }
                            //        }

                            //        verse_ht.Remove(" ");
                            //        verse_ht.Remove("");

                            //        ht = new Hashtable(verse_ht.Count);

                            //        // below: now I will remove all verse_ht values that are less than the number of words
                            //        foreach (string t in verse_ht.Keys)
                            //        {
                            //            if ((int)verse_ht[t] == word_ht.Count) ht[t] = 1;

                            //            if (ht.Count >= 1000) break; // TEMP break at max of results
                            //        }

                            //        ht.Remove(" ");
                            //        ht.Remove("");

                            //        if (ht.Count > 0)
                            //        {

                            if (word_ht.Count == 1)
                            {
                                result = translation.ResultSearchForWord + @": """ + line + @""". " + translation.ResultNumberOfAppearWords + " " + verseList.Count();
                            }
                            else if (word_ht.Count > 1)
                            {
                                result = translation.ResultSearchForWords + @": """ + line + @""". " + translation.ResultNumberOfAppearWords + " " + verseList.Count();
                            }

                            //        }
                            //        else
                            //        {
                            //            op = -1;
                            //            result = "";//data.TranslationForKey("the_words") + @" """ + line + @""" " + data.TranslationForKey("not_all_together");
                            //        }
                            //    }
                            //    else
                            //    {
                            //        op = -1;
                            //        result = "Word verse Hashtable length zero";
                            //    }

                            op = 1;
                        }
                        else
                        {
                            op = -1;

                            if (word_ht.Count == 1)
                            {
                                result = translation.ResultTheWord + @" """ + line + @""" " + translation.ResultWasNotFound;
                            }
                            else if (word_ht.Count > 1)
                            {
                                result = translation.ResultTheWords + @" """ + line + @""" " + translation.ResultWereNotFound;
                            }
                        }
                    }
                    else
                    {
                    }
                }
                else
                {
                    op = -1;
                    result = "Could not parse words";
                }
            }
            else
            {
                op = -1;
                result = translation.ResultEmptyField;
            }

            return verseList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Ia.Islamic.Cl.Model.Ui.VerseUi ReturnSingleVerseUsingLanguageAndChapterNumberAndVerseNumber(Ia.Cl.Model.Language language, int chapterNumber, int verseNumber, out int op, out string result)
        {
            return Ia.Islamic.Cl.Model.Data.Default.ReturnSingleVerseUsingLanguageAndChapterNumberAndVerseNumber(language, chapterNumber, verseNumber, out op, out result);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public Ia.Islamic.Cl.Model.Ui.VerseTopicUi ReturnVerseSpanUsingLanguageAndChapterNumberAndVerseNumberAndNumberOfVerses(Ia.Cl.Model.Language language, int chapterNumber, int verseNumber, int numberOfVerses, out int op, out string result)
        {
            return Ia.Islamic.Cl.Model.Ui.VerseTopicUi.ReturnVerseTopicUsingLanguageAndChapterNumberAndVerseNumberAndNumberOfVerses(language, chapterNumber, verseNumber, numberOfVerses, out op, out result);
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// <remarks>HttpApplication has both Request and Session variables</remarks>
        /// </summary>
        public static void GlobalSession_Start(HttpContext context)
        {
            string countryCode, languageCode;

            //System.Data.DataSet ds;
            //System.Data.DataRow dr;

            countryCode = "";
            languageCode = "";

            /*
            try
            {
                // below: find the Geoip information of this IP

                Ia.Cs.Sr.GeoipClient sr = new Ia.Cs.Sr.GeoipClient();

                ds = sr.Ip_Country(this.Context.Request.UserHostAddress);

                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count == 1)
                {
                    dr = ds.Tables[0].Rows[0];

    #if DEBUG
                    languageCode = "fa";
    #else
                    languageCode = Ia.Cs.Language.ReturnLanguageCodeUsedInCountryFromCountyCode(dr["country_code"].ToString());
    #endif
                }
                else
                {
                }
            }
            catch (Exception)
            {
            }
             */

            context.Session["countryCode"] = countryCode;
            context.Session["languageCode"] = languageCode;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }
}