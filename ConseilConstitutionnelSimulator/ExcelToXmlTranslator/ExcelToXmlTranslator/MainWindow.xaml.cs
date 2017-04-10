using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace ExcelToXmlTranslator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Controls.Label excelFile;
        string excelPath;

        System.Windows.Controls.Label outPutDir;
        string outputPath;

        System.Windows.Controls.Label doneLabel;

        public MainWindow()
        {
            InitializeComponent();
            excelFile = FindName("excelName") as System.Windows.Controls.Label;
            outPutDir = FindName("outputDir") as System.Windows.Controls.Label;
            doneLabel = FindName("doneLab") as System.Windows.Controls.Label;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                excelPath = openFileDialog.FileName;
                excelFile.Content = "Output Directory : " + outputPath;
            }

            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog openFileDialog = new FolderBrowserDialog();

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                outputPath = openFileDialog.SelectedPath;
                outPutDir.Content = "Excel File: " + excelPath;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            int i = 0;
            foreach(var line in File.ReadLines(excelPath))
            {
                if(i >= int.Parse(FromLineInput.Text) && i<= int.Parse(ToLineInput.Text))
                {
                    string[] cases = line.Split('\t');

                    Law tmpLaw = new Law();
                    tmpLaw.Id = int.Parse(cases[0]);
                    tmpLaw.Title = cases[1];
                    tmpLaw.Description = cases[3];
                    // Yes Stuff
                    if(cases[6] != "")
                        if(int.Parse(cases[6]) != -1)
                            tmpLaw.YesLawsToAdd.Add( int.Parse(cases[6]));
                    if(cases[7] != "")
                        tmpLaw.YesPropertyModifiers.Add(new PropertyModifier( GameProperty.GameProperty_GOVERNMENTOPINION, int.Parse(cases[7] ) ) );
                    if (cases[8] != "")
                        tmpLaw.YesPropertyModifiers.Add(new PropertyModifier(GameProperty.GameProperty_POPULACEOPINION, int.Parse(cases[8])));
                    if (cases[9] != "")
                        tmpLaw.YesPropertyModifiers.Add(new PropertyModifier(GameProperty.GameProperty_ECONOMY, int.Parse(cases[9])));
                    if (cases[10] != "")
                        tmpLaw.YesPropertyModifiers.Add(new PropertyModifier(GameProperty.GameProperty_EMPLOYMENT, int.Parse(cases[10])));
                    if (cases[11] != "")
                        tmpLaw.YesPropertyModifiers.Add(new PropertyModifier(GameProperty.GameProperty_RELIGION, int.Parse(cases[11])));
                    if(cases[12] != "")
                        tmpLaw.YesPropertyModifiers.Add(new PropertyModifier(GameProperty.GameProperty_PERSONALMONEY, int.Parse(cases[12])));

                    // No Stuff
                    if (cases[13] != "")
                        if( int.Parse(cases[13]) != -1 )
                            tmpLaw.NoLawsToAdd.Add(int.Parse(cases[13]));
                    if (cases[14] != "")
                        tmpLaw.NoPropertyModifiers.Add(new PropertyModifier(GameProperty.GameProperty_GOVERNMENTOPINION, int.Parse(cases[14])));
                    if (cases[15] != "")
                        tmpLaw.NoPropertyModifiers.Add(new PropertyModifier(GameProperty.GameProperty_POPULACEOPINION, int.Parse(cases[15])));
                    if (cases[16] != "")
                        tmpLaw.NoPropertyModifiers.Add(new PropertyModifier(GameProperty.GameProperty_ECONOMY, int.Parse(cases[16])));
                    if (cases[17] != "")
                        tmpLaw.NoPropertyModifiers.Add(new PropertyModifier(GameProperty.GameProperty_EMPLOYMENT, int.Parse(cases[17])));
                    if (cases[18] != "")
                        tmpLaw.NoPropertyModifiers.Add(new PropertyModifier(GameProperty.GameProperty_RELIGION, int.Parse(cases[18])));
                    if (cases[19] != "")
                        tmpLaw.NoPropertyModifiers.Add(new PropertyModifier(GameProperty.GameProperty_PERSONALMONEY, int.Parse(cases[19])));

                    // SoftenCond
                    if (cases[20] != "")
                        tmpLaw.MinimizeLawsToAdd.Add( int.Parse(cases[20]));

                    // HardenCond

                    if (cases[21] != "")
                        tmpLaw.MaximizeLawsToAdd.Add(int.Parse(cases[21]));

                    XmlSerializerHelper<Law>.SerializeXmlFile(outputPath + "/Law" + tmpLaw.Id+".xml", tmpLaw);
                }

                i++;
            }
            doneLabel.Content = "Done !!!";
        }
    }
}
