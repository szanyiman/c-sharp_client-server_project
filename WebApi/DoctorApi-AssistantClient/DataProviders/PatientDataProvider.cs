using DoctorApi_Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DoctorApi_AssistantClient.DataProviders
{
    public class PatientDataProvider
    {
        private const string _url = "http://localhost:5000/api/patient";

        public static IEnumerable<Patient> GetPatient()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(_url).Result;

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
                var content = new StringContent(rawData,Encoding.UTF8,"application/json");

                var response = client.PostAsync(_url, content).Result;
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

                var response = client.PutAsync(_url, content).Result;
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
                var response = client.DeleteAsync($"{_url}/{id}").Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException(response.StatusCode.ToString());
                }
            }
        }

    }
}