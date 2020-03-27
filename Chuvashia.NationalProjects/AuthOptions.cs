using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chuvashia.NationalProjects
{
    public class AuthOptions
    {
        public const string ISSUER = "Server"; // издатель токена
        public const string AUDIENCE = "Client"; // потребитель токена
        const string KEY = "nZI8dk1ToRvY7ddJ";   // ключ для шифрации
        public const int LIFETIME = 60; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
