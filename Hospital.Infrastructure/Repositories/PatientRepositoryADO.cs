using Hospital.Core.Entities;
using Hospital.Core.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Hospital.Infrastructure.Repositories
{
    public class PatientRepositoryADO : IRepository<Patient>
    {
        private readonly string _connectionString;

        public PatientRepositoryADO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Patient patient)
        {
            using var connection = new SqlConnection(_connectionString);

            string query = @"INSERT INTO Patients 
                            (Name, Age, Condition, AppointmentDate, DoctorId)
                            VALUES (@Name, @Age, @Condition, @Date, @DoctorId)";

            var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Name", patient.Name);
            command.Parameters.AddWithValue("@Age", patient.Age);
            command.Parameters.AddWithValue("@Condition", patient.Condition);
            command.Parameters.AddWithValue("@Date", patient.AppointmentDate);
            command.Parameters.AddWithValue("@DoctorId", patient.DoctorId);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public List<Patient> GetAll()
        {
            var patients = new List<Patient>();

            using var connection = new SqlConnection(_connectionString);

            string query = "SELECT * FROM Patients";

            var command = new SqlCommand(query, connection);

            connection.Open();

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                patients.Add(new Patient
                {
                    PatientId = (int)reader["PatientId"],
                    Name = reader["Name"].ToString(),
                    Age = (int)reader["Age"],
                    Condition = reader["Condition"].ToString(),
                    AppointmentDate = (DateTime)reader["AppointmentDate"],
                    DoctorId = (int)reader["DoctorId"]
                });
            }

            return patients;
        }

        public Patient GetById(int id)
        {
            return null;
        }

        public void Update(Patient entity)
        {
        }

        public void Delete(int id)
        {
        }

        IEnumerable<Patient> IRepository<Patient>.GetAll()
        {
            return GetAll();
        }
    }
}