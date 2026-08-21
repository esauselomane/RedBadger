namespace RBRobotLibrary;

public class RedBadgerRobot
{
    private readonly HashSet<(int X, int Y)> _dangerousPositions = new();
    private readonly int _upperBoundX;
    private readonly int _upperBoundY;
    private readonly int _lowerBoundX;
    private readonly int _lowerBoundY;
    private readonly List<Robot> _robotPositions = new();

    public RedBadgerRobot(int[] upperBounds, string[][] robots)
    {
        if(upperBounds.Length != 2)
        {
            throw new ArgumentException("Upper bounds must be an array of two integers.");
        }
        if(robots == null || robots.Length == 0)
        {
            throw new ArgumentException("Robots array cannot be null or empty.");
        }

        //for brevity, I will not implement checking the robots array for further validation and will assume it is valid.

        _upperBoundX = upperBounds[0];
        _upperBoundY = upperBounds[1];
        _lowerBoundX = 0;
        _lowerBoundY = 0;

        foreach (var robot in robots)
        {
            //the first element of the robot array is a string of coordinates and the orientation, we need to split it and parse it into a tuple of integers and a string for the orientation
            var coordinatesAndOrientation = robot[0].Split(' ');

            //the second element of the robot array is a string of instructions, we will just store it as is for now   
            _robotPositions.Add(new Robot((int.Parse(coordinatesAndOrientation[0]), int.Parse(coordinatesAndOrientation[1])), coordinatesAndOrientation[2], robot[1]));
        }
    }
    public void Move()
    {
        foreach (var robot in _robotPositions)
        {
            // Logic to move the robot based on its instructions
            
            foreach (char instruction in robot.Instructions)
            {
                int newX = robot.Coordinates.x;
                int newY = robot.Coordinates.y;

                switch (instruction)
                {
                    case 'R':
                        newY++;
                        break;
                    case 'L':
                        newY--;
                        break;
                    case 'F':
                        newX++;
                        break;
                    //add more cases for other instructions as needed
                    default:
                        throw new InvalidOperationException($"Invalid instruction '{instruction}' for robot at ({robot.Coordinates.x}, {robot.Coordinates.y}).");
                }

                AddDangerousPosition(newX, newY);

                if (IsPositionDangerous(newX, newY))
                {
                    // If the current position is dangerous, we should not move the robot but instead add the new position to the dangerous positions list and continue to the next instruction.
                    //TODO: add robot state to indicate it has been lost and save its return details
                    continue;
                }
            }
        }
    }

    public void SwitchOrientation(Robot robot, int newOrientation)
    {
        //TODO: Will pass -90 for left and +90 for right, and will add to the current orientation to get the new orientation. Will need to handle wrapping around from 0 to 360 degrees.
        //TODO: add check to ensure newOrientation is a valid orientation (0, 90, 180, 270)
        //PS: Use Math.Abs to ensure the new orientation is always positive and within the range of 0-360 degrees.
        //if new orientation is greater than 360, subtract 360 from it until it is less than or equal to 360. If new orientation is less than 0, add 360 to it until it is greater than or equal to 0.
        //
        robot.InitialDirection = robot.InitialDirection + newOrientation.ToString();
    }

    public void AddDangerousPosition(int x, int y)
    {
        if (x < _lowerBoundX || x > _upperBoundX || y < _lowerBoundY || y > _upperBoundY)
        {
            _dangerousPositions.Add((x, y));
        }
    }

    public bool IsPositionDangerous(int x, int y)
    {
        return _dangerousPositions.Contains((x, y));
    }

}
