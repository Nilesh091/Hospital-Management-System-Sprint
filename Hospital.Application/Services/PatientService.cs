using System;
using System.Collections.Generic;
using System.Linq;
using Hospital.Core.Entities;
using Hospital.Core.Interfaces;
using Hospital.Core.Exceptions;

namespace Hospital.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IRepository<Patient> _patientRepository;
        private readonly IRepository<Doctor> _doctorRepository;

        public PatientService(
            IRepository<Patient> patientRepository,
            IRepository<Doctor> doctorRepository)
        {
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
        }

        public void AddPatient(Patient patient)
        {

            if (patient.Age <= 0)
                throw new InvalidOperationException("Age must be greater than zero.");


            if (patient.AppointmentDate.Date < DateTime.Today)
                throw new InvalidOperationException("Appointment date cannot be in the past.");


            var doctor = _doctorRepository.GetById(patient.DoctorId);
            if (doctor == null)
                throw new InvalidDoctorException("Doctor does not exist.");


            _patientRepository.Add(patient);
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return _patientRepository.GetAll();
        }

        public Patient GetPatientById(int id)
        {
            var patient = _patientRepository.GetById(id);

            if (patient == null)
                throw new PatientNotFoundException("Patient not found.");

            return patient;
        }

        public IEnumerable<Patient> FindPatientByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Enumerable.Empty<Patient>();

            return _patientRepository
                .GetAll()
                .Where(p => p.Name != null &&
                            p.Name.ToLower().Contains(name.ToLower()));
        }

        public void UpdatePatient(Patient updatedPatient)
        {
            var existing = _patientRepository.GetById(updatedPatient.PatientId);

            if (existing == null)
                throw new PatientNotFoundException("Patient not found.");


            if (updatedPatient.Age <= 0)
                throw new InvalidOperationException("Age must be greater than zero.");


            if (updatedPatient.AppointmentDate.Date < DateTime.Today)
                throw new InvalidOperationException("Appointment date cannot be in the past.");


            var doctor = _doctorRepository.GetById(updatedPatient.DoctorId);
            if (doctor == null)
                throw new InvalidDoctorException("Doctor does not exist.");

            _patientRepository.Update(updatedPatient);
        }

        public void DeletePatient(int id)
        {
            var existing = _patientRepository.GetById(id);

            if (existing == null)
                throw new PatientNotFoundException("Patient not found.");

            _patientRepository.Delete(id);
        }
    }
}