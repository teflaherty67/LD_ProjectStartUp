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
    public partial class frmProjectTemplate : Window
    {
        public frmProjectTemplate()
        {
            InitializeComponent();
           
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

            // create a list of foundation types
            List<string> listFoundations = new List<string> { "Basement", "Crawlspace", "Slab" };

            // add each type to the combobox
            foreach (string foundation in listFoundations)
            {
                cmbFoundation.Items.Add(foundation);
            }

            // set the default selection to Slab
            cmbFoundation.SelectedIndex = 0;
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


