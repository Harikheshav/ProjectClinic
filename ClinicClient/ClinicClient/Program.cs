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

                    List<List<string>> login_dtls = cd.SQL_Lst(cd.selectdata(tablename: "frontoffice", wherewhat: "where username='" + username + "' and password='" + password + "'"));
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
                                List<List<string>> dtls = cd.SQL_Lst(cd.selectdata(tablename: "Doctor"));
                                List<string> cols = cd.getcolname(tablename: "Doctor").Keys.ToList();
                                foreach (List<string> dtl in dtls)
                                {
                                    for (int i = 0; i < cols.Count; i++)
                                    {
                                        Console.WriteLine(cols[i].ToUpper() + ":" + dtl[i]);
                                    }

                                }
                                Console.WriteLine();
                            }
                            if (choice.ToLower() == "add patient" || choice == "2" || choice.ToLower() == "ap")
                            {
                                Patient p = new Patient();
                                Console.WriteLine("First Name");
                                p.FName = Console.ReadLine();
                                Console.WriteLine("Last Name");
                                p.LName = Console.ReadLine();
                                Console.WriteLine("Sex");
                                p.Sex = Convert.ToChar(Console.ReadLine());
                                Console.WriteLine("Age");
                                p.Age = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Date Of Birth");
                                p.Dob = DateTime.Parse(Console.ReadLine());
                                cd.insertdata(tablename: "patient", new Hashtable()
                                {
                                    { "firstname", p.FName },
                                    { "lastname", p.LName },
                                    { "sex", p.Sex },
                                    { "age", p.Age },
                                    { "dob", p.Dob }
                                });
                            }
                            if (choice.ToLower() == "schedule appointment" || choice == "3" || choice.ToLower() == "sa")
                            {
                                Appoinment app = new Appoinment();
                                List<List<string>> dtls = cd.SQL_Lst(cd.selectdata(tablename: "Patient"));
                                List<string> cols = cd.getcolname(tablename: "Patient").Keys.ToList();
                                foreach (List<string> dtl in dtls)
                                {
                                    for (int i = 0; i < cols.Count; i++)
                                    {
                                        Console.WriteLine(cols[i].ToUpper() + ":" + dtl[i]);
                                    }

                                }
                                Console.WriteLine("Patient Id");
                                app.Patient_Id = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Specialisation");
                                string spec = Console.ReadLine();
                                dtls = cd.SQL_Lst(cd.selectdata(tablename: "Doctor",wherecolname: "specialisation", what: spec));
                                cols = cd.getcolname(tablename: "Doctor").Keys.ToList();
                                List<int> doc_ids = new List<int>();
                                foreach (List<string> dtl in dtls)
                                {
                                    doc_ids.Add(Convert.ToInt32(dtl[0])); // Add doc_id
                                    for (int i = 0; i < cols.Count; i++)
                                    {
                                        Console.WriteLine(cols[i].ToUpper() + ":" + dtl[i]);
                                        
                                    }

                                }
                                Console.WriteLine("Doctor Id");
                                app.Doctor_Id = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Visit Date");
                                app.Visitdate = DateTime.Parse(Console.ReadLine());
                                Console.WriteLine("Slot Start Time");
                                app.From = Convert.ToInt32(Console.ReadLine());
                                app.To = app.From + 1;
                                if (doc_ids.Contains(app.Doctor_Id))
                                {
                                    int to = Convert.ToInt32(cd.SQL_Lst(cd.selectdata(tablename: "Doctor", colname: "to", wherecolname: "doctor_id", what: app.Doctor_Id.ToString()))[0][0]); 
                                    //only one value possible due to column being pk constraint with int type  
                                    //if Appoinment does not start at doctor's slot end or after that
                                    if (app.From <= to)
                                        cd.insertdata(tablename:"Appoinment",new Hashtable() { { "visit_date", app.Visitdate }, { "_from", app.From }, { "_to", app.To } });
                                    else
                                        Console.WriteLine("Incorrect Slot");
                                }
                                else
                                    Console.WriteLine("Incorrect Doctor Id");
                            }
                            if (choice.ToLower() == "cancel appointment" || choice == "4" || choice.ToLower() == "ca")
                            {
                                string appid;
                                cd.selectdata(tablename: "appointment");
                                Console.WriteLine("Appoinment Id");
                                appid = Console.ReadLine();
                                cd.deletedata(tablename: "appointment", wherecolname: "appid", what: appid.ToString());
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

                else if (choice.ToLower() == "r" || choice.ToLower() == "register")
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