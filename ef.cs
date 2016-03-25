using System;
using System.Web;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Data;
//using System.Data.Linq.Mapping;
//using System.Data.Objects;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Data.Entity.Edm;

namespace Ia.Cl.Model
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary>
    ///
    /// </summary>
    public class Ef
    {
        ////////////////////////////////////////////////////////////////////////////

        /// <summary publish="true">
        /// Entity Framework support function
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
        public Ef() { }

        /* TO DO:
         * EF XML BACKUP/RESTORE OF DATABASE (setup procedure to backup and restore tables?)
         */

        /// <summary/>
        public enum F
        {
            /// <summary/>
            Unknown,

            /// <summary/>
            Not_Found_Now_Created,

            /// <summary/>
            Found_Pending_Ready,

            /// <summary/>
            Found_Pending_Past_Due,

            /// <summary/>
            Found_Ready
        };

        /*
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void BulkInsert<T>(string connection, string tableName, IList<T> list)
        {
            using (var bulkCopy = new SqlBulkCopy(connection))
            {
                Type t = typeof(T);
                bulkCopy.BatchSize = list.Count;
                bulkCopy.DestinationTableName = tableName;

                var table = new DataTable();
                var props = t.GetProperties().Where(propertyInfo => propertyInfo.PropertyType.Namespace.Equals("System")).ToArray();

                foreach (var propertyInfo in props)
                {
                    bulkCopy.ColumnMappings.Add(propertyInfo.Name, propertyInfo.Name);
                    table.Columns.Add(propertyInfo.Name, Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType);
                }

                var values = new object[props.Length];
                foreach (var item in list)
                {
                    for (var i = 0; i < values.Length; i++)
                    {
                        values[i] = props[i].GetValue(item);
                    }

                    table.Rows.Add(values);
                }

                bulkCopy.WriteToServer(table);
            }

            /*
            using (SqlConnection dbConnection = new SqlConnection("Data Source=ProductHost;Initial Catalog=yourDB;Integrated Security=SSPI;"))
            {
                dbConnection.Open();
                using (SqlBulkCopy s = new SqlBulkCopy(dbConnection))
                {
                    s.DestinationTableName = "Your table name";
                    foreach (var column in csvFileData.Columns)
                        s.ColumnMappings.Add(column.ToString(), column.ToString());
                    s.WriteToServer(csvFileData);
                }
            } 
             * /
        }
         */ 

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>

        public struct EfTableUpdateType
        {
            public const int DoNothing = 1;
            public const int InsertNewRecordsOnly = 2;
            public const int UpdateRecords = 3;
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static bool UpdateTable<T>(System.Data.Entity.DbSet<T> t, ref List<T> tList, int efTableUpdateType) where T : class
        {
            // below:
            // - Key must be defined for the T entity
            // - Any list must have unique key values

            bool b;
            ArrayList keyArrayList;
            List<T> list;

            b = true;

            PropertyInfo propInfo;
            object itemValue;

            if (efTableUpdateType == EfTableUpdateType.DoNothing)
            {
            }
            else if (efTableUpdateType == EfTableUpdateType.InsertNewRecordsOnly)
            {
                // below: we will read all keys into ArrayList

                keyArrayList = new ArrayList(tList.Count());

                list = (from q in t select q).ToList();

                foreach (T se in list)
                {
                    propInfo = se.GetType().GetProperty("IMPU");
                    itemValue = propInfo.GetValue(se, null);

                    keyArrayList.Add(itemValue);
                }

                foreach (T se in tList)
                {
                    propInfo = se.GetType().GetProperty("IMPU");
                    itemValue = propInfo.GetValue(se, null);

                    if (!keyArrayList.Contains(itemValue)) t.Add(se);
                }
            }
            else if (efTableUpdateType == EfTableUpdateType.UpdateRecords)
            {

            }

            return b;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }
}
