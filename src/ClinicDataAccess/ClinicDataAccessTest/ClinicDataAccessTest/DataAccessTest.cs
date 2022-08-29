using ClinicDataAccess;
using ClinicBusiness;
using SQL_Con;
using System.Data.SqlClient;
namespace ClinicDataAccessTest
{
    public class Tests
    {
        DataAccess da;
        SQLCommands obj;
        List<List<string>> login_dtls;
        int expected_count;
        int pid;
        [SetUp]
        public void Setup()
        {
            da = new DataAccess();
            obj = new SQLCommands("ClinicDb");
            expected_count = 1;
        }

        [Test]
        public void Login_Test()
        {
            login_dtls = da.login_check(obj, "hari", "hari@123");
            Assert.AreEqual(expected_count,login_dtls.Count());
        }
        [Test]
        public void Add_Patient_Test()
        {
            pid = da.add_patient(obj, new Patient("Ram","Charan",'M',34,new DateTime(1988,05,24)));
            Assert.NotZero(pid);
            
        }
    }
}