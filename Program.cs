const string DIR = "dir", TARGET_PATH = "ROOT";
int _num = 1;
await foreach (string line in File.ReadLinesAsync(DIR))
{
    if (string.IsNullOrWhiteSpace(line))
    {
        continue;
    }

    string path = Path.Combine(TARGET_PATH, line[3..].Replace('\\', Path.DirectorySeparatorChar));
    string dir = Path.GetDirectoryName(path);
    Directory.CreateDirectory(dir);
    try
    {
        await File.WriteAllTextAsync(path, line);
    }
    catch (PathTooLongException ex)
    {
        Console.WriteLine(ex.Message);
        Console.WriteLine($"line: {line}");
        Console.WriteLine($"path: {path}");
        Console.WriteLine($"dir: {dir}");

        Console.WriteLine($"文件名太长，进行截取！");
        string fileName = Path.GetFileName(path);
        string new_fileName = fileName[..fileName.IndexOf(' ')];
        string new_path = Path.Combine(dir, new_fileName);
        Console.WriteLine($"fileName: {fileName}");
        Console.WriteLine($"new_fileName: {new_fileName}");
        Console.WriteLine($"new_path: {new_path}");

        await File.WriteAllTextAsync(new_path, line);
    }

    Console.WriteLine($"line {_num}: ok");
    _num++;
}
