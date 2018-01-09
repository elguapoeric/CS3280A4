using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// Interaction logic for wndAddPassenger.xaml
    /// </summary>
    public partial class wndAddPassenger : Window
    {
        /// <summary>
        /// Connects to the database
        /// </summary>
        clsDataAccess cDA;
        /// <summary>
        /// Cotains queries as strings
        /// </summary>
        SQLQueries sql;
        /// <summary>
        /// Gets database information
        /// </summary>
        GetDBInfo gDbI;
        
        /// <summary>
        /// constructor for the add passenger window, instansiating objects
        /// </summary>
        public wndAddPassenger()
        {
           

            try
            {
                InitializeComponent();
                cDA = new clsDataAccess();
                sql = new SQLQueries();
                gDbI = new GetDBInfo();
                //clear textboxes each time the window is open
                txtLastName.Clear();
                txtFirstName.Clear();
                
                
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
               
            }
        }
      
        /// <summary>
        /// only allows letters to be input
        /// </summary>
        /// <param name="sender">sent object</param>
        /// <param name="e">key argument</param>
        private void txtLetterInput_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //Only allow letters to be entered
                if (!(e.Key >= Key.A && e.Key <= Key.Z))
                {
                    //Allow the user to use the backspace, delete, tab and enter
                    if (!(e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Tab || e.Key == Key.Enter))
                    {
                        //No other keys allowed besides numbers, backspace, delete, tab, and enter
                        e.Handled = true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// exception handler that shows the error
        /// </summary>
        /// <param name="sClass">the class</param>
        /// <param name="sMethod">the method</param>
        /// <param name="sMessage">the error message</param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }

        /// <summary>
        /// Will add the first and last name to database which will give passenger an id
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtFirstName.Text != null && txtLastName.Text != null)
                {
                    gDbI.addPassengerToDB(txtFirstName.Text, txtLastName.Text);
                    GlobalVariables.FirstName = txtFirstName.Text;
                    GlobalVariables.LastName = txtLastName.Text;
                    //Im gonna set this to true to indicate the user did the save the name and will need to disable panels in the main window
                    GlobalVariables.PAddOn = true;

                    this.Close();
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
