using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Assignment4_N01390572_A_KunalSailor.Models
{
    public class SchoolDbContext
    {
        //These are readonly "secret" properties. 
        //Only the SchoolDBContext class can use them.
        //Change these to match your own local blog database!
        private static string User { get { return "root"; } }
        private static string Password { get { return ""; } }
        private static string Database { get { return "school"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }

        //ConnectionString is a series of credentials used to connect to the database.
        protected static string ConnectionString
        {
            get
            {
                return "server = " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password;
            }
        }
        //This is the method we actually use to get the database!
        /// <summary>
        /// Returns a connection to the blog database.
        /// </summary>
        /// <example>
        /// private SchoolDBContext School = new SchoolDBContext();
        /// MySqlConnection Conn = School.AccessDatabase();
        /// </example>
        /// <returns>A MySqlConnection Object</returns>
        public MySqlConnection AccessDatabase()
        {
            //We are instantiating the MySqlConnection Class to create an object
            //the object is a specific connection to our blog database on port 3307 of localhost
            return new MySqlConnection(ConnectionString);
        }
    }
}