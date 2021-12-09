using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models
{
    public class RegisterModel : LoginModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Пароль не подтверждён")]
        public string ConfirmPassword { get; set; }
    }
}
