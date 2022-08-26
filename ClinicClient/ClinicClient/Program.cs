using SQL_Con;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using ClinicBusiness;
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
                if (choice.ToLower() == "q"||choice=="3"||choice.ToLower()=="quit")
                    break;
                FrontOffice fo = new FrontOffice(); 
                if (choice.ToLower() == "l" || choice == "1" || choice.ToLower() == "login")
                {
                    string username, password;
                    Console.Clear();
                    Console.WriteLine("                 Login                  ");
                    Console.WriteLine("Username");
                    username = Console.ReadLine();
                    Console.WriteLine("Password");
                    password = Console.ReadLine();

                    List<List<string>> login_dtls = cd.SQL_Lst(cd.selectdata(tablename: "frontoffice", wherewhat: "where username='" + username + "' and password='" + password + "'"));
                    if (login_dtls.Count == 1)
                    {
                        //display()
                        Console.WriteLine("Hello, " + login_dtls[0][1] + " " + login_dtls[0][2]);
                        Console.WriteLine("1.View Doctors");
                        Console.WriteLine("2.Add Patient");
                        Console.WriteLine("3.Schedule Appointment");
                        Console.WriteLine("4.Cancel Appointment");
                        Console.WriteLine("5.Logout");
                        Console.WriteLine("Choice:");
                        choice = Console.ReadLine();
                        if (choice.ToLower()=="View Doctors" || choice=="1"||choice.ToLower()=="VD")
                        {
                            List<List<string>> doc_dtls = cd.SQL_Lst(cd.selectdata(tablename: "Doctor"));
                            List<string> cols = cd.getcolname(tablename: "Doctor").Keys.ToList();
                            foreach (List<string> doc_dtl in doc_dtls)
                            {
                                for (int i = 0; i < cols.Count; i++)
                                {
                                    Console.WriteLine(cols[i].ToUpper() + ":" + doc_dtl[i]);
                                }
                                //display()
                            }
                        }
                        if (choice.ToLower() == "Add Patient" || choice == "2" || choice.ToLower() == "AP")
                        {
                            //Patient p = new Patient();
                            //Console.WriteLine("First Name");
                            //p.FName = Console.ReadLine();
                            //Console.WriteLine("Last Name");
                            //p.LName = Console.ReadLine();
                            //Console.WriteLine("Sex");
                            //p.Sex = Convert.ToChar(Console.ReadLine());
                            //Console.WriteLine("Age");
                            //p.Age = Convert.ToInt32(Console.ReadLine());
                            //Console.WriteLine("Date Of Birth");
                            //p.Dob = DateOnly.Parse(Console.ReadLine());
                            //cd.insertdata(tablename: "patient", new Hashtable() {
                            //{ "firstname", p.FName },
                            //{"lastname",p.LName },
                            //{"sex",p.Sex },
                            //{"age",p.Age },
                            //{"dob",p.Dob }
                            //});
                        }
                        if (choice.ToLower() == "Schedule Appointment" || choice == "3" || choice.ToLower() == "SA")
                        {
                            Console.WriteLine(choice);
                        }
                        if (choice.ToLower() == "Cancel Appointment" || choice == "4" || choice.ToLower() == "CA")
                        {
                            Console.WriteLine(choice);
                        }
                        if (choice.ToLower() == "Logout" || choice == "5" || choice.ToLower() == "L")
                        {
                            Console.Clear();
                            Console.WriteLine("Logged out Successfully!");
                            break;
                        }




                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Incorrect Login! Try again...");
                    }
                }
                else if(choice.ToLower() == "r" || choice == "2" || choice.ToLower() == "register")
                {
                    Console.WriteLine("Username");
                    fo.Uname = Console.ReadLine();
                    Console.WriteLine("First Name");
                    fo.FName = Console.ReadLine();
                    Console.WriteLine("Last Name");
                    fo.LName = Console.ReadLine();
                    Console.WriteLine("Password");
                    fo.Password = Console.ReadLine();
                    cd.insertdata(tablename: "frontoffice", new Hashtable() { 
                        { "username", fo.Uname },
                        {"firstname",fo.FName },
                        {"lastname",fo.LName },
                        {"password",fo.Password }
                    }) ;

                }

            }while(true);
        }

    }
}