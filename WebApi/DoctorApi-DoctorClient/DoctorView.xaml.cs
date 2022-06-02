using DoctorApi_AssistantClient.DataProviders;
using DoctorApi_Common.Models;
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



namespace DoctorApi_DoctorClient
{
    /// <summary>
    /// Interaction logic for DoctorView.xaml
    /// </summary>
    public partial class DoctorView : Window
    {
        public DoctorView()
        {
            
            InitializeComponent();
            UpdatePatientBox();
          


        }

        private void PatientBox_Selected(object sender, SelectionChangedEventArgs args)
        {
            var selectedPerson = PatientsBox.SelectedItem as Patient;
            if(selectedPerson != null)
            {
                var window = new ClientWindow(selectedPerson);
                if(window.ShowDialog() ?? false)
                {
                    UpdatePatientBox();
                }

            }
        }

        public void UpdatePatientBox()
        {
            
            var patient = PatientDataProvider.GetPatient().ToList();
            PatientsBox.ItemsSource = patient;
        }
        

        private void LoadData_Click (object sender,RoutedEventArgs args)
        {
            UpdatePatientBox();
        }

    }
}
