using DesktopContactsApp.Classes;
using SQLite;
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
using System.Windows.Shapes;

namespace DesktopContactsApp
{
    /// <summary>
    /// ContactDetailsWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ContactDetailsWindow : Window
    {
        private Contact contact;
        public ContactDetailsWindow(Contact contact)
        {
            InitializeComponent();

            this.contact = contact;
            nameTextBox.Text = contact.Name;
            emailTextBox.Text = contact.Email;
            phoneTextBox.Text = contact.Phone;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you item update?", "Update", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
                return;

            contact.Name = nameTextBox.Text;
            contact.Email = emailTextBox.Text;
            contact.Phone = phoneTextBox.Text;

            using(SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Contact>();
                connection.Update(contact);
            }

            Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you Item Delete?", "Delete", MessageBoxButton.YesNo);
            if(result == MessageBoxResult.Yes)
            {
                using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
                {
                    connection.CreateTable<Contact>();
                    connection.Delete<Contact>(contact.Id);
                }

                Close();
            }
        }
    }
}
