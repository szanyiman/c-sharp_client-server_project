namespace DoctorApi_AssistantClient.DataProviders
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using DoctorApiCommon.Models;
    using Newtonsoft.Json;

#pragma warning disable LRT001 // There is only one restricted namespace
    public class PatientDataProvider
    {
        private const string Url = "http://localhost:5000/api/patient";

        public static IEnumerable<Patient> GetPatient()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(Url).Result;

                if (response.IsSuccessStatusCode)
                {
                    var rawData = response.Content.ReadAsStringAsync().Result;
                    var patients = JsonConvert.DeserializeObject<IEnumerable<Patient>>(rawData);
                    return patients;
                }

                throw new InvalidOperationException(response.StatusCode.ToString());
            }
        }

        public static void CreatePatient(Patient patient)
        {
            using (var client = new HttpClient())
            {
                var rawData = JsonConvert.SerializeObject(patient);
                var content = new StringContent(rawData, Encoding.UTF8, "application/json");

                var response = client.PostAsync(Url, content).Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException(response.StatusCode.ToString());
                }
            }
        }

        public static void UpdatePatient(Patient person)
        {
            using (var client = new HttpClient())
            {
                var rawData = JsonConvert.SerializeObject(person);
                var content = new StringContent(rawData, Encoding.UTF8, "application/json");

                var response = client.PutAsync(Url, content).Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException(response.StatusCode.ToString());
                }
            }
        }

        public static void DeletePatient(long id)
        {
            using (var client = new HttpClient())
            {
                var response = client.DeleteAsync($"{Url}/{id}").Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException(response.StatusCode.ToString());
                }
            }
        }

    }
#pragma warning restore LRT001 // There is only one restricted namespace
}