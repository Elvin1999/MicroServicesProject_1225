using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using UIApp.Entities;

namespace UIApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Contact contact;

        public Contact Contact
        {
            get { return contact; }
            set { contact = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Contact> allContacts;

        public ObservableCollection<Contact> AllContacts
        {
            get { return allContacts; }
            set { allContacts = value; OnPropertyChanged(); }
        }

        HttpClient httpClient = new HttpClient();
        HttpResponseMessage response = new HttpResponseMessage();

        private async void GetAllContacts()
        {
            response = await httpClient.GetAsync($"https://localhost:22950/c");
            var str = await response.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<List<Contact>>(str);
            AllContacts = new ObservableCollection<Contact>(items);
        }

        public MainWindow()
        {
            InitializeComponent();
            Contact = new Contact();
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChangedEventHandler? handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private void GetAll_Click(object sender, RoutedEventArgs e)
        {
            GetAllContacts();
        }

        private async void DeleteContact()
        {
            if (Contact != null && Contact.Id != 0)
            {
                response = await httpClient.DeleteAsync($"https://localhost:22950/c/{Contact.Id}");
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    GetAllContacts();
                    MessageBox.Show("Contact deleted successfully");
                }
            }
            else
            {
                MessageBox.Show("Please select any contact");
            }
            Contact = new Contact();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DeleteContact();
        }

        private async void addContact_Click(object sender, RoutedEventArgs e)
        {
            var myContent = JsonConvert.SerializeObject(Contact);
            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            response = await httpClient.PostAsync("https://localhost:22950/c", byteContent);
            var str = await response.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject<Contact>(str);
            if (item.Id != 0)
            {
                GetAllContacts();
                MessageBox.Show("Added successfully");
            }
            else
            {
                MessageBox.Show("Error in add contact");
            }
        }

        private async void updateContact_Click(object sender, RoutedEventArgs e)
        {
            if (Contact != null && Contact.Id != 0)
            {
                var myContent = JsonConvert.SerializeObject(Contact);
                var buffer = Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                response = await httpClient.PutAsync($"https://localhost:22950/c/{Contact.Id}", byteContent);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    GetAllContacts();
                    MessageBox.Show("Contact Updated successfully");
                }
            }
        }
    }
}
