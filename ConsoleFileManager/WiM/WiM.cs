using System.Drawing;

namespace CFM
{
    internal class WiM
    {
        int cur = 0;
        string str;
        Point StartP, EndP;
        public WiM(Point startP, Point endP)
        {
            this.StartP = startP;
            this.EndP = endP;
            str = "";
        }
        void Clear()
        {
            Console.CursorVisible = false;
            for(int i = StartP.X; i <= EndP.X; i++)
            {
                for(int j = StartP.Y; j <= EndP.Y; j++)
                {
                    Console.Write(' ');
                }
            }
            Console.CursorVisible = true;
            Console.SetCursorPosition(StartP.X, StartP.Y);
        }
        void SetCurPos()
        {
            int x = 0, y = 0;
            for(int i = 0; i < str.Length; i++)
            {
                if (i == cur)
                    break;
                x++;
                if(str[i] == '\n')
                {
                    x = 0;
                    y++;
                }
            }
            Console.SetCursorPosition(x, y);
        }
        void Save()
        {
            string writePath = @"test.txt";
            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {
                    sw.Write(str);
                }
                Console.SetCursorPosition(10, 15);
                Console.WriteLine("Запись выполнена");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        void Set()
        {
            string path = @"test.txt";

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    str = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
            }
            cur = str.Length;
        }
        public void StartWrite()
        {
            string tmp = "";
            Set();
            ConsoleKeyInfo key;
            Clear();
            Console.Write(str);
            while (true)
            {
                Clear();
                Console.Write(str);
                SetCurPos();
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    cur++;
                    tmp = "\n";
                    str = str.Insert(cur - 1, tmp);
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (cur > 0)
                    {
                        str = str.Remove(cur - 1, 1);
                        cur--;
                    }

                }
                else if (key.Key == ConsoleKey.Delete)
                {
                    if (cur+1 > 0)
                    {
                        str = str.Remove(cur, 1);
                        //cur--;
                    }

                }
                else if (key.Key == ConsoleKey.Insert)
                {
                    Save();
                    return;
                }
                else if ((int)key.Key >= 37 && (int)key.Key <= 40)
                {
                    switch (key.Key)
                    {
                        case ConsoleKey.RightArrow:
                            if (cur < str.Length)
                                cur++;
                            break;
                        case ConsoleKey.LeftArrow:
                            if (cur > 0)
                                cur--;
                            break;
                    }
                }
                else
                {
                    cur++;
                    tmp = "";
                    tmp += key.KeyChar;
                    str = str.Insert(cur - 1, tmp);
                }
                Clear();
                  
                Console.Write(str);
            }
        }
    }
}
