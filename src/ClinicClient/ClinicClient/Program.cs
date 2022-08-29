using SQL_Con;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using ClinicBusiness;
using ClinicDataAccess;
namespace ClinicClient
{
    internal class Program
    {
        static void Main()
        {
            SQLCommands cd = new SQLCommands(databasename: "ClinicDb");
            string choice;
            do
            {
            Console.WriteLine("           Login/Register/Quit               ");
            Console.WriteLine("Choice:");
            choice = Console.ReadLine();
            FrontOffice fo = new FrontOffice();
                try
                {
                    if (choice.ToLower() == "l" || choice.ToLower() == "login")
                    {
                        string username, password;
                        Console.Clear();
                        Console.WriteLine("                 Login                  ");
                        Console.WriteLine("Username");
                        username = Console.ReadLine();
                        Console.WriteLine("Password");
                        password = Console.ReadLine();
                        DataAccess da = new DataAccess();
                        List<List<string>> login_dtls = da.login_check(cd, username, password);
                        //Checks if a login instance is true.
                        if (login_dtls.Count == 1)
                        {

                            do
                            {
                                Console.WriteLine("Hello, " + login_dtls[0][1] + " " + login_dtls[0][2]);
                                Console.WriteLine("1.View Doctors");
                                Console.WriteLine("2.Add Patient");
                                Console.WriteLine("3.Schedule Appointment");
                                Console.WriteLine("4.Cancel Appointment");
                                Console.WriteLine("5.Logout");
                                Console.WriteLine("Choice:");
                                choice = Console.ReadLine();
                                //Function for displaying all doctor details from database
                                if (choice.ToLower() == "view doctors" || choice == "1" || choice.ToLower() == "vd")
                                {
                                    da.doctor_view(cd);
                                }
                                //Function for adding a patient detail to database
                                if (choice.ToLower() == "add patient" || choice == "2" || choice.ToLower() == "ap")
                                {
                                    Patient P = new Patient();
                                    Console.WriteLine("First Name");
                                    P.FName = Console.ReadLine();
                                    Console.WriteLine("Last Name");
                                    P.LName = Console.ReadLine();
                                    Console.WriteLine("Sex");
                                    P.Sex = Convert.ToChar(Console.ReadLine());
                                    Console.WriteLine("Age");
                                    P.Age = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Date Of Birth");
                                    P.Dob = DateTime.Parse(Console.ReadLine());
                                    int pid = da.add_patient(cd,P);
                                    if (pid != 0)
                                        Console.WriteLine("Patient Id:" + pid);
                                    else
                                        Console.WriteLine("Some error occured!");

                                }
                                //Function for scheduling a appointment for a Patient to a Doctor.
                                if (choice.ToLower() == "schedule appointment" || choice == "3" || choice.ToLower() == "sa")
                                {
                                    int appid = da.schedule_app(cd);
                                    if (appid != 0)
                                        Console.WriteLine("Appoinment Id:" +appid );
                                    else
                                        Console.WriteLine("Some error occured!");
                                }
                                //Function for cancelling a appointment registered for a Patient to a Doctor.
                                if (choice.ToLower() == "cancel appointment" || choice == "4" || choice.ToLower() == "ca")
                                {
                                    da.cancel_app(cd);
                                }
                                //Logout-Call
                                if (choice.ToLower() == "Logout" || choice == "5" || choice.ToLower() == "l")
                                {
                                    Console.Clear();
                                    Console.WriteLine("Logged out Successfully!");
                                    break;
                                }
                            } while (choice.ToLower() != "Logout" || choice != "5" || choice.ToLower() != "l");



                        }
                        //If the login instance is not available
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Incorrect Login! Try again...");
                        }
                    }
                    //Registration Call
                    if (choice.ToLower() == "r" || choice.ToLower() == "register")
                    {
                        Console.WriteLine("Username");
                        fo.Uname = Console.ReadLine();
                        Console.WriteLine("First Name");
                        fo.FName = Console.ReadLine();
                        Console.WriteLine("Last Name");
                        fo.LName = Console.ReadLine();
                        Console.WriteLine("Password");
                        fo.Password = Console.ReadLine();
                        cd.insertdata(tablename: "frontoffice", new Hashtable()
                    {
                        { "username", fo.Uname },
                        { "firstname", fo.FName },
                        { "lastname", fo.LName },
                        { "password", fo.Password }
                    });

                    }
                    //Query for coming out of Console App
                    if (choice.ToLower() == "q" || choice.ToLower() == "quit")
                    {
                        Console.Clear();
                        Console.WriteLine("Quitted Successfully!");
                        break;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (choice.ToLower() != "q" || choice.ToLower() != "quit");
        }
    }
}