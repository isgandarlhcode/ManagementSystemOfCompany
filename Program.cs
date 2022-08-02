using KadrMelumatlariApp;
using KadrMelumatlariApp.ToolKits;
using System;
using System.Threading;

namespace KadrMelumatlarApp

{
    class Program
    {
        static void Main(string[] args)
        {
        START:
            try
            {
                Console.WriteLine("------------------ CodeAcademy Internal Employee Management Application 1.0 Beta ---------------------- ");
                Console.WriteLine("------------------------------------ Menyu --------------------------------------------------------------");
                bool tocontunie = true;
                do
                {
                    Console.WriteLine("1. Isci giris  "); // 
                    Thread.Sleep(150);
                    Console.WriteLine("2. Isci cixis "); // 
                    Thread.Sleep(150);
                    Console.WriteLine("3 . Sorgulama 1. Bir Iscinin melumatlarinin gosterilmesi (ID ile )"); //
                    Thread.Sleep(150);
                    Console.WriteLine("4. Sorgulama 2. Bir Iscinin Ayliq Maasinin Is Saatina gore hesablanmasi");
                    Thread.Sleep(150);
                    Console.WriteLine("5. Sorgulama 3. Vezifeye gore Iscilerin Siyahisi "); // 
                    Thread.Sleep(150);
                    Console.WriteLine("6. Sorgulama 4. Ise gec gelen iscilerin siyahisi");
                    Thread.Sleep(150);
                    Console.WriteLine("7. Sorgulama 5. Butun iscilerin siyahisi "); //
                    Thread.Sleep(150);
                    Console.WriteLine("8. Sorgulama 6. Ayin Tarixine gore Iscilerin Giris ve Cixis Saatlari");
                    Console.WriteLine("8. Yenileme 1. Yeni iscinin elave edilmesi ."); //
                    Thread.Sleep(150);
                    Console.WriteLine("9. Yenileme 2. Isci melumatlarinin yenilenmesi. "); //
                    Thread.Sleep(150);
                    Console.WriteLine("10. Yenileme 3. Isci melumatlarinin silinmesi  "); //

                    string query = Console.ReadLine();
                    switch (query)
                    {
                        case "1":
                            Person.Entrance();
                            break;
                        case "2":
                            Person.Exit();
                            break;
                        case "3":
                            Person.FindInfobyID();
                            break;
                        case "4":
                            Person.SalaryCounterandShow();
                            break;
                        case "5":
                            Person.GetPersonelInfobyPosition();
                            break;
                        case "6":
                            Person.ShowLateInfo();
                            break;
                        case "7":
                            Person.GetAllWorkInfo();
                            break;
                        case "8":
                            Person.ShowWorkingHourbyDate();
                            break;
                        case "9":
                            Person.PersonAdd();
                            break;
                        case "10":
                            Person.UpdatePerson();
                            break;
                        case "11":
                            Person.DeletePerson();
                            break;
                    }
                    Thread.Sleep(1000);
                    Console.WriteLine("Davam etmek isteyirsinizmi ? 1 . Menyuya qayitmaq 2 . Cixis");
                    int tocontunieornot = Convert.ToInt32(Console.ReadLine());
                    if (tocontunieornot != 1)
                    {
                        tocontunie = false;
                    }
                } while (tocontunie);

            }
            catch (Exception)
            {

                Console.WriteLine("Proqram duzgun islemedi . Xahis olunur duzgun deyisenler daxil edesiniz .");
                goto START;
            }
        }
    }
}

