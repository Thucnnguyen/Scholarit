﻿using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace Scholarit.Utils
{
    public class PasswordHelper
    {
        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                return BitConverter.ToString(hashBytes).Replace("-", "");
            }
        }
    }
}
