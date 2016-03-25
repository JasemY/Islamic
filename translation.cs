using System;
using System.Web;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Configuration;
using System.IO;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using System.Linq;
using System.Xml.Linq;
using System.Reflection;
using System.Globalization;

namespace Ia.Islamic.Cl.Model
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
    ////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public class Translation
    {
        private static XDocument xd;
        private XElement xe;

        /// <summary/>
        public string Id { get; set; }
        /// <summary/>
        public string MainName { get; set; }
        /// <summary/>
        public string MainHome { get; set; }
        /// <summary/>
        public string MainTitle { get; set; }
        /// <summary/>
        public string MainMeta { get; set; }
        /// <summary/>
        public string MainBismillah { get; set; }
        /// <summary/>
        public string IntroductionName { get; set; }

        /// <summary/>
        public string SearchTitle { get; set; }
        /// <summary/>
        public string SearchInstruction { get; set; }
        /// <summary/>
        public string SearchHintName { get; set; }
        /// <summary/>
        public string SearchHint { get; set; }
        /// <summary/>
        public string SearchButton { get; set; }
        /// <summary/>
        public string SearchCase { get; set; }
        /// <summary/>
        public string SearchBackwards { get; set; }

        /// <summary/>
        public string ResultEmptyField { get; set; }
        /// <summary/>
        public string ResultTitle { get; set; }
        /// <summary/>
        public string ResultSpace { get; set; }
        /// <summary/>
        public string ResultPeriod { get; set; }
        /// <summary/>
        public string ResultComma { get; set; }
        /// <summary/>
        public string ResultOnly { get; set; }
        /// <summary/>
        public string ResultSearchForWord { get; set; }
        /// <summary/>
        public string ResultSearchForWordEnd { get; set; }
        /// <summary/>
        public string ResultSearchForWords { get; set; }
        /// <summary/>
        public string ResultSearchForWordsEnd { get; set; }
        /// <summary/>
        public string ResultQuoteOn { get; set; }
        /// <summary/>
        public string ResultQuoteOff { get; set; }
        /// <summary/>
        public string ResultAnd { get; set; }
        /// <summary/>
        public string ResultSearchIs { get; set; }
        /// <summary/>
        public string ResultSensitiveForWord { get; set; }
        /// <summary/>
        public string ResultSensitiveForWords { get; set; }
        /// <summary/>
        public string ResultWildcards { get; set; }
        /// <summary/>
        public string ResultTheWord { get; set; }
        /// <summary/>
        public string ResultTheWords { get; set; }
        /// <summary/>
        public string ResultWasNotFound { get; set; }
        /// <summary/>
        public string ResultWereNotFound { get; set; }
        /// <summary/>
        public string ResultAppearsIn { get; set; }
        /// <summary/>
        public string ResultNumberOfAppearWords { get; set; }
        /// <summary/>
        public string ResultOneVerse { get; set; }
        /// <summary/>
        public string ResultManyVerses { get; set; }
        /// <summary/>
        public string ResultNotAllTogether { get; set; }
        /// <summary/>
        public string ResultAll { get; set; }
        /// <summary/>
        public string ResultStartingVerse { get; set; }
        /// <summary/>
        public string ResultStartingVerseEnd { get; set; }
        /// <summary/>
        public string ResultNext { get; set; }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        public Translation(string langaugeId)
        {
            xe = (from q in XDocument.Elements("language").Elements("lang") where q.Attribute("symbol").Value == langaugeId select q).First();

            this.MainName = xe.Element("main").Attribute("name").Value;
            this.MainHome = xe.Element("main").Element("home").Value;
            this.MainTitle = xe.Element("main").Element("title").Value;
            this.MainMeta = xe.Element("main").Element("meta").Value;
            this.MainBismillah = xe.Element("main").Element("bismillah").Value;
        
            this.IntroductionName = xe.Element("intro").Attribute("name").Value;

            this.SearchTitle = xe.Element("search").Element("main").Element("title").Value;
            this.SearchInstruction = xe.Element("search").Element("main").Element("instruction").Value;
            this.SearchHintName = xe.Element("search").Element("main").Element("hint").Attribute("name").Value;
            this.SearchHint = xe.Element("search").Element("main").Element("hint").Value;
            this.SearchButton = xe.Element("search").Element("main").Element("button").Value;
            this.SearchCase = xe.Element("search").Element("main").Element("case").Value;
            this.SearchBackwards = xe.Element("search").Element("main").Element("backwards").Value;

            this.ResultEmptyField = xe.Element("search").Element("result").Element("empty_field").Value;
            this.ResultTitle = xe.Element("search").Element("result").Element("title").Value;
            this.ResultSpace = xe.Element("search").Element("result").Element("space").Value;
            this.ResultPeriod = xe.Element("search").Element("result").Element("period").Value;
            this.ResultComma = xe.Element("search").Element("result").Element("comma").Value;

            this.ResultOnly = xe.Element("search").Element("result").Element("only").Value;
            this.ResultSearchForWord = xe.Element("search").Element("result").Element("search_for_word").Value;
            this.ResultSearchForWords = xe.Element("search").Element("result").Element("search_for_words").Value;
            this.ResultSearchForWordEnd = xe.Element("search").Element("result").Element("search_for_word_end").Value;
            this.ResultSearchForWordsEnd = xe.Element("search").Element("result").Element("search_for_words_end").Value;
            this.ResultQuoteOn = xe.Element("search").Element("result").Element("quote_on").Value;
            this.ResultQuoteOff = xe.Element("search").Element("result").Element("quote_off").Value;
            this.ResultAnd = xe.Element("search").Element("result").Element("and").Value;
            this.ResultSearchIs = xe.Element("search").Element("result").Element("search_is").Value;
            this.ResultSensitiveForWord = xe.Element("search").Element("result").Element("sensitive_for_word").Value;
            this.ResultSensitiveForWords = xe.Element("search").Element("result").Element("sensitive_for_words").Value;
            this.ResultWildcards = xe.Element("search").Element("result").Element("wildcards").Value;
            this.ResultTheWord = xe.Element("search").Element("result").Element("the_word").Value;

            this.ResultWasNotFound = xe.Element("search").Element("result").Element("was_not_found").Value;
            this.ResultTheWords = xe.Element("search").Element("result").Element("the_words").Value;
            this.ResultWereNotFound = xe.Element("search").Element("result").Element("were_not_found").Value;
            this.ResultAppearsIn = xe.Element("search").Element("result").Element("appears_in").Value;
            this.ResultNumberOfAppearWords = xe.Element("search").Element("result").Element("number_of_appear_words").Value;
            this.ResultOneVerse = xe.Element("search").Element("result").Element("one_verse").Value;

            this.ResultManyVerses = xe.Element("search").Element("result").Element("many_verses").Value;
            this.ResultNotAllTogether = xe.Element("search").Element("result").Element("not_all_together").Value;
            this.ResultAll = xe.Element("search").Element("result").Element("all").Value;
            this.ResultStartingVerse = xe.Element("search").Element("result").Element("starting_verse").Value;
            this.ResultStartingVerseEnd = xe.Element("search").Element("result").Element("starting_verse_end").Value;
            this.ResultNext = xe.Element("search").Element("result").Element("next").Value;
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
         
        private XDocument XDocument
        {
            get
            {
                Assembly _assembly;
                StreamReader streamReader;

                xd = null;
                _assembly = Assembly.GetExecutingAssembly();
                streamReader = new StreamReader(_assembly.GetManifestResourceStream("Ia.Islamic.Cl.model.translation.xml"));

                try
                {
                    if (streamReader.Peek() != -1)
                    {
                        xd = System.Xml.Linq.XDocument.Load(streamReader);
                    }
                }
                catch (Exception)
                {
                }
                finally
                {
                }

                return xd;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }
}
