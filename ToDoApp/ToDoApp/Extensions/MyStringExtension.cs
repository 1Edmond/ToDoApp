using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.Extensions
{
    internal static class MyStringExtension
    {

        public static string PremiereLettreMajiscule(this string lettre) => lettre.Substring(0,1).ToUpper() + lettre.Substring(1);
        



    }
}
