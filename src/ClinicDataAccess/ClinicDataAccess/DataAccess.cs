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
            obj.insertdata(tablename: "patient", new Hashtable()
                                {
                                    { "firstname", p.FName },
                                    { "lastname", p.LName },
                                    { "sex", p.Sex },
                                    { "age", p.Age },
                                    { "dob", p.Dob }
                                });
            int pid;
            Int32.TryParse(obj.SQL_Lst(obj.selectdata(tablename: "patient", colname: "patient_id",
                wherewhat: "where firstname='" + p.FName + "' and " + "lastname='" + p.LName + 
                "' and " + "sex='" + p.Sex +
                "' and " + "age=" + p.Age))[0][0],out pid);
            return pid;
        }
        public void schedule_app(SQLCommands obj)
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
            app.Doctor_Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Visit Date");
            app.Visitdate = DateTime.Parse(Console.ReadLine());
            int doc_from = Convert.ToInt32(obj.SQL_Lst(obj.selectdata(tablename: "Doctor", colname: "_from", wherecolname: "doctor_id", what: app.Doctor_Id.ToString()))[0][0]);
            int doc_to = Convert.ToInt32(obj.SQL_Lst(obj.selectdata(tablename: "Doctor", colname: "_to", wherecolname: "doctor_id", what: app.Doctor_Id.ToString()))[0][0]);
            List<string> app_from = new List<string>();
            try
            {
                app_from = obj.SQL_Lst(obj.selectdata(tablename: "Appointment", colname: "_from", wherecolname: "doctor_id", what: app.Doctor_Id.ToString()))[0];
            }
            catch(InvalidOperationException)
            {
                Console.WriteLine("No Prior Appoinments");
            }
            catch(ArgumentOutOfRangeException)
            {
                Console.WriteLine("No Prior Appoinments");
            }
            List<int> slots = new List<int>();
            for (int slot= doc_from; slot< doc_to; slot++)
            {
                //if(!app_from.Contains(slot.ToString()))
                    Console.WriteLine(slot);
                    slots.Add(slot);
            }
            if (slots.Count == 0)
                Console.WriteLine("Slot Empty");
            foreach (int slot in slots)
                Console.WriteLine("Slots Available:" + slot);
            Console.WriteLine("Slot Start Time");
            int f = Convert.ToInt32(Console.ReadLine()); //Temporary Variable 
            if (slots.Contains(f))
            {
                app.From = f;
                app.To = f + 1;
            }
            else
                Console.WriteLine("Slot not Available");
            if (doc_ids.Contains(app.Doctor_Id))
            {
                //only one value possible due to column being pk constraint with int type  
                //if Appoinment does not start at doctor's slot end or after that
                if (app.From <= doc_to)
                    obj.insertdata(tablename: "Appointment", new Hashtable() { { "doctor_id", app.Doctor_Id }, { "patient_id", app.Patient_Id }, { "visitdate", app.Visitdate }, { "_from", app.From }, { "_to", app.To } });
                else
                    Console.WriteLine("Incorrect Slot");
            }
            else
                Console.WriteLine("Incorrect Doctor Id");
        }
        public void cancel_app(SQLCommands obj)
        {
            Console.Clear();
            string appid;
            List<List<string>> dtls = obj.SQL_Lst(obj.selectdata(tablename: "appointment"));
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

        }
    }
}