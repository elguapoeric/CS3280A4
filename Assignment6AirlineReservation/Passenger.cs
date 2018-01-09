using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// Class used to hold the properties of a Passenger on a plane
    /// </summary>
    class Passenger
    {
        /// <summary>
        /// Passengers Id number
        /// </summary>
        private string passengerId;
        /// <summary>
        /// passengers first name
        /// </summary>
        private string first;
        /// <summary>
        /// passengers last name
        /// </summary>
        private string last;
        /// <summary>
        /// seat passenger is occupying
        /// </summary>
        private string seat;

        public Passenger(string PId, string first, string last, string seat)
        {
            this.PassengerId = PId;
            this.FirstName = first;
            this.LastName = last;
            this.Seat = seat;
        }
        public string FirstName
        {
            get { return first; }
            set { first = value; }
        }
        public string LastName
        {
            get { return last; }
            set { last = value; }
        }
        public string PassengerId
        {
            get { return passengerId; }
            set { passengerId = value; }
        }
        public string Seat
        {
            get { return seat; }
            set { seat = value; }
        }
        /// <summary>
        /// Overriden method to print name of passenger
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {   
            return FirstName + " " + LastName;
        }
    }
}
