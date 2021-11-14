using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinUI.Models;
using WinUI.Services;

namespace WinUI
{
    public partial class Main : Form
    {
        private readonly IUserService _userService;
        private readonly IUserSubscriptionService _userSubscriptionService;
        public Main(IUserService userService, IUserSubscriptionService userSubscriptionService)
        {
            _userService = userService;
            _userSubscriptionService = userSubscriptionService;
            InitializeComponent();
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            HideElements();
            textBox1.Text = $"{await _userService.GetNumberOfUsersAsync()} korisnika";
            textBox1.Show();

        }
        private void HideElements()
        {
            textBox1.Hide();
            dataGridView1.Hide();
            richTextBox1.Hide();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            HideElements();
            dataGridView1.DataSource = (await _userSubscriptionService.GetUserSubscriptionsAsync()).Select(x => new {
                Id = x.Id,
                DatumPlacanja = x.PaymentDate,
                DatumKraja = x.EndDate
            }).ToList();
            dataGridView1.Show();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            HideElements();
            richTextBox1.Text = "";
            var numberOfUserSubscriptionsPerMonths = await _userSubscriptionService.GetProfitForUserSubscriptionsForYear(dateTimePicker1.Value.Year);
            for (int i = 0; i < numberOfUserSubscriptionsPerMonths.Count ; i++)
            {
                richTextBox1.Text = $"{richTextBox1.Text}{Environment.NewLine}{new DateTime(1, i+1, 1).ToString("MMM", CultureInfo.InvariantCulture)}: {numberOfUserSubscriptionsPerMonths[i].Number}";
            }
            richTextBox1.Show();
        }
    }
}
