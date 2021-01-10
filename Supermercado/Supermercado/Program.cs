using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermercado
{
    class Program
    {
        static void Main(string[] args)
        {

            ListaUsers listaUtilizadores = new ListaUsers();

            listaUtilizadores.leituraUsers();
            listaUtilizadores.Login();

           Console.ReadKey();
            
        }
    }
}
