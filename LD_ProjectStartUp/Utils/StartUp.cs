using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
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
            // hard-code Excel file
            string excelFile = "S:\\Shared Folders\\!RBA Addins\\Lifestyle Design\\Data Source\\NewProjectSetup.xlsx";

            // create a list to hold the sheetdata
            List<List<string>> dataSheets = new List<List<string>>();

            frmNewProject curForm = new frmNewProject()
            {
                Width = 320,
                Height = 420,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
                Topmost = true,
            };

            curForm.ShowDialog();

            // get form data and do something

            #region Excel

            using (var package = new ExcelPackage(excelFile))
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                ExcelWorkbook wb = package.Workbook;

                ExcelWorksheet ws;

                if (curForm.GetComboboxFoundation() == "Basement" && curForm.GetComboboxFloors() == "1")
                    ws = wb.Worksheets[0];
                else if (curForm.GetComboboxFoundation() == "Basement" && curForm.GetComboboxFloors() == "2")
                    ws = wb.Worksheets[1];
                else if (curForm.GetComboboxFoundation() == "Crawlspace" && curForm.GetComboboxFloors() == "1")
                    ws = wb.Worksheets[2];
                else if (curForm.GetComboboxFoundation() == "Crawlspace" && curForm.GetComboboxFloors() == "2")
                    ws = wb.Worksheets[3];
                else if (curForm.GetComboboxFoundation() == "Slab" && curForm.GetComboboxFloors() == "1")
                    ws = wb.Worksheets[4];
                else
                    ws = wb.Worksheets[5];

                // get row & column count
                int rows = ws.Dimension.Rows;
                int columns = ws.Dimension.Columns;

                // read Excel data into a list
                for (int i = 1; i <= rows; i++)
                {
                    List<string> rowData = new List<string>();
                    for (int j = 1; j <= columns; j++)
                    {
                        string cellContent = ws.Cells[i, j].Value.ToString();
                        rowData.Add(cellContent);
                    }
                    dataSheets.Add(rowData);
                }

                dataSheets.RemoveAt(0);
            }

            #endregion

            #region Variables

            // get value for Plate 0
            string dimPlate0 = curForm.GetTextBoxPlate0();
            double dblPlate0 = 0;

            bool heightPlate0 = UnitFormatUtils.TryParse(curDoc.GetUnits(), SpecTypeId.Length, dimPlate0, out dblPlate0);

            // get value for Plate 1
            string dimPlate1 = curForm.GetComboBoxPlate1();
            double dblPlate1 = 0;

            bool heightPlate1 = UnitFormatUtils.TryParse(curDoc.GetUnits(), SpecTypeId.Length, dimPlate1, out dblPlate1);

            // get value for Plate 2
            string dimPlate2 = curForm.GetComboBoxPlate2();
            double dblPlate2 = 0;

            bool heightPlate2 = UnitFormatUtils.TryParse(curDoc.GetUnits(), SpecTypeId.Length, dimPlate2, out dblPlate2);

            // get value for floor thickness
            string floorSys = curForm.GetComboBoxFloorSys();
            string heightFloor = "";

            if (floorSys == "2x12")
            {
                heightFloor = "1'-0";
            }
            else if (floorSys == "14\" Truss")
            {
                heightFloor = "1'-2 3/4\"";
            }
            else if (floorSys == "16\" Truss")
            {
                heightFloor = "1'-4 3/4\"";
            }
            else
            {
                heightFloor = "1'-6 3/4\"";
            }

            double thicknessFloor = 0;

            bool resultFloor = UnitFormatUtils.TryParse(curDoc.GetUnits(), SpecTypeId.Length, heightFloor, out thicknessFloor);

            string numFloors = curForm.GetComboboxFloors();

            int numberFloors = 0;

            if (numFloors == "One")
                numberFloors = 1;
            else if (numFloors == "Two")
                numberFloors = 2;
            else if (numFloors == "Three")
                numberFloors = 3;

            string newElev = curForm.GetComboboxElevation();

            string newFilter = "";

            if (newElev == "Elevation A")
                newFilter = "1";
            else if (newElev == "Elevation B")
                newFilter = "2";
            else if (newElev == "Elevation C")
                newFilter = "3";
            else if (newElev == "Elevation D")
                newFilter = "4";
            else if (newElev == "Elevation S")
                newFilter = "5";
            else if (newElev == "Elevation T")
                newFilter = "6";

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

            ProjectInfo clientInfo = curDoc.ProjectInformation;

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

            UIDocument activeDoc = uiapp.OpenAndActivateDocument(savePath);

            #endregion

            #region Transaction

            using (Transaction t = new Transaction(curDoc))
            {
                t.Start("Project Setup");

                #region Project Information

                if (null != clientInfo)
                {
                    clientInfo.ClientName = nameClient;
                    clientInfo.Name = curForm.GetTextBoxPlanName();
                    clientInfo.Number = curForm.GetTextBoxLivingArea();
                }

                #endregion

                #region Create Levels

                #region Basement

                if (curForm.GetComboboxFoundation() == "Basement")
                {
                    // set the values of the levels

                    // get the levels to modify
                    Level plate0 = Utils.GetLevelByName(curDoc, "Plate 0");
                    Level lowerLevel = Utils.GetLevelByName(curDoc, "Lower Level");
                    Level plate1 = Utils.GetLevelByName(curDoc, "Plate 1");

                    // set the height value of levels
                    plate0.Elevation = thicknessFloor * -1;
                    lowerLevel.Elevation = dblPlate0 * -1;
                    plate1.Elevation = dblPlate1;

                    if (numberFloors == 1)
                    {
                        // delete unused levels

                        // get the levels by name
                        Level upperLevel = Utils.GetLevelByName(curDoc, "Upper Level");
                        Level plate2 = Utils.GetLevelByName(curDoc, "Plate 2");

                        // delete the levels
                        curDoc.Delete(upperLevel.Id);
                        curDoc.Delete(plate2.Id);
                    }

                    if (numberFloors > 1)
                    {
                        // create the above grade levels: Upper Level & Plate 2
                    }
                }

                #endregion

                #region Crawlspace

                if (curForm.GetComboboxFoundation() == "Crawlspace")
                {
                    // set the values of the below grade levels: Lower Level & Plate 0

                    // create the above grade level Plate 1
                    Level plate1 = Level.Create(curDoc, dblPlate1);
                    plate1.Name = "Plate 1";

                    if (numberFloors == 1)
                    {
                        // delete the unused levels
                    }
                    else if (numberFloors > 1)
                    {
                        // set the heights of Upper Level & Plate 2
                    }
                }

                #endregion

                #region Slab

                if (curForm.GetComboboxFoundation() == "Slab")
                {
                    // set the levels used for slabs
                    Level plate1 = Utils.GetLevelByName(curDoc, "Plate 1");
                    Level secondFloor = Utils.GetLevelByName(curDoc, "Second Floor");
                    Level plate2 = Utils.GetLevelByName(curDoc, "Plate 2");

                    // set the height of Plate 1
                    plate1.Elevation = dblPlate1;

                    if (numberFloors == 1)
                    {
                        // delete unused levels                        
                        curDoc.Delete(secondFloor.Id);
                        curDoc.Delete(plate2.Id);
                    }
                    else if (numberFloors > 1)
                    {
                        // set the height of Second Floor & Plate 2
                        secondFloor.Elevation = dblPlate1 + thicknessFloor;
                        plate2.Elevation = secondFloor.Elevation + dblPlate2;
                    }
                }

                #endregion

                #endregion

                #region Create Sheets

                foreach (List<string> curSheetData in dataSheets)
                {
                    // get the titleblock
                    FamilySymbol tblock = Utils.GetTitleBlockByNameContains(curDoc, curSheetData[2]);
                    ElementId tBlockId = tblock.Id;

                    // create the sheet
                    ViewSheet curSheet = ViewSheet.Create(curDoc, tBlockId);

                    // add elevation designation to sheet number
                    curSheet.SheetNumber = curSheetData[0] + curForm.GetComboboxElevation().ToLower();
                    curSheet.Name = curSheetData[1];

                    // set parameter values                    
                    Utils.SetParameterByName(curSheet, "Category", "Active");
                    Utils.SetParameterByName(curSheet, "Group", "Elevation " + curForm.GetComboboxElevation());
                    Utils.SetParameterByName(curSheet, "Elevation Designation", curForm.GetComboboxElevation());
                    Utils.SetParameterByName(curSheet, "Code Filter", newFilter);
                    Utils.SetParameterByName(curSheet, "Index Position", int.Parse(curSheetData[3]));
                }

                #endregion

                #region Delete Unused Schedules

                // if number of floors = 1, delete multi schedules

                // if number of fllors > 1, delete single schedules

                #endregion

                t.Commit();
            }

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
            UIDocument activeDoc = uiapp.OpenAndActivateDocument(revitFile);
        }
    }
}
