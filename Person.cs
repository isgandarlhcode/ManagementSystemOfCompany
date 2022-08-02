using KadrMelumatlariApp.ToolKits;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace KadrMelumatlariApp
{
    public class Person
    {
        public static string connectiondata = "Data Source=DESKTOP-TVVE847;Initial Catalog=PEMS;Integrated Security=True";

        public int BeingLateTimes = 0;

        public int ID { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime JobStartDate { get; set; }

        public string Address { get; set; }

        public string Position { get; set; }

        public decimal SalaryRate { get; set; }

        public float MonthlyWorkingMinutes { get; set; }

        public static Person employer = new Person();

        public static List<Person> BeingLateList = new List<Person>();
        public static Person PersonAdd()
        {
            Random workingminutes = new Random();
            employer.MonthlyWorkingMinutes = workingminutes.Next(14000, 15000);
            Console.WriteLine("Isci Nomresi Yaradilir . Xahis olunur biraz gozleyin ...");
            employer.ID = CustomIDGen.CustomID();
            Thread.Sleep(1500);
            Console.WriteLine("Yeni isci nomresi : " + employer.ID);
            Console.WriteLine("Iscinin adini daxil ediniz : ");
            employer.Name = Console.ReadLine();
            Console.WriteLine("Iscinin soyadini daxil ediniz : ");
            employer.Surname = Console.ReadLine();
            Console.WriteLine("Iscinin vezifesini daxil ediniz :  ( Junior / Front-End / Back-End / Senior / Full-Stack) ");
            employer.Position = Console.ReadLine();
            Console.WriteLine("Iscinin ise baslama tarixini daxil ediniz : (Numune : GG.AA.IIII )");
            employer.JobStartDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Iscinin Adresini daxil ediniz : ");
            employer.Address = Console.ReadLine();
            switch (employer.Position)
            {
                case "Junior":
                    employer.SalaryRate = 2;
                    break;
                case "Front-End":
                    employer.SalaryRate = (decimal)2.5;
                    break;
                case "Back-End":
                    employer.SalaryRate = 3;
                    break;
                case "Senior":
                    employer.SalaryRate = (decimal)4.5;
                    break;
                case "Full-Stack":
                    employer.SalaryRate = 4;
                    break;
            }
            Console.WriteLine("Sizin Maas Emsaliniz : " + employer.SalaryRate);

            Console.WriteLine("Gozleyin ...");
            Thread.Sleep(500);
            Console.WriteLine("Isci Muveffeqiyyetle elave olundu .");
            Console.WriteLine("Is saatiniz novbeti gun saat 09:00 dan hesablanacaqdir .");
            Person.AddInformationToDB(employer);
            return employer;
        }
        public static void UpdatePerson()
        {
            Console.WriteLine("Isci ID Daxil edin :");
            int pendingID = Convert.ToInt32(Console.ReadLine());
            Thread.Sleep(100);
            Console.WriteLine("Hansi Melumatlari yenilemek isteyirsiniz ? : (Soyad /Adres / Emsal)");
            string changedinfotype = Console.ReadLine().ToLower();
            SqlConnection sqlcon = new SqlConnection(connectiondata);
            sqlcon.Open();
            if (changedinfotype == "soyad")
            {
                Console.WriteLine("Yeni soyadi daxil edin : ");
                string surnamechange = Console.ReadLine();
                string query = "Update Employee set SurName = @Surname Where ID = @ID";
                SqlCommand sqlCommand = new SqlCommand(query, sqlcon);
                sqlCommand.Parameters.AddWithValue("@ID", pendingID);
                sqlCommand.Parameters.AddWithValue("@SurName", surnamechange);
                sqlCommand.ExecuteNonQuery();
            }

            else if (changedinfotype == "address")
            {
                Console.WriteLine("Yeni adresi daxil edin : ");
                string addresschange = Console.ReadLine();
                string query = "Update Employee set Address=@Address Where ID = @ID";
                SqlCommand sqlCommand = new SqlCommand(query, sqlcon);
                sqlCommand.Parameters.AddWithValue("@ID", pendingID);
                sqlCommand.Parameters.AddWithValue("@Address", changedinfotype);
                sqlCommand.ExecuteNonQuery();
            }
            else
            {
                Console.WriteLine("Yeni maas emsali daxil edin : ");
                int wagechange = Convert.ToInt32(Console.ReadLine());
                string query = "Update Employee set WageRate=@WageRate Where ID = @ID";
                SqlCommand sqlCommand = new SqlCommand(query, sqlcon);
                sqlCommand.Parameters.AddWithValue("@ID", pendingID);
                sqlCommand.Parameters.AddWithValue("@WageRate", wagechange);
                sqlCommand.ExecuteNonQuery();
            }
            sqlcon.Close();
        }
        public static void DeletePerson()
        {

            Console.WriteLine("Isci ID Daxil edin :");
            int pendingID = Convert.ToInt32(Console.ReadLine());
            Thread.Sleep(100);
            SqlConnection sqlcon = new SqlConnection(connectiondata);
            sqlcon.Open();
            string query = "DELETE FROM Employee WHERE ID=@ID";
            SqlCommand sqlCommand = new SqlCommand(query, sqlcon);
            sqlCommand.Parameters.AddWithValue("@ID", pendingID);
            sqlCommand.ExecuteNonQuery();
            sqlcon.Close();

        }
        public static void SalaryCounterandShow()
        {
            Console.WriteLine("Maasini oyrenmek istediyiniz iscinin ID sini daxil ediniz : ");
            int quest = Convert.ToInt32(Console.ReadLine());
            SqlConnection sqlcon = new SqlConnection(connectiondata);
            SqlDataReader sqlDataReader;
            sqlcon.Open();

            string query = "Select ID,Name,Surname,JobStartDate,Address,WageRate,MonthlyWorkingTime from Employee;";
            SqlCommand sqlCommand = new SqlCommand(query, sqlcon);
            sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                if (quest == Convert.ToInt32(sqlDataReader.GetValue(0)))
                {
                    int workingminutes = Convert.ToInt32(sqlDataReader.GetValue(6));
                    decimal workinghours = workingminutes / 60;
                    decimal salary = workinghours * 10 * Convert.ToDecimal(sqlDataReader.GetValue(5));
                    Console.WriteLine($"{sqlDataReader.GetValue(0)} | {sqlDataReader.GetValue(1)} {sqlDataReader.GetValue(2)} : {workinghours} saat | {salary} AZN ");
                }
            }
        }


        public static void AddInformationToDB(Person person)
        {
            SqlConnection sqlcon = new SqlConnection(connectiondata);
            sqlcon.Open();
            string request = "insert into dbo.Employee(ID,Name,Surname,JobStartDate,Address,WageRate,MonthlyWorkingTime) values(@ID,@Name,@Surname,@JobStartDate,@Address,@WageRate,@MontlyWorkingTime); ";
            SqlCommand sqlCommand = new SqlCommand(request, sqlcon);
            sqlCommand.Parameters.AddWithValue("@ID", employer.ID);
            sqlCommand.Parameters.AddWithValue("@Name", employer.Name);
            sqlCommand.Parameters.AddWithValue("@Surname", employer.Surname);
            sqlCommand.Parameters.AddWithValue("@JobStartDate", employer.JobStartDate);
            sqlCommand.Parameters.AddWithValue("@Address", employer.Address);
            sqlCommand.Parameters.AddWithValue("@WageRate", employer.SalaryRate);
            sqlCommand.Parameters.AddWithValue("@MontlyWorkingTime", employer.MonthlyWorkingMinutes);
            sqlCommand.ExecuteNonQuery();
            sqlcon.Close();
        }
        public static void GetAllWorkInfo()
        {
            SqlConnection sqlcon = new SqlConnection(connectiondata);
            SqlDataReader sqlDataReader;
            sqlcon.Open();
            string query = "Select ID,Name,Surname,JobStartDate,Address,WageRate from Employee;";
            SqlCommand sqlCommand = new SqlCommand(query, sqlcon);
            sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {

                Console.WriteLine("ID :" + sqlDataReader.GetValue(0));
                Console.WriteLine("Adi :" + sqlDataReader.GetValue(1));
                Console.WriteLine("Soyadi :" + sqlDataReader.GetValue(2));
                Console.WriteLine("Ise baslama tarixi :" + sqlDataReader.GetValue(3));
                Console.WriteLine("Adresi :" + sqlDataReader.GetValue(4));
                Console.WriteLine("Maas emsali : " + sqlDataReader.GetValue(5));
                Console.WriteLine("-----------------------------------------------");
            }
            sqlcon.Close();
        }
        public static void Entrance()
        {
            int beinglatecount = 0;
            Console.WriteLine("Isci ID Daxil edin :");
            Console.WriteLine("Isci siyahisi yuklenir ...");
            Person.GetAllWorkInfo();
            string pendingID = Console.ReadLine();
            Thread.Sleep(100);
            SqlConnection sqlcon1 = new SqlConnection(connectiondata);
            SqlConnection sqlcon2 = new SqlConnection(connectiondata);
            sqlcon1.Open();
            sqlcon2.Open();

            SqlDataReader sqlDataReader;
            SqlConnection sqlcon = new SqlConnection(connectiondata);
            sqlcon.Open();
            string query = "Select EmployeeID,BeingLateDate,BeingLateTime from BeingLateTab;";
            SqlCommand sqlCommand = new SqlCommand(query, sqlcon);
            sqlDataReader = sqlCommand.ExecuteReader();
            sqlDataReader.Read();

            beinglatecount = (int)sqlDataReader.GetValue(2) + 1;


            string squery = "insert into WorkingTimeTable(EmployeeID,Date,EntranceHour,EntranceMinute) values(@EmployeeID,@Date,@EntranceHour,@EntranceMinute)";
            SqlCommand sqlCommand1 = new SqlCommand(squery, sqlcon1);
            sqlCommand1.Parameters.AddWithValue("@EmployeeID", pendingID);
            sqlCommand1.Parameters.AddWithValue("@Date", DateTime.Now);
            sqlCommand1.Parameters.AddWithValue("@EntranceHour", DateTime.Now.Hour);
            sqlCommand1.Parameters.AddWithValue("@EntranceMinute", DateTime.Now.Minute);
            sqlCommand1.ExecuteNonQuery();


            string squerys = $"UPDATE BeingLateTab SET BeingLateTime = {beinglatecount} , BeingLateDate = @BeingLateDate  WHERE EmployeeID=@EmployeeID)";
            SqlCommand sqlCommandd = new SqlCommand(squerys, sqlcon2);
            if ((Convert.ToInt32(DateTime.Now.Hour) > 9 || (Convert.ToInt32(DateTime.Now.Hour) == 9 && Convert.ToInt32(DateTime.Now.Minute) > 0)))
            {

                sqlCommandd.Parameters.AddWithValue("@EmployeeID", pendingID);
                sqlCommandd.Parameters.AddWithValue("@BeingLateDate", DateTime.Now);
                sqlCommandd.Parameters.AddWithValue("@BeingLateTime", beinglatecount);
            }

            sqlCommandd.ExecuteNonQuery();
            sqlcon.Close();
            sqlcon1.Close();
            sqlcon2.Close();

        }
        public static void Exit()
        {
            Console.WriteLine("Proqram , avtamatik olaraq hal hazirdaki saati goturur . ");
            Console.WriteLine("Isci ID Daxil edin :");
            Console.WriteLine("Isci siyahisi yuklenir ...");
            Person.GetAllWorkInfo();
            string pendingID = Console.ReadLine();
            Thread.Sleep(100);
            SqlConnection sqlcon = new SqlConnection(connectiondata);
            sqlcon.Open();
            string squery = "insert into WorkingTimeTable(EmployeeID,Date,ExitHour,ExitMinute) values(@EmployeeID,@Date,@ExitHour,@ExitMinute)";
            SqlCommand sqlCommand = new SqlCommand(squery, sqlcon);
            sqlCommand.Parameters.AddWithValue("@EmployeeID", pendingID);
            sqlCommand.Parameters.AddWithValue("@Date", DateTime.Now);
            sqlCommand.Parameters.AddWithValue("@ExitHour", DateTime.Now.Hour);
            sqlCommand.Parameters.AddWithValue("@ExitMinute", DateTime.Now.Minute);
            sqlCommand.ExecuteNonQuery();
            sqlcon.Close();
        }
        public static void FindInfobyID()
        {
            Console.WriteLine("Isci ID Daxil edin :");
            SqlDataReader sqlDataReader;
            int pendingID = Convert.ToInt32(Console.ReadLine());
            Thread.Sleep(100);
            SqlConnection sqlcon = new SqlConnection(connectiondata);
            sqlcon.Open();
            string query = "Select ID,Name,Surname,JobStartDate,Address,WageRate from Employee;";
            SqlCommand sqlCommand = new SqlCommand(query, sqlcon);
            sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                if (pendingID == (int)sqlDataReader.GetValue(0))
                {
                    Console.WriteLine("Adi :" + sqlDataReader.GetValue(1));
                    Console.WriteLine("Soyadi :" + sqlDataReader.GetValue(2));
                    Console.WriteLine("Ise baslama tarixi :" + sqlDataReader.GetValue(3));
                    Console.WriteLine("Adresi :" + sqlDataReader.GetValue(4));
                    Console.WriteLine("Maas emsali : " + sqlDataReader.GetValue(5));
                }
            }

            sqlcon.Close();

        }
        public static void GetPersonelInfobyPosition()
        {
            Console.WriteLine("Isci Vezifesi Daxil edin : (Junior/Front-End/Back-End/Senior/Full-Stack)");
            SqlDataReader sqlDataReader;
            string pendingPosition = Console.ReadLine();
            double searchedPositionSalaryRate = 0;
            Thread.Sleep(100);
            SqlConnection sqlcon = new SqlConnection(connectiondata);
            sqlcon.Open();
            string query = "Select ID,Name,Surname,JobStartDate,Address,WageRate from Employee;";
            SqlCommand sqlCommand = new SqlCommand(query, sqlcon);
            sqlDataReader = sqlCommand.ExecuteReader();
            switch (pendingPosition)
            {
                case "Junior":
                    searchedPositionSalaryRate = 2;
                    break;
                case "Front-End":
                    searchedPositionSalaryRate = 2.5;
                    break;
                case "Back-End":
                    searchedPositionSalaryRate = 3;
                    break;
                case "Senior":
                    searchedPositionSalaryRate = 3.5;
                    break;
                case "Full-Stack":
                    searchedPositionSalaryRate = 4;
                    break;
                default:
                    break;
            }
            while (sqlDataReader.Read())
            {
                if (searchedPositionSalaryRate == Convert.ToDouble(sqlDataReader.GetValue(5)))
                {
                    Console.WriteLine("ID : " + sqlDataReader.GetValue(0));
                    Console.WriteLine("Adi :" + sqlDataReader.GetValue(1));
                    Console.WriteLine("Soyadi :" + sqlDataReader.GetValue(2));
                    Console.WriteLine("Ise baslama tarixi :" + sqlDataReader.GetValue(3));
                    Console.WriteLine("Adresi :" + sqlDataReader.GetValue(4));
                    Console.WriteLine("------------------------------------------------------");
                }

            }
        }
        public static void ShowLateInfo()
        {
            SqlConnection sqlcon = new SqlConnection(connectiondata);
            Random beinglateinfo = new Random();
            int beniglatetime = beinglateinfo.Next(1, 50);
            SqlDataReader sqlDataReader;
            sqlcon.Open();
            string query = "Select EmployeeID,BeingLateDate,BeingLateTime from BeingLateTab;";
            SqlCommand sqlCommand = new SqlCommand(query, sqlcon);
            sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Console.WriteLine("ID :" + sqlDataReader.GetValue(0));
                Console.WriteLine("Gecikme Tarixi :" + sqlDataReader.GetValue(1));
                Console.WriteLine("Gecikme sayi :" + beniglatetime);
            }
            sqlcon.Close();
        }

        public static void ShowWorkingHourbyDate()
        {
            Console.WriteLine("Gormek istediyiniz Tarixi Daxil edin : ()");
            DateTime quest = Convert.ToDateTime(Console.ReadLine());
            SqlConnection sqlcon = new SqlConnection(connectiondata);
            SqlDataReader sqlDataReader;
            sqlcon.Open();
            string query = "Select EmployeeID,Date,EntranceHour,EntranceMinute,ExitHour,ExitMinute from WorkingTimeTable";
            SqlCommand sqlCommand = new SqlCommand(query, sqlcon);
            sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                Console.WriteLine("ID :" + sqlDataReader.GetValue(0));
                if(sqlDataReader.GetValue(2) != DBNull.Value )
                {
                    Console.WriteLine("Giris :" + sqlDataReader.GetValue(2) + ":" + sqlDataReader.GetValue(3) + "");
                }
                if(sqlDataReader.GetValue(4) != DBNull.Value)
                {
                    Console.WriteLine("Cixis :" + sqlDataReader.GetValue(4) + ":" + sqlDataReader.GetValue(5));
                }

                
            }
            sqlcon.Close();


        }
    }
}


