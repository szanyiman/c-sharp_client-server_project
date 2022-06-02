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
using DoctorApi_AssistantClient;
using DoctorApi_AssistantClient.DataProviders;

namespace DoctorApi_DoctorClient
{
    /// <summary>
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        private readonly Patient _patient;
        public ClientWindow(Patient patient)
        {
            InitializeComponent();

            if (patient != null)
            {
                _patient = patient;

                NameTextBox.Content = _patient.Name;
                AddressTextBox.Content = _patient.Address;
                SocialSecNumTextBox.Content = _patient.SocialSecurityNumber;
                ComplaintTextBox.Content = _patient.Complaint;

                if (_patient.Diagnosis == null)
                {

                }
                else
                {
                    DiagnosisTextBox.Text = _patient.Diagnosis;
                }
            }
        }



        private void UpdateButton_Click(object sender, RoutedEventArgs args)
        {
            _patient.Diagnosis = DiagnosisTextBox.Text;
            PatientDataProvider.UpdatePatient(_patient);
            DialogResult = true;
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Biztos ki akarod törölni?", "Kérdés", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                DialogResult = true;
                PatientDataProvider.DeletePatient(_patient.Id);
                Close();

            }
        }
    }
}
