using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Ia.Islamic.Cl.Model.Ui
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Koran Reference Network Class Library support functions: UI model
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
    public class VerseTopicUi
    {
        public string Id { get; set; }
        public int Number { get; set; }
        public int ChapterNumber { get; set; }
        public int? TopicId { get; set; }
        public int? NumberOfVerses { get; set; }

        public string Content { get; set; }

        public string ChapterVerseNumber
        {
            get
            {
                return ChapterNumber + ":" + Number;
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        public static List<VerseTopicUi> ReturnVerseTopicListUsingLanguageSymbolAndChapter(string languageSymbol, int chapterNumber, out int op, out string result)
        {
            List<VerseTopicUi> verseTopicList;

            op = 0;
            result = "";
            verseTopicList = null;

            try
            {
                using (var db = new Ia.Islamic.Cl.Model.Context.Koran())
                {
                    var query = from q in db.VerseTopics
                                //join q2 in db.Verses on q.VerseNumber equals q2.Number
                                where /*q2.Chapter.Koran.Language.Symbol == languageSymbol &&*/ q.ChapterNumber == chapterNumber
                                select new VerseTopicUi
                                {
                                    Id = q.Id,
                                    Number = q.VerseNumber,
                                    ChapterNumber = q.ChapterNumber,
                                    NumberOfVerses = q.NumberOfVerses,
                                    TopicId = q.TopicId,
                                    Content = "" //q2.Content
                                };

                    verseTopicList = (query).OrderBy(p => p.Id).ToList();

                    // below: a single content
                    VerseTopicUi tempVerseTopicUI;

                    foreach (VerseTopicUi vt in verseTopicList)
                    {
                        tempVerseTopicUI = ReturnVerseTopicUsingLanguageAndChapterNumberAndVerseNumberAndNumberOfVerses(new Ia.Cl.Model.Language("ar"), chapterNumber, vt.Number, vt.NumberOfVerses, out op, out result);

                        if (tempVerseTopicUI.Content != null) vt.Content = tempVerseTopicUI.Content;
                    }

                    if (verseTopicList.Count() > 0) op = 1;
                }
            }
            catch (Exception ex)
            {
                op = -1;
                result = "Exception: " + ex.Message + ". ";
                verseTopicList = null;
            }

            return verseTopicList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        public static VerseTopicUi ReturnVerseTopicUsingLanguageAndChapterNumberAndVerseNumberAndNumberOfVerses(Ia.Cl.Model.Language language, int chapterNumber, int verseNumber, int? numberOfVerses, out int op, out string result)
        {
            VerseTopicUi verse;
            List<VerseTopicUi> verseList;

            op = 0;
            result = "";
            verse = new VerseTopicUi();

            try
            {
                using (var db = new Ia.Islamic.Cl.Model.Context.Koran())
                {
                    verseList = (from q in db.Verses
                                 where q.Chapter.Koran.LanguageIso == language.Id
                                 && q.Chapter.Number == chapterNumber
                                 && q.Number >= verseNumber
                                 && q.Number <= verseNumber + (numberOfVerses - 1)
                                 select new VerseTopicUi
                                 {
                                     Id = q.Id,
                                     Number = q.Number,
                                     ChapterNumber = q.Chapter.Number,
                                     Content = q.Content
                                 }).OrderBy(p => p.Id).ToList();

                    if (verseList.Count() > 0)
                    {
                        verse.Number = verseList[0].Number;
                        verse.ChapterNumber = verseList[0].ChapterNumber;
                        verse.NumberOfVerses = numberOfVerses;

                        foreach (VerseTopicUi v in verseList)
                        {
                            verse.Content += v.Content + " ";
                        }

                        op = 1;
                    }
                    else
                    {
                        //op = -1;
                        //result = "Error: ReturnTopicVerseUsingLanguageAndChapterNumberAndVerseNumberAndNumberOfVerses()";
                    }
                }
            }
            catch (Exception ex)
            {
                op = -1;
                result = "Exception: " + ex.Message + ". ";
                verse = null;
            }

            return verse;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}
