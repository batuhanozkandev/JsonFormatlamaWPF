using System;
using System.Text.Json;
using System.Text.Json.Serialization;
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
using System.Data.OleDb;
using System.Diagnostics;

namespace Gorsel_Programlama_Final_Proje_3
{
    /// <summary>
    /// Hava adında gün, derece ve yeri alabileceğim bir sınıf oluşturdum.
    /// </summary>
    public class Weather
    {
        public string Day { get; set; }
        public string TemperatureCelsius { get; set; }
        public string Place { get; set; }
    }
    public partial class MainWindow : Window
    {  
        /// <summary>
        /// Başlangıç değerleri boş olacak şekilde verilerimi oluşturdum.
        /// </summary>
        string jsonString = "";
        string dateString = "";
        string celciusString = "";
        string place = "";
        /// <summary>
        /// Veritabanı olarak Accessi kullandım.
        /// Bu yüzden Access ile bağlantı kurmamı sağlayacak kodları buraya global olmasına dikkat ederek yazdım.
        /// </summary>
        OleDbConnection db_connection = new OleDbConnection("Provider=Microsoft.ACE.OleDb.12.0; Data Source=JsonFormatlari.accdb");
        OleDbCommand db_command = new OleDbCommand();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /// <summary>
            /// Butona basıldığı zaman TextBoxlardaki değerleri oluşturduğum değerlere atadım.
            /// </summary>
            dateString = input1.Text;
            celciusString = input2.Text;
            place = input3.Text;
            /// <summary>
            /// "Weather" sınıfımdan bir nesne ürettim.
            /// </summary>
            var weatherState = new Weather
            {
                Day = dateString,
                TemperatureCelsius = celciusString,
                Place = place
            };
            /// <summary>
            /// Json Formatımın oluştuğu kod. Kütüphaneden yararlandım.
            /// </summary>
            jsonString = JsonSerializer.Serialize(weatherState);
            jsonText.Text = jsonString;
            try
            {
                /// <summary>
                /// Veritabanına oluşan Json Formatını ekliyorum.
                /// </summary>
                db_connection.Open();
                db_command.Connection = db_connection;
                db_command.CommandText = "INSERT INTO JsonFormatlari(Json) VALUES('"+ jsonString.ToString()+ "')";
                db_command.ExecuteNonQuery();
                MessageBox.Show("Json Formatı başarıyla eklendi!");
            }
                /// <summary>
                /// Herhangi bir hatada hatamı mesaj box ile görüyorum.
                /// </summary>
            catch (Exception hata)
            {
                MessageBox.Show("Ekleme başarısız" + hata.Message.ToString());
            }

        }
        public MainWindow()
        {
            
            InitializeComponent();

        }
        private void jsonText_TextChanged(object sender, TextChangedEventArgs e)
        {
            /// <summary>
            /// Oluşan Json Formatını TextBox'ımda gösteriyorum.
            /// </summary>
            jsonText.Text = jsonString;
        }
        private void input1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
