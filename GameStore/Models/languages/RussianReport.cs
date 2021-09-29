using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesForGameStore.languages
{
    public class RussianReport : IReport
    {
        public string InvalidSymbols => "Логин содержит запрещённые символы.";

        public string LongLogin => "Логин слишком длинный. Длинна логина должна быть меньше или равна 20.";

        public string LongPassword => "Пароль слишком длинный. Длинна пароля должна быть меньше или равна 20";

        public string PasswordsDontMacth => "Парольи не совпадают.";

        public string ShortLogin => "Логин слишком короткий. Длинна логина должна быть больше или равна 4.";

        public string ShortPassword => "Пароль слишком короткий. Длинна пароля должна быть больше или равна 6.";

        public string LoginBusy => "Логин занят.";
    }
}
