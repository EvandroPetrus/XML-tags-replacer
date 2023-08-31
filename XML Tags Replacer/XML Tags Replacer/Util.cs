public static class Util
{
    public static void LogaMensagem(string msg)
    {
        Console.WriteLine($"{DateTime.Now:HH:mm:ss} > {msg}");
    }
    public static void GaranteExistenciaDiretorio(string outputDirectory)
    {
        if (!Directory.Exists(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
        }
    }
}