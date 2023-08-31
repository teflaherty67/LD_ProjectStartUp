#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Versioning;
using System.Windows.Markup;
using System.Windows.Media;
using Forms = System.Windows.Forms;

#endregion

namespace LD_ProjectStartUp
{
    internal class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication app)
        {

            app.Idling += Application_LDStartup;

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            a.Idling -= Application_LDStartup;
            return Result.Succeeded;
        }
        private static void Application_LDStartup(object sender, Autodesk.Revit.UI.Events.IdlingEventArgs e)
        {
            var uiapp = sender as UIApplication;
            uiapp.Idling -= Application_LDStartup;

            frmProjectStartUp curForm = new frmProjectStartUp()
            {
                Width = 260,
                Height = 180,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
                Topmost = true,
            };

            curForm.ShowDialog();

            // get data from the form

            string typeProject = curForm.GetGroupProjectType();

            if (typeProject == "New Project")
            {
                //TaskDialog.Show("Project", "You have selected to start a new project");

                frmNewProject newForm = new frmNewProject()
                {
                    Width = 260,
                    Height = 180,
                    WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
                    Topmost = true,
                };

                newForm.ShowDialog();

                // set variable for client info
                string txtClient = newForm.GetComboboxClient();

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

                ProjectInfo clientInfo = curDoc.ProjectInformation;

                #region Template File

                // set path for template files
                string templateBasement = "S:\\Shared Folders\\Lifestyle USA Design\\Library 2023\\Template\\LGI-Basement.rte";
                string templateCrawlspace = "S:\\Shared Folders\\Lifestyle USA Design\\Library 2023\\Template\\LGI-Crawlspace.rte";
                string templateSlab = "S:\\Shared Folders\\Lifestyle USA Design\\Library 2023\\Template\\LGI-Slab.rte";

                // set path for file save location
                string acronymClient = nameClient.Split('-')[1];

                string savePath = "S:\\Shared Folders\\Lifestyle USA Design\\LGI Homes\\" + newForm.GetComboboxClient() + "\\" + newForm.GetTextBoxPlanName() + "-" + acronymClient + ".rvt";

                Document newDoc = null;

                if (newForm.GetComboboxFoundation() == "Basement")
                {
                    newDoc = app.NewProjectDocument(templateBasement);
                }
                else if (newForm.GetComboboxFoundation() == "Crawlspace")
                {
                    newDoc = app.NewProjectDocument(templateCrawlspace);
                }
                else
                {
                    newDoc = app.NewProjectDocument(templateSlab);
                }

                newDoc.SaveAs(savePath);

                UIDocument activeDoc = uiapp.OpenAndActivateDocument(savePath);

                #endregion
            }
            else if (typeProject == "Existing Project")
            {
                //TaskDialog.Show("Project", "You have selected to work on an existing project");

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
                UIDocument activeDoc = uiapp.OpenAndActivateDocument(revitFile);
            }             
        }
    }
}