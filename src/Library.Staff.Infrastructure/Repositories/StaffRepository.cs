using Microsoft.Data.SqlClient;
using System.Data;

namespace Library.Staff.Infrastructure.Repositories
{
    public class StaffRepository : Core.Interfaces.IStaffRepository
    {
        private readonly string _connectionString;
        public StaffRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Library.Staff.Core.Entities.Staff> GetAllStaffs()
        {
            var staffs = new List<Library.Staff.Core.Entities.Staff>();

            using SqlConnection con = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("usp_GetAllStaff", con);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                con.Open();
                using SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    staffs.Add(
                        new Library.Staff.Core.Entities.Staff
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = reader["name"] is DBNull ? string.Empty : reader["name"]!.ToString()!,
                            Role = reader["role"] is DBNull ? string.Empty : reader["role"]!.ToString()!
                        });

                }
            }
            catch (Exception)
            {
                return new List<Library.Staff.Core.Entities.Staff>();
                throw;
            }
            finally
            {
                con.Close();
            }
            return staffs;
        }

        public void AddStaff(Library.Staff.Core.Entities.Staff staff)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("usp_AddStaff", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@name", staff.Name);
            cmd.Parameters.AddWithValue("@role", staff.Role);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                con.Close();
            }
        }
    }
}
