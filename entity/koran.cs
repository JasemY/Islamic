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
    public class Koran
    {
        /// <summary/>
        public Koran() 
        {
            Chapters = new List<Ia.Islamic.Cl.Model.Chapter>();
        }

        /// <summary/>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        /// <summary/>
        public string Name { get; set; }

        /// <summary/>
        public string LanguageIso { get; set; }

        /// <summary/>
        public string Introduction { get; set; }

        /// <summary/>
        public virtual ICollection<Chapter> Chapters { get; set; }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Create(Koran koran)
        {
            bool b;

            b = false;

            using (var db = new Ia.Islamic.Cl.Model.Context.Koran())
            {
                db.Korans.Add(koran);
                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static Koran Read(string id)
        {
            Koran koran;

            using (var db = new Ia.Islamic.Cl.Model.Context.Koran())
            {
                koran = (from q in db.Korans where q.Id == id select q).SingleOrDefault();
            }

            return koran;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static List<Koran> ReadList()
        {
            List<Koran> list;

            using (var db = new Ia.Islamic.Cl.Model.Context.Koran())
            {
                list = (from q in db.Korans select q).ToList();
            }

            return list;
        }


        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Update(Koran updatedKoran)
        {
            bool b, isUpdated;
            Ia.Islamic.Cl.Model.Koran koran;

            b = false;

            using (var db = new Ia.Islamic.Cl.Model.Context.Koran())
            {
                koran = (from q in db.Korans where q.Id == updatedKoran.Id select q).SingleOrDefault();

                if (koran == null)
                {
                    db.Korans.Add(updatedKoran);
                }
                else
                {
                    isUpdated = koran.IsUpdated(updatedKoran);

                    if (isUpdated)
                    {
                        db.Korans.Attach(koran);

                        db.Entry(koran).State = System.Data.Entity.EntityState.Modified;
                    }
                }

                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        private bool IsUpdated(Koran updatedKoran)
        {
            bool updated;

            updated = false;

            //if (this.Chapters != updatedKoran.Chapters) { this.Chapters = updatedKoran.Chapters; updated = true; }
            if (this.Introduction != updatedKoran.Introduction) { this.Introduction = updatedKoran.Introduction; updated = true; }
            if (this.LanguageIso != updatedKoran.LanguageIso) { this.LanguageIso = updatedKoran.LanguageIso; updated = true; }
            if (this.Name != updatedKoran.Name) { this.Name = updatedKoran.Name; updated = true; }

            return updated;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool Delete(Koran koran)
        {
            bool b;

            b = Delete(koran.Id);

            return b;
        }

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
                var v = (from q in db.Korans where q.Id == id select q).FirstOrDefault();

                db.Korans.Remove(v);
                db.SaveChanges();

                b = true;
            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }
}