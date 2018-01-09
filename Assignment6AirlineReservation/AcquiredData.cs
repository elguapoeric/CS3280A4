using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Assignment6AirlineReservation
{
    class GetDBInfo
    {
        //public static List<Passenger> passengerList { get; set; }
        SQLQueries SQL = new SQLQueries();
        clsDataAccess accessDatabase = new clsDataAccess();
        

        /// <summary>
        /// Returns flightid first name, last name, seat of a flight
        /// </summary>
        /// <param name="flight">pass either 1 for airbus 2 for boeing for airbus</param>
        /// <returns>returns data set and out contains number of rows</returns>
        public DataSet getPassengersByPlane(string flightId, out int rows)
        {
            try
            {
                DataSet passengersDs;
                int planeRows = 0;
                passengersDs = this.accessDatabase.ExecuteSQLStatement(SQL.getPassengersByFlight(flightId), ref planeRows);
                rows = planeRows;
                return passengersDs;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }

        }
        /// <summary>
    ///Used to get the flight id, flight number, and Aircraft type out will contain the number of rows in the dataset
    /// </summary>
    /// <param name="rows"></param>
    /// <returns>Returns A dataset cotaining flighId flightNumber Aircraft type</returns>
        public DataSet getFlights(out int rows)
        {
            try
            {
                int numPlanes = 0;
                DataSet flights = this.accessDatabase.ExecuteSQLStatement(SQL.getFlight(), ref numPlanes);
                rows = numPlanes;
                return flights;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }
        /// <summary>
        /// Returns a List of flight objects Cotaining flightid firstname lastname
        /// </summary>
        /// <returns></returns>
        public List<Flight> flightInfoList()
        {
            try
            {
                List<Flight> flightList = new List<Flight>();
                int rows = 0;
                DataSet fl = new DataSet();
                fl = getFlights(out rows);
                for (int i = 0; i < rows; i++)
                {
                    flightList.Add(new Flight(fl.Tables[0].Rows[i][0].ToString(), fl.Tables[0].Rows[i][1].ToString(), fl.Tables[0].Rows[i][2].ToString()));
                }
                return flightList;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }
        /// <summary>
        /// This method will return a list of Passenger object which contain the flightid firstname lastname and seat number
        /// </summary>
        /// <param name="flightId">FlightId the flight you would like to get passengers</param>
        /// <returns></returns>
        public List<Passenger> passengersOnPlane(string flightId)
        {
            try
            {
                List<Passenger> passengerList = new List<Passenger>();
                int rows = 0;
                DataSet pOP = new DataSet();
                pOP = getPassengersByPlane(flightId, out rows);
                for (int i = 0; i < rows; i++)
                {
                    passengerList.Add(new Passenger(pOP.Tables[0].Rows[i][0].ToString(), pOP.Tables[0].Rows[i][1].ToString(), pOP.Tables[0].Rows[i][2].ToString(), pOP.Tables[0].Rows[i][3].ToString()));
                }
                return passengerList;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }
        /// <summary>
        /// Adds a passenger to the database 
        /// </summary>
        /// <param name="name">Passenger first name</param>
        /// <param name="last">Passenger last name</param>
        public void addPassengerToDB(string name, string last)
        {
            try
            {
                accessDatabase.ExecuteNonQuery(SQL.addPassenger(name, last));
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }
        /// <summary>
        /// Find the passengers Id based on first and last name
        /// </summary>
        /// <param name="first">Passenger First name</param>
        /// <param name="last">Passenger Last name</param>
        /// <returns>Passenger Id as string</returns>
        public string FindPassengersId(string first, string last)
        {
            try
            {
                string pId = accessDatabase.ExecuteScalarSQL(SQL.getPassengerId(first, last));
                return pId;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }
        /// <summary>
        /// Add link to database table flight id passenger id and seat
        /// </summary>
        /// <param name="fId">flight id</param>
        /// <param name="pId">passenger id</param>
        /// <param name="seat">seat number</param>
        public void addLink(string fId, string pId, string seat)
        {
            try
            {
                accessDatabase.ExecuteNonQuery(SQL.addLink(fId, pId, seat));
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }

        }
        /// <summary>
        /// Method will delete the link from the database and passenger id from passenger table
        /// </summary>
        /// <param name="flightId"></param>
        /// <param name="passengerId"></param>
        public void deletePassenger(string flightId, string passengerId)
        {
            try
            {
                accessDatabase.ExecuteNonQuery(SQL.deleteLink(flightId, passengerId));
                accessDatabase.ExecuteNonQuery(SQL.deletePassenger(passengerId));
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }
        /// <summary>
        /// Updates the seat number for a passenger
        /// </summary>
        /// <param name="seat"></param>
        /// <param name="flightId"></param>
        /// <param name="passengerId"></param>
        public void updateSeatNumber(string seat, string flightId, string passengerId)
        {
            try
            {
                accessDatabase.ExecuteNonQuery(SQL.updateSeatNumber(seat, flightId, passengerId));
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }

        /// <summary>
        /// Method used to show the path of the catch statements
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }



    }
}
