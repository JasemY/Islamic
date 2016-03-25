using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Ia.Islamic.Cl.Model.Context
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="true">
    /// Koran Reference Network Data Context
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
    public class Koran : DbContext
    {
        /// <summary/>
        public Koran() : base("DefaultConnection") { }

        /// <summary/>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Koran, Ia.Islamic.Cl.Migrations.Configuration>());
        }

        /// <summary/>
        public DbSet<Ia.Islamic.Cl.Model.Koran> Korans { get; set; }
        /// <summary/>
        public DbSet<Chapter> Chapters { get; set; }
        /// <summary/>
        public DbSet<Verse> Verses { get; set; }
        /// <summary/>
        public DbSet<Word> Words { get; set; }

        /// <summary/>
        public DbSet<VerseTopic> VerseTopics { get; set; }
    }
}
