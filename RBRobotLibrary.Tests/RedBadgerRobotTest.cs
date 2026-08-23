namespace RBRobotLibrary.Tests;

public class RedBadgerRobotTest
{
    [Fact]
    public void SampleScenarioProducesExpectedResults()
    {
        int[] bounds = new[] { 5, 3 };

        // Each robot is represented as ["x y orientation", "instructions"]
        string[][] robots = new[]
        {
            new[] { "1 1 E", "RFRFRFRF" },
            new[] { "3 2 N", "FRRFLLFFRRFLL" },
            new[] { "0 3 W", "LLFFFLFLFL" },
            new[] { "2 2 N", "F" },
            new[] { "4 1 S", "LL" }
        };

        var sut = new RedBadgerRobot(bounds, robots);

        var results = sut.MoveRobots();

        var expected = new List<string>
        {
            "1 1 E",
            "3 3 N LOST",
            "2 3 S",
            "2 3 N",
            "4 1 N"
        };

        Assert.Equal(expected, results);
    }
}
