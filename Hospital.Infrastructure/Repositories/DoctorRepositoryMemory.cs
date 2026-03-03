using System;
using Hospital.Core.Entities;
using Hospital.Core.Interfaces;
namespace Hospital.Infrastructure.Repositories
{
    public class DoctorRepositoryMemory : IRepository<Doctor>
    {
        private static List<Doctor> _doctors = new();
        private static int _idCounter = 1;

        public void Add(Doctor doctor)
        {
            doctor.DoctorId = _idCounter++;
            _doctors.Add(doctor);
        }

        public IEnumerable<Doctor> GetAll()
        {
            return _doctors;
        }

        public Doctor GetById(int id)
        {
            return _doctors.FirstOrDefault(d => d.DoctorId == id);
        }

        public void Update(Doctor doctor)
        {
            var existing = GetById(doctor.DoctorId);
            if (existing != null)
            {
                existing.Name = doctor.Name;
                existing.Specialization = doctor.Specialization;
                existing.ConsultationFee = doctor.ConsultationFee;
            }
        }

        public void Delete(int id)
        {
            var doctor = GetById(id);
            if (doctor != null)
            {
                _doctors.Remove(doctor);
            }
        }
    }
}
