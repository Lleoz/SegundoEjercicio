using System;

namespace Ejercicio2.Api.Transversal.Common.Tools
{
    public static class DateTool
    {
        public static int GetAge(DateTime birthDate)
        {
            int age = 0;
            age = DateTime.Now.Year - birthDate.Year;
            if (DateTime.Now.DayOfYear < birthDate.DayOfYear)
            {
                age = age - 1;
            }

            return age;
        }
    }
}
