using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesForGameStore
{
    public interface IReport
    {
        string InvalidSymbols { get; }

        string ShortLogin { get; }

        string LongLogin { get; }

        string ShortPassword { get; }

        string LongPassword { get; }

        string PasswordsDontMacth { get; }

        string LoginBusy { get; }
    }
}
