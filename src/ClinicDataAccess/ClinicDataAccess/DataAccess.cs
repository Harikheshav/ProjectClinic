using SQL_Con;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using ClinicBusiness;
namespace ClinicDataAccess
{
    public class DataAccess
    {
        public DataAccess()
        {

        }
        public List<List<string>> login_check(SQLCommands obj,string username,string password)
        {
            return obj.SQL_Lst(obj.selectdata(tablename: "frontoffice", wherewhat: "where username='" + username + "' and password='" + password + "'"));
        }
        public void doctor_view(SQLCommands obj)
        {
            Console.Clear();
            List<List<string>> dtls = obj.SQL_Lst(obj.selectdata(tablename: "Doctor"));
            List<string> cols = obj.getcolname(tablename: "Doctor").Keys.ToList();
            if (dtls.Count < 0)
                Console.WriteLine("No Doctors");
            foreach (List<string> dtl in dtls)
            {
                for (int i = 0; i < cols.Count; i++)
                {
                    Console.WriteLine(cols[i].ToUpper() + ":" + dtl[i]);
                }

            }
            Console.WriteLine();
        }
        public int add_patient(SQLCommands obj,Patient p)
        {
            Console.Clear();
            try
            {
                obj.insertdata(tablename: "patient", new Hashtable()
                                {
                                    { "firstname", p.FName },
                                    { "lastname", p.LName },
                                    { "sex", p.Sex },
                                    { "age", p.Age },
                                    { "dob", p.Dob }
                                });
            }
            catch
            {
                Console.WriteLine("Wrong Input Type");
            }
            int pid;
            Int32.TryParse(obj.SQL_Lst(obj.selectdata(tablename: "patient", colname: "patient_id",
                wherewhat: "where firstname='" + p.FName + "' and " + "lastname='" + p.LName + 
                "' and " + "sex='" + p.Sex +
                "' and " + "age=" + p.Age +
                " and " + "dob='" + p.Dob.Year + "-" + p.Dob.Month + "-" + p.Dob.Day +"'"
                ))[0][0],out pid);
            return pid;
        }
        public int schedule_app(SQLCommands obj)
        {
            Console.Clear();
            Appoinment app = new Appoinment();
            List<List<string>> dtls = obj.SQL_Lst(obj.selectdata(tablename: "Patient"));
            List<string> cols = obj.getcolname(tablename: "Patient").Keys.ToList();
            foreach (List<string> dtl in dtls)
            {
                for (int i = 0; i < cols.Count; i++)
                {
                    Console.WriteLine(cols[i].ToUpper() + ":" + dtl[i]);
                }

            }
            Console.WriteLine("Patient Id");
            app.Patient_Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Specialisations Available:");
            Console.WriteLine("General, Internal Medicine, Pediatrics, Orthopedics, Ophthalmology");
            Console.WriteLine("Specialisation");
            string spec = Console.ReadLine();
            dtls = obj.SQL_Lst(obj.selectdata(tablename: "Doctor", wherecolname: "specialisation", what: spec));
            cols = obj.getcolname(tablename: "Doctor").Keys.ToList();
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
            int did = Convert.ToInt32(Console.ReadLine());
            if (doc_ids.Contains(did))
                app.Doctor_Id = did;
            else
            {
                Console.WriteLine("Invalid Doctor Id");
                return 0;
            }
            Console.WriteLine("Visit Date");
            app.Visitdate = DateTime.Parse(Console.ReadLine());
            int doc_from = Convert.ToInt32(obj.SQL_Lst(obj.selectdata(tablename: "Doctor", colname: "_from", wherecolname: "doctor_id", what: app.Doctor_Id.ToString()))[0][0]);
            int doc_to = Convert.ToInt32(obj.SQL_Lst(obj.selectdata(tablename: "Doctor", colname: "_to", wherecolname: "doctor_id", what: app.Doctor_Id.ToString()))[0][0]);
            List<string> app_from; 
            List<int> slots = new List<int>();
            try
            {
                
                app_from = obj.SQL_Lst(obj.selectdata(tablename: "Appointment", colname: "_from", wherewhat:"where doctor_id=" + app.Doctor_Id.ToString() + " and visitdate='" + app.Visitdate.Year+"-"+app.Visitdate.Month+"-"+app.Visitdate.Day+"'"))[0];
            }
            catch (SQLCommands.WrongSQLCommand)
            {
                app_from = new List<string>();
            }
            catch(IndexOutOfRangeException)
            {
                app_from = new List<string>();
            }
            catch(ArgumentOutOfRangeException)
            {
                app_from = new List<string>();
            }
            for (int slot= doc_from; slot< doc_to; slot++)
            {
                if (!app_from.Contains(slot.ToString()))
                {
                    slots.Add(slot);
                }
            }
            if (slots.Count == 0)
            {
                Console.WriteLine("Slot Empty");
                return 0;
            }
            foreach (int slot in slots)
                Console.WriteLine("Slots Available:{0}-{1}",slot,Convert.ToInt32(slot+1));
            Console.WriteLine("Slot Start Time");
            int f = Convert.ToInt32(Console.ReadLine()); //Temporary Variable 
            if (slots.Contains(f))
            {
                app.From = f;
                app.To = f + 1;
            }
            else
            {
                Console.WriteLine("Slot not Available");
                return 0;
            }
            if (doc_ids.Contains(app.Doctor_Id))
            {
                //only one value possible due to column being pk constraint with int type  
                //if Appoinment does not start at doctor's slot end or after that
                if (app.From <= doc_to)
                {
                    obj.insertdata(tablename: "Appointment", colfields: new Hashtable() { { "doctor_id", app.Doctor_Id }, { "patient_id", app.Patient_Id }, { "visitdate", app.Visitdate }, { "_from", app.From }, { "_to", app.To } });
                    int aid;
                    Int32.TryParse(obj.SQL_Lst(obj.selectdata(tablename: "Appointment", colname: "appid",
                        wherewhat: "where doctor_id=" + app.Doctor_Id + " and " + "patient_id=" + app.Patient_Id +
                        " and " + "visitdate='" + app.Visitdate.Year+"-"+app.Visitdate.Month+"-"+app.Visitdate.Day +
                        "' and " + "_from=" + app.From + " and " + "_to=" + app.To))[0][0], out aid);
                    return aid;
                }
                else
                {
                    Console.WriteLine("Incorrect Slot");
                    return 0;
                }

            }
            else
            {
                Console.WriteLine("Incorrect Doctor Id");
                return 0;
            }
        }
        public void cancel_app(SQLCommands obj)
        {
            Console.Clear();
            string appid;
            List<List<string>> dtls = obj.SQL_Lst(obj.selectdata(tablename: "appointment"));
            if (dtls.Count == 0)
            {
                Console.WriteLine("No Appoinments");
                return;
            }
            List<string> cols = obj.getcolname(tablename: "appointment").Keys.ToList();
            foreach (List<string> dtl in dtls)
            {
                for (int i = 0; i < cols.Count; i++)
                {
                    Console.WriteLine(cols[i].ToUpper() + ":" + dtl[i]);

                }

            }

            Console.WriteLine("Appoinment Id");
            appid = Console.ReadLine();
            obj.deletedata(tablename: "appointment", wherecolname: "appid", what: appid);
            return;
        }
    }
}