using BLL.Services;
using DAL.Models;
using Microcharts;
using Xamarin.Forms;

namespace MobileTestApp
{
	public partial class MainPage : ContentPage
	{
	    private IClientService _clientService;
        private Microcharts.Entry[] _entries;
	    private BarChart _barChart;

        public MainPage()
		{
			InitializeComponent();
            _clientService = new ClientService();
		    var model = new TestModel {Name = "Arseni", LastName = "Barsegyan"};
		    BindingContext = model;
            //InitializeChart();
		}

	    //private void InitializeChart()
	    //{
	    //    _entries = new[]
	    //    {
	    //        new Microcharts.Entry(200)
	    //        {
	    //            Label = "January",
	    //            ValueLabel = "200",
	    //            Color = SKColor.Parse("#266489")
	    //        },
	    //        new Microcharts.Entry(400)
	    //        {
	    //            Label = "February",
	    //            ValueLabel = "400",
     //               Color = SKColor.Parse("#68B9C0")
	    //        },
	    //        new Microcharts.Entry(-100)
	    //        {
	    //            Label = "March",
	    //            ValueLabel = "100",
	    //            Color = SKColor.Parse("#90D585")
	    //        }
	    //    };
     //       _barChart = new BarChart { Entries = _entries };
	    //    ChartView.Chart = _barChart;
	    //}

	    //private void FirstChartValue_OnUnfocused(object sender, EventArgs e)
	    //{
	    //    var number = int.Parse(FirstChartValue.Text);
	    //    _entries.ElementAt(0).ValueLabel = number.ToString();
	    //    _barChart.Entries = _entries;
	    //}

	    //private void SecondChartValue_OnUnfocused(object sender, EventArgs e)
	    //{
	    //    var number = int.Parse(SecondChartValue.Text);
     //       _entries.ElementAt(1).ValueLabel = number.ToString();
	    //    _barChart.Entries = _entries;
     //   }

	    //private void ThirdChartValue_OnUnfocused(object sender, EventArgs e)
	    //{
	    //    var number = int.Parse(ThirdChartValue.Text);
     //       _entries.ElementAt(2).ValueLabel = number.ToString();
	    //    _barChart.Entries = _entries;
     //   }
	}
}
