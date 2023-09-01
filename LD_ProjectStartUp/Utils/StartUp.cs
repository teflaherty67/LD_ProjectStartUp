using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using Forms = System.Windows.Forms;

namespace LD_ProjectStartUp
{
    internal static class StartUp
    {
        internal static void NewProject(UIApplication uiapp)
        {

            Application app = uiapp.Application;

            frmNewProject curForm = new frmNewProject()
            {
                Width = 320,
                Height = 420,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
                Topmost = true,
            };

            curForm.ShowDialog();

            // get form data and do something

            #region Variables

            // set variable for client info
            string txtClient = curForm.GetComboboxClient();

            string nameClient = "";

            if (txtClient == "Central Texas")
                nameClient = "LGI-CTX";
            else if (txtClient == "Dallas/Fort Worth")
                nameClient = "LGI-DFW";
            else if (txtClient == "Houston")
                nameClient = "LGI-HOU";
            else if (txtClient == "Maryland")
                nameClient = "LGI-MD";
            else if (txtClient == "Minnesota")
                nameClient = "LGI-MN";
            else if (txtClient == "Oklahoma")
                nameClient = "LGI-OK";
            else if (txtClient == "Pennsylvania")
                nameClient = "LGI-PA";
            else if (txtClient == "Southeast")
                nameClient = "LGI-SE";
            else if (txtClient == "Virginia")
                nameClient = "LGI-VA";
            else if (txtClient == "West Virginia")
                nameClient = "LGI-WV";            

            #endregion

            #region Template File

            // set path for template files
            string templateBasement = "S:\\Shared Folders\\Lifestyle USA Design\\Library 2023\\Template\\LGI-Basement.rte";
            string templateCrawlspace = "S:\\Shared Folders\\Lifestyle USA Design\\Library 2023\\Template\\LGI-Crawlspace.rte";
            string templateSlab = "S:\\Shared Folders\\Lifestyle USA Design\\Library 2023\\Template\\LGI-Slab.rte";

            // set path for file save location
            string acronymClient = nameClient.Split('-')[1];

            string savePath = "S:\\Shared Folders\\Lifestyle USA Design\\LGI Homes\\" + curForm.GetComboboxClient() + "\\" + curForm.GetTextBoxPlanName() + "-" + acronymClient + ".rvt";

            Document newDoc = null;

            if (curForm.GetComboboxFoundation() == "Basement")
            {
                newDoc = app.NewProjectDocument(templateBasement);
            }
            else if (curForm.GetComboboxFoundation() == "Crawlspace")
            {
                newDoc = app.NewProjectDocument(templateCrawlspace);
            }
            else
            {
                newDoc = app.NewProjectDocument(templateSlab);
            }

            newDoc.SaveAs(savePath);

            UIDocument curDoc = uiapp.OpenAndActivateDocument(savePath);

            #endregion
        }

        internal static void ExistingProject(UIApplication uiapp)
        {
            Forms.OpenFileDialog selectFile = new Forms.OpenFileDialog();
            selectFile.Filter = "Revit files|*.rvt;*.rfa;*.rte";
            selectFile.InitialDirectory = "S:\\";
            selectFile.Multiselect = false;

            //selectFile.ShowDialog();

            string revitFile = "";

            if (selectFile.ShowDialog() == Forms.DialogResult.OK)
                revitFile = selectFile.FileName;

            if (revitFile == "")
            {
                TaskDialog.Show("Error", "Please select a Revit file.");
            }

            // open Revit file
            UIDocument curDoc = uiapp.OpenAndActivateDocument(revitFile);
        }
    }
}
