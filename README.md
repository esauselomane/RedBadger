# RedBadger

The main point of this application is to track if the 
- [ ] robot is going up or down(Will make changes to the y coordinate. -1 if down, +1 if down)
- [ ] Robot is going left or right: Will make changes to the x coordinate. -1 if left, +1 if right

Checking edges:
- [ ] If going left or right, robot will “fall off” if x, coordinate is < 0 or > than initial x coordinate
    - [ ] If robot falls off, save current location in a lookup(hash table)
- [ ] If going up or down, robot will fall off if  y coordinate is <0 or y > initial y coordinate
- [ ] Once robot falls off, ignore further instructions

ASSUMPTIONS:
The instruction ‘If a robot falls off the edge of the grid the word “LOST”
should be printed after the position and orientation.’ Is not very clear. I cannot conclude that I should be tracking the location where the robot was before it fell off(it sound wrong because the robot may not move in the same orientation or direction in the next move as the one is fell off) or the coordinates it landed on when it got off bounds.

I will make the assumption to use the latter.
