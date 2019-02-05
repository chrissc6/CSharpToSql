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
            var user = new User(0, "xxx5", "xxx5", "userx", "usesrx", "5135551234", "info@user.com",true,true);
            var returnCode = User.InsertUser(user);

            User[] users = User.GetAllUsers();
            foreach (var item in users)
            {
                if (item == null)
                {
                    continue; //skip the rest of the body
                }
                Console.WriteLine(item.ToPrint());
            }

            User userpk = User.GetUserByPrimaryKey(1);
            Console.WriteLine(userpk.ToPrint());
        }
    }
}
 