using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using DoctorApi_Common.Models;
using DoctorApi_Server.Repositories;

namespace DoctorApi_Server.Controllers
{
    [ApiController]
    [Route("api/patients")]
    public class PatientController : Controller
    {
        [HttpGet]
        public ActionResult<IEnumerable<Patient>> Get()
        {
            var patients = PatientRepository.GetPatients();
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public ActionResult<Patient> Get(int id)
        {
            var patients = PatientRepository.GetPatients();

            var patient = patients.FirstOrDefault(x => x.Id == id);

            if (patient != null)
            {
                return Ok(patient);
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult Post([FromBody]Patient patient)
        {
            var patients = PatientRepository.GetPatients().ToList();

            patient.Id = GetNewId(patients);
            patients.Add(patient);

            PatientRepository.StorePatients(patients);
            return Ok();
        }

        [HttpPut]
        public ActionResult Put([FromBody] Patient patient)
        {
            var patients = PatientRepository.GetPatients().ToList();
            var patientToUpdate = patients.FirstOrDefault(p => p.Id == patient.Id);

            if (patientToUpdate != null)
            {
                patientToUpdate.Diagnosis = patient.Diagnosis;
                PatientRepository.StorePatients(patients);
                return Ok();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var patients = PatientRepository.GetPatients().ToList();
            var patientToUpdate = patients.FirstOrDefault(p => p.Id == id);

            if (patientToUpdate != null)
            {
                patients.Remove(patientToUpdate);
                PatientRepository.StorePatients(patients);
                return Ok();
            }

            return NotFound();
        }


        private long GetNewId(IEnumerable<Patient> patients)
        {
            long newId = 0;
            foreach (var patient in patients)
            {
                if (newId < patient.Id)
                {
                    newId = patient.Id;
                }
            }

            return newId + 1;
        }
    }
}