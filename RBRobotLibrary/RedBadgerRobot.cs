using System.Diagnostics;

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
        if(upperBounds[0] < 0 || upperBounds[0] > 50 || upperBounds[1] < 0 || upperBounds[1] > 50)
        {
            throw new ArgumentException("Upper bounds must be between 0 and 50.");
        }

        //for brevity, I will not implement checking the robots array for further validation and will assume it is valid.

        _upperBoundX = upperBounds[0];
        _upperBoundY = upperBounds[1];
        _lowerBoundX = 0;
        _lowerBoundY = 0;

        //format of robots array is expected to be a 2D array where each row represents a robot and the first element is "x y orientation", second element is instructions.
        foreach (var robot in robots)
        {
            var coordinatesAndOrientation = robot[0].Split(' ');
        
            _robotPositions.Add(new Robot((int.Parse(coordinatesAndOrientation[0]), int.Parse(coordinatesAndOrientation[1])), 
            coordinatesAndOrientation[2], robot[1]));
        }
    }
    public List<string> MoveRobots()
    {
        var results = new List<string>();
        var isLost = false;

        for (int i = 0; i < _robotPositions.Count; i++)
        {
            var robot = _robotPositions[i];

            // Check if the robot's starting position is out of bounds and mark its position as dangerous if so
            if (AddDangerousPosition(robot.Coordinates.x, robot.Coordinates.y))
            {
                results.Add($"{robot.Coordinates.x} {robot.Coordinates.y} {robot.InitialDirection} Starting position was out of bounds");
                continue;
            }

            for (int j = 0; j < robot.Instructions.Length; j++)
            {
                // If the robot is lost, we stop processing its instructions
                if (isLost)
                {
                    break;
                }

                switch (robot.Instructions[j])
                {
                    case 'R':
                        robot.InitialDirection = SwitchOrientation(robot, 90);
                        break;
                    case 'L':
                        robot.InitialDirection = SwitchOrientation(robot, -90);
                        break;
                    case 'F':
                        var newCoordinates = Move(robot);
                        if(IsPositionDangerous(newCoordinates.x, newCoordinates.y))
                        {
                            //there is a scent of a lost robot, so we will ignore this instruction and continue to the next one.
                            continue;
                        }

                        if(AddDangerousPosition(newCoordinates.x, newCoordinates.y))
                        {
                            results.Add($"{robot.Coordinates.x} {robot.Coordinates.y} {robot.InitialDirection} LOST");
                            isLost = true;
                        }
                        else
                        {
                            robot.Coordinates = newCoordinates;
                        }
                    break;
                    //add more cases for other instructions as needed
                    default:
                        throw new InvalidOperationException($"Invalid instruction '{robot.Instructions[j]}' for robot at ({robot.Coordinates.x}, {robot.Coordinates.y}).");
                }
                
            }

            if (isLost)
            {
                isLost = false;
                continue;
            }

            // If the robot is not lost, add its final position and orientation to the results
            results.Add($"{robot.Coordinates.x} {robot.Coordinates.y} {robot.InitialDirection}");
        }
        return results;
    }

    public (int x, int y) Move(Robot robot)
    {
        var newCoordinates = robot.Coordinates;

        switch (robot.InitialDirection)
        {
            case "N":
                newCoordinates= (robot.Coordinates.x, robot.Coordinates.y + 1);
                break;
            case "E":
                newCoordinates = (robot.Coordinates.x + 1, robot.Coordinates.y);
                break;
            case "S":
                newCoordinates = (robot.Coordinates.x, robot.Coordinates.y - 1);
                break;
            case "W":
                newCoordinates = (robot.Coordinates.x - 1, robot.Coordinates.y);
                break;
            default:
                throw new ArgumentException("Invalid orientation value. Must be N, E, S, or W.");
        }

        return newCoordinates;
    }

    public string SwitchOrientation(Robot robot, int newOrientation = 0)
    {
        var currentOrientation = 0;

        switch (robot.InitialDirection)
        {
            case "N":
                currentOrientation = 360;
                break;
            case "E":
                currentOrientation = 90;
                break;
            case "S":
                currentOrientation = 180;
                break;
            case "W":
                currentOrientation = 270;
                break;
            default:
                throw new ArgumentException("Invalid orientation value. Must be between 0 and 3.");
        }

        //Ensure the value is alway positive
        var newDirection = Math.Abs(currentOrientation + newOrientation);

        return GetRobotOrientation(currentOrientation + newOrientation);
    }

    public string GetRobotOrientation(int orientation)
    {
        //edge case. when orientation is greater than 360, we will reset it to 90 (East) as the next orientation.
        if (orientation > 360)
        {
            return "E";
        }

        switch (orientation)
        {
            case 360: case 0:
                return "N";
            case 90:      
                return "E";
            case 180:
                return "S";
            case 270:
                return "W";
            default:
                throw new ArgumentException("Invalid orientation value. Must be between 0 and 3.");
        }   
    }

    public bool AddDangerousPosition(int x, int y)
    {
        if (x < _lowerBoundX || x > _upperBoundX || y < _lowerBoundY || y > _upperBoundY)
        {
            return _dangerousPositions.Add((x, y));
        }
        return false;
    }

    public bool IsPositionDangerous(int x, int y)
    {
        return _dangerousPositions.Contains((x, y));
    }

}
