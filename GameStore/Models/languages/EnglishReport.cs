using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesForGameStore
{
    public class EnglishReport : IReport
    {
        public string InvalidSymbols => "Invalid symbols."; 

        public string LongLogin => "Long login. Login length should be less or equal 20."; 

        public string LongPassword => "Long password. Password length should be less or equal 20."; 

        public string PasswordsDontMacth => "Passwords don`t macth."; 

        public string ShortLogin => "Short login. Login length should be more or equal 4."; 

        public string ShortPassword => "Short password. Password length should be more or equal 6.";

        public string LoginBusy => "Login busy.";
    }
}
