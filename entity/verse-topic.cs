using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ia.Islamic.Cl.Model
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Koran Reference Network Class Library support functions: Entity model
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
    public class VerseTopic
    {
        /// <summary/>
        public VerseTopic() { }

        /// <summary/>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        /// <summary/>
        public int TopicId { get; set; }

        /// <summary/>
        public int NumberOfVerses { get; set; }

        /// <summary/>
        public int VerseNumber { get; set; }

        /// <summary/>
        public int ChapterNumber { get; set; }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static VerseTopic Read(string id)
        {
            VerseTopic verseTopic;

            using (var db = new Ia.Islamic.Cl.Model.Context.Koran())
            {
                verseTopic = (from q in db.VerseTopics where q.Id == id select q).SingleOrDefault();
            }

            return verseTopic;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<VerseTopic> ReadList()
        {
            List<VerseTopic> verseTopicList;

            using (var db = new Ia.Islamic.Cl.Model.Context.Koran())
            {
                verseTopicList = (from q in db.VerseTopics select q).ToList();
            }

            return verseTopicList;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Delete(string id, out string result)
        {
            bool b;

            b = false;
            result = "";

            using (var db = new Ia.Islamic.Cl.Model.Context.Koran())
            {
                var x = (from q in db.VerseTopics where q.Id == id select q).FirstOrDefault();

                db.VerseTopics.Remove(x);
                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}