namespace CFM;


struct Point
{
    int X { get; set; }
    int Y { get; set; }

    public Point(int x,int y)
    {
        X = x;
        Y = y;
    }
}

struct Size
{
    int H { get; set; }
    int W { get; set; }

    public Size(int height, int width)
    {
        H = height;
        W = width;
    }
}