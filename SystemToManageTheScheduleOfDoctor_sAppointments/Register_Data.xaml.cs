using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace SystemToManageTheScheduleOfDoctor_sAppointments
{
    /// <summary>
    /// Logika interakcji dla klasy Register_Data.xaml
    /// </summary>
    public partial class Register_Data : Window
    {
        private int _userId;

        public Register_Data(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private void saveDataOne(object sender, RoutedEventArgs e)
        {
            string firstName = FirstName.Text;
            string lastName = LastName.Text;
            string pessel = Pessel.Text;
            string numberPhone = Phone.Text;
            string e_Mail = Mail.Text;
            string bloodType = ((ComboBoxItem)BloodType.SelectedItem).Content.ToString();
            DateTime? birthDate = BirthDatePicker.SelectedDate;


            if (string.IsNullOrWhiteSpace(firstName))
                MessageBox.Show("Imię nie może być puste.");
            else if (string.IsNullOrWhiteSpace(lastName))
                MessageBox.Show("Nazwisko nie może być puste.");
            else if (pessel.Length != 11 || !long.TryParse(pessel, out long _))
                MessageBox.Show("PESEL musi być poprawnym numerem o długości 11 cyfr.");
            else if (string.IsNullOrEmpty(numberPhone))
                MessageBox.Show("Numer telefonu nie może być pusty.");
            else if (string.IsNullOrWhiteSpace(e_Mail))
                MessageBox.Show("E-mail nie może być pusty.");
            else if (string.IsNullOrWhiteSpace(bloodType))
                MessageBox.Show("Typ krwi nie może być pusty.");
            else if (!birthDate.HasValue)
                MessageBox.Show("Data urodzenia musi być wybrana.");
            else
            {
                SaveToDataBase(firstName, lastName, pessel, numberPhone, e_Mail, bloodType, birthDate.Value);
                MessageBox.Show("Dane zapisane!");
                ClearTexBox();
                var mainWinwod = new MainWindow();
                this.Close();
                mainWinwod.Show();
            }
        }

        private void SaveToDataBase(string firstName, string lastName, string pessel, string numberPhone,
    string e_Mail, string bloodType, DateTime birthDate)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SDoctor_sDB"].ConnectionString;
            string query = "INSERT INTO [dbo].[ClientData] (UserId, firstName, lastName, pessel, numberPhone, e_Mail, bloodType, birthDate) VALUES (@UserId, @FirstName, @LastName, @Pessel, @NumberPhone, @E_Mail, @BloodType, @BirthDate)";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, sqlConnection);

   
                cmd.Parameters.AddWithValue("@UserId", _userId);
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@Pessel", pessel);
                cmd.Parameters.AddWithValue("@NumberPhone", numberPhone);
                cmd.Parameters.AddWithValue("@E_Mail", e_Mail);
                cmd.Parameters.AddWithValue("@BloodType", bloodType);
                cmd.Parameters.AddWithValue("@BirthDate", birthDate);

                try
                {
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Wystąpił błąd: {ex.Message}");
                }
            }
        }

        private void ClearTexBox()
        {
            FirstName.Clear();
            LastName.Clear();
            Pessel.Clear();
            Phone.Clear();
            Mail.Clear();
            bloodtype = null;
            BirthDatePicker.SelectedDate = null;
        }
    }
}
