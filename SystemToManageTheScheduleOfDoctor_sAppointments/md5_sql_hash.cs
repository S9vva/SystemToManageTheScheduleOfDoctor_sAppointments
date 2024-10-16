using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace md5_hash
{
    internal class md5_sql_hash
    {
        public static string hashPassword(string password)
        {
            MD5 md5 = MD5.Create();

            byte[] b = Encoding.ASCII.GetBytes(password);
            byte[] hash = md5.ComputeHash(b);
            
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var b2 in hash )
                stringBuilder.Append(b2.ToString("X2"));

            return Convert.ToString(stringBuilder);
        }
    }
}
