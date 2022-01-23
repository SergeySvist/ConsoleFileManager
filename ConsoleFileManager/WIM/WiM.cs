namespace CFM;
using System.Drawing;
internal class WiM
{
    private string _string;
    public Point StartPoint { get; init; }
    public Point EndPoint { get; init; }
    public WiM(Point startPoint, Point endPoint)
    {
        this.StartPoint = startPoint;
        this.EndPoint = endPoint;
        //this._strings = new List<string>();
    }

    public void StartWrite()
    {
        ConsoleKeyInfo key;
        string tmp;
        int i = 0;
        //this._strings.Add("");
        _string = " ";

        while (true)
        {
            key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.Enter:
                    _string += '\n';
                    break;
                case ConsoleKey.Backspace:
                    tmp = new(_string, 0, _string.Length - 1);


                    _string =  ;
                    break;
                default:
                    _string += key.KeyChar;
                    break;
            }
            Console.SetCursorPosition(0, 0);
            Console.Write(_string);
        }
    }
}
