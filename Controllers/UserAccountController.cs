using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using PROGPOETask.Models;
using System.Web.Caching;
using System.Web.UI.WebControls;
using CalculationLibrary;

namespace PROGPOETask.Controllers
{

  
    public class UserAccountController : Controller
    {

      
        public static string username = "";

        public static double hold;
        public static string Gross = "";
        public static string Tax = "";
        public static string Groc = "";
        public static string Water = "";
        public static string Travel = "";
        public static string Phone = "";
        public static string Other = "";
        public static string Rent = "";
        public static string PropPrice = "";
        public static string AccDeposit = "";
        public static string AccRate = "";
        public static string Months = "";
        public static string ModelMake = "";
        public static string VehPrice = "";
        public static string VehDeposit = "";
        public static string VehRate = "";
        public static string VehIns = "";
        public static string IncomeTotal = "";
        public static string AccTotal = "";
        public static string VehTotal = "";
        public static string MonthTotal = "";
        public static string savTotal = "";
        public static string savYears = "";
        public static string savInt = "";
        public static string savMonthly = "";

        // GET: UserAccount
        //  private SqlConnection _sqlConnection = "Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=UsersDatabase;Integrated Security=True;Pooling=False";

        [HttpPost]
        public ActionResult Data(UserAccount acc)
        {
            ViewBag.ErrorMessage = null;

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Saiypher\source\repos\PROGPOETask\App_Data\ProgPoeDatabase.mdf;Integrated Security=True";
              SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();


            SqlCommand scmd = new SqlCommand("Select * from TableUsers where username = '" + acc.username + "' AND  password = '" + acc.password + "'");

            scmd.Connection = conn;

            SqlDataReader sdr ; 

            sdr = scmd.ExecuteReader();

            if (sdr.Read())
            {
                 username = acc.username;


                TempData["Message"] = "Welcome";
                conn.Close();
                return View("MonthlyPage");
            }
            else
            {
                TempData["Message"] = null ;
                conn.Close();
                return View("Login");
            }

          //  scmd.ExecuteNonQuery();
            

            //String sqlQuery = "Select * from TableUsers";
            //SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, conn);


            //  _sqlConnection = new SqlConnection { ConnectionString = connectionString };
           
            
            
        }

       

        public ActionResult register(UserAccount acc)
        {

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Saiypher\source\repos\PROGPOETask\App_Data\ProgPoeDatabase.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand scmd = new SqlCommand("Select * from TableUsers where username = '" + acc.username + "' AND  password = '" + acc.password + "'");

            scmd.Connection = conn;

            SqlDataReader sdr ;

            sdr = scmd.ExecuteReader();

            if (sdr.Read())
            {
               
                ViewBag.ErrorMessage = "User already exists";
                conn.Close();
                return View("RegisterPage");
            }
            else
            {
                ViewBag.ErrorMessage = "Enrty has been added";

                scmd = new SqlCommand("INSERT INTO TableUsers(username,password,gross,tax,groc,waterLights,travel,phone,other,rent,propPrice,accDeposit,accRate,months,modelMake,vehPrice,vehDeposit,vehRate,vehIns,incomeTotal ,accTotal ,vehTotal,monthTotal) VALUES('" + acc.username + "' , '" + acc.password + "' , 0 , 0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0)", conn);

                sdr.Close();
                scmd.ExecuteNonQuery();

                conn.Close();
                return View("Login");
            }

          
        }

        public ActionResult updateMonthly(UserAccount acc)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Saiypher\source\repos\PROGPOETask\App_Data\ProgPoeDatabase.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            ClassCalc myClass = new ClassCalc();

            acc.incomeTotal = myClass.monthlyCalc(Convert.ToDouble(acc.gross), Convert.ToDouble(acc.tax), Convert.ToDouble(acc.groc), Convert.ToDouble(acc.water), Convert.ToDouble(acc.travel), Convert.ToDouble(acc.phones), Convert.ToDouble(acc.other)).ToString();

            SqlCommand scmd = new SqlCommand("UPDATE TableUsers SET gross = '" + acc.gross+ "' ,tax = '" + acc.tax+ "' ,groc = '" + acc.groc+ "' , waterLights = '" + acc.water + "' , travel = '" + acc.travel + "', phone = '" + acc.phones + "', other = '" + acc.other + "' , incomeTotal = '" + acc.incomeTotal + "'WHERE username = '" + username + "'", conn);

            Gross = acc.gross;
            Tax = acc.tax;
            Groc = acc.groc;
            Water = acc.water;
            Travel = acc.travel;
            Phone = acc.phones;
            Other = acc.other;
            IncomeTotal = acc.incomeTotal;

            scmd.ExecuteNonQuery();

            conn.Close();

            return View("Monthly");
        }
        [HttpPost]
        public ActionResult ViewUserData(UserAccount acc)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Saiypher\source\repos\PROGPOETask\App_Data\ProgPoeDatabase.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand scmd = new SqlCommand("Select * from TableUsers where username = '" + username + "'");

            String query = "Select * from TableUsers where username = '" + username + "'";

            scmd.Connection = conn;


            conn.Open();

            SqlDataReader sdr;

            sdr = scmd.ExecuteReader();

         

            // acc.tax = acc.tax.Trim();
            conn.Close();
            return View();
        }
        //  public ActionResult updateAccomodations(UserAccount acc)
        //{
        //    string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Saiypher\source\repos\PROGPOETask\App_Data\ProgPoeDatabase.mdf;Integrated Security=True";
        //    SqlConnection conn = new SqlConnection(connectionString);
        //    conn.Open();

        //  //  SqlCommand scmd = new SqlCommand($"UPDATE TableUsers SET rent = '{acc.rent}' ,propPrice = '{acc.propPrice}' ,accDeposit = '{acc.accDep}' , accRate = '{acc.accRate}' , months = '{acc.accMonths}' WHERE username = '" + username + "'", conn);

        //    SqlCommand scmd = new SqlCommand("UPDATE TableUsers SET rent = '" + acc.rent + "' ,propPrice = '" + acc.propPrice+ "' ,accDeposit = '" + acc.accDep + "' , accRate = '" + acc.accRate + "' , months = '" + acc.accMonths + "' WHERE username = '" + username + "'", conn);

        //    ClassCalc myClass = new ClassCalc();

        //    acc.accTotal = myClass.monthLoan(Convert.ToDouble(acc.propPrice) , Convert.ToDouble(acc.accDep), Convert.ToDouble(acc.accRate), Convert.ToDouble(acc.accMonths)).ToString();

        //    scmd.ExecuteNonQuery();
            
        //    conn.Close();

        //    return View("Accomodations");
        //}

        public ActionResult updateBuyAccomodations(UserAccount acc)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Saiypher\source\repos\PROGPOETask\App_Data\ProgPoeDatabase.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            //  SqlCommand scmd = new SqlCommand($"UPDATE TableUsers SET rent = '{acc.rent}' ,propPrice = '{acc.propPrice}' ,accDeposit = '{acc.accDep}' , accRate = '{acc.accRate}' , months = '{acc.accMonths}' WHERE username = '" + username + "'", conn);

            ClassCalc myClass = new ClassCalc();

            acc.accTotal = myClass.monthLoan(Convert.ToDouble(acc.propPrice), Convert.ToDouble(acc.accDep), Convert.ToDouble(acc.accRate), Convert.ToDouble(acc.accMonths)).ToString();

            SqlCommand scmd = new SqlCommand("UPDATE TableUsers SET rent = '0' ,propPrice = '" + acc.propPrice+ "' ,accDeposit = '" + acc.accDep + "' , accRate = '" + acc.accRate + "' , months = '" + acc.accMonths + "', accTotal = '" + acc.accTotal + "' WHERE username = '" + username + "'", conn);

          
            scmd.ExecuteNonQuery();
            
            conn.Close();

            PropPrice = acc.propPrice;
            AccDeposit = acc.accDep;
            AccRate = acc.accRate;
            Months = acc.accMonths;

            return View("Accomodations");
        }

        public ActionResult updateRentAccomodations(UserAccount acc)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Saiypher\source\repos\PROGPOETask\App_Data\ProgPoeDatabase.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            //  SqlCommand scmd = new SqlCommand($"UPDATE TableUsers SET rent = '{acc.rent}' ,propPrice = '{acc.propPrice}' ,accDeposit = '{acc.accDep}' , accRate = '{acc.accRate}' , months = '{acc.accMonths}' WHERE username = '" + username + "'", conn);

            ClassCalc myClass = new ClassCalc();

            acc.accTotal = myClass.monthLoan(Convert.ToDouble(acc.propPrice), Convert.ToDouble(acc.accDep), Convert.ToDouble(acc.accRate), Convert.ToDouble(acc.accMonths)).ToString();

            SqlCommand scmd = new SqlCommand("UPDATE TableUsers SET rent = '" + acc.rent + "' ,propPrice = '0' ,accDeposit = '0' , accRate = '0' , months = '0' WHERE username = '" + username + "'", conn);

            

            scmd.ExecuteNonQuery();

            conn.Close();

            Rent = acc.rent;
            AccTotal = Rent;

            return View("Accomodations");
        }

        public ActionResult updateVehicle(UserAccount acc)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Saiypher\source\repos\PROGPOETask\App_Data\ProgPoeDatabase.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            ClassCalc myClass = new ClassCalc();

            VehTotal = myClass.vehicle(Convert.ToDouble(acc.vehPrice.Trim()), Convert.ToDouble(acc.vehDep.Trim()), Convert.ToDouble(acc.vehRate.Trim()), Convert.ToDouble(acc.vehInsu.Trim())).ToString();

            SqlCommand scmd = new SqlCommand($"UPDATE TableUsers SET modelMake = '" + acc.model + "' ,vehPrice = '" + acc.vehPrice + "' ,vehDeposit = '" + acc.vehDep + "' , vehRate = '" + acc.vehRate + "' , vehIns = '" + acc.vehInsu + "' ,vehTotal = '"+ VehTotal + "' WHERE username = '" + username + "'", conn);

            

            scmd.ExecuteNonQuery();

            conn.Close();

            ModelMake = acc.model;
            VehPrice = acc.vehPrice;
            VehDeposit = acc.vehDep;
            VehRate = acc.vehRate;
            VehIns = acc.vehInsu;

            return View("Vehicle");
        }

        public ActionResult updateSavings(UserAccount acc)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Saiypher\source\repos\PROGPOETask\App_Data\ProgPoeDatabase.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            ClassCalc myClass = new ClassCalc();

            try { savMonthly = myClass.savings(Convert.ToDouble(acc.savTotal.Trim()), Convert.ToDouble(acc.savYears.Trim()), Convert.ToDouble(acc.savInt.Trim())).ToString();
            }
            catch(Exception e)
            {

            }
           

            SqlCommand scmd = new SqlCommand($"UPDATE TableUsers SET savTotal = '" + acc.savTotal + "' ,savYears = '" + acc.savYears + "' ,saveInt = '" + acc.savInt + "' , saveMonthly = '" + savMonthly + "'  WHERE username = '" + username + "'", conn);

           

            scmd.ExecuteNonQuery();

            conn.Close();

            return View("Savings");
        }

        public ActionResult Login()
        {
            return View();
            
        }

        public ActionResult MonthlyPage()
        {
            
            return View();
        }

        public ActionResult Monthly()
        {
            return View();
        }

        public ActionResult Accomodations(UserAccount acc)
        {
            
            return View();
        }

        public ActionResult Vehicle()
        {
            return View();
        }

        public ActionResult Savings()
        {
            return View();
        }

        public ActionResult RegisterPage()
        {
            return View();
        }

        public ActionResult ViewAccount()
        {


            ClassCalc myClass = new ClassCalc();

            //MonthTotal = myClass.totalCalc(Convert.ToDouble(savMonthly), Convert.ToDouble(IncomeTotal), Convert.ToDouble(AccTotal), Convert.ToDouble(VehTotal)).ToString();
          //  hold = myClass.totalCalc(Convert.ToDouble(savMonthly), Convert.ToDouble(IncomeTotal), Convert.ToDouble(AccTotal), Convert.ToDouble(VehTotal));
            //MonthTotal = hold.ToString();
         //   MonthTotal = 

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Saiypher\source\repos\PROGPOETask\App_Data\ProgPoeDatabase.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();


            SqlCommand scmd = new SqlCommand("Select * from TableUsers where username = '" + username + "'");

            scmd.Connection = conn;

            SqlDataReader sdr;

            sdr = scmd.ExecuteReader();

            sdr.Read();

            Gross = sdr.GetString(3);            //lets input value output this
            Tax = sdr.GetString(4);
            Groc = sdr.GetString(5);
            Water = sdr.GetString(6);
            Travel = sdr.GetString(7);
            Phone = sdr.GetString(8);
            Other = sdr.GetString(9);
            Rent = sdr.GetString(10);
            PropPrice = sdr.GetString(11);
            AccDeposit = sdr.GetString(12);
            AccRate = sdr.GetString(13);
            Months = sdr.GetString(14);
            ModelMake = sdr.GetString(15);
            VehPrice = sdr.GetString(16);
            VehDeposit = sdr.GetString(17);
            VehRate = sdr.GetString(18);
            VehIns = sdr.GetString(19);
            IncomeTotal = sdr.GetString(20);
            AccTotal = sdr.GetString(21);
            VehTotal = sdr.GetString(22);
            MonthTotal = sdr.GetString(23);
            savTotal = sdr.GetString(24);
            savYears = sdr.GetString(25);
            savInt = sdr.GetString(26);
            savMonthly = sdr.GetString(27);

            conn.Close();

            ViewData["Gross"] = Gross;
            ViewData["Tax"] = Tax;
            ViewData["Groc"] = Groc;
            ViewData["WaterLights"] = Water;
            ViewData["Travel"] = Travel;
            ViewData["Phone"] = Phone;
            ViewData["Other"] = Other;
            ViewData["Rent"] = Rent;
            ViewData["PropPrice"] = PropPrice;
            ViewData["AccDeposit"] = AccDeposit;
            ViewData["AccRate"] = AccRate;
            ViewData["Months"] = Months;
            ViewData["ModelMake"] = ModelMake;
            ViewData["VehPrice"] = VehPrice;
            ViewData["VehDeposit"] = VehPrice;
            ViewData["VehRate"] = VehRate;
            ViewData["VehIns"] = VehIns;
            ViewData["IncomeTotal"] = IncomeTotal;
            ViewData["AccTotal"] = AccTotal;
            ViewData["VehTotal"] = VehTotal;
            ViewData["MonthTotal"] = MonthTotal;
            ViewData["SaveTotal"] = savTotal;
            ViewData["SaveYears"] = savYears;
            ViewData["SaveInt"] = savInt;
            ViewData["SaveMonthly"] = savMonthly;

            return View();
        }

       

       

    }
}