using System;
using Hospital.Core.Entities;
using Hospital.Core.Interfaces;
namespace Hospital.Infrastructure.Repositories
{
    public class PatientRepositoryMemory : IRepository<Patient>
    {
        private static List<Patient> _patients = new();
        private static int _idCounter = 1;

        public void Add(Patient patient)
        {
            patient.PatientId = _idCounter++;
            _patients.Add(patient);
        }

        public IEnumerable<Patient> GetAll()
        {
            return _patients;
        }

        public Patient GetById(int id)
        {
            return _patients.FirstOrDefault(p => p.PatientId == id);
        }

        public void Update(Patient patient)
        {
            var existing = GetById(patient.PatientId);
            if (existing != null)
            {
                existing.Name = patient.Name;
                existing.Age = patient.Age;
                existing.Condition = patient.Condition;
                existing.AppointmentDate = patient.AppointmentDate;
                existing.DoctorId = patient.DoctorId;
            }
        }

        public void Delete(int id)
        {
            var patient = GetById(id);
            if (patient != null)
            {
                _patients.Remove(patient);
            }
        }
    }
}
