using SQL_Con;
using System.Linq;

namespace ClinicBusiness
{

    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {

        }
    }
    public class FrontOffice
    {
        string uname;
        string fname;
        string lname;
        string password;
        private bool hasSpecChar(string str)
        {
            string spec = "!@#$%^&*()_+=-:;'{}[]|<>,.?/~`/*+-)(=";
            spec += '\"';
            foreach (char c in spec)
            {
                if (str.Contains(c))
                {
                    return true;
                }
            }
            return false;


        }
        public FrontOffice(string uname, string fname, string lname, string password)
        {
            this.uname = uname;
            this.fname = fname;
            this.lname = lname;
            this.password = password;
        }
        public FrontOffice()
        {

        }
        public string Uname
        {
            get
            {
                return uname;

            }
            set
            {
                if (value.Length >= 10)
                {
                    throw new ValidationException("Requires maximum 10 characters");
                }
                else if (hasSpecChar(value))
                {
                    throw new ValidationException("Cannot contain special characters");
                }

                else
                {
                    uname = value;
                }
            }
        }
        public string FName
        {
            get
            {
                return fname;
            }
            set
            {
                fname = value;
            }
        }
        public string LName
        {
            get
            {
                return lname;
            }
            set
            {
                lname = value;
            }
        }
        public string Password
        {
            get
            {
               return password;
            }
            set
            {
                if (!value.Contains('@'))
                {
                    throw new ValidationException("Should contain @ in password");
                }
                else
                { 
                    password = value;
                }
            }
        }
    }
    public class Doctor
    {
        static int doctor_id=1;
        string fname;
        string lname;
        char sex;
        string spec;
        int from;
        int to;
        public Doctor(string fname, string lname, char sex, string spec, int from,int to)
        {
            this.fname = fname;
            this.lname = lname;
            this.sex = sex;
            this.spec = spec;
            this.from = from;
            this.to = to;
        }

        public int Doctor_id
        {
            get { return doctor_id; }
            set { doctor_id=value; }
        }
        public char Sex
        {
            get { return sex; }
            set { sex = value; }
        }
        public string FName
        {
            get
            {
                return fname;
            }
            set
            {
                fname = value;
            }
        }
        public string LName
        {
            get
            {
                return lname;
            }
            set
            {
                lname = value;
            }
        }
        public string Spec
        {
            get { return spec; }
            set { spec = value; }
        }
        public int From
        {
            get { return from; }
            set
            {
                from = value;
            }
        }
        public int To
        {
            get { return to; }
            set
            {
                to = value;
            }
        }
    }
    public class Patient
    {
        static int patient_id = 1;
        string fname;
        string lname;
        char sex;
        int age;
        DateTime dob;
        public Patient()
        {

        }
        public Patient(string fname, string lname, char sex, int age, DateTime dob)
        {
            this.fname = fname;
            this.lname = lname;
            this.sex = sex;
            this.age = age;
            this.dob = dob;

        }

        public int Patient_id
        {
            get { return patient_id; }
            set { patient_id = value; }
        }
        public char Sex
        {
            get { return sex; }
            set { sex = value; }
        }
        public string FName
        {
            get
            {
                return fname;
            }
            set
            {
                fname = value;
            }
        }
        public string LName
        {
            get
            {
                return lname;
            }
            set
            {
                lname = value;
            }
        }
        public int Age
        {
            get { return age; }
            set
            {
                if (age < 0 || age > 120)
                {
                    throw new ValidationException("Age is invalid");
                }
                else
                {
                    age = value;
                }
            }
        }
        public DateTime Dob
        {
            get { return dob; }
            set
            {
                dob = value;
            }
        }
    }
        public class Appoinment
        {
            int patient_id;
            string spec;
            int doctor_id;
            DateTime visitdate;
            int from;
            int to;
            public Appoinment()
            {


            }
            public Appoinment(int patient_id, string spec, int doctor_id, DateTime visitdate, int from,int to)
            {
                this.patient_id = patient_id;
                this.spec = spec;
                this.doctor_id = doctor_id;
                this.visitdate = visitdate;
                this.from = from;
                this.to = to;

            }
            public int Patient_Id
            {
                get { return patient_id; }
                set { patient_id = value; }
            }
            public string Spec
            {
                get { return spec; }
                set { spec = value; }
            }
            public int Doctor_Id
            {
                get { return doctor_id; }
                set { doctor_id = value; }
            }
            public DateTime Visitdate
            {
                get { return Visitdate; }
                set { Visitdate = value; }
            }
            public int From
            {
                get { return from; }
                set
                {
                    from = value;
                }
            }
            public int To
            {
                get { return to; }
                set
                {
                    to = value;
                }
            }
        }
    }

