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
            string path = string.Empty, arquivoUnificado = string.Empty;
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
                    Console.Write($"\nDigite o nome do {i}º arquivo: "); string arquivo = Console.ReadLine();
                    path = $@"{pasta}\{arquivo}.csv";
                    arquivoUnificado = $@"{pasta}\ArquivoUnificado.csv";
                    string[] lines = File.ReadAllLines(path);
                    using StreamWriter sw = File.AppendText(arquivoUnificado);
                    foreach (string line in lines)
                        lstArquivos.Add(line);
                    Console.WriteLine($"Foram adicionados {lines.Skip(1).Count()} linhas ao Arquivo Unificado");
                }
                else
                {
                    Console.Write($"\nDigite o nome do {i}º arquivo: "); string arquivo = Console.ReadLine();
                    path = $@"{pasta}\{arquivo}.csv";
                    arquivoUnificado = $@"{pasta}\ArquivoUnificado.csv";
                    string[] lines = File.ReadAllLines(path);
                    using StreamWriter sw = File.AppendText(arquivoUnificado);
                    foreach (string line in lines.Skip(1)) 
                        lstArquivos.Add(line);
                    Console.WriteLine($"Foram adicionados {lines.Skip(1).Count()} linhas ao Arquivo Unificado");
                }
            }
            Console.WriteLine();

            using (StreamWriter sw = File.AppendText(arquivoUnificado))
            {
                foreach (string line in lstArquivos)
                {
                    sw.WriteLine(line);
                }
            }
            Console.WriteLine("Unificação finalizada com sucesso");
        }
    }
}
