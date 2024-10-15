using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Configuration;
using System.Data;

namespace SystemToManageTheScheduleOfDoctor_sAppointments
{
    /// <summary>
    /// Logika interakcji dla klasy Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        private SqlConnection sqlConnection = null;
        public Register()
        {
            InitializeComponent();
        }

        private void Register_Btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SDoctor_sDB"].ConnectionString);
                sqlConnection.Open();

                string add_data = "INSERT INTO [dbo].[Client] VALUES(@username, @password)";
                SqlCommand cmd = new SqlCommand(add_data, sqlConnection);

                cmd.Parameters.AddWithValue("@username", username.Text);
                cmd.Parameters.AddWithValue("@password", password.Password);
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
                username.Text = string.Empty;
                password.Password = string.Empty;
                MainWindow w1 = new MainWindow();
                this.Close();
                w1.Show();
            }

            catch
            { 

            }
        }
    }
}
