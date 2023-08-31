using System;
using System.Collections.Generic;
using System.Linq;
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

namespace LD_ProjectStartUp
{
    /// <summary>
    /// Interaction logic for frmNewProject.xaml
    /// </summary>
    public partial class frmNewProject : Window
    {
        public frmNewProject()
        {
            InitializeComponent();

            cbxSchedules.IsEnabled = false;

            // create a list of LGI division clients
            List<string> listClients = new List<string> { "Central Texas", "Dallas/Ft Worth",
                "Florida", "Houston", "Maryland", "Minnesota", "Oklahoma", "Pennsylvania",
                "Southeast", "Virginia", "West Virginia" };

            // add each client to the comboxbox
            foreach (string client in listClients)
            {
                cmbClient.Items.Add(client);
            }

            // set the default selection to the first client in the list (Central Texas)
            cmbClient.SelectedIndex = 0;

            // create a list of typical elevaiton designations
            List<string> listElevations = new List<string> { "Elevation A", "Elevation B", "Elevation C", "Elevation D", "Elevation S", "Elevation T" };

            // add each elevation to the combobox
            foreach (string elevation in listElevations)
            {
                cmbElevation.Items.Add(elevation);
            }

            // set the default selection to first elevation in the list (Elevation A)
            cmbElevation.SelectedIndex = 0;

            // create a list for number of floors
            List<string> listFloors = new List<string> { "One", "Two", "Three" };

            // add each one to the combobox
            foreach (string floor in listFloors)
            {
                cmbFloors.Items.Add(floor);
            }

            // set the default selection to One
            cmbFloors.SelectedIndex = 0;

            // create a list of foundation types
            List<string> listFoundations = new List<string> { "Basement", "Crawlspace", "Slab" };

            // add each type to the combobox
            foreach (string foundation in listFoundations)
            {
                cmbFoundation.Items.Add(foundation);
            }

            // set the default selection to Slab
            cmbFoundation.SelectedIndex = 0;

            // create a list of typical plate heights
            List<string> listPlates = new List<string> { "8'-1 1/8\"", "9'-1 1/8\"", "10'-1 1/8\"" };

            // add each plate to the comboboxes
            foreach (string plate in listPlates)
            {
                cmbPlate1.Items.Add(plate);
                cmbPlate2.Items.Add(plate);
            }

            // set the default selection for Plate 1 (8'-1 1/8")
            cmbPlate1.SelectedIndex = 0;

            // set the default selection for Plate 2 (8'-1 1/8")
            cmbPlate2.SelectedIndex = 0;

            // create a list of typical floor systems
            List<string> listFloorSys = new List<string> { "2x12", "14\" Truss", "16\" Truss", "18\" Truss" };

            //add each system to the combobox
            foreach (string floorSys in listFloorSys)
            {
                cmbFloorSys.Items.Add(floorSys);
            }

            // set the default selection to 2x12
            cmbFloorSys.SelectedIndex = 0;
        }

        internal string GetComboboxClient()
        {
            return cmbClient.SelectedItem.ToString();
        }

        internal string GetTextBoxPlanName()
        {
            return tbxPlanName.Text.ToString();
        }

        internal string GetTextBoxLivingArea()
        {
            return tbxLiving.Text.ToString();
        }

        internal string GetComboboxFloors()
        {
            return cmbFloors.SelectedItem.ToString();
        }

        internal string GetComboboxFoundation()
        {
            return cmbFoundation.SelectedItem.ToString();
        }

        internal string GetComboboxElevation()
        {
            return cmbElevation.SelectedItem.ToString();
        }

        internal string GetTextBoxPlate0()
        {
            return tbxPlate0.Text.ToString();
        }

        internal string GetComboBoxPlate1()
        {
            return cmbPlate1.SelectedItem.ToString();
        }

        internal string GetComboBoxPlate2()
        {
            return cmbPlate2.SelectedItem.ToString();
        }

        internal string GetComboBoxFloorSys()
        {
            return cmbFloorSys.SelectedItem.ToString();
        }

        private void cmbElevation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbElevation.SelectedItem.ToString() != "Elevation A") { cbxSchedules.IsEnabled = true; }
            else { cbxSchedules.IsEnabled = false; cbxSchedules.IsChecked = false; };
        }

        private void cmbFoundation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox myCombo = sender as ComboBox;
            string stringFoundation = myCombo.SelectedItem as string;

            string stringFloors = cmbFloors.SelectedItem.ToString();

            if (stringFoundation == "Basement" && stringFloors == "One")
            {
                // show tbxPlate0 at grid row 4
                grpPlate0.SetValue(Grid.RowProperty, 4);
                grpPlate0.Visibility = Visibility.Visible;

                // show cmbPlate1 at grid row 5
                grpPlate1.SetValue(Grid.RowProperty, 5);
                grpPlate1.Visibility = Visibility.Visible;

                // show cmbFloorSys @ grid row 6
                grpFloorSys.SetValue(Grid.RowProperty, 6);
                grpFloorSys.Visibility = Visibility.Visible;

                // hide cmbPlate2
                grpPlate2.Visibility = Visibility.Hidden;
            }
            else if (stringFoundation == "Basement" && stringFloors == "Two")
            {
                // show tbxPlate0 at grid row 4
                grpPlate0.SetValue(Grid.RowProperty, 4);
                grpPlate0.Visibility = Visibility.Visible;

                // show cmbPlate1 at grid row 5
                grpPlate1.SetValue(Grid.RowProperty, 5);
                grpPlate1.Visibility = Visibility.Visible;

                // show cmbPlate2 at grid row 6
                grpPlate2.SetValue(Grid.RowProperty, 6);
                grpPlate2.Visibility = Visibility.Visible;

                // show cmbFloorSys @ grid row 7
                grpFloorSys.SetValue(Grid.RowProperty, 7);
                grpFloorSys.Visibility = Visibility.Visible;
            }
            else if (stringFoundation == "Crawlspace" && stringFloors == "One")
            {
                // show tbxPlate0 at grid row 4
                grpPlate0.SetValue(Grid.RowProperty, 4);
                grpPlate0.Visibility = Visibility.Visible;

                // show cmbPlate1 at grid row 5
                grpPlate1.SetValue(Grid.RowProperty, 5);
                grpPlate1.Visibility = Visibility.Visible;

                // show cmbFloorSys @ grid row 6
                grpFloorSys.SetValue(Grid.RowProperty, 6);
                grpFloorSys.Visibility = Visibility.Visible;

                // hide cmbPlate2
                grpPlate2.Visibility = Visibility.Hidden;
            }
            else if (stringFoundation == "Crawlspace" && stringFloors == "Two")
            {
                // show tbxPlate0 at grid row 4
                grpPlate0.SetValue(Grid.RowProperty, 4);
                grpPlate0.Visibility = Visibility.Visible;

                // show cmbPlate1 at grid row 5
                grpPlate1.SetValue(Grid.RowProperty, 5);
                grpPlate1.Visibility = Visibility.Visible;

                // show cmbPlate2 at grid row 6
                grpPlate2.SetValue(Grid.RowProperty, 6);
                grpPlate2.Visibility = Visibility.Visible;

                // show cmbFloorSys @ grid row 7
                grpFloorSys.SetValue(Grid.RowProperty, 7);
                grpFloorSys.Visibility = Visibility.Visible;
            }
            else if (stringFoundation == "Slab" && stringFloors == "One")
            {
                // show cmbPlate1 @ grid row 4
                grpPlate1.SetValue(Grid.RowProperty, 4);
                grpPlate1.Visibility = Visibility.Visible;

                // hide tbxPlate0, cmbPlate2 & cmbFloorSys
                grpPlate0.Visibility = Visibility.Hidden;
                grpPlate2.Visibility = Visibility.Hidden;
                grpFloorSys.Visibility = Visibility.Hidden;
            }
            else
            {
                // show cmbPlate1 at grid row 4
                grpPlate1.SetValue(Grid.RowProperty, 4);
                grpPlate1.Visibility = Visibility.Visible;

                // show cmbPlate2 at grid row 5
                grpPlate2.SetValue(Grid.RowProperty, 5);
                grpPlate2.Visibility = Visibility.Visible;

                // show cmbFloorSys @ grid row 6
                grpFloorSys.SetValue(Grid.RowProperty, 6);
                grpFloorSys.Visibility = Visibility.Visible;

                // hide tbxPlate0
                grpPlate0.Visibility = Visibility.Hidden;
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}


