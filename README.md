# RedBadger

The main point of this application is to track if the 
- [ ] robot is going up or down(Will make changes to the y coordinate. -1 if down, +1 if down)
- [ ] Robot is going left or right: Will make changes to the x coordinate. -1 if left, +1 if right

Checking edges:
- [ ] If going left or right, robot will “fall off” if x, coordinate is < 0 or > than initial x coordinate
    - [ ] If the robot fell, but the location already exist in hash, do not move robot(revert to previous location)
    - [ ] If robot falls off, save current location in a lookup(hash table)
- [ ] If going up or down, robot will fall off if  y coordinate is <0 or y > initial y coordinate
    - [ ] If robot falls off, save current location in a lookup(hash table)
- [ ] Once robot falls off, ignore further instructions

#### ASSUMPTIONS
The instruction ‘If a robot falls off the edge of the grid the word “LOST”
should be printed after the position and orientation.’ Is not very clear. I cannot conclude that I should be tracking the location where the robot was before it fell off(it sound wrong because the robot may not move in the same orientation or direction in the next move as the one is fell off) or the coordinates it landed on when it got off bounds.

I will make the assumption to use the latter.

#### Parameters

| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `upperBounds` | `string[]` | *Yes* | This holds the upper-right coordinates of the rectangle. |
| `robots` | `string[][]` | *Yes* | The first element in the array contains the starting coordinates(separated by spaces) for the robot and the second contains the instructions to move it. |


#### Exceptions
* `ArgumentNullException`: Thrown if either `upperBounds` or `robots`  are null or whitespaces.
* `ArgumentOutOfRangeException`: Thrown if `upperBounds[0]`  or `upperBounds[1]` is less than or equal to 0. The robot will not be able to move if the dimension of the rectangle is zero.
* If a robot's starting position is out of bounds(that is, its coordinates falls out of the range of the initial coordinates, I will output "Starting position was out of bounds" after the position and orientation. This is not in the initial requirement

  #### Running project
  
* from root of your application folder, run `dotnet test` on the terminal. You can add more test scenarios in RedBadgerRobotTest file
* to run the console app, go to the RBRobotConsole folder on the terminal and run `dontnet run`: 
