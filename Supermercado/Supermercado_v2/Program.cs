using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Supermercado_v2
{
    class Program
    {
        static void Main(string[] args)
        {
            Stock stock = new Stock();
            Registos registos = new Registos();
            ListaFaturas faturas = new ListaFaturas();




            //registos.RegistarFuncionario("gerente", "123", "gerente");

            //Carregar os ficheiros
            stock.leituraStock();
            registos.leituraRegistos();
            faturas.leituraFaturas();
            //Login
            Console.WriteLine("############################\n");
            Console.WriteLine("\t   LOGIN\n");
            Console.Write("Username  : ");
            string username = Console.ReadLine();
            Console.Write("Password  : ");
            string pw = Console.ReadLine();
            Console.WriteLine("\n");
            Console.WriteLine("############################\n");
            Console.ReadKey();


            string resultadoLogin = registos.Login(username, pw);

            if (String.Compare("gerente", resultadoLogin, true) == 0)
            {
            startGerente:
                Console.Clear();
                Console.WriteLine("############################\n");
                Console.WriteLine("\t   Menu\n");
                Console.WriteLine("1 - Adicionar Funcionário");
                Console.WriteLine("2 - Apagar Funcionário");
                Console.WriteLine("3 - Lista de Funcionário");
                Console.WriteLine("4 - Vender");
                Console.WriteLine("5 - Faturas");
                Console.WriteLine("0 - Logout");
                Console.WriteLine("\n############################\n");

                string respostaMenu = Console.ReadLine();

                switch (respostaMenu)
                {
                    case "1":
                        Console.Clear();
                        Console.Write("\t Username  : ");
                        string usernameNovo = Console.ReadLine();
                        Console.Write("\t Password  : ");
                        string pwNova = Console.ReadLine();
                        Console.Write("\t Cargo  : ");
                        string cargoNovo = Console.ReadLine();
                        Console.WriteLine("\n");
                        Console.Clear();

                        //RegistarFuncionario() devolve:
                        // 0 se o utilizador já existir
                        // 1 se o registo for aceite
                        int resultadoRegisto = registos.RegistarFuncionario(usernameNovo, pwNova, cargoNovo);

                        if (resultadoRegisto == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Já existe");
                            Console.ReadKey();
                            goto startGerente;
                        }

                        else
                        {
                            Console.WriteLine("Registo efetuado com sucesso");
                            registos.SaveRegistos();
                            Console.ReadKey();
                            goto startGerente;
                        }
                        break;

                    case "2":
                        Console.Clear();
                        Console.WriteLine("Insira o username do funcionario a apagar: ");
                        string userApagar = Console.ReadLine();

                        //apagarFuncionarios devolve:
                        // 1 se apagar
                        // 0 user não existir
                        int apagou = registos.apagarFuncionario(userApagar);
                        if (apagou == 1)
                        {
                            Console.Clear();
                            Console.WriteLine("User apagado com sucesso");
                            Console.ReadKey();
                            goto startGerente;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("User não existe");
                            Console.ReadKey();
                            goto startGerente;
                        }
                        break;

                    case "3":
                        Console.Clear();
                        registos.ListarFuncionarios();
                        Console.ReadKey();
                        goto startGerente;
                        break;

                    case "4":
                        Console.Clear();
                        Console.WriteLine("Insira o nome do cliente:");
                        string nomeCliente = Console.ReadLine();
                        Console.Clear();
                        string descProduto = "";
                        List<Produto> listaProdutosVendidos = new List<Produto>();
                        float preçoTotal = 0;
                        ArrayList arrayQuantidades = new ArrayList();

                        do
                        {
                            stock.ListarProdutos();
                            Console.WriteLine("\n\nInsira a descrição do produto a adicionar");
                            descProduto = Console.ReadLine();


                            if (descProduto == "0") break;
                            else
                            {

                                Console.WriteLine("Insira a quantidade do produto a adicionar:");
                                int quantidade = int.Parse(Console.ReadLine());
                                arrayQuantidades.Add(quantidade);

                                stock.venderProduto(descProduto, quantidade);
                                Console.Clear();
                                listaProdutosVendidos.Add(stock.getProduto(descProduto));
                                preçoTotal += stock.getProduto(descProduto).preço * quantidade;
                            }
                        } while (descProduto != "0");


                        faturas.RegistarFatura(username, nomeCliente, preçoTotal, listaProdutosVendidos, arrayQuantidades);
                        faturas.SaveFaturas();
                        Console.Clear();
                        Fatura faturaNova = new Fatura();
                        faturaNova = faturas.GetFatura(username, nomeCliente, preçoTotal, listaProdutosVendidos);


                        Console.WriteLine(faturaNova.ToString());
                        Console.ReadKey();
                        goto startGerente;

                        break;

                    case "5":
                        Console.Clear();
                        faturas.ListarFaturas();
                        Console.ReadKey();
                        goto startGerente;
                        break;
                        break;
                    case "0":
                        //Dá restart na aplicação
                        //"Logout"                       
                        var fileName = Assembly.GetExecutingAssembly().Location;
                        System.Diagnostics.Process.Start(fileName);
                        Environment.Exit(0);
                        break;

                    default:
                        break;
                }

            }

            if (String.Compare("repositor", resultadoLogin, true) == 0)
            {
            startRepositor:
                Console.Clear();
                Console.WriteLine("############################\n");
                Console.WriteLine("\t   Menu\n");
                Console.WriteLine("\n1 - Listar Produtos");
                Console.WriteLine("2 - Adicionar Produtos");
                Console.WriteLine("3 - Atualizar stock");
                Console.WriteLine("4 - Remover Produtos");
                Console.WriteLine("5 - Limpar Lista");
                Console.WriteLine("0 - Logout");
                Console.WriteLine("\n############################\n");
                string respostaRepositor = Console.ReadLine();

                switch (respostaRepositor)
                {
                    case "0":
                        //Dá restart na aplicação
                        //"Logout"
                        var fileName = Assembly.GetExecutingAssembly().Location;
                        System.Diagnostics.Process.Start(fileName);
                        Environment.Exit(0);
                        break;

                    case "2":
                        //AdicionarProduto
                        //Devolve 1 se adicionar corretamente
                        //Devolve 0 se não adicionar

                        Console.Clear();
                        Console.WriteLine("Escolha a categoria:");
                        Console.WriteLine("1 - Congelados");
                        Console.WriteLine("2 - Prateleira");
                        Console.WriteLine("3 - Enlatados");
                        int escolha = int.Parse(Console.ReadLine());
                        Produto.Categoria categoriaa = (Produto.Categoria)escolha;

                        Console.Clear();
                        Console.WriteLine("Descrição: ");
                        string descricaoProduto = Console.ReadLine();
                        Console.WriteLine("Preço:");
                        float preço = float.Parse(Console.ReadLine());
                        Console.WriteLine("Quantidade a adicionar:");
                        int quantidade = int.Parse(Console.ReadLine());

                        int resultadoAdicionar = stock.AdicionarProduto(categoriaa, descricaoProduto, preço, quantidade);
                        if (resultadoAdicionar == 1)
                        {
                            Console.Clear();
                            Console.WriteLine("Adicionado com Sucesso!");
                            Console.ReadKey();
                            goto startRepositor;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Nao Adicionou!");
                            Console.ReadKey();
                            goto startRepositor;
                        }

                        break;

                    case "1":
                        Console.Clear();
                        stock.ListarProdutos();
                        Console.ReadKey();
                        goto startRepositor;
                        break;


                    case "4":
                        Console.Clear();
                        Console.WriteLine("Insira o produto a apagar: ");
                        string produtoApag = Console.ReadLine();
                        int x = stock.RemoverStock(produtoApag);

                        if (x == 1)
                        {
                            Console.Clear();
                            Console.WriteLine("Produto apagado com sucesso");
                            Console.ReadKey();
                            goto startRepositor;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Produto não existe");
                            Console.ReadKey();
                            goto startRepositor;
                        }

                    case "3":
                        Console.Clear();
                        Console.WriteLine("Insira o produto a atualizar: ");
                        string produtoAtualizar = Console.ReadLine();
                        Console.WriteLine("Insira a quantidade a atualizar: +/- quantidade");
                        string quantiAtualizar = Console.ReadLine();
                        int novaQuantidade = stock.AtualizarStock(produtoAtualizar, quantiAtualizar);
                        Console.Clear();
                        if (novaQuantidade > 0)
                        {

                            Console.WriteLine("Stock atualizado com sucesso!\nO seu produto tem agora {0} unidades", novaQuantidade);
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Stock atualizado com sucesso!");
                            Console.ReadKey();
                            Console.Clear();
                            Console.WriteLine("Produto: {0} encontra-se sem stock", produtoAtualizar);
                            Console.ReadKey();
                        }
                        goto startRepositor;

                    default:

                        break;
                }
            }

            if (String.Compare("caixa", resultadoLogin, true) == 0)
            {
            startCaixa:
                Console.Clear();
                Console.WriteLine("############################\n");
                Console.WriteLine("\t   Menu\n");
                Console.WriteLine("1 - Vender");
                Console.WriteLine("\n0 - Logout");
                Console.WriteLine("\n############################\n");
                string respostaCaixa = Console.ReadLine();

                switch (respostaCaixa)
                {
                    case "0":
                        var fileName = Assembly.GetExecutingAssembly().Location;
                        System.Diagnostics.Process.Start(fileName);
                        Environment.Exit(0);
                        break;
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Insira o nome do cliente:");
                        string nomeCliente = Console.ReadLine();
                        Console.Clear();
                        string descProduto = "";
                        List<Produto> listaProdutosVendidos = new List<Produto>();
                        float preçoTotal = 0;
                        ArrayList arrayQuantidades = new ArrayList();
                        string x = "";

                        do
                        {
                            stock.ListarProdutos();
                            Console.WriteLine("\n\nInsira a descrição do produto a adicionar");
                            descProduto = Console.ReadLine();


                            if (descProduto == "0") break;
                            else
                            {

                                Console.WriteLine("Insira a quantidade do produto a adicionar:");
                                int quantidade = int.Parse(Console.ReadLine());
                                x = quantidade.ToString();
                                stock.AtualizarStockFatura(descProduto, x);        //Remove a quantidade 2x
                                arrayQuantidades.Add(quantidade);
                                stock.venderProduto(descProduto, quantidade);
                                Console.Clear();
                                listaProdutosVendidos.Add(stock.getProduto(descProduto));
                                preçoTotal += stock.getProduto(descProduto).preço * quantidade;                                                             
                            }
                        } while (descProduto != "0");
                    
                        faturas.RegistarFatura(username, nomeCliente, preçoTotal, listaProdutosVendidos, arrayQuantidades);             
                        faturas.SaveFaturas();
                        Console.Clear();
                        Fatura faturaNova = new Fatura();
                        faturaNova = faturas.GetFatura(username, nomeCliente, preçoTotal, listaProdutosVendidos);
                        


                        Console.WriteLine(faturaNova.ToString());
                        Console.ReadKey();
                        goto startCaixa;

                    default:

                        break;

                }           
            }
        }
    }
}
