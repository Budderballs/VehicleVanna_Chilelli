using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VehicleVannaUI_Chilelli
{
    public partial class VVC : Form
    {
        public VVC()
        {
            InitializeComponent();
        }

        private async void submitButton_Click(object sender, EventArgs e)
        {
            VehicleEnum newVehicle = new VehicleEnum(makeText.Text, modelText.Text, yearText.Text, vehicleTypes.SelectedIndex.ToString(), (Int32)listPriceText.Value,
                buyerEmailText.Text, buyerFirstNameText.Text, buyerLastNameText.Text);
            string url2 = "https://vehiclevannafchilelli.azurewebsites.net/api/VehicleVannaF?code=BmlKc5RlasbN6HnIeALZl8gR8MoffpZv2yDeJBC42xgH8OHMBgEKow==";
            //string localURL = "http://localhost:7071/api/VehicleVannaF";
            var client = new HttpClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(url2, newVehicle);
            this.Hide();
            MessageBox.Show(response.Content.ReadAsStringAsync().Result.ToString());
            this.Show();
        }
    }
}