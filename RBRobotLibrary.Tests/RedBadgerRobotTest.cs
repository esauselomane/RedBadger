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
            new[] { "3 2 N", "FRRFLLFFRRFLL" },//when passing the same robot twice at the same position, it should not be lost the second time, as the scent is left behind
            new[] { "0 3 W", "LLFFFLFLFL" },
            new[] { "6 7 N", "F" }
        };

        var sut = new RedBadgerRobot(bounds, robots);

        var results = sut.MoveRobots();

        var expected = new List<string>
        {
            "1 1 E",
            "3 3 N LOST",
            "3 2 N", 
            "2 3 S",
            "6 7 N Starting position was out of bounds"
        };

        Assert.Equal(expected, results);
    }
}
