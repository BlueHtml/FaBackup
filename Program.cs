const string DIR = "dir", TARGET_PATH = "ROOT";
int _num = 1;
await foreach (string line in File.ReadLinesAsync(DIR))
{
    if (string.IsNullOrWhiteSpace(line))
    {
        continue;
    }

    string path = Path.Combine(TARGET_PATH, line[3..]);
    Directory.CreateDirectory(Path.GetDirectoryName(path));
    await System.IO.File.WriteAllTextAsync(path, line);
    Console.WriteLine($"line {_num}: ok");
    _num++;
}
