using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models
{
    /// <summary>
    /// Description of the login model.
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// User login.
        /// </summary>
        [Required(ErrorMessage = "Не указан никнэйм")]
        public string Login { get; set; }

        /// <summary>
        /// User password.
        /// </summary>
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
