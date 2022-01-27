using CFM;
using System.Text;
Console.OutputEncoding = Encoding.UTF8;


FileManager fm = new FileManager();
fm.Start();
/*
int x = 20,
    y = 25,
    sx = 10,
    sy = 10;
Shield();
string str;
Restore();
WiM wm = new(str, new(sx, sy), new(x, y));
str = wm.Write();
Save();


void Shield()
{
    for (int i = sx - 1; i <= x; i++)
    {
        Console.SetCursorPosition(i, sy - 1);
        Console.Write('#');
        Console.SetCursorPosition(i, y);
        Console.Write('#');
    }
    for (int i = sy - 1; i <= y; i++)
    {
        Console.SetCursorPosition(sx - 1, i);
        Console.Write('#');
        Console.SetCursorPosition(x, i);
        Console.Write('#');
    }
}
void Save()
{
    using (StreamWriter sw = new StreamWriter(@"test.txt", false, System.Text.Encoding.Default))
    {
        sw.Write(str);
    }
    Console.SetCursorPosition(x / 2, y + 1);
    Console.WriteLine("Запись выполнена");
}
void Restore()
{
    try
    {
        using (StreamReader sr = new StreamReader(@"test.txt"))
        {
            str = sr.ReadToEnd();
        }
    }
    catch
    {
        str = "";
    }
}*/