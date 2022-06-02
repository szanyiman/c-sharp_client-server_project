using DoctorApi_AssistantClient.DataProviders;
using DoctorApi_Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DoctorApi_AssistantClient
{
    /// <summary>
    /// Interaction logic for AddPatient.xaml
    /// </summary>
    public partial class AddPatient : Window
    {
        public static bool hasSavePatient = false;
        private readonly Patient _patient;
        public AddPatient(Patient patient)
        {
            InitializeComponent();
        }

        public AddPatient()
        {
            _patient = new Patient();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidatePatient())
            {
                _patient.Name = NameTextBox.Text;
                _patient.Address = AddressTextBox.Text;
                _patient.SocialSecurityNumber = SocialSecNumTextBox.Text;
                _patient.Complaint = ComplaintTextBox.Text;
                PatientDataProvider.CreatePatient(_patient);
                NameTextBox.Text = null;
                AddressTextBox.Text = null;
                SocialSecNumTextBox.Text = null;
                ComplaintTextBox.Text = null;
                hasSavePatient = true;
                
            }

        }

        public bool ValidatePatient()
        {
            var regexItem = new Regex(@"^[a-zA-ZíéáüűúóöőÍÉÁÜŰÚÖŐÓ]+[\s][a-zA-ZíéáüűúóöőÍÉÁÜŰÚÖŐÓ]+[\s]?[a-zA-ZíéáüűúóöőÍÉÁÜŰÚÖŐÓ]+$");
            Regex regex = new Regex(@"^[0-9]{3}[\s][0-9]{3}[\s][0-9]{3}$");
            Match match = regex.Match(SocialSecNumTextBox.Text);

            if (string.IsNullOrEmpty(NameTextBox.Text) || string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Kérem adja meg a beteg nevét!");
                return false;
            }
            if (!regexItem.IsMatch(NameTextBox.Text))
            {
                MessageBox.Show("Nem adott meg helyes nevet!");
                return false;
            }
            if (string.IsNullOrEmpty(AddressTextBox.Text) || string.IsNullOrWhiteSpace(AddressTextBox.Text))
            {
                MessageBox.Show("Kérem adja meg a beteg lakcímét!");
                return false;
            }
            if (string.IsNullOrEmpty(SocialSecNumTextBox.Text) || string.IsNullOrWhiteSpace(SocialSecNumTextBox.Text))
            {
                MessageBox.Show("Kérem adja meg a beteg TAJ számát!");
                return false;
            }
            if (!match.Success)
            {
                MessageBox.Show("A TAJ szám nem megfelelő formátumban van írva!");
                return false;
            }
            if (string.IsNullOrEmpty(ComplaintTextBox.Text) || string.IsNullOrWhiteSpace(ComplaintTextBox.Text))
            {
                MessageBox.Show("Meg kell adni a beteg panaszát röviden.");
                return false;
            }
            return true;
        }
    }
}
