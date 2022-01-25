namespace CFM;

using System.Diagnostics;
using System.Drawing;


class FileManager
{
    public Point firstPoint;
    public string Path { get; set; }
    List<FileSystemInfo> dirs;

    public FileManager()
    {
        firstPoint = new Point(0, 0);
        Path = @"C:\";
        dirs = new List<FileSystemInfo>();
    }

    public void PrintDirectoryList(int index=-2)
    {
        UpgradeDirectoryList();
        //Console.Clear();
        Console.SetCursorPosition(0, 0);
        Console.WriteLine(Path);
        Console.WriteLine();
        if (index == -1)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
        }
        Console.WriteLine("...");
        Console.ResetColor();

        for (int i = 0; i < dirs.Count; i++)
        {
            if (i == index-1)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            if (dirs[i] is FileInfo)
                Console.WriteLine($"{dirs[i].Name}: {(dirs[i] as FileInfo).Length / 1024} KB, {dirs[i].LastWriteTimeUtc}");
            else
                Console.WriteLine($"{dirs[i].Name}: {dirs[i].LastWriteTimeUtc}, {dirs[i].Attributes}");
            Console.ResetColor();
        }
    }

    public void UpgradeDirectoryList()
    {
        dirs.Clear();
        dirs.AddRange(new DirectoryInfo(Path).GetDirectories().Where(file =>
            (file.Attributes & FileAttributes.Hidden) == 0));
        dirs.AddRange(new DirectoryInfo(Path).GetFiles().Where(file =>
            (file.Attributes & FileAttributes.Hidden) == 0));
    }

    public void OpenDefault(string path)
    {
        using Process fileopener = new Process();

        fileopener.StartInfo.FileName = "explorer";
        fileopener.StartInfo.Arguments = "\"" + path + "\"";
        fileopener.Start();
    }

    public void RunExe(FileSystemInfo file)
    {
        Process.Start(file.FullName);
    }

    public void OpenBackFolder()
    {
        Path = Path.TrimEnd('\\');
        int position = Path.LastIndexOf(@"\");
        Path = Path.Substring(0, position+1);
    }

    public void Choose(int index)
    {
        if (index > 0)
        {
            if (dirs[index - 1].Extension == "")//dirs[index - 1].Attributes == FileAttributes.Directory)
            {
                Console.Clear();
                Path += $@"{dirs[index - 1].Name}\";
                UpgradeDirectoryList();
            }
            else if (dirs[index - 1].Extension == ".exe")
            {
                RunExe(dirs[index - 1]);
            }
            else
            {
                OpenDefault(Path + $@"{dirs[index - 1].Name}");
            }
        }
        else if (index == -1)
        {
            Console.Clear();
            OpenBackFolder();
        }
    }

    static void ClearBuffer()
    {
        while (Console.KeyAvailable)
        {
            Console.ReadKey(false);
        }
    }

    public void Start()
    {
        int i = -1;
        ConsoleKey key = ConsoleKey.Spacebar;
        while (key != ConsoleKey.Escape)
        {
            ClearBuffer();
            if (key == ConsoleKey.DownArrow)
                i++;
            if (key == ConsoleKey.UpArrow)
                i--;
            if (i < -1)
                i = dirs.Count;
            if (i > dirs.Count)
                i = -1;

            PrintDirectoryList(i);

            key = Console.ReadKey().Key;
            if (key == ConsoleKey.Enter)
                Choose(i);
        }
    }
}

