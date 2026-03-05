using System;
using Hospital.Core.Entities;
using Hospital.Core.Exceptions;
using Hospital.Core.Interfaces;
using Hospital.Infrastructure.Data;

namespace Hospital.Infrastructure.Repositories
{
    public class DoctorRpoEF : IRepository<Doctor>
    {
        private readonly AppDbContext _context;
        public DoctorRpoEF(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
        }
        public IEnumerable<Doctor> GetAll()
        {

            return _context.Doctors.ToList();
        }
        public Doctor GetById(int id)
        {
            return _context.Doctors.FirstOrDefault(d => d.DoctorId == id);
        }
        public void Update(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var doctor = GetById(id);
            if (id == null)
            {
                throw new DoctorNotFoundException("Doctor not found");
            }
            _context.Doctors.Remove(doctor);
            _context.SaveChanges();
        }
    }
}
