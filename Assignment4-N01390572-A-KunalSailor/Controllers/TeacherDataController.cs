using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Assignment4_N01390572_A_KunalSailor.Models;
using Microsoft.AspNetCore.Cors;
using MySql.Data.MySqlClient;

namespace Assignment4_N01390572_A_KunalSailor.Controllers
{
    //Referenced from BLOG-PROJECT 4,5 
    //Name: Christine Bittle
    //FROM WHERE: GitHub Repository
    //Date: December 03, 2020
    //Used for: Add and Delete functionality for users
    public class TeacherDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext school = new SchoolDbContext();

        //This Controller Will access the teachers table of our school database.
        /// <summary>
        /// Returns a list of Teacher in the system
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <returns>
        /// A list of teachers (first names and last names)
        /// </returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey = null)
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teachers
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string employeenumber = ResultSet["employeenumber"].ToString(); ;
             

                // This technique will work,
                //DateTime TeacherJoinDate = (DateTime)ResultSet["teacherjoindate"];

                // This technique is safer!
                DateTime TeacherJoinDate;
                DateTime.TryParse(ResultSet["hiredate"].ToString(), out TeacherJoinDate);

                Teacher AddTeacher = new Teacher();
                AddTeacher.TeacherId = TeacherId;
                AddTeacher.TeacherFname = TeacherFname;
                AddTeacher.TeacherLname = TeacherLname;
                AddTeacher.employeenumber = employeenumber;

                //Add the Teacher Name to the List
                Teachers.Add(AddTeacher);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of teacher names
            return Teachers;
        }



        /// <summary>
        /// Finds an teacher from the database
        /// </summary>
        /// <param name="id">The id to match against a primary key record in the MySQL Database</param>
        /// <returns>An teacher object</returns>
        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers where teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string employeenumber = ResultSet["employeenumber"].ToString();

                // This technique will work,
                //DateTime teacherJoinDate = (DateTime)ResultSet["teacherjoindate"];

                DateTime TeacherJoinDate;
                DateTime.TryParse(ResultSet["hiredate"].ToString(), out TeacherJoinDate);

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.employeenumber = employeenumber;
               
            }
            Conn.Close();

            return NewTeacher;
        }


        /// <summary>
        /// Removes an Teacher from the database
        /// </summary>
        /// <param name="id">The ID of the teacher to remove</param>
        /// <example>POST : /api/TeacherData/DeleteTeacher/3</example>
        /// <returns>Does not return anything.</returns>
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();


        }

        /// <summary>
        /// Adds an Teacher to the MySQL Database.
        /// </summary>
        /// <param name="NewTeacher">An object with fields that map to the columns of the teacher's table. Non-Deterministic.</param>
        /// <example>
        /// POST api/TeacherData/AddTeacher
        /// </example>
        [HttpPost]
        public void AddTeacher([FromBody] Teacher NewTeacher)
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            Debug.WriteLine(NewTeacher.TeacherFname);

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "insert into teachers (teacherfname, teacherlname, employeenumber) values (@TeacherFname,@TeacherLname,@employeenumber)";
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@employeenumber", NewTeacher.employeenumber);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();



        }

    }
}
