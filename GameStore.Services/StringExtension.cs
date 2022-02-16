using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Services
{
    public static class StringExtensions
    {
        public static byte[] GetMD5Hash(this string str)
        {
            var hasher = MD5.Create();
            return hasher.ComputeHash(Encoding.ASCII.GetBytes(str));
        }
    }
}
