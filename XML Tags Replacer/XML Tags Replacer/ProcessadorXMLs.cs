using System.Diagnostics;

public static class ProcessadorXMLs
{
    public static void ProcessaArquivos(string diretorioBase)
    {
        string inputDirectory = Path.Combine(diretorioBase, "input");
        string outputDirectory = Path.Combine(diretorioBase, "output");

        Util.GaranteExistenciaDiretorio(outputDirectory);

        List<string> arquivosSemElementoEscolhido = new();
        Stopwatch sw = Stopwatch.StartNew();

        string[] arquivosXML = Directory.GetFiles(inputDirectory, "*.xml");

        foreach (string arquivoXML in arquivosXML)
        {
            if (AtualizaArquivoXML(arquivoXML, outputDirectory))
            {
                continue;
            }

            arquivosSemElementoEscolhido.Add(arquivoXML);
        }

        sw.Stop();
        LogProcessingResult(sw.Elapsed.TotalSeconds, arquivosSemElementoEscolhido.Count);
    }

    public static bool AtualizaArquivoXML(string inputFilePath, string outputDirectory)
    {
        string elementoASerTrocado = "<nfeProc versao=\"4.00\">";
        string elementoASerInserido = "<nfeProc xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"4.00\">";
        string conteudoXML = File.ReadAllText(inputFilePath);

        if (conteudoXML.Contains(elementoASerTrocado))
        {
            conteudoXML = conteudoXML.Replace(elementoASerTrocado, elementoASerInserido);
            string destinationFilePath = Path.Combine(outputDirectory, Path.GetFileName(inputFilePath));
            File.WriteAllText(destinationFilePath, conteudoXML);
            return true;
        }

        return false;
    }

    public static void LogProcessingResult(double elapsedTime, int filesWithoutElementCount)
    {
        Util.LogaMensagem($"Operação feita em {elapsedTime} segundos.");
        Util.LogaMensagem($"Arquivos totais sem o elemento buscado: {filesWithoutElementCount}");
    }
}
