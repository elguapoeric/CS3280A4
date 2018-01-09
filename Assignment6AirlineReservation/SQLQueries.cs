using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Assignment6AirlineReservation
{
    class SQLQueries
    {
        /// <summary>
        /// Gets the flights
        /// </summary>
        /// <returns></returns>
        public string getFlight()
        {
            string fail = null;
            try
            {
                string sSQL = "SELECT Flight_ID, Flight_Number, Aircraft_Type FROM FLIGHT";
                return sSQL;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return fail;
            }
        }
        /// <summary>
        /// get passenger by flight 
        /// </summary>
        /// <param name="flight">enter flight id as string</param>
        /// <returns></returns>
        public string getPassengersByFlight(string flight)
        {
            string fail = null;
            try
            {
                string sSQL = "SELECT PASSENGER.Passenger_ID, First_Name, Last_Name, Seat_Number " +
                                "FROM FLIGHT_PASSENGER_LINK, FLIGHT, PASSENGER " +
                                "WHERE FLIGHT.FLIGHT_ID = FLIGHT_PASSENGER_LINK.FLIGHT_ID AND " +
                                "FLIGHT_PASSENGER_LINK.PASSENGER_ID = PASSENGER.PASSENGER_ID AND " +
                                "FLIGHT.FLIGHT_ID = " + flight;
                return sSQL;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return fail;
            }
        }
     
        /// <summary>
        /// Updates seat number
        /// </summary>
        /// <returns></returns>
        public string updateSeatNumber(string seat, string fId, string pId)
        {
            string fail = null;
            try
            {
                string sSQL = "UPDATE FLIGHT_PASSENGER_LINK " +
                                "SET Seat_Number = '" + seat + "' " +
                                "WHERE FLIGHT_ID = " + fId.ToString() + " AND PASSENGER_ID = " + pId.ToString();
                return sSQL;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return fail;
            }

        }
        /// <summary>
        /// Inserting a Passenger
        /// </summary>
        /// <returns></returns>
        public string addPassenger(string firstname, string lastname)
        {
            string fail = null;
            try
            {
                string sSQL = "INSERT INTO PASSENGER(First_Name, Last_Name) VALUES('" + firstname + "','" + lastname + "')";
                return sSQL;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return fail;
            }
        }
        /// <summary>
        /// Inserts value into Link Table
        /// </summary>
        /// <returns></returns>
        public string addLink(string fId, string pId, string seat)
        {
            string fail = null;
            try
            {
                string sSQL = "INSERT INTO Flight_Passenger_Link(Flight_ID, Passenger_ID, Seat_Number) " +
                                "VALUES( " + fId + ", " + pId + ", " + seat + ")";
                return sSQL;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return fail;
            }
        }
        /// <summary>
        /// returns query that will delete the passenger from passenger table
        /// </summary>
        /// <param name="passengerId"></param>
        /// <returns></returns>
        public string deletePassenger(string passengerId)
        {
            string fail = null;
            try
            {
                string sSQL = "DELETE FROM PASSENGER " +
                              "WHERE PASSENGER_ID = " + passengerId;
                return sSQL;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return fail;
            }
        }
        /// <summary>
        /// Delete value in the Link Table
        /// </summary>
        /// <returns></returns>
        public string deleteLink(string fId, string pId)
        {
            string fail = null;
            try
            {
                string sSQL = "Delete FROM FLIGHT_PASSENGER_LINK " +
                                 "WHERE FLIGHT_ID = " + fId + " AND " +
                                    "PASSENGER_ID = " + pId;
                return sSQL;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return fail;
            }

        }
        /// <summary>
        /// Gets the passengers ID
        /// </summary>
        /// <returns></returns>
        public string getPassengerId(string name, string last)
        {
            string fail = null;
            try
            {
                string sSQL = "SELECT Passenger_ID from Passenger where First_Name = '" + name + "' AND Last_Name = '" + last + "'";
                return sSQL;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return fail;
            }
        }
    }
}
