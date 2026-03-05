using System;
using System.Runtime.CompilerServices;
using Hospital.Core.Entities;
using Hospital.Infrastructure.Repositories;
using Hospital.Core.Exceptions;
using Hospital.Core.Interfaces;
namespace Hospital.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IRepository<Doctor> _doctorRepo;

        public DoctorService(IRepository<Doctor> doctorRepositoryMemory)
        {
            _doctorRepo = doctorRepositoryMemory;
        }

        public void AddDoctor(Doctor doctor)
        {
            if (string.IsNullOrWhiteSpace(doctor.Name))
                throw new InvalidDoctorException("Doctor name cannot be empty.");

            if (string.IsNullOrWhiteSpace(doctor.Specialization))
                throw new InvalidDoctorException("Specialization cannot be empty.");

            if (doctor.ConsultationFee <= 0)
                throw new InvalidDoctorException("Consultation fee must be greater than zero.");
            _doctorRepo.Add(doctor);
        }

        public IEnumerable<Doctor> GetAllDoctors()
        {
            return _doctorRepo.GetAll();
        }

        public Doctor GetDoctorById(int id)
        {
            var doctor = _doctorRepo.GetById(id);
            if (doctor == null)
            {
                throw new DoctorNotFoundException("Doctor not found.");
            }
            return doctor;
        }
        public void UpdateDoctor(Doctor doctor)
        {
            var existing = _doctorRepo.GetById(doctor.DoctorId);

            if (existing == null)
                throw new DoctorNotFoundException("Doctor not found.");

            _doctorRepo.Update(doctor);
        }
        public void DeleteDoctor(int id)
        {
            var existing = _doctorRepo.GetById(id);
            if (existing == null)
            {
                throw new DoctorNotFoundException("Doctor Not found");
            }
            _doctorRepo.Delete(id);
        }
    }
}
