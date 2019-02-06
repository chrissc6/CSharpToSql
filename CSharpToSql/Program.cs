﻿using System;
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

            const int Id = 4;
            User userpk = User.GetUserByPrimaryKey(Id);
            Console.WriteLine(userpk);

            userpk.Password = "ABCXYZ";
            var updateSuccess = User.UpdateUser(userpk);
            if (updateSuccess)
                Console.WriteLine("Update successful");
            else
            {
                Console.WriteLine("Update failed");
            }
            Console.ReadKey();


            var deleteSuccess = User.DeleteUser(Id);
            //if(deleteSuccess == false)
            if(!deleteSuccess) //better way
            {
                Console.WriteLine("Delete failed");
            }
            deleteSuccess = User.DeleteUser(6);
            if (!deleteSuccess) //better way
            {
                Console.WriteLine("Delete failed on non-existent ID");
            }
            Console.ReadKey();
        }
    }
}
 