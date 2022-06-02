namespace DoctorApi_AssistantClient
{
    using System.Text.RegularExpressions;
    using System.Windows;
    using DoctorApi_AssistantClient.DataProviders;
    using DoctorApiCommon.Models;

    /// <summary>
    /// Interaction logic for AddPatient.xaml
    /// </summary>
#pragma warning disable LRT001 // There is only one restricted namespace
    public partial class AddPatient : Window
    {
        private readonly Patient patient;

        public AddPatient(Patient patient)
        {
            this.InitializeComponent();
        }

        public AddPatient()
        {
            this.patient = new Patient();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.ValidatePatient())
            {
                this.patient.Name = this.NameTextBox.Text;
                this.patient.Address = this.AddressTextBox.Text;
                this.patient.SocialSecurityNumber = this.SocialSecNumTextBox.Text;
                this.patient.Complaint = this.ComplaintTextBox.Text;
                PatientDataProvider.CreatePatient(this.patient);
                this.NameTextBox.Text = null;
                this.AddressTextBox.Text = null;
                this.SocialSecNumTextBox.Text = null;
                this.ComplaintTextBox.Text = null;
            }
        }

        public bool ValidatePatient()
        {
            var regexItem = new Regex(@"^[a-zA-ZíéáüűúóöőÍÉÁÜŰÚÖŐÓ]+[\s][a-zA-ZíéáüűúóöőÍÉÁÜŰÚÖŐÓ]+[\s]?[a-zA-ZíéáüűúóöőÍÉÁÜŰÚÖŐÓ]+$");
            Regex regex = new Regex(@"^[0-9]{3}[\s][0-9]{3}[\s][0-9]{3}$");
            Match match = regex.Match(this.SocialSecNumTextBox.Text);

            if (string.IsNullOrEmpty(this.NameTextBox.Text) || string.IsNullOrWhiteSpace(this.NameTextBox.Text))
            {
                MessageBox.Show("Kérem adja meg a beteg nevét!");
                return false;
            }

            if (!regexItem.IsMatch(this.NameTextBox.Text))
            {
                MessageBox.Show("Nem adott meg helyes nevet!");
                return false;
            }

            if (string.IsNullOrEmpty(this.AddressTextBox.Text) || string.IsNullOrWhiteSpace(this.AddressTextBox.Text))
            {
                MessageBox.Show("Kérem adja meg a beteg lakcímét!");
                return false;
            }

            if (string.IsNullOrEmpty(this.SocialSecNumTextBox.Text) || string.IsNullOrWhiteSpace(this.SocialSecNumTextBox.Text))
            {
                MessageBox.Show("Kérem adja meg a beteg TAJ számát!");
                return false;
            }

            if (!match.Success)
            {
                MessageBox.Show("A TAJ szám nem megfelelő formátumban van írva!");
                return false;
            }

            if (string.IsNullOrEmpty(this.ComplaintTextBox.Text) || string.IsNullOrWhiteSpace(this.ComplaintTextBox.Text))
            {
                MessageBox.Show("Meg kell adni a beteg panaszát röviden.");
                return false;
            }

            return true;
        }
    }
#pragma warning restore LRT001 // There is only one restricted namespace
}
