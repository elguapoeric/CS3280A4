using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// Classes holds the flightId, flightNumber, and airCraft type
    /// </summary>
    class Flight
    {
        /// <summary>
        /// flightId
        /// </summary>
        private string flightId;
        /// <summary>
        /// Stores the flightNumber
        /// </summary>
        private string flightNumber;
        /// <summary>
        /// Stores aircraft type
        /// </summary>
        private string airCraftType;
        /// <summary>
        /// insantiating flight object
        /// </summary>
        /// <param name="fId"></param>
        /// <param name="fN"></param>
        /// <param name="aCT"></param>
        public Flight(string fId, string fN, string aCT)
        {
            this.flightId = fId;
            this.flightNumber = fN;
            this.airCraftType = aCT;
        }
        public string FlightId
        {
            get { return flightId; }
            set { flightId = value; }
        }
        public string FlightNumber
        {
            get { return flightNumber; }
            set { flightNumber = value; }
        }
        public string AirCraftType
        {
            get { return airCraftType; }
            set { airCraftType = value; }
        }
       
        /// <summary>
        /// Overrides the ToString method to later user in a comboBox.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return FlightNumber + " - " + AirCraftType;
        }
    }
}
