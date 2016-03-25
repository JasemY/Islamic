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
    public class Verse
    {
        /// <summary/>
        public Verse()
        {
            this.Words = new List<Word>();
        }

        /// <summary/>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        /// <summary/>
        public int Number { get; set; }

        /// <summary/>
        public string Content { get; set; }

        /// <summary/>
        public virtual Chapter Chapter { get; set; }

        /// <summary/>
        public virtual ICollection<Word> Words { get; set; }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Delete(string id)
        {
            bool b;

            b = false;

            using (var db = new Ia.Islamic.Cl.Model.Context.Koran())
            {
                var v = (from q in db.Verses where q.Id == id select q).FirstOrDefault();

                db.Verses.Remove(v);
                db.SaveChanges();

                b = true;
            }

            return b;
        }
    }

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
}