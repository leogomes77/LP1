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

            //User user1 = new User("teste1", "teste1", "Gerente");
            //User user2 = new User("teste2", "teste2", "Funcionario");




            listaUtilizadores.leituraUsers();

            int resposta;
            Console.WriteLine("Escolha a opção:\n");
            Console.WriteLine("1 - Login");
            Console.WriteLine("2 - Registar");
            resposta = int.Parse(Console.ReadLine());

            
            switch(resposta){
                case 1:
                    listaUtilizadores.Login();
                    break;

                case 2:

                    Console.Clear();
                    Console.Write("\t Username  : ");
                    string user1 = Console.ReadLine();
                    Console.Write("\t Password  : ");
                    string pw1 = Console.ReadLine();
                    Console.WriteLine("\n");
                    break;

                default:
                    Console.WriteLine("Não é possivel");
                    break;
            }


            

        


           Console.ReadKey();
            
        }
    }
}
