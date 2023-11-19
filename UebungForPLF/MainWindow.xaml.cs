using Microsoft.Win32;
using System.IO;

using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UebungForPLF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Patient> patientList = new List<Patient>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenEvent(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            if(openFileDialog.ShowDialog() == true) {
                string[]lines = File.ReadAllLines(openFileDialog.FileName);
                for (int tempLines = 0; tempLines < lines.Length; tempLines++)
                {
                    if (Patient.TryParse(lines[tempLines],out Patient pat)){
                        patientList.Add(pat);

                     }
                    else
                    {

                    }
                }
                int PatCountForLabel = 0;
                int TestCountForLabel = 0;
                foreach(Patient p in patientList)
                {
                    PatCountForLabel++;
                    foreach(Audiometrie ad in p.AudiometrieList)
                    {
                        TestCountForLabel++;
                    }
                }
                lab_patCount.Content = PatCountForLabel;
                lab_testCount.Content = TestCountForLabel;
                
                for (int i = 0; i < patientList.Count; i++)
                {
                   
                    TreeViewItem tv = new TreeViewItem();
                    tv.Header = patientList[i].ToString();
                    foreach (Audiometrie ad in patientList[i].AudiometrieList)
                    {
                        
                        tv.Items.Add(ad);

                    }
                    PatientTreeView.Items.Add(tv);
                }


                DrawLeft();
            }
        }

        private void DrawLeft()
        {
            can_leftEar.Children.Clear();
            List<int> w1 = new List<int> { 86, 59, 90, 74 };
           
            int highest =w1.Max();
            double oneheight = (can_leftEar.ActualHeight*0.95)/highest;
            PointCollection points = new PointCollection {
                 new Point {Y = can_leftEar.ActualHeight  - oneheight * w1[0], X = can_leftEar.ActualWidth*0.05 },
                 new Point {Y = can_leftEar.ActualHeight  - oneheight * w1[1], X =  can_leftEar.ActualWidth*0.3 },
                 new Point {Y = can_leftEar.ActualHeight  - oneheight * w1[2],  X =  can_leftEar.ActualWidth*0.6 },
                 new Point {Y = can_leftEar.ActualHeight  - oneheight * w1[3], X = can_leftEar.ActualWidth *0.95 },
                 new Point {Y = can_leftEar.ActualHeight *0.95, X = can_leftEar.ActualWidth*0.95},
                 new Point {Y = can_leftEar.ActualHeight *0.95, X = can_leftEar.ActualWidth*0.05}
            };

            Polygon pg = new Polygon();
            pg.Points = points;
            pg.Fill = new SolidColorBrush(Colors.Yellow);

            can_leftEar.Children.Add(pg);


            for (int i = 0; i < 4; i++)
            {
                TextBlock lb = new TextBlock();
                lb.Foreground = new SolidColorBrush(Colors.White);
                lb.Text = w1[i]+"";
                lb.TextAlignment = TextAlignment.Center;
                Canvas.SetLeft(lb, points[i].X );
                Canvas.SetTop(lb, points[i].Y);
                double angle = Math.Atan2(points[i+1].Y - points[i].Y, points[i+1].X - points[i].X) * 180 / Math.PI;
                lb.RenderTransform = new RotateTransform(angle, lb.ActualWidth / 2, lb.ActualHeight / 2);
                
                can_leftEar.Children.Add(lb);

            }
            
        }

        private void SaveAsEvent(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            if (saveFileDialog.ShowDialog()==true)
            {
                StreamWriter wr = new StreamWriter(saveFileDialog.FileName);
                foreach(Patient patient in patientList)
                {
                    wr.WriteLine(patient.toCSV());
                }
                wr.Close();
            }




        }

        private void ExitEvent(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
       
            if( MessageBox.Show("Do you want to Close?", "Closing Window", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {

            }
            else
            {
                e.Cancel = true;
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void PatientTreeView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DrawLeft();
        }
    }
}