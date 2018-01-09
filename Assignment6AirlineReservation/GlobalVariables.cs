using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// Used for passing first and last name to different windows
    /// </summary>
    class GlobalVariables
    {
        /// <summary>
        /// The new passengers first name
        /// </summary>
        private static string firstname = "";
        /// <summary>
        /// The new passengers last name
        /// </summary>
        private static string lastname = "";
        /// <summary>
        /// Stores weather user actually saved new passenger or hit cancel instead in newpassenger window
        /// </summary>
        private static bool pAddOn = false;

        public static string FirstName
        {
            get { return firstname; }
            set { firstname = value; }

        }

        public static string LastName
        {
            get { return lastname; }
            set { lastname = value; }
        }

        public static Boolean PAddOn
        {
            get { return pAddOn; }
            set { pAddOn = value; }
        }
    }
}
