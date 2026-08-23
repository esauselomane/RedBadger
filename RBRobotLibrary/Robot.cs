public struct Robot
{
    public (int x, int y) Coordinates { get; set;}
    public string Instructions { get; }
    public string InitialDirection { get; set; } 

    public Robot((int x, int y) coordinates, string initialDirection, string instructions)
    {
        Coordinates = coordinates;
        InitialDirection = initialDirection;
        Instructions = instructions;
    }
}