# Game "Rear end collision"
## Gameplay description
"Rear end collision" is a game where a group of vehicles run around in a labyrinth. 
Each vehicle is controlled by a human player or an AI (depending on the available time AI may or may not be included in the final product. 
The vehicles cannot stop moving and can only turn in 90 degree angles (for simplicity).
Reversing the direction is not possible. When a player's vehicle places itself behind another one in a straight line without obsticles between them, 
it catches it's Slipstream and increases its speed. This mechanic enables the chasing player to catch the other player from behind. When the chaising 
player reaches the front one, the front player is destroyed.If a player hits a wall, he is destroyed (e.g. if he enters a dead end). 
If two players hit each other heads-on then both of them are destroyed. See the file simple_situations.txt for examples.

## Basic architecture
From the programmer point of view the game will be split into the following modules (sorry, due to the problems with my computer I cannot currently provide you with 
more descriptive images):
* Game engine
This will be responsible for getting inputs from the players, updating their position on the field, detecting collisions, keeping score, etc.
If network play is to be implemented the engine myst have a way to synchronize its state to the game host via the network.
* Map generator
This will provide the engine with a map (ideally randomly generated, but can be also loaded from a file or whatever).
* Player controll 
Responsible for getting the player inputs and providing them to the engine. Can be from keyboard, from AI, or from the network. 
* Visualizer 
This will be responsible for visualizing the information provided by the engine to the screen. 
Can be graphical or console based interface.
May include sound effects maybe?
* Menus and game controll
Here will be contained all the logic for starting a game, adding players, selecting game mode (if implemented), creating a network game (if implemented).

## Game modes
This has not been discussed, but as a part of the gameplaye different game modes can be developed (this must be discussed maybe):
* Last man standing
The easiest to implement. The last player not killed wins.
* Deathmatch
Every player has a score that increases when he manages to kill another player. Respawn when killed.
* Capture the flag. 
Not sure if it makes sense to implement this. A flag in the middle of the field where two teams try to carry to their "base".

## Task division (for now)
* ShwangShwing
Engine
* flash
Visualizer
* Anton
Visualizer (doesn't have much time until the end of the year)
* vas
Map generator (the labyrinth must have mainly corridors with field wide to prevent the players from turning, very few dead ends and plenty of different ways to get to one place to another)
* gchankov
Networking
* Mitko
Input from the console for player control. (Input requires the game to know if currently the player is pressing the button or no. No button buffering.)

## Summary
Please see the information above and discuss. A more detailed architecture will be available soon. There are untouched parts of the idea (like the menus) that you can also explore if you want. Please if some important discussion is had int he slack channel, upload a summary somewhere so that all the people from the team (who may have not seen the chat) can see.
