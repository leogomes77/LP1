using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermercado
{
    [Serializable]
    class User
    { 
        public string username;
        public string password;
        public string cargo;

        public User(string username, string password, string cargo)
        {
            this.username = username;
            this.password = password;
            this.cargo = cargo;
        }

        public override string ToString()
        {
            return " | Username: " + username + " | Password:" + password + " | Cargo:" + cargo;
        }
    }
}
