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
        public void add_patient(SQLCommands obj,Patient p)
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
            Console.WriteLine("Slot Start Time");
            app.From = Convert.ToInt32(Console.ReadLine());
            app.To = app.From + 1;
            if (doc_ids.Contains(app.Doctor_Id))
            {
                int to = Convert.ToInt32(obj.SQL_Lst(obj.selectdata(tablename: "Doctor", colname: "_to", wherecolname: "doctor_id", what: app.Doctor_Id.ToString()))[0][0]);
                //only one value possible due to column being pk constraint with int type  
                //if Appoinment does not start at doctor's slot end or after that
                if (app.From <= to)
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