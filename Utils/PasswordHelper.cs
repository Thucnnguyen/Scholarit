using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using BC = BCrypt.Net.BCrypt;
namespace Scholarit.Utils
{
    public class PasswordHelper
    {
        public string HashPassword(string password)
        {
          
            string pwdHash = BC.HashPassword(password);
            return pwdHash;
        }

        public bool CheckHashPwd(string input, string hashPwd)
        {
            bool verified = BC.Verify(input, hashPwd);
            return verified;
        }
    }
}
