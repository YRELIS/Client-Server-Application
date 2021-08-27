using ApplicationTZ.Core.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Windows;
using System.Windows.Controls;

namespace ClientWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Phone> phones = new ObservableCollection<Phone>();
        public MainWindow()
        {
            InitializeComponent();
            ListViewProducts.ItemsSource = phones;
            foreach (var item in GetPhones())
            {
                phones.Add(item);
            }
        }
        /// <summary>
        /// Return all items
        /// </summary>
        /// <returns></returns>
        private List<Phone> GetPhones()
        {
            List<Phone> listPhone = new List<Phone>();
            try
            {
                WebRequest request = WebRequest.Create("https://localhost:44318/Phone");
                WebResponse response = request.GetResponse();
                string strReader = "";
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        strReader = reader.ReadToEnd();
                    }
                }
                JArray joResponse = JArray.Parse(strReader);
                listPhone = new List<Phone>();
                foreach (var item in joResponse)
                {
                    var objId = item["Id"];
                    var objCreatedOn = item["CreatedOn"];
                    var objModel = item["Model"];
                    var objUpdatedOn = item["UpdatedOn"];
                    var objbase64Image = item["base64Image"];
                    Guid id = new Guid(objId.ToString());
                    DateTime createdOn = DateTime.Parse(objCreatedOn.ToString());
                    string model = objModel.ToString();
                    DateTime updatedOn = DateTime.Parse(objUpdatedOn.ToString());
                    string baseImage = objbase64Image.ToString();
                    listPhone.Add(new Phone(id, createdOn, model, updatedOn, baseImage));
                }
                response.Close();
                return listPhone;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Server error: {ex.Message}");
            }
            return listPhone;
        }
        /// <summary>
        /// Button method adding item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_Insert(object sender, RoutedEventArgs e)
        {
            InsertPhone ip = new InsertPhone();
            ip.Notify += Ip_Activated;
            ip.Show();
        }

        private void Ip_Activated(Phone phone)
        {
            phones.Add(phone);
        }
        /// <summary>
        /// Button method change item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_Edit(object sender, RoutedEventArgs e)
        {
            Guid guid = Guid.Empty;
            Button button = (Button)sender;
            guid = new Guid( button.DataContext.ToString());
            EditPhone ep = new EditPhone(guid);
            ep.Notify += Ep_Activated;
            ep.Show();
        }

        private void Ep_Activated(Phone phone)
        {
            phones.Clear();
            foreach (var item in GetPhones())
            {
                phones.Add(item);
            }
        }
        /// <summary>
        /// Button method Delete item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_Delete(object sender, RoutedEventArgs e)
        {
            Guid guid = Guid.Empty;
            Button button = (Button)sender;
            List<Phone> list = new List<Phone>();
            list.Add((Phone)button.DataContext);
            guid = list.First().Id;
            WebRequest request = WebRequest.Create("https://localhost:44318/Phone/DeleteByID?");
            request.Method = "POST";
            string sName = string.Format("guid={0}", HttpUtility.UrlEncode(guid.ToString()));
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(sName);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
            phones.Remove(list.First());
        }
        /// <summary>
        /// Button method sorting by model up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_SortUp(object sender, RoutedEventArgs e)
        {
            List<Phone> ph = new List<Phone>();
            foreach (var item in phones)
            {
                ph.Add(item);
            }
            phones.Clear();
            var sortedUsers = from u in ph
                              orderby u.Model ascending
                              select u;

            foreach (var item in sortedUsers)
            {
                phones.Add(item);
            }
        }
        /// <summary>
        /// Button method sorting by model down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_SortDown(object sender, RoutedEventArgs e)
        {
            List<Phone> ph = new List<Phone>();
            foreach (var item in phones)
            {
                ph.Add(item);
            }
            phones.Clear();
            var sortedUsers = from u in ph
                              orderby u.Model descending
                              select u;

            foreach (var item in sortedUsers)
            {
                phones.Add(item);
            }
        }
    }
}
