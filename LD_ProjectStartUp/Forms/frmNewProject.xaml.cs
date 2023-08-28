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
    /// Interaction logic for frmProjectStartUp.xaml
    /// </summary>
    public partial class frmNewProject : Window
    {
        public frmNewProject()
        {
            InitializeComponent();
        }

        public string GetGroupProjectType()
        {
            if (rbnNew.IsChecked == true)
                return rbnNew.Content.ToString();
            else
                return rbnExisting.Content.ToString();            
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
    }
}
