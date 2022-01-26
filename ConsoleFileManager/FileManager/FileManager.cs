namespace CFM;

using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO.Compression;

class FileManager
{
    public Point firstPoint;
    public string Path { get; set; }
    List<FileSystemInfo> dirs;

    public FileManager()
    {
        firstPoint = new Point(0, 0);
        Path = @"";
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
                Console.WriteLine($"{dirs[i].Name}: {(dirs[i] as FileInfo)?.Length / 1024} KB, {dirs[i].LastWriteTimeUtc}");
            else
                Console.WriteLine($"{dirs[i].Name}: {dirs[i].LastWriteTimeUtc}, {dirs[i].Attributes}");
            Console.ResetColor();
        }
    }

    public void PrintDrivers(int index = -1)
    {
        Console.SetCursorPosition(0, 0);

        DriveInfo[] allDrives = DriveInfo.GetDrives();
        for(int i = 0; i < allDrives.Length; i++)
        {
            if (i == index - 1)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            Console.WriteLine($"{allDrives[i].Name}, {allDrives[i].DriveType}, {((allDrives[i].TotalFreeSpace / 1024) / 1024) / 1024}/{((allDrives[i].TotalSize/1024)/1024)/1024}");
            Console.ResetColor();
        }
    }

    public void DriversControl()
    {
        DriveInfo[] allDrives = DriveInfo.GetDrives();
        int i = 0;
        ConsoleKey key = ConsoleKey.Spacebar;
        while (key != ConsoleKey.Escape)
        {
            ClearBuffer();
            if (key == ConsoleKey.DownArrow)
                i++;
            if (key == ConsoleKey.UpArrow)
                i--;
            if (i < 0)
                i = allDrives.Length;
            if (i > allDrives.Length)
                i = 0;

            PrintDrivers(i);

            key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.Enter)
            {
                Path += allDrives[i-1].Name;
                break;
            }
            
        }
        Console.Clear();
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

    public string OpenBackFolder()
    {
        Path = Path.TrimEnd('\\');
        int position = Path.LastIndexOf(@"\");
        Path = Path.Substring(0, position+1);
        return Path;
    }

    public void Choose(int index)
    {
        if (index > 0)
        {
            if (dirs[index - 1].Extension == "" || dirs[index - 1].Attributes == FileAttributes.Directory)
            {
                Console.Clear();
                Path += $@"{dirs[index - 1].Name}\";
                UpgradeDirectoryList();
            }
            else if (dirs[index - 1].Extension == ".exe")
            {
                RunExe(dirs[index - 1]);
            }
            else if (dirs[index - 1].Extension == ".zip")
            {
                Console.Clear();
                OpenZip(index);
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

    public void CreateFile()
    {
        string? name="";
        Console.SetCursorPosition(50, 0);
        Console.WriteLine("Введите имя и расширение (example.txt): ");
        Console.SetCursorPosition(50, 1);
        name = Console.ReadLine();

        if(name?.IndexOf(".") == -1)
            Directory.CreateDirectory(Path + name);
        else
            File.Create(Path + name);
        Console.Clear();
    }

    public void DeleteFile(int index)
    {
        Console.Clear();
        if (dirs[index - 1].Extension == "" || dirs[index - 1].Attributes == FileAttributes.Directory)
            Directory.Delete(dirs[index - 1].FullName, true);
        else
            File.Delete(dirs[index - 1].FullName);
    }

    public void Zip()
    {
        string? name = "";
        Console.SetCursorPosition(50, 0);
        Console.WriteLine("Введите имя архива (example): ");
        Console.SetCursorPosition(50, 1);
        name = Console.ReadLine();

        string tmp = Path;
        ZipFile.CreateFromDirectory(Path, OpenBackFolder() + $"{name}.zip");
        File.Move(Path +$"{name}.zip", tmp + $"{name}.zip");
        Path = tmp;
    }

    public void UnZip(int index)
    {
        ZipFile.ExtractToDirectory(dirs[index - 1].FullName, Path);
    }

    public void OpenZip(int index)
    {
        Console.WriteLine(Path);
        Console.WriteLine("\n");

        ZipArchive z = ZipFile.Open(dirs[index - 1].FullName, ZipArchiveMode.Read);
        ReadOnlyCollection<ZipArchiveEntry> r = z.Entries;
        foreach (ZipArchiveEntry entry in r)
        {
            Console.WriteLine(entry);
        }
        Console.ReadKey(true);
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

            if (Path == "")
                DriversControl();
            PrintDirectoryList(i);

            key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.Enter)
            {
                Choose(i);
                i = 0;
            }
            if (key == ConsoleKey.N)
                CreateFile();
            if (key == ConsoleKey.Z)
                Zip();
            if (key == ConsoleKey.U)
                UnZip(i);
            if (key == ConsoleKey.Delete)
                DeleteFile(i);
        }
    }

    
}

