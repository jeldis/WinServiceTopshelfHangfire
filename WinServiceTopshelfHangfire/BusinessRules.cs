using System;

namespace WinServiceTopshelfHangfire
{
    public class BusinessRules
    {
        public static void Execute(DateTime date)
        {
            Console.WriteLine($"Se ejecutó el servicio {date}");
        }
    }
}
