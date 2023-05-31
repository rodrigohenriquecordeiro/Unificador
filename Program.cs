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
            List<string> lstArquivos = new List<string>();
            ConsoleColor corTextoOriginal = Console.ForegroundColor;

            Console.WriteLine(">---> Unificador de Arquivos <---<");
            Console.Write("\nDigite o local da pasta em que pretende trabalhar: ");

            Console.ForegroundColor = ConsoleColor.Green;
            string pasta = Console.ReadLine();
            Console.ForegroundColor = corTextoOriginal;

            Console.Write("Quantos arquivos você pretende unificar? ");
            Console.ForegroundColor = ConsoleColor.Green;
            int quantidadeDeArquivos = int.Parse(Console.ReadLine());
            Console.ForegroundColor = corTextoOriginal;

            for (int i = 1; i <= quantidadeDeArquivos; i++)
            {
                Console.Write($"\nDigite o nome do {i}º arquivo: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string[] lines = File.ReadAllLines($@"{pasta}\{Console.ReadLine()}.csv");

                foreach (string line in lines)
                    lstArquivos.Add(line);

                LogLinhasAdicionadas(corTextoOriginal, lines);
            }
            Console.WriteLine();
            Console.ForegroundColor = corTextoOriginal;
            EscreveArquivoUnificado(lstArquivos, pasta);
            FinalizaProcessamento(lstArquivos, corTextoOriginal);
            
            Console.WriteLine();
            Console.ForegroundColor = corTextoOriginal;
            Console.Write("\nTecle enter para sair ");
            Console.ReadLine();
        }

        private static void LogLinhasAdicionadas(ConsoleColor corTextoOriginal, string[] lines)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\tForam adicionados {lines.Count() - 1} linhas ao Arquivo Unificado");
            Console.ForegroundColor = corTextoOriginal;
        }

        private static void EscreveArquivoUnificado(List<string> lstArquivos, string pasta)
        {
            CorrigeCabecalhoDaLista(lstArquivos);

            using (StreamWriter sw = File.AppendText($@"{pasta}\ArquivoUnificado.csv"))
            {
                foreach (string line in lstArquivos)
                    sw.WriteLine(line);
            }
        }

        private static List<string> CorrigeCabecalhoDaLista(List<string> lstArquivos)
        {
            string cabecalho = lstArquivos[0];

            foreach (var item in lstArquivos.Skip(0))
                if (cabecalho == item) lstArquivos.Remove(cabecalho);

            lstArquivos.Insert(0, cabecalho);
            return lstArquivos;
        }

        private static void FinalizaProcessamento(List<string> lstArquivos, ConsoleColor corTextoOriginal)
        {
            Console.WriteLine($"Processamento finalizado com sucesso!");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"\tGerado Arquivo Unificado com ");
            Console.Write($"{CorrigeCabecalhoDaLista(lstArquivos).Count - 1} linhas de conteúdo");
            Console.ForegroundColor = corTextoOriginal;
        }
    }
}
