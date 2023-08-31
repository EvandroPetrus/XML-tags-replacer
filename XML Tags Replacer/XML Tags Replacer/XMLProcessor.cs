using System.Diagnostics;

public static class XMLProcessor
{
    public static void ProcessFiles(string diretorioBase)
    {
        string inputDirectory = Path.Combine(diretorioBase, "input");
        string outputDirectory = Path.Combine(diretorioBase, "output");

        Util.GaranteExistenciaDiretorio(outputDirectory);

        List<string> arquivosSemElementoEscolhido = new();
        Stopwatch stopwatch = Stopwatch.StartNew();

        string[] xmlFiles = Directory.GetFiles(inputDirectory, "*.xml");

        foreach (string xmlFilePath in xmlFiles)
        {
            if (UpdateXmlFile(xmlFilePath, outputDirectory))
            {
                continue;
            }

            arquivosSemElementoEscolhido.Add(xmlFilePath);
        }

        stopwatch.Stop();
        LogProcessingResult(stopwatch.Elapsed.TotalSeconds, arquivosSemElementoEscolhido.Count);
    }

    public static bool UpdateXmlFile(string inputFilePath, string outputDirectory)
    {
        string elementToReplace = "<nfeProc versao=\"4.00\">";
        string elementToInsert = "<nfeProc xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"4.00\">";
        string xmlContent = File.ReadAllText(inputFilePath);

        if (xmlContent.Contains(elementToReplace))
        {
            xmlContent = xmlContent.Replace(elementToReplace, elementToInsert);
            string destinationFilePath = Path.Combine(outputDirectory, Path.GetFileName(inputFilePath));
            File.WriteAllText(destinationFilePath, xmlContent);
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
