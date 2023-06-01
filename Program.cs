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
            ConsoleColor corTextoOriginal = Console.ForegroundColor;
            List<string> lstArquivos = new List<string>();
            List<string> lstArquivosInseridos = new List<string>();
            Console.WriteLine(">---> Unificador de Arquivos <---<");

            Console.Write("\nDigite o local da pasta em que pretende trabalhar: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string pasta;
            while (ValidaLocalDoArquivo(pasta = Console.ReadLine()))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\tVocê precisa inserir um caminho válido, tente novamente!");
                Console.ForegroundColor = corTextoOriginal;
                Console.Write("\nDigite o local da pasta em que pretende trabalhar: ");
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.ForegroundColor = corTextoOriginal;

            Console.Write("O cabeçalho no arquivo possui quantas linhas? ");
            Console.ForegroundColor = ConsoleColor.Green;
            int cabecalho = 1;
            while (!int.TryParse(Console.ReadLine(), out cabecalho))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\tValor inválido, tente novamente!");
                Console.ForegroundColor = corTextoOriginal;
                Console.Write("\nO cabeçalho no arquivo possui quantas linhas? ");
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.ForegroundColor = corTextoOriginal;

            int quantidadeDeArquivos = QuantidadeDeArquivos(corTextoOriginal);
            for (int i = 1; i <= quantidadeDeArquivos; i++)
            {
                string[] lines = ValidaArquivo(lstArquivos, lstArquivosInseridos, pasta, i);
                LogLinhasAdicionadas(corTextoOriginal, lines, cabecalho);
            }
            Console.WriteLine();
            Console.ForegroundColor = corTextoOriginal;
            EscreveArquivoUnificado(lstArquivos, pasta, cabecalho);
            FinalizaProcessamento(lstArquivos, corTextoOriginal, cabecalho);

            Console.WriteLine();
            Console.ForegroundColor = corTextoOriginal;
            Console.Write("\nTecle enter para sair ");
            Console.ReadLine();
        }

        private static bool ValidaLocalDoArquivo(string pasta)
        {
            return string.IsNullOrWhiteSpace(pasta);
        }

        private static int QuantidadeDeArquivos(ConsoleColor corTextoOriginal)
        {
            int quantidadeDeArquivos;
            Console.Write("Quantos arquivos você pretende unificar? ");
            Console.ForegroundColor = ConsoleColor.Green;
            while (!int.TryParse(Console.ReadLine(), out quantidadeDeArquivos))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\tOpção inválida, tente novamente");
                Console.ForegroundColor = corTextoOriginal;
                Console.Write("\nQuantos arquivos você pretende unificar? ");
            }
            Console.ForegroundColor = corTextoOriginal;
            return quantidadeDeArquivos;
        }

        private static string[] ValidaArquivo(List<string> lstArquivos, List<string> lstArquivosInseridos, string pasta, int i)
        {
            ConsoleColor corTextoOriginal = Console.ForegroundColor;
            string[] arquivosParaUnificar = Directory.GetFiles(pasta);

            string arquivo = VerificaSeArquivoJaFoiInserido(lstArquivosInseridos, i);
            string arquivoCompleto = @$"{pasta}\{arquivo}.csv";

            while (!arquivosParaUnificar.Contains(arquivoCompleto))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\tArquivo {arquivo} não localizado, tente novamente");
                Console.ForegroundColor = corTextoOriginal;
                Console.Write($"\nDigite o nome do {i}º arquivo: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                arquivo = Console.ReadLine();
                arquivoCompleto = @$"{pasta}\{arquivo}.csv";
            }

            string[] lines = File.ReadAllLines($@"{arquivoCompleto}");

            foreach (string line in lines)
                lstArquivos.Add(line);
            lstArquivosInseridos.Add(arquivo);
            return lines;
        }

        private static string VerificaSeArquivoJaFoiInserido(List<string> lstArquivosInseridos, int i)
        {
            ConsoleColor corTextoOriginal = Console.ForegroundColor;
            Console.Write($"\nDigite o nome do {i}º arquivo: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            string arquivo = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(arquivo))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\tO nome do Arquivo não pode ser nulo ou vazio. Tente novamente!");
                Console.ForegroundColor = corTextoOriginal;

                Console.Write($"\nDigite o nome do {i}º arquivo: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                arquivo = Console.ReadLine();
                Console.ForegroundColor = corTextoOriginal;
            }

            while (lstArquivosInseridos.Contains(arquivo) && i > 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\tArquivo {arquivo} já foi inserido");
                
                Console.ForegroundColor = corTextoOriginal;
                Console.Write("\nVocê gostaria de inserir novamente? Sim ou Não? ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string escolha = Console.ReadLine();
                
                if (escolha.Trim().ToUpper().Substring(0, 1) == "S")
                {
                    Console.ForegroundColor = corTextoOriginal;
                    break;
                }
                else
                {
                    Console.ForegroundColor = corTextoOriginal;
                    Console.Write($"\nDigite o nome do {i}º arquivo: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    arquivo = Console.ReadLine();
                    Console.ForegroundColor = corTextoOriginal;
                }
            }
            return arquivo;
        }

        private static void LogLinhasAdicionadas(ConsoleColor corTextoOriginal, string[] lines, int cabecalho)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\tForam adicionados {lines.Count() - cabecalho} linhas ao Arquivo Unificado");
            Console.ForegroundColor = corTextoOriginal;
        }

        private static void EscreveArquivoUnificado(List<string> lstArquivos, string pasta, int cabecalho)
        {
            CorrigeCabecalhoDaLista(lstArquivos, cabecalho);

            using (StreamWriter sw = File.AppendText($@"{pasta}\ArquivoUnificado.csv"))
            {
                foreach (string line in lstArquivos)
                    sw.WriteLine(line);
            }
        }

        private static List<string> CorrigeCabecalhoDaLista(List<string> lstArquivos, int cabecalho)
        {
            for (int i = 0; i < cabecalho; i++)
            {
                string cabecalhoParaRemover = lstArquivos[i];

                foreach (var item in lstArquivos.Skip(0))
                    if (cabecalhoParaRemover == item) lstArquivos.Remove(cabecalhoParaRemover);

                lstArquivos.Insert(i, cabecalhoParaRemover);
            }
            return lstArquivos;
        }

        private static void FinalizaProcessamento(List<string> lstArquivos, ConsoleColor corTextoOriginal, int cabecalho)
        {
            Console.WriteLine($"Processamento finalizado com sucesso!");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"\tGerado Arquivo Unificado com ");
            Console.Write($"{CorrigeCabecalhoDaLista(lstArquivos, cabecalho).Count - cabecalho} linhas de conteúdo");
            Console.ForegroundColor = corTextoOriginal;
        }
    }
}
