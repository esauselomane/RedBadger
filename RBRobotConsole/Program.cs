using System;
using System.Collections.Generic;
using RBRobotLibrary;

string? firstLine;
do
{
    Console.WriteLine("Enter upper bounds (e.g., '5 3'):");
	firstLine = Console.ReadLine();
	if (firstLine == null) return; // no input
	firstLine = firstLine.Trim();
} while (firstLine.Length == 0);

var boundsParts = firstLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
if (boundsParts.Length < 2 || !int.TryParse(boundsParts[0], out int maxX) || !int.TryParse(boundsParts[1], out int maxY))
{
	Console.Error.WriteLine("Invalid upper bounds. Expected: '<maxX> <maxY>'");
	return;
}

var bounds = new[] { maxX, maxY };

// Read robot lines (pairs: position line, instruction line)
var robots = new List<string[]>();
while (true)
{
    Console.WriteLine("Enter robot position and direction (e.g., '1 1 E') (or empty line to finish):");
	var positionLine = Console.ReadLine();
	if (positionLine == null) break;
	positionLine = positionLine.Trim();
	if (positionLine.Length == 0) break;

    Console.WriteLine("Enter robot instructions (e.g., 'RFRFRFRF'):");
	var instructionsLine = Console.ReadLine();
	if (instructionsLine == null) break;
	instructionsLine = instructionsLine.Trim();

	robots.Add(new[] { positionLine, instructionsLine });
}

if (robots.Count == 0)
{
	Console.Error.WriteLine("No robots provided.");
	return;
}

var robotArray = robots.ToArray();

try
{
	var engine = new RedBadgerRobot(bounds, robotArray);
	var results = engine.MoveRobots();
	foreach (var r in results)
	{
		Console.WriteLine(r);
	}
}
catch (Exception ex)
{
	Console.Error.WriteLine($"Error: {ex.Message}");
}
