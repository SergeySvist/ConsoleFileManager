using System.Drawing;
namespace CFM
{
    internal class WiM
    {
        int x, y;
        int _currentPos;
        string _text;
        private int ind;

        Point StartPos { get; set; }
        Point EndPos { get; set; }

        public WiM(string text, Point startPos, Point endPos)
        {
            this.StartPos = startPos;
            this.EndPos = endPos;
            _text = text;
            _currentPos = 0;
        }
        private void PressEnter()
        {
            _text = _text.Insert(_currentPos, "\n");
            _currentPos++;
            Console.CursorVisible = false;
            SetCurPos();
            Console.CursorVisible = true;
            if (y >= EndPos.Y + ind)
                ind++;
        }
        private void PressBackspace()
        {
            if (_currentPos > 0)
            {
                _text = _text.Remove(_currentPos - 1, 1);
                _currentPos--;
                Console.CursorVisible = false;
                SetCurPos();
                Console.CursorVisible = true;
                if (y < StartPos.Y + ind && ind > 0)
                    ind--;
            }
        }
        private void PressDelete()
        {
            if (_currentPos + 1 > 0 && _text.Length - 1 > 0)
                _text = _text.Remove(_currentPos, 1);
        }
        private void PressArrow(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.RightArrow:
                    if (_currentPos < _text.Length)
                        _currentPos++;
                    break;
                case ConsoleKey.LeftArrow:
                    if (_currentPos > 0)
                        _currentPos--;
                    break;
                case ConsoleKey.UpArrow:
                    if (y > StartPos.Y)
                    {
                        y--;
                        if (y < StartPos.Y + ind && ind > 0)
                            ind--;
                        FindCurPos();
                    }
                    break;
                case ConsoleKey.DownArrow:
                    y++;
                    if (y >= EndPos.Y + StartPos.Y + ind)
                        ind++;
                    FindCurPos();
                    break;
            }
        }
        private void FindCurPos()
        {
            int tX = StartPos.X, tY = StartPos.Y;
            for (int i = 0; i < _text.Length; i++)
            {
                _currentPos = i;
                if (tY  == y && tX == x)
                    break;
                else if (tY == y &&
                    (tX >= EndPos.X || _text[i] == '\n' || _text[i] == '\0'))
                {
                    break;
                }
                if ( tX >= EndPos.X || _text[i] == '\n' || _text[i] == '\0')
                {
                    tX = StartPos.X;
                    tY++;
                }
                else
                tX++;
            }
            x = tX;
            y = tY;
        }
        private void SetCurPos()
        {
            x = StartPos.X;
            y = StartPos.Y;
            for (int i = 0; i < _text.Length; i++, x++)
            {
                if (i == _currentPos)
                    break;
                if (x + 1>= EndPos.X || _text[i] == '\n' || _text[i] == '\0')
                {
                    x = StartPos.X;
                    y++;
                }
            }
            Console.SetCursorPosition(x - 1, y - ind);
        }
        public void Read()
        {
            int x = 0,
                y = 0;
            Console.CursorVisible = false;
            for (int i = 0; i < _text.Length; i++)
            {
                if (_text[i] == '\r')
                {
                    Console.Write('#');
                    i++;
                }
                if (y >= ind)
                {
                    Console.SetCursorPosition(StartPos.X + x, StartPos.Y + y - ind);
                    Console.Write(_text[i]);
                    x++;
                    if (_text[i] == '\n' || _text[i] == '\0')
                    {
                        for (int j = x + StartPos.X; j < EndPos.X - 1; j++, x++)
                        {
                            Console.SetCursorPosition(j - 1, StartPos.Y +y - ind);
                            Console.Write(' ');
                        }
                    }
                }
                else
                {
                    while(y < ind)
                    {
                        i++;
                        if (_text[i] == '\n' || _text[i] == '\0')
                        {
                            y++;
                        }

                    }
                }
                if (StartPos.X + x >= EndPos.X - 1)
                {
                    y++;
                    x = 0;
                }
                if (y >= this.y && y > EndPos.Y - StartPos.Y + ind - 1)
                    break;
            }
            FeelSpace(x, y);
            Console.CursorVisible = true;
        }
        private void FeelSpace(int x, int y)
        {
            if (y >= ind)
            {
                for (int i = StartPos.Y + y - ind + 1; i < EndPos.Y - StartPos.Y; i++)
                {
                    for (int j = StartPos.X; j < EndPos.X - StartPos.X; j++)
                    {
                        Console.SetCursorPosition(j, i);
                        Console.Write(' ');
                    }
                }
            }
        }
        public static void ClearBuffer()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(false);
            }
        }
        private void ProcButt(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.Enter)
                PressEnter();
            else if (key.Key == ConsoleKey.Backspace)
                PressBackspace();
            else if ((int)key.Key >= 37 && (int)key.Key <= 40)
                PressArrow(key.Key);
            else if (key.Key == ConsoleKey.Delete)
                PressDelete();
            else
            {
                _currentPos++;
                _text = _text.Insert(_currentPos - 1, $"{key.KeyChar}");
            }
        }
        public string Write()
        {
            ConsoleKeyInfo key;
            while (true)
            {
                ClearBuffer();
                Read();
                SetCurPos();
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Insert)
                    return _text;
                ProcButt(key);
            }
        }
    }
}
