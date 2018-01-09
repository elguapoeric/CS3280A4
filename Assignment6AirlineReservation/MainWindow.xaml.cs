using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;


namespace Assignment6AirlineReservation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Window used to add passenger names to the database
        /// </summary>
        wndAddPassenger wndAddPass;
        /// <summary>
        /// Creates a connection to database
        /// </summary>
        GetDBInfo gDbI;
        /// <summary>
        /// Contains strings of SQL gueries
        /// </summary>
        SQLQueries SQL;
        /// <summary>
        /// Used to change the seat click event method
        /// </summary>
        bool changeSeatMode;

        /// <summary>
        /// insansiates all classes and adds objects to the both comboboxes
        /// </summary>
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                SQL = new SQLQueries();
                gDbI = new GetDBInfo();
                //gVars = new GlobalVariables();
                DataSet ds = new DataSet();
                changeSeatMode = false;


                //Adding an Actual object to the combobox to make it easier to access various properties
                List<Flight> flightList = gDbI.flightInfoList();

                foreach (Flight flt in flightList)
                {
                    cbChooseFlight.Items.Add(flt);
                }

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Will show the seating arrangment for A380 or 767 depending on what is selected and Load the name of the passengers in the dropbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbChooseFlight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                Flight flight = cbChooseFlight.SelectedItem as Flight;
                string flightId = flight.FlightId.ToString();

                cbChoosePassenger.IsEnabled = true;
                gPassengerCommands.IsEnabled = true;
                DataSet ds = new DataSet();


                if (flightId == "1")
                {
                    CanvasA380.Visibility = Visibility.Visible;
                    Canvas767.Visibility = Visibility.Hidden;

                }
                else
                {
                    Canvas767.Visibility = Visibility.Visible;
                    CanvasA380.Visibility = Visibility.Hidden;
                }

                cbChoosePassenger.Items.Clear();

                List<Passenger> pList = gDbI.passengersOnPlane(flightId);
                foreach (Passenger p in pList)
                {
                    cbChoosePassenger.Items.Add(p);
                }
                LoadSeats();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Will Color the seats depending on the availablity and what seat is flight is chosen
        /// </summary>
        private void LoadSeats()
        {
            //Had to switch the seating arrangements for the Boeing and A380 in framework because in the database boeing had a seat 18. 
            //A380 didnt have a seat 18 in the database so I'm guessing the seating arrangements were switched.  I also didn't want to have to use a flightId = 1 for boeing and
            // use flightId = 2 for A380  thoroughout the c# code when in the database flightId = 1 is A380 and flightId = 2 is boeing.  

            try
            {
                //Checking to see which plane is chosen
                Flight flight = cbChooseFlight.SelectedItem as Flight;
                string flightId = flight.FlightId;
                if (flightId == "2")
                {
                    //Creating a list of passenger object each passenger contains the id first last seat
                    List<Passenger> aList = gDbI.passengersOnPlane(flightId);

                    //Setting all labels to blue to prepare for filling in seats
                    foreach (Label lSeat in c767_Seats.Children)
                    {
                        lSeat.Background = Brushes.Blue;
                    }
                    //Taking each passenger class in the list and checking the seat number
                    foreach (Passenger passenger in aList)
                    {
                        foreach (Label lSeat in c767_Seats.Children)
                        {
                            if (lSeat.Content.ToString() == passenger.Seat)
                            {
                                lSeat.Background = Brushes.Red;
                            }

                        }
                    }
                }
                else
                {

                    List<Passenger> bList = gDbI.passengersOnPlane(flightId);

                    ///Going to first set all labels to blue except for the title
                    foreach (Label lSeat in cA380_Seats.Children)
                    {
                        lSeat.Background = Brushes.Blue;
                    }
                    foreach (Passenger passenger in bList)
                    {
                        foreach (Label lSeat in cA380_Seats.Children)
                        {

                            if (lSeat.Content.ToString() == passenger.Seat)
                            {
                                lSeat.Background = Brushes.Red;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Opens a new Window to add new Passenger
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAddPassenger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Creating the new window and showing it
                wndAddPass = new wndAddPassenger();
                wndAddPass.ShowDialog();
                //checking to see if the clicked cancel or save. If saved then dissable the panels
                if (GlobalVariables.PAddOn)
                {
                    cbChoosePassenger.IsEnabled = false;
                    gPassengerCommands.IsEnabled = false;
                    cbChooseFlight.IsEnabled = false;

                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
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
        /// <summary>
        /// Will turn seat green if used by s
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbChoosePassenger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string seat;
                //Need to check that a passenger is selected  
                if (cbChoosePassenger.SelectedItem != null)
                {
                    //Need to get the seat number for the passenger currently selected
                    Passenger passenger = cbChoosePassenger.SelectedItem as Passenger;
                    seat = passenger.Seat;
                    //Show the seat number in the text box
                    lblPassengersSeatNumber.Content = passenger.Seat;

                    //for boeing 
                    if (cbChooseFlight.SelectedItem.ToString() == "412 - Boeing 767")
                    {
                        // loop through all seats and change color based on passenger seat number
                        foreach (Label lblSeat in c767_Seats.Children)
                        {
                            if (lblSeat.Background == Brushes.Green)
                            {
                                lblSeat.Background = Brushes.Red;
                            }
                            if (lblSeat.Content.ToString() == seat)
                            {
                                lblSeat.Background = Brushes.Green;
                            }

                        }
                    }
                    //if not boeing 767 must be Airbus
                    else
                    {
                        foreach (Label lblSeat in cA380_Seats.Children)
                        {
                            if (lblSeat.Background == Brushes.Green)
                            {
                                lblSeat.Background = Brushes.Red;
                            }
                            if (lblSeat.Content.ToString() == seat)
                            {
                                lblSeat.Background = Brushes.Green;
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Upon selecting a taken seat it will show the current holder of that seat in the combobox and change it to green.  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Seats_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //Must first check that a flight is choosen
                if (cbChooseFlight.SelectedIndex > -1)
                {

                    //casting sender to label to find the seat number
                    Label seatSelected = (Label)sender;
                    string choosenSeat = seatSelected.Content.ToString();
                    //casting selected flight to get the flightId
                    Flight flight = cbChooseFlight.SelectedItem as Flight;
                    string flightId = flight.FlightId;
                    //If the user has just added a new passenger and clicks a seat or else run regular seat selected turns to green and displays name
                    if (GlobalVariables.PAddOn)
                    {
                        //getting passengersId
                        string passengersId = gDbI.FindPassengersId(GlobalVariables.FirstName.ToString(), GlobalVariables.LastName.ToString());

                        //This if statement will make sure that the user can only select an empty seat 
                        if (seatSelected.Background == Brushes.Blue)
                        {
                            //adding link to database then enabling the panels again if the user selects an empty seat
                            gDbI.addLink(flightId, passengersId, choosenSeat);
                            GlobalVariables.PAddOn = false;
                            cbChooseFlight.IsEnabled = true;
                            cbChoosePassenger.IsEnabled = true;
                            gPassengerCommands.IsEnabled = true;

                            //clearing passenger combobox and updating the passenger list with the newly added passenger
                            cbChoosePassenger.Items.Clear();
                            List<Passenger> pList = gDbI.passengersOnPlane(flightId);
                            foreach (Passenger p in pList)
                            {
                                cbChoosePassenger.Items.Add(p);
                            }
                            LoadSeats();
                            //Selecting the new passenger in the passenger combobox
                            var item_iter = cbChoosePassenger.Items.Cast<Passenger>()
                                      .Where(x => x.ToString() == GlobalVariables.FirstName + " " + GlobalVariables.LastName);
                            foreach (Passenger item in item_iter)
                            {
                                cbChoosePassenger.SelectedIndex = cbChoosePassenger.Items.IndexOf(item);
                            }
                        }
                    }
                    //changing on click for change seat
                    else if (changeSeatMode)
                    {
                        if (seatSelected.Background == Brushes.Blue)
                        {
                            //Finding seat choosen
                            Passenger passenger = cbChoosePassenger.SelectedItem as Passenger;
                            string passengerId = passenger.PassengerId;
                            //updating the database with new seat number
                            gDbI.updateSeatNumber(choosenSeat, flightId, passengerId);
                            //enabling all the panels again
                            cbChoosePassenger.IsEnabled = true;
                            gPassengerCommands.IsEnabled = true;
                            cbChooseFlight.IsEnabled = true;
                            //changing modoe so that this if statement isn't true again until required
                            changeSeatMode = false;
                            //getting the current index of the passenger so that I can set it again
                            int indexofCurrentPassenger = cbChoosePassenger.SelectedIndex;
                            //reloading the passenger combobox with the updated passenger object and new seat
                            cbChoosePassenger.Items.Clear();
                            List<Passenger> pList = gDbI.passengersOnPlane(flightId);
                            foreach (Passenger p in pList)
                            {
                                cbChoosePassenger.Items.Add(p);
                            }
                            LoadSeats();
                            //setting passenger to same person that recently had there seat changed
                            cbChoosePassenger.SelectedIndex = indexofCurrentPassenger;
                        }
                    }

                    else
                    {
                        //casting to a label
                        Label seatPressed = (Label)sender;
                        //will send which label was selected if its blue

                        if (seatPressed.Background == Brushes.Blue)
                        {
                            // openSeat = seatPressed.Content.ToString();
                        }
                        //if color is red change the combobox to show the owner of the seat
                        else if (seatPressed.Background == Brushes.Red)
                        {
                            //casting the selected item in the combobox to flight to access properties
                            //Flight flight = cbChooseFlight.SelectedItem as Flight;
                            string seatNumber = seatPressed.Content.ToString();

                            string passengerName = "";
                            //first must find the name of the passenger who own thats that seat
                            foreach (Passenger p in cbChoosePassenger.Items)
                            {
                                if (p.Seat == seatNumber)
                                {
                                    passengerName = p.FirstName + " " + p.LastName;
                                }
                            }
                            //if the name is found take find the name in cbChoosePassenger combobox and set that to the current index
                            if (passengerName != "")
                            {
                                var item_iter = cbChoosePassenger.Items.Cast<Passenger>()
                                      .Where(x => x.ToString() == passengerName);
                                foreach (Passenger item in item_iter)
                                {
                                    cbChoosePassenger.SelectedIndex = cbChoosePassenger.Items.IndexOf(item);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Deletes a passengers and deletes the passenger in the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDeletePassenger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbChoosePassenger.SelectedIndex > -1)
                {
                    Passenger passenger = cbChoosePassenger.SelectedItem as Passenger;
                    string passengerId = passenger.PassengerId;
                    Flight flight = cbChooseFlight.SelectedItem as Flight;
                    string flightId = flight.FlightId;
                    gDbI.deletePassenger(flightId, passengerId);

                    cbChoosePassenger.Items.Clear();

                    List<Passenger> pList = gDbI.passengersOnPlane(flightId);
                    foreach (Passenger p in pList)
                    {
                        cbChoosePassenger.Items.Add(p);
                    }
                    LoadSeats();
                }
                else
                {
                    MessageBox.Show("Must Select a Passenger to Delete");
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
    
        /// <summary>
        /// Method will allow the user to change the seat a passenger is currently in
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdChangeSeat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbChoosePassenger.SelectedIndex > -1)
                {
                    cbChoosePassenger.IsEnabled = false;
                    gPassengerCommands.IsEnabled = false;
                    cbChooseFlight.IsEnabled = false;
                    changeSeatMode = true;
                }
                else
                {
                    MessageBox.Show("Must First Select a Passenger To Change Seat");
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}
