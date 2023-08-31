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