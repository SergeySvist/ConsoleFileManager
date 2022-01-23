namespace CFM;
using System.Drawing;

class Button
{
    public Point firstPoint;
    private ConsoleColor _color;
    private char _sym;

    public string Name { get; set; }

    public event Action Operation;

    public Button(Point firstPoint,string name)
    {
        this.firstPoint = new Point(firstPoint.X, firstPoint.Y);
        Name = name;

        _sym = '█';
        _color = ConsoleColor.Black;
    }

    public void Print()
    {
        Console.ForegroundColor = _color;
        Console.BackgroundColor = _color;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                Console.SetCursorPosition(firstPoint.X + j, firstPoint.Y + i);
                Console.WriteLine(_sym);
            }
        }
        Console.SetCursorPosition(firstPoint.X + 1, firstPoint.Y + 1);

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(Name);

        Console.SetCursorPosition(0, (firstPoint.Y + 10) + 10);
        Console.ResetColor();
    }

    public void Click()
    {
        Operation?.Invoke();
    }
}
