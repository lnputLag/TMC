using MySql.Data.MySqlClient;
using System;
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

namespace TMC
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DBConnection.Init();
        }

        private void AddCartridgeButton_Click(object sender, RoutedEventArgs e)
        {
            string model = CartridgeModelTextBox.Text.Trim();
            if (!int.TryParse(CartridgeQuantityTextBox.Text.Trim(), out int quantity))
            {
                MessageBox.Show("Введите корректное количество картриджей.");
                return;
            }

            if (string.IsNullOrWhiteSpace(model))
            {
                MessageBox.Show("Введите модель картриджа.");
                return;
            }

            try
            {
                string query = "INSERT INTO cartridges (model, quantity) VALUES (@model, @quantity)";
                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@model", model),
                    new MySqlParameter("@quantity", quantity)
                };

                int rowsAffected = DBConnection.ExecuteNonQuery(query, parameters); // предполагаем, что методы находятся в классе DbHelper

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Картридж добавлен.");
                    CartridgeModelTextBox.Clear();
                    CartridgeQuantityTextBox.Clear();
                }
                else
                {
                    MessageBox.Show("Не удалось добавить картридж.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
    }
}
