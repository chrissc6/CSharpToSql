using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpToSql
{
    class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsReviewer { get; set; }
        public bool IsAdmin { get; set; }

        public static bool InsertUser(User user)
        {
            var connStr = @"server=STUDENT05\SQLEXPRESS; database=PrsDb; trusted_connection=true;";
            var Connection = new SqlConnection(connStr);
            Connection.Open();

            if (Connection.State != System.Data.ConnectionState.Open)
            {
                Console.WriteLine("Connection did not open");
                return false;
            }

            var isReviewer = user.IsReviewer ? 1 : 0;
            var isAdmin = user.IsAdmin ? 1 : 0;
            var sql = $"INSERT into users (Username, Password, Firstname, Lastname, Phone, Email, IsReviewer, IsAdmin)"
                + $"values ('{user.Username}', '{user.Password}', '{user.Firstname}', '{user.Lastname}', '{user.Phone}', '{user.Email}', {isReviewer}, {isAdmin})";
            var cmd = new SqlCommand(sql, Connection);
            var recsAffected = cmd.ExecuteNonQuery();
            Connection.Close();
            return recsAffected == 1;
        }

        public static User GetUserByPrimaryKey(int Id)
        {
            var connStr = @"server=STUDENT05\SQLEXPRESS; database=PrsDb; trusted_connection=true;";
            var Connection = new SqlConnection(connStr);
            Connection.Open();

            if (Connection.State != System.Data.ConnectionState.Open)
            {
                Console.WriteLine("Connection did not open");
                return null;
            }

            var sql = $"SELECT * from Users WHERE Id = {Id};";
            var cmd = new SqlCommand(sql, Connection);
            var reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                Console.WriteLine("Result set has no row.");
                Connection.Close();
                return null;
            }

            reader.Read();

            var user = new User();
            user.Id = (int)reader["Id"];
            user.Username = (string)reader["Username"];
            user.Firstname = (string)reader["Firstname"];
            user.Lastname = (string)reader["Lastname"];
            user.Phone = reader["Phone"] == DBNull.Value ? null : (string)reader["Phone"];
            user.Email = reader["Email"] == DBNull.Value ? null : (string)reader["Email"];
            user.IsReviewer = (bool)reader["IsReviewer"];
            user.IsAdmin = (bool)reader["IsAdmin"];

            Connection.Close();
            return user;
        }

        public static User[] GetAllUsers()
        {
            //connection string (server,database, authentication)
            var connStr = @"server=STUDENT05\SQLEXPRESS; database=PrsDb; trusted_connection=true;"; //at sign used for backslash
            // "uid=sa;pwd=sa;" - could be used instead of trusted connection, but has to be setup in sql

            //connection object
            var Connection = new SqlConnection(connStr);

            //open connection
            Connection.Open();

            //check if opened worked
            if (Connection.State != System.Data.ConnectionState.Open)
            {
                Console.WriteLine("Connection did not open");
                return null;
            }

            //sqlcommand
            var sql = "SELECT * from Users;";
            var cmd = new SqlCommand(sql, Connection);

            //sqldatareader object
            var reader = cmd.ExecuteReader(); //this returns a sqldatareader (for a select), and causes statement to excute
            if (!reader.HasRows)
            {
                Console.WriteLine("Result set has no row.");
                Connection.Close();
                return null;
            }

            var users = new User[100];
            var index = 0;

            while (reader.Read()) //moves pointer to the next vaild row
            {
                var user = new User();
                user.Id = (int)reader["Id"]; //should always reurn the column name
                user.Username = (string)reader["Username"];
                user.Firstname = (string)reader["Firstname"];
                user.Lastname = (string)reader["Lastname"];
                //var fullname = $"{user.Firstname} {user.Lastname}";
                //var Password = (string)reader["Password"];
                user.Phone = reader["Phone"] == DBNull.Value ? null : (string)reader["Phone"];
                user.Email = reader["Email"] == DBNull.Value ? null : (string)reader["Email"];
                user.IsReviewer = (bool)reader["IsReviewer"];
                user.IsAdmin = (bool)reader["IsAdmin"];

                users[index++] = user;
                //index++;

                //Console.WriteLine($"Id = {user.Id}, Firstname = {user.Firstname}, Lastname = {user.Lastname}, Password = {Password}, Phone = {user.Phone}, Email = {user.Email}, IsReviewer = {user.IsReviewer}, IsAdmin = {user.IsAdmin}");

            }

            //for (int i = 0; i < index; i++)
            //{
            //    var user = users[i];
            //    Console.WriteLine($"Id = {user.Id}, Firstname = {user.Firstname}, Lastname = {user.Lastname}"
            //        + $", Phone = {user.Phone}, Email = {user.Email}, IsReviewer = {user.IsReviewer}, IsAdmin = {user.IsAdmin}");
            //}

            //Console.ReadKey();


            //statement to close
            Connection.Close();
            return users;
        }

        //const

        public User()
        { }

        public User(int id, string username, string password, string firstname, string lastname, string phone, string email, bool isReviewewr, bool isAdmin)
        {
            Id = id;
            Username = username;
            Password = password;
            Firstname = firstname;
            Lastname = lastname;
            Phone = phone;
            Email = email;
            IsReviewer = isReviewewr;
            IsAdmin = isAdmin;
        }

        public string ToPrint()
        {
            return $"[ToPrint()] Id={Id}, Username={Username}, Name={Firstname} {Lastname}";
        }
    }
}
