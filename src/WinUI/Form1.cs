using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinUI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace WinUI
{
    public partial class Form1 : Form
    {
        private readonly IAuthService _authService;
        private readonly Main _main;
        public Form1(IAuthService authService, Main main)
        {
            InitializeComponent();
            _authService = authService;
            _main = main;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            var result = await _authService.LoginAsync(textBox1.Text, textBox2.Text);
            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Pogrešna email adresa ili lozinka");
            }
            else
            {
                Hide();
                _main?.Show();
            }



        }
    }
}
