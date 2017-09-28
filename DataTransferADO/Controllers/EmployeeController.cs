using DataTransferADO.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataTransferADO.Controllers
{
    public class EmployeeController : Controller
    {
        private SqlConnection con;
        public void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["myConnectionString"].ToString();
            con = new SqlConnection(constr);
        }

        // GET: Employee
        [HttpGet]
        public ActionResult AddEmployee()
        {
            return View();
        }

        // POST: Employee
        [HttpPost]
        public ActionResult AddEmployee(EmpModel Emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    b1 EmpRepo = new b1();

                    if (EmpRepo.AddEmployees(Emp))
                    {
                        ViewBag.Message = "Employee details added successfully";
                    }
                }

                return View();
            }
            catch
            {
                return View();
            }
        }
        //To Add Employee details
        public class b1:EmployeeController
        {
            public bool AddEmployees(EmpModel obj)
            {

                connection();
                SqlCommand com = new SqlCommand("addEmployee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Name", obj.Name);
                com.Parameters.AddWithValue("@City", obj.City);
                com.Parameters.AddWithValue("@Address", obj.Address);
                con.Open();
                int i = com.ExecuteNonQuery();
                con.Close();
                if (i >= 1)
                {

                    return true;

                }
                else
                {

                    return false;
                }
            }
        }
    }
}