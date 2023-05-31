using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Unificador
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = string.Empty;
            List<string> lstArquivos = new List<string>();
            
            Console.WriteLine(">---> Unificador de Arquivos <---<");
            Console.Write("\nDigite o local da pasta em que pretende trabalhar: ");
            string pasta = Console.ReadLine();

            Console.Write("Quantos arquivos você pretende unificar? ");
            int quantidadeDeArquivos = int.Parse(Console.ReadLine());

            for (int i = 1; i <= quantidadeDeArquivos; i++)
            {
                if (i == 1)
                {
                    Console.Write($"Digite o nome do {i}º arquivo: "); string arquivo = Console.ReadLine();
                    path = $@"{pasta}\{arquivo}.csv";
                    string arquivoUnificado = $@"{pasta}\ArquivoUnificado.csv";
                    string[] lines = File.ReadAllLines(path);
                    using StreamWriter sw = File.AppendText(arquivoUnificado);
                    foreach (string line in lines) lstArquivos.Add(line);
                }
                else
                {
                    Console.Write($"Digite o nome do {i}º arquivo: "); string arquivo = Console.ReadLine();
                    path = $@"{pasta}\{arquivo}.csv";
                    string arquivoUnificado = $@"{pasta}\ArquivoUnificado.csv";
                    string[] lines = File.ReadAllLines(path);
                    using StreamWriter sw = File.AppendText(arquivoUnificado);
                    foreach (string line in lines.Skip(1)) lstArquivos.Add(line);
                }
            }

            foreach (var item in lstArquivos)
            {
                Console.WriteLine($"{item}");
            }
            Console.ReadLine();
            //Console.Write("Digite o nome do primeiro arquivo: ");
            //string primeiroArquivo = Console.ReadLine();

            //Console.Write("Digite o nome do segundo arquivo: ");
            //string segundoArquivo = Console.ReadLine();

            //Console.WriteLine($"Pasta: {pasta}" +
            //    $"\nPrimeiro Arquivo: {primeiroArquivo}" +
            //    $"\nSegundo Arquivo: {segundoArquivo}");

            //string path1 = @$"{pasta}\{primeiroArquivo}.csv";
            //string path2 = @$"{pasta}\{segundoArquivo}.csv";

            //Console.WriteLine($"Path1: {path1}");
            //Console.WriteLine($"Path2: {path2}");
            //Console.ReadLine();

            //string path1 = @"C:\Projetos\Back-End\CSharp\ProjetosPessoais\Unificador\Arquivos\Sudeste.csv";
            //string path2 = @"C:\Projetos\Back-End\CSharp\ProjetosPessoais\Unificador\Arquivos\Sul.csv";
            //string arquivoUnificado = @"C:\Projetos\Back-End\CSharp\ProjetosPessoais\Unificador\Arquivos\SulSudeste.csv";

            //try
            //{
            //    string[] lines1 = File.ReadAllLines(path1);
            //    string[] lines2 = File.ReadAllLines(path2);

            //    using (StreamWriter sw = File.AppendText(arquivoUnificado))
            //    {
            //        foreach (string line in lines1) 
            //        {
            //            sw.WriteLine(line);
            //        }
            //    }

            //    using (StreamWriter sw = File.AppendText(arquivoUnificado))
            //    {
            //        foreach (string line in lines2.Skip(1))
            //        {
            //            sw.WriteLine(line);
            //        }
            //    }

            //    Console.WriteLine("Unificação finalizada com sucesso");
            //}
            //catch (IOException e)
            //{
            //    Console.WriteLine(e.Message);
            //}
        }
    }
}
