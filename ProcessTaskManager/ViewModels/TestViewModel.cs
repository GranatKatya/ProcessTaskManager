using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Prism.Mvvm;

using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using ProcessTaskManager.Model;
using static System.Net.Mime.MediaTypeNames;




using System.Windows;





using System.Management;

namespace ProcessTaskManager.ViewModels
{
    class TestViewModel : BindableBase
    {
      //  public List<Keyvalue> tempList = new List<Keyvalue>();
      //  private TestModel testModel = new TestModel();

       
          //  GroupBox cb;
        public TestViewModel(GroupBox GroupBoxDynamicChart, List<Keyvalue> v )
        {
            Chart dynamicChart;
            LineSeries lineseries;
            //   cb = GroupBoxDynamicChart;
            // List<Keyvalue> tempList = new List<Keyvalue>();
            //  tempList.Add(v);
            //tempList.Add(new Keyvalue() { Key = "Rs.100", Value = 50 });
            //tempList.Add(new Keyvalue() { Key = "Rs.500", Value = 18 });
            //tempList.Add(new Keyvalue() { Key = "Rs.1000", Value = 25 });
            //tempList.Add(new Keyvalue() { Key = "Rs.2000", Value = 5 });

            ////Bind the sttaic Chart                     
            // TestModel.DataList = tempList;
            ////Create the Dynamic Chart, Bind it and add it into the 2nd GroupBox
            dynamicChart = new Chart();
            lineseries = new LineSeries();
            lineseries.ItemsSource = v;
            lineseries.DependentValuePath = "Value";
            lineseries.IndependentValuePath = "Key";
            dynamicChart.Series.Add(lineseries);
            GroupBoxDynamicChart.Content = dynamicChart;
        }
        //public TestModel TestModel
        //{
        //    get { return testModel; }
        //    set { SetProperty(ref testModel, value); }
        //}
        //public void  Add(GroupBox GroupBoxDynamicChart,Keyvalue v)
        //{
        //    tempList.Add(v);
          

        //   // TestModel.DataList = tempList;
        //}

    }
}

