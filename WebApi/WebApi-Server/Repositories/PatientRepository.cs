using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using DoctorApi_Common.Models;

namespace DoctorApi_Server.Repositories
{
    public static class PatientRepository
    {
        private const string fileName = "Patients.json";

        public static IEnumerable<Patient> GetPatients()
        {
            if (File.Exists(fileName))
            {
                var rawDate = File.ReadAllText(fileName);
                var patients = JsonSerializer.Deserialize<IEnumerable<Patient>>(rawDate);
                return patients;
            }

            return new List<Patient>();
        }

        public static void StorePatients(IEnumerable<Patient> patients)
        {
            var rawData = JsonSerializer.Serialize(patients);
            File.WriteAllText(fileName, rawData);
        }
    }
}