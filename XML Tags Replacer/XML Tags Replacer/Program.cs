try
{
    string diretorioBase = @"C:\Users\infis001091\Desktop\Chamado 16851";
    string diretorioEntrada = Path.Combine(diretorioBase, "input");
    string diretorioSaida = Path.Combine(diretorioBase, "output");
    string elementoASerTrocado = "<nfeProc versao=\"4.00\">";
    string elementoASerInserido = "<nfeProc xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"4.00\">";
    List<string> elementosNaoEncontrados = new();

    if (!Directory.Exists(diretorioSaida))
    {
        Directory.CreateDirectory(diretorioSaida);
    }

    if (!Directory.Exists(diretorioEntrada))
    {
        throw new DirectoryNotFoundException("O caminho informado para entrada de arquivos não existe.");
    }

    string[] xmlFiles = Directory.GetFiles(diretorioEntrada, "*.xml");

    Console.WriteLine("Começando processamento de arquivos.");

    foreach (string xmlFilePath in xmlFiles)
    {

        string xmlContent = File.ReadAllText(xmlFilePath);

        if (xmlContent.Contains(elementoASerTrocado))
        {
            xmlContent = xmlContent.Replace(elementoASerTrocado, elementoASerInserido);

            string destinationFilePath = Path.Combine(diretorioSaida, Path.GetFileName(xmlFilePath));

            File.WriteAllText(destinationFilePath, xmlContent);

        }
        else
        {
            elementosNaoEncontrados.Add(xmlFilePath);
        }
    }
    Console.WriteLine($"Operação terminada. Total de registros sem elementos encontrados: {elementosNaoEncontrados.Count}.");
}
catch (Exception ex)
{
    Console.WriteLine("Erro inesperado: " + ex.Message);
}
