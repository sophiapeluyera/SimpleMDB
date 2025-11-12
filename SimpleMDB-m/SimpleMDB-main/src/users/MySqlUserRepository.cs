using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SimpleMDB
{
    public class MySqlUserRepository : IUserRepository
    {
        private readonly string connectionString;

        public MySqlUserRepository(string connectionString)
        {
            this.connectionString = connectionString;
            // Uncomment the line below if you want to auto-create the table
            // Init();
        }

        private void Init()
        {
            using var dbc = OpenDb();
            if (dbc == null) return;

            using var cmd = dbc.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Users (
                    id INT AUTO_INCREMENT PRIMARY KEY,
                    username NVARCHAR(64) NOT NULL UNIQUE,
                    password NVARCHAR(64) NOT NULL,
                    salt NVARCHAR(4096),
                    role ENUM('Admin', 'User') NOT NULL
                );";
            cmd.ExecuteNonQuery();
        }

        public MySqlConnection? OpenDb()
        {
            var dbc = new MySqlConnection(connectionString);
            try
            {
                dbc.Open();
                Console.WriteLine("Connection successful!");
                return dbc;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection failed: " + ex.Message);
                return null;
            }
        }

        public async Task<PagedResult<User>> ReadAll(int page, int size)
        {
            var dbc = OpenDb();
            if (dbc == null) return new PagedResult<User>(new List<User>(), 0);

            using var countCmd = dbc.CreateCommand();
            countCmd.CommandText = "SELECT COUNT(*) FROM Users";
            int totalCount = Convert.ToInt32(await countCmd.ExecuteScalarAsync());

            using var cmd = dbc.CreateCommand();
            cmd.CommandText = "SELECT * FROM Users LIMIT @offset, @limit";
            cmd.Parameters.AddWithValue("@offset", (page - 1) * size);
            cmd.Parameters.AddWithValue("@limit", size);

            using var rows = await cmd.ExecuteReaderAsync();
            var users = new List<User>();

            while (await rows.ReadAsync())
            {
                users.Add(new User
                {
                    Id = rows.GetInt32("id"),
                    Username = rows.GetString("username"),
                    Password = rows.GetString("password"),
                    Salt = rows.GetString("salt"),
                    Role = rows.GetString("role"),
                });
            }

            return new PagedResult<User>(users, totalCount);
        }

        public async Task<User?> Create(User user)
        {
            var dbc = OpenDb();
            if (dbc == null) return null;

            using var cmd = dbc.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO Users (username, password, salt, role)
                VALUES (@username, @password, @salt, @role);
                SELECT LAST_INSERT_ID();";
            cmd.Parameters.AddWithValue("@username", user.Username);
            cmd.Parameters.AddWithValue("@password", user.Password);
            cmd.Parameters.AddWithValue("@salt", user.Salt);
            cmd.Parameters.AddWithValue("@role", user.Role);

            user.Id = Convert.ToInt32(await cmd.ExecuteScalarAsync());
            return user;
        }

        public async Task<User?> Read(int id)
        {
            var dbc = OpenDb();
            if (dbc == null) return null;

            using var cmd = dbc.CreateCommand();
            cmd.CommandText = "SELECT * FROM Users WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new User
                {
                    Id = reader.GetInt32("id"),
                    Username = reader.GetString("username"),
                    Password = reader.GetString("password"),
                    Salt = reader.GetString("salt"),
                    Role = reader.GetString("role"),
                };
            }

            return null;
        }

        public async Task<User?> Update(int id, User newUser)
        {
            var dbc = OpenDb();
            if (dbc == null) return null;

            using var cmd = dbc.CreateCommand();
            cmd.CommandText = @"
                UPDATE Users SET 
                    username = @username,
                    password = @password,
                    salt = @salt,
                    role = @role
                WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@username", newUser.Username);
            cmd.Parameters.AddWithValue("@password", newUser.Password);
            cmd.Parameters.AddWithValue("@salt", newUser.Salt);
            cmd.Parameters.AddWithValue("@role", newUser.Role);

            return Convert.ToInt32(await cmd.ExecuteNonQueryAsync()) > 0 ? newUser : null;
        }

        public async Task<User?> Delete(int id)
        {
            var dbc = OpenDb();
            if (dbc == null) return null;

            User? user = await Read(id);
            if (user == null) return null;

            using var cmd = dbc.CreateCommand();
            cmd.CommandText = "DELETE FROM Users WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", id);

            return Convert.ToInt32(await cmd.ExecuteNonQueryAsync()) > 0 ? user : null;
        }

        public async Task<User?> GetUserbyUsername(string username)
        {
            var dbc = OpenDb();
            if (dbc == null) return null;

            using var cmd = dbc.CreateCommand();
            cmd.CommandText = "SELECT * FROM Users WHERE username = @username";
            cmd.Parameters.AddWithValue("@username", username);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new User
                {
                    Id = reader.GetInt32("id"),
                    Username = reader.GetString("username"),
                    Password = reader.GetString("password"),
                    Salt = reader.GetString("salt"),
                    Role = reader.GetString("role"),
                };
            }

            return null;
        }
    }
}

