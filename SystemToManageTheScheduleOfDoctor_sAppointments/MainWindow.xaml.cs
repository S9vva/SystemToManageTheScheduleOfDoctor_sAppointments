using md5_hash;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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

namespace SystemToManageTheScheduleOfDoctor_sAppointments
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SqlConnection sqlConnection = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Register_Btn_Click_Two(object sender, RoutedEventArgs e)
        {
            Register r1  = new Register();
            this.Close();
            r1.Show();
        }

        private void Login_Btn_Click(object sender, RoutedEventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SDoctor_sDB"].ConnectionString);
            sqlConnection.Open();

            string add_data = "SELECT * FROM [dbo].[Client] WHERE username=@username AND password=@password";
            SqlCommand cmd = new SqlCommand(add_data, sqlConnection);

            var password1 = md5_sql_hash.hashPassword(password.Password);

            cmd.Parameters.AddWithValue("@username", username.Text);
            cmd.Parameters.AddWithValue("@password", password1);

            cmd.ExecuteNonQuery();
            int Count = Convert.ToInt32(cmd.ExecuteScalar());

            sqlConnection.Close();
            username.Text = string.Empty;
            password.Password = string.Empty;
            if (Count > 0)
            {
                MainClient mainClient = new MainClient();
                this.Close();
                mainClient.Show();
            }
            else
            {
                MessageBox.Show("Password or Username is not correct");
            }
        }
    }
}
