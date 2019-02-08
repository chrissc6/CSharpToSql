using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient; // need this namespace


namespace CSharpToSql
{
    class Program
    {
        // STUDENT05\SQLEXPRESS
        static void Main(string[] args)
        {
            //insert user

            var user = new User(0, "xxxgdidsjh6", "sd", "userhx", "usesrx", "5135551234", "info@user.com", true, true);
            var returnCode = User.InsertUser(user);
            Console.ReadKey();

            User[] users = User.GetAllUsers();
            foreach (var item in users)
            {
                if (item == null)
                {
                    continue; //skip the rest of the body
                }
                Console.WriteLine(item);
            }

            const int Id = 13;

            //update user

            User userpk = User.GetUserByPrimaryKey(Id);
            Console.WriteLine(userpk);

            userpk.Password = "newpass2";
            var updateSuccess = User.UpdateUser(userpk);
            if (updateSuccess)
                Console.WriteLine("Update successful");
            else
            {
                Console.WriteLine("Update failed");
            }
            Console.ReadKey();


            //delete user

            var deleteSuccess = User.DeleteUser(Id);
            ////if(deleteSuccess == false)
            if (!deleteSuccess) //better way
            {
                Console.WriteLine("Delete failed on non-existent ID");
            }
            else
            {
                Console.WriteLine("Delete success");
            }
            Console.ReadKey();
        }
    }
}
 