using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Xamarin.Forms;

namespace AreaSelectorSample
{
    public partial class MainPage : ContentPage
    {
        private List<AreaModel> _provinceList;

        private List<AreaModel> _cityList;

        private List<AreaModel> _areaList;

        public MainPage()
        {
            InitializeComponent();
            InitializeArea();
        }

        private async void InitializeArea()
        {
            HttpWebRequest request = WebRequest.CreateHttp("https://apis.map.qq.com/ws/district/v1/list?key=T5PBZ-NIACI-ACPG6-57ZG3-6XO5E-IAFL6");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
            JToken result = JObject.Parse(await streamReader.ReadToEndAsync()).SelectToken("result");
            _provinceList = JsonConvert.DeserializeObject<List<AreaModel>>(result[0].ToString());
            _cityList = JsonConvert.DeserializeObject<List<AreaModel>>(result[1].ToString());
            _areaList = JsonConvert.DeserializeObject<List<AreaModel>>(result[2].ToString());
            Picker.ItemsSource = _provinceList;
            Picker2.ItemsSource = _cityList.Where(x => x.Id.StartsWith(_provinceList[0].Id.Remove(2))).ToList();
            Picker.SelectedIndex = 0;
        }

        private void Picker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = (Picker)sender;
            if (picker.SelectedIndex > -1)
            {
                string id = ((List<AreaModel>)picker.ItemsSource)[picker.SelectedIndex].Id.Remove(2);
                Picker2.ItemsSource = _cityList.Where(x => x.Id.StartsWith(id)).ToList();
                Picker2.SelectedIndex = 0;
            }
        }

        private void Picker2_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = (Picker)sender;
            if (picker.SelectedIndex > -1)
            {
                string id = ((List<AreaModel>)picker.ItemsSource)[picker.SelectedIndex].Id.Remove(4);
                List<AreaModel> areaList = _areaList.Where(x => x.Id.StartsWith(id)).ToList();
                Picker3.ItemsSource = areaList;
                if (areaList.Any())
                {
                    Picker3.SelectedIndex = 0;
                }
                else
                {
                    Label.Text = Picker.SelectedItem + "-" + picker.SelectedItem;
                }
            }
        }

        private void Picker3_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Label.Text = Picker.SelectedItem + "-" + Picker2.SelectedItem + "-" + ((Picker)sender).SelectedItem;
        }
    }
}
