using Hospital.Core.Entities;
using Hospital.Core.Interfaces;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace Hospital.Infrastructure.Repositories
{
    public class DoctorRepositoryADO : IRepository<Doctor>
    {
        private readonly string _connectionString;

        public DoctorRepositoryADO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Doctor doctor)
        {
            using var connection = new SqlConnection(_connectionString);

            string query = @"INSERT INTO Doctors (Name, Specialization, ConsultationFee)
                             VALUES (@Name, @Spec, @Fee)";

            var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Name", doctor.Name);
            command.Parameters.AddWithValue("@Spec", doctor.Specialization);
            command.Parameters.AddWithValue("@Fee", doctor.ConsultationFee);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public List<Doctor> GetAll()
        {
            var doctors = new List<Doctor>();

            using var connection = new SqlConnection(_connectionString);

            string query = "SELECT * FROM Doctors";

            var command = new SqlCommand(query, connection);

            connection.Open();

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                doctors.Add(new Doctor
                {
                    DoctorId = (int)reader["DoctorId"],
                    Name = reader["Name"].ToString(),
                    Specialization = reader["Specialization"].ToString(),
                    ConsultationFee = (int)reader["ConsultationFee"]
                });
            }

            return doctors;
        }

        public Doctor GetById(int id)
        {
            return null;
        }

        public void Update(Doctor entity)
        {
        }

        public void Delete(int id)
        {
        }

        IEnumerable<Doctor> IRepository<Doctor>.GetAll()
        {
            return GetAll();
        }
    }
}