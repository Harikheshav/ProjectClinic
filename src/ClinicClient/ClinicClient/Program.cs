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
        static void disp_dtls(string tablename,SQLCommands obj)
        {
            List<List<string>> dtls = obj.SQL_Lst(obj.selectdata(tablename: tablename));
            List<string> cols = obj.getcolname(tablename: tablename).Keys.ToList();
            Hashtable colfields = new Hashtable();
            foreach (List<string> dtl in dtls)
            {
                for (int i = 0; i < cols.Count; i++)
                {
                    Console.WriteLine(cols[i].ToUpper() + ":" + dtl[i]);
                    colfields[cols[i].ToUpper()] = dtl[i].ToString();
                }

            }
            Console.WriteLine();
        }
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
                    List<List<string>> login_dtls = da.login_check(cd,username, password);
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
                            if (choice.ToLower() == "view doctors" || choice == "1" || choice.ToLower() == "vd")
                            {
                                da.doctor_view(cd);
                            }
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
                                Console.WriteLine("Patient Id:"+da.add_patient(cd,P));
                                
                            }
                            if (choice.ToLower() == "schedule appointment" || choice == "3" || choice.ToLower() == "sa")
                            {
                                da.schedule_app(cd);
                            }
                            if (choice.ToLower() == "cancel appointment" || choice == "4" || choice.ToLower() == "ca")
                            {
                                da.cancel_app(cd);
                            }
                            if (choice.ToLower() == "Logout" || choice == "5" || choice.ToLower() == "l")
                            {
                                Console.Clear();
                                Console.WriteLine("Logged out Successfully!");
                                break;
                            }
                        } while (choice.ToLower() != "Logout" || choice != "5" || choice.ToLower() != "l");



                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Incorrect Login! Try again...");
                    }
                }

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
                if (choice.ToLower() == "q" || choice.ToLower() == "quit")
                {
                    Console.Clear();
                    Console.WriteLine("Quitted Successfully!");
                    break;
                }

            } while (choice.ToLower() != "q" || choice.ToLower() != "quit");
        }
    }
}