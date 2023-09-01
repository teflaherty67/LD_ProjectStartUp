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
            Application app = uiapp.Application;
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
                StartUp.NewProject(uiapp);
            }
            else if (typeProject == "Existing Project")
            {
                StartUp.ExistingProject(uiapp);                
            }             
        }
    }
}