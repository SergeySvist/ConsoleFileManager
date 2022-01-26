using CFM;
//void Shield()
//{
//    for (int i = 0; i <= x; i++)
//    {
//        Console.SetCursorPosition(i, 0);
//        Console.Write('#');
//        Console.SetCursorPosition(i, y);
//        Console.Write('#');
//    }
//    for (int i = 0; i <= y; i++)
//    {
//        Console.SetCursorPosition(0, i);
//        Console.Write('#');
//        Console.SetCursorPosition(x, i);
//        Console.Write('#');
//    }
//}
//void Save()
//{
//    using (StreamWriter sw = new StreamWriter(@"C:\Users\Kirill\Desktop\ConsoleFileManager\ConsoleFileManager\bin\Debug\net6.0\test.txt", false, System.Text.Encoding.Default))
//    {
//        sw.Write(str);
//    }
//    Console.SetCursorPosition(x/2, y+1);
//    Console.WriteLine("Запись выполнена");
//}
//void Restore()
//{
//    try
//    {
//        using (StreamReader sr = new StreamReader(@"C:\Users\Kirill\Desktop\ConsoleFileManager\ConsoleFileManager\bin\Debug\net6.0\test.txt"))
//        {
//            str = sr.ReadToEnd();
//        }
//    }
//    catch {
//        str = "";
//    }
//}
using System.Text;
Console.OutputEncoding = Encoding.UTF8;

FileManager fm = new FileManager();
fm.Start();
/*fm.PrintDirectoryList(13);

Console.ReadKey(true);
Console.WriteLine("\n\n\n\n");
fm.Choose(13);
fm.PrintDirectoryList();*/
