using System;
using System.Web;
using System.Xml;
using System.IO;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ia.Islamic.Cl.Model.Ui
{
    ////////////////////////////////////////////////////////////////////////////

    /// <summary publish="false">
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
    public abstract class Default
    {
        private static string languageCode;

        ////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        ///
        /// </summary>
        public Default() 
        { 
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///
        /// </summary>
        public static void Theme(System.Web.UI.Page page, System.Web.Profile.ProfileBase profile)
        {
            // below: use mobile master pages if this browser is mobile
            if (HttpContext.Current.Request.Browser.IsMobileDevice)
            {
                page.MasterPageFile = page.MasterPageFile.Replace("/", "/m/");
                page.Theme = "m";
            }
            else
            {
                if (page.Session["languageCode"] != null)
                {
                    languageCode = page.Session["languageCode"].ToString();

                    if (languageCode != "ar" && languageCode != "fa") languageCode = "ar";
                }
                else languageCode = "ar";

                page.Theme = languageCode;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
    }
}
