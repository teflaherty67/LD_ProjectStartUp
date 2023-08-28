#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            frmProjectStartUp curForm = new frmProjectStartUp()
            {
                Width = 240,
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

                frmNewProject curForm2 = new frmNewProject()
                {
                    Width = 240,
                    Height = 180,
                    WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
                    Topmost = true,
                };
            }
            else if (typeProject == "Existing Project")
            {
                TaskDialog.Show("Project", "You have selected to work on an existing project");

                //Forms.OpenFileDialog selectFile = new Forms.OpenFileDialog();
                //selectFile.Filter = "Revit files|*.rvt;*.rfa;*.rte";
                //selectFile.InitialDirectory = "S:\\";
                //selectFile.Multiselect = false;
            }

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }


    }
}
