using ProcessTaskManager.Model;
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
using System.Timers;
using System.Threading;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;
using ProcessTaskManager.ViewModels;
//using System.Threading;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Devices;

using System.Management;
using System.Windows.Controls.DataVisualization.Charting;

namespace ProcessTaskManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private System.Timers.Timer Timer;
        ProcessesThreds processesThreds;

        private System.Timers.Timer _timer;
        private System.Timers.Timer _timerMemory;
        private System.Timers.Timer _memory;
        private System.Timers.Timer _cleatmemory;

        TestViewModel testViewModel;
        //  List<Keyvalue> tempList = new List<Keyvalue>();



        public MainWindow()
        {
            InitializeComponent();


            // GroupBoxDynamicChart


            //  testViewModel.TestModel.DataList = tempList;

            //Chart dynamicChart = new Chart();
            //LineSeries lineseries = new LineSeries();
            //// lineseries.ItemsSource = tempList;

            //lineseries.DependentValuePath = "Value";
            //lineseries.IndependentValuePath = "Key";
            //dynamicChart.Series.Add(lineseries);
            //GroupBoxDynamicChart.Content = dynamicChart;


            Timer = new System.Timers.Timer();

            // Timer.Interval = 5000;
            //// Timer.Elapsed += Async;
            // Timer.AutoReset = true;
            // Timer.Enabled = true;


            _timer = new System.Timers.Timer
            {
                // Interval set to 1 millisecond.
                Interval = 5000,
                AutoReset = true,
            };

            _timer.Elapsed += _timer_Elapsed;
            _timer.Enabled = true;
            _timer.Start();

            _timerMemory = new System.Timers.Timer
            {
                Interval = 2000,
                AutoReset = true,
            };
            _timerMemory.Elapsed += _timer_Memory;
            _timerMemory.Enabled = true;
            _timerMemory.Start();


            _memory = new System.Timers.Timer
            {
                Interval = 5000,
                AutoReset = true
            };
            _memory.Elapsed += M1;
            _memory.Enabled = true;
            _memory.Start();


            _cleatmemory = new System.Timers.Timer
            {
                Interval = 60000,
                AutoReset = true
            };
            _cleatmemory.Elapsed += Clear;
            _cleatmemory.Enabled = true;
            _cleatmemory.Start();


            processesThreds = new ProcessesThreds();
            // List<ProcessesThreds> processesThredslist = 

            //var res = from p in processesThreds.GetActiveProcesses() orderby p.ProcessName select p;
            GridView myGridView = new GridView();

            myGridView.AllowsColumnReorder = true;
            myGridView.Columns.Add(new GridViewColumn() { Header = "Id", Width = 50, DisplayMemberBinding = new Binding("Id") });
            myGridView.Columns.Add(new GridViewColumn() { Header = "Name", Width = 200, DisplayMemberBinding = new Binding("ProcessName") });
            myGridView.Columns.Add(new GridViewColumn() { Header = "Threads", Width = 100, DisplayMemberBinding = new Binding("Threads.Count") });
            myGridView.Columns.Add(new GridViewColumn() { Header = "HandleCount", Width = 100, DisplayMemberBinding = new Binding("HandleCount") });
            myGridView.Columns.Add(new GridViewColumn() { Header = "PrivateMemorySize64", Width = 200, DisplayMemberBinding = new Binding("PrivateMemorySize64") });


            ProcessesListView.View = myGridView;

            ProcessesListView.ItemsSource = from p in processesThreds.GetActiveProcesses() orderby p.ProcessName select p;




            ManagementObjectSearcher ramMonitor =    //запрос к WMI для получения памяти ПК
          new ManagementObjectSearcher("SELECT TotalVisibleMemorySize,FreePhysicalMemory FROM Win32_OperatingSystem");

            ulong res = 0;
            foreach (ManagementObject objram in ramMonitor.Get())
            {
                ulong totalRam = Convert.ToUInt64(objram["TotalVisibleMemorySize"]);    //общая память ОЗУ
                ulong busyRam = totalRam - Convert.ToUInt64(objram["FreePhysicalMemory"]);         //занятная память = (total-free)
                res = ((busyRam * 100) / totalRam);
                Console.WriteLine(((busyRam * 100) / totalRam));       //вычисляем проценты занятой памяти
            }

            List<Keyvalue> list = new List<Keyvalue>() { new Keyvalue() { Key = DateTime.Now.ToString("h:mm:ss tt"), Value = res } };
         

            testViewModel = new TestViewModel(GroupBoxDynamicChart, list); //TestViewModel

            //  testViewModel.Add(GroupBoxDynamicChart,new Keyvalue() { Key = DateTime.Now.ToString("h:mm:ss tt"), Value = 89 });
            //  testViewModel.Add(GroupBoxDynamicChart, new Keyvalue() { Key = DateTime.Now.ToString("h:mm:ss tt"), Value = 90 });
            //  this.DataContext = testViewModel;



            //Memory();
         
        }
        private void Clear(object sender, System.Timers.ElapsedEventArgs e)
        {//60
            Application.Current.Dispatcher.Invoke(() => list.Clear());
        }



        private void M1(object sender, System.Timers.ElapsedEventArgs e) {//5
            Application.Current.Dispatcher.Invoke(() => testViewModel = new TestViewModel(GroupBoxDynamicChart, list));
        }

        private void _timer_Memory(object sender, System.Timers.ElapsedEventArgs e)//1
        {
           Application.Current.Dispatcher.Invoke(() => Memory());
        }
        List<Keyvalue> list = new List<Keyvalue>();

        private void Memory()
        {

            ManagementObjectSearcher ramMonitor =    //запрос к WMI для получения памяти ПК
            new ManagementObjectSearcher("SELECT TotalVisibleMemorySize,FreePhysicalMemory FROM Win32_OperatingSystem");

            ulong res = 0;
            foreach (ManagementObject objram in ramMonitor.Get())
            {
                ulong totalRam = Convert.ToUInt64(objram["TotalVisibleMemorySize"]);    //общая память ОЗУ
                ulong busyRam = totalRam - Convert.ToUInt64(objram["FreePhysicalMemory"]);         //занятная память = (total-free)
                res = ( (busyRam * 100) / totalRam);
                Console.WriteLine(((busyRam * 100) / totalRam));       //вычисляем проценты занятой памяти
            }
            // testViewModel.TestModel.DataList =  new List < Keyvalue >{ new Keyvalue() { Key = DateTime.Now.ToString("h:mm:ss tt"), Value =  10 } };
            //   tempList.Add(new Keyvalue() { Key = DateTime.Now.ToString("h:mm:ss tt"), Value = res });
            list.Add( new Keyvalue() { Key = DateTime.Now.ToString("h:mm:ss tt"), Value = res } );
           // list.Add(new Keyvalue() { Key = DateTime.Now.ToString("h:mm:ss tt"), Value = 91 });
            //list.Add(new Keyvalue() { Key = DateTime.Now.ToString("h:mm:ss tt"), Value = 100 });

         

            //  testViewModel.Add(GroupBoxDynamicChart, new Keyvalue() { Key = DateTime.Now.ToString("h:mm:ss tt"), Value = res });
            //  testViewModel.TestModel.DataList = testViewModel.TestModel;

            //   this.DataContext = testViewModel;

        }

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // This handler is not executed on the gui thread so
            // you'll have to marshal the call to the gui thread
            // and then update your property.

            //var grabber = new CpuInfoGrabber();
            //var data = grabber.CpuPerUsed();
            Application.Current.Dispatcher.Invoke(() => ProcessesListView.ItemsSource = from p in processesThreds.GetActiveProcesses() orderby p.ProcessName select p);


            if (selectedthread != null)
            {


                int selecteditem = (from p in processesThreds.GetActiveProcesses() where p.Id == selectedthread.Id select p.Id).FirstOrDefault();

                IEnumerable<Process> arr = null;// = from p in processesThreds.GetActiveProcesses() orderby p.ProcessName select p;
                Application.Current.Dispatcher.Invoke(() => arr = (from p in processesThreds.GetActiveProcesses() orderby p.ProcessName select p));

                var f = arr.ToArray();
                int res = -1;
                for (int i = 0; i < f.Length; i++)
                {
                    if (selecteditem == f[i].Id)
                    {
                        res = i;
                        break;
                    }
                }

                Application.Current.Dispatcher.Invoke(() => ProcessesListView.SelectedIndex = res);
            }
        }


        public  void Factorial()
        {
            this.Dispatcher.Invoke(new Action(
                             delegate () { ProcessesListView.ItemsSource = from p in processesThreds.GetActiveProcesses() orderby p.ProcessName select p; }));
        }

        //        private void UpdateData1()
        //        {
        //            ProcessesThreds processesThreds = new ProcessesThreds();
        //            ProcessesListView.ItemsSource = processesThreds.GetActiveProcesses();
        //        }
        //        private void UpdateData(object sender, ElapsedEventArgs e)
        //        {
        //            Thread.Sleep(8000);
        //            this.Dispatcher.Invoke(new Action(
        //   delegate ()
        //   {
        //       ProcessesListView.ItemsSource = from p in processesThreds.GetActiveProcesses() orderby p.ProcessName select p;
        //   }
        //));

        //            //  ProcessesThreds processesThreds = new ProcessesThreds();
        //            // ProcessesListView.ItemsSource = processesThreds.GetActiveProcesses();
        //        }

        async void Async(object sender, ElapsedEventArgs e)
        {
          
          await Task.Run(() => Factorial());
        }

       

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
          Process s =  ProcessesListView.SelectedItem  as Process;
            s.Kill();
            ProcessesListView.ItemsSource = from p in processesThreds.GetActiveProcesses() orderby p.ProcessName select p;

        }

        private void UpdateItems(object sender, RoutedEventArgs e)
        {
            ProcessesListView.ItemsSource = from p in processesThreds.GetActiveProcesses() orderby p.ProcessName select p;


        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void open(object sender, RoutedEventArgs e)
        {
            string txtEditor;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                 txtEditor = (openFileDialog.FileName);
                var calc = Process.Start(openFileDialog.FileName); //C:\Windows\WinSxS\wow64_microsoft-windows-calc_31bf3856ad364e35_10.0.17134.1_none_999337e4b8471fe2
            }

        }

        private void High(object sender, RoutedEventArgs e)
        {
            _timer.Enabled = true;
            _timer.Interval = 500;
        }

        private void Usual(object sender, RoutedEventArgs e)
        {

            //MenuItem a = e.OriginalSource as MenuItem;

            _timer.Enabled = true;
            _timer.Interval = 5000;
        }


        //  MenuItem menu = sender as MenuItem;
        //  Color col = (Color)ColorConverter.ConvertFromString("Black");//new SolidColorBrush(col);

        private void Low(object sender, RoutedEventArgs e)
        {
            

          //  MenuItem m = (MenuItem)sender; // That's the sepcific item that has been clicked.
           // m.Background = new SolidColorBrush(Colors.Violet);
         

            _timer.Enabled = true;
            _timer.Interval = 10000;
        }

        private void Suspended(object sender, RoutedEventArgs e)
        {
           
            _timer.Enabled = false;
        }

        Process selectedthread;
        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProcessesListView.SelectedItem != null)
            {
                selectedthread = ProcessesListView.SelectedItem as Process;
            }
        }
    }
}
