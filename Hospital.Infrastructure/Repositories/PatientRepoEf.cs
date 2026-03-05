using System;
using System.Runtime.CompilerServices;
using Hospital.Core.Entities;
using Hospital.Core.Exceptions;
using Hospital.Core.Interfaces;
using Hospital.Infrastructure.Data;

namespace Hospital.Infrastructure.Repositories
{
    public class PatientRepoEf : IRepository<Patient>
    {

        private readonly AppDbContext _context;
        public PatientRepoEf(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Patient entity)
        {
            _context.Patients.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var patient = GetById(id);
            if (patient == null)
            {
                throw new PatientNotFoundException("patient not found");
            }
            _context.Patients.Remove(patient);
            _context.SaveChanges();
        }

        public IEnumerable<Patient> GetAll()
        {
            return _context.Patients.ToList();
        }
        public Patient GetById(int id)
        {
            return _context.Patients.FirstOrDefault(s => s.PatientId == id);
        }

        public void Update(Patient entity)
        {
            _context.Patients.Update(entity);
            _context.SaveChanges();
        }
    }
}
