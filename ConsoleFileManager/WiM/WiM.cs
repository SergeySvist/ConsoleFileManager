using System.Drawing;
namespace CFM
{
    internal class WiM
    {
        int x, y;
        int _currentPos;
        string _text;
        Point StartPos { get; set; }
        Point EndPos { get; set; }
        public WiM(string text, Point startPos, Point endPos)
        {
            this.StartPos = startPos;
            this.EndPos = endPos;
            _text = text;
            _currentPos = _text.Length;
        }
        private void PressEnter()
        {
            _currentPos++;
            _text = _text.Insert(_currentPos - 1, "\n");
        }
        private void PressBackspace()
        {
            if (_currentPos > 0)
            {
                _text = _text.Remove(_currentPos - 1, 1);
                _currentPos--;
            }
        }
        private void PressDelete()
        {
            if (_currentPos + 1 > 0)
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
                    if (y > 1)
                    {
                        y--;
                        FindCurPos();
                    }
                    break;
                case ConsoleKey.DownArrow:
                    y++;
                    FindCurPos();
                    break;
            }
        }
        private void FindCurPos()
        {
            int tX = 0, tY = 0;
            for (int i = 0; i < _text.Length; i++)
            {
                _currentPos = i;
                if (tY == y - 1 && tX == x)
                    break;
                else if(tY == y - 1 &&
                    (StartPos.X + tX >= EndPos.X || _text[i] == '\n' || _text[i] == '\0'))
                {
                    break;
                }
                if (StartPos.X + tX >= EndPos.X || _text[i] == '\n' || _text[i] == '\0')
                {
                    tX = 0;
                    tY++;
                }
                tX++;
            }
            x = tX;
            y = tY;
        }
        private void SetCurPos()
        {
            x = StartPos.X;
            y = StartPos.Y;
            for (int i = 0; i < _text.Length; i++)
            {
                if (i == _currentPos)
                    break;
                if (StartPos.X + x >= EndPos.X || _text[i] == '\n' || _text[i] == '\0')
                {
                    x = 0;
                    y++;
                }
                x++;
            }
            Console.SetCursorPosition(x, y);
        }
        public void Read()
        {
            int x = 0,
                y = 0;
            Console.CursorVisible = false;
            for (int i = 0; i < _text.Length; i++)
            {
                Console.SetCursorPosition(StartPos.X + x, StartPos.Y + y);
                Console.Write(_text[i]);
                x++;
                if (_text[i] == '\n' || _text[i] == '\0')
                {
                    for (int j = x + StartPos.X; x < EndPos.X - 1; j++, x++)
                    {
                        Console.SetCursorPosition(j - 1, StartPos.Y + y);
                        Console.Write(' ');
                    }
                }
                if (StartPos.X + x >= EndPos.X - 1)
                {
                    y++;
                    x = 0;
                }
            }
            FeelSpace(x, y);
            Console.CursorVisible = true;
        }
        private void FeelSpace(int x, int y)
        {
            for (int j = x + StartPos.X; j < EndPos.X; j++)
            {
                Console.SetCursorPosition(j, StartPos.Y + y);
                Console.Write(' ');
            }
            for (int i = StartPos.Y + y; i < StartPos.Y; i++)
            {
                for (int j = StartPos.X; j < EndPos.X; j++)
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write(' ');
                }
            }
        }
        static void ClearBuffer()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(false);
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
                if (key.Key == ConsoleKey.Enter)
                    PressEnter();
                else if (key.Key == ConsoleKey.Backspace)
                    PressBackspace();
                else if ((int)key.Key >= 37 && (int)key.Key <= 40)
                    PressArrow(key.Key);
                else if (key.Key == ConsoleKey.Delete)
                    PressDelete();
                else if (key.Key == ConsoleKey.Insert)
                    return _text;
                else
                {
                    _currentPos++;
                    _text = _text.Insert(_currentPos - 1, $"{key.KeyChar}");
                }
            }
        }
    }
}
