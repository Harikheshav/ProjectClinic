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
        static int doctor_id;
        string fname;
        string lname;
        char sex;
        string spec;
        int visithrs;
        public Doctor(string fname, string lname, char sex, string spec, int visithrs)
        {
            this.fname = fname;
            this.lname = lname;
            this.sex = sex;
            this.spec = spec;
            this.visithrs = visithrs;
        }

        public int Doctor_id
        {
            get { return doctor_id; }
            set { doctor_id++; }
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
        public int Visithrs
        {
            get { return visithrs; }
            set
            {
                visithrs = value;
            }
        }
    }
    public class Patient
    {
        static int patient_id;
        string fname;
        string lname;
        char sex;
        int age;
        DateOnly dob;
        public Patient()
        {

        }
        public Patient(string fname, string lname, char sex, int age, DateOnly dob)
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
            set { patient_id++; }
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
        public DateOnly Dob
        {
            get { return dob; }
            set
            {
                dob = value;
            }
        }
        public class Appoinment
        {
            int patient_id;
            string spec;
            int doctor_id;
            DateOnly visitdate;
            TimeOnly apptime;
            public Appoinment(int patient_id, string spec, int doctor_id, DateOnly visitdate, TimeOnly apptime)
            {
                this.patient_id = patient_id;
                this.spec = spec;
                this.doctor_id = doctor_id;
                this.visitdate = visitdate;
                this.apptime = apptime;
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
            public DateOnly Visitdate
            {
                get { return Visitdate; }
                set { Visitdate = value; }
            }
            public TimeOnly Apptime
            {
                get { return Apptime; }
                set { Apptime = value; }
            }
        }
    }
}