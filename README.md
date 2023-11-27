#############################

Project title: Space Invader
Author: NGUYEN Viet Hoang

#############################

GAME DESCRIPTION 

Endless Space Invader: The player will try to shoot as many Invader as possible to get the point. The game end when the player touches the Invader or the Invader escape.

#############################

INSTALLATION

+ This game is better running on PC with the aspect ratio of 9:16. 
+ How to run the game:
	. In Unity: Go to "Game Scene" scene -> press Play
	. Or use the build that include with the source code.

#############################

ARCHITECT

+ MVC pattern: 
	. All game mecanism was implemented inside the CONTROLER (Managers's folder):		
BulletControler for Bullet Physic Logic and movement.
InvaderControler for Invader Behavior such as Change skin, Death, ability,…
MoveControler for the moving along the path.
PlayerControler for the Player movement and physics.
ScoreControler for Score System.
LocalManager is the biggest controller, that will control every controller and View and put them together. If we need to scale this game to multiplayer mode, we just need to convert this manager to the Server.		
	. All UI Tab was controlled by VIEW (View's folder): the view stores all UI element, UI Update function (like Score update). The View was only being called by LocalManager.
	. All other data object (like Input, Path, Stage, Invader) is MODEL (Models's folder): These models can only contain data.
	. The Manager should be implemented with singleton pattern, but we only have 1 scene, so we don't need to implement that pattern.
+ The object pooling: All Bullet and Invader in the game were implemented with Object pooling design: We don't need to Instantiate the object in need or destroy it when it is outside the screen, so the Fps will be stable while running the game. 

#############################

SCALABILITY

+ Thanks to the MVC pattern, almost every component is independent, so if I need to scale the game, I just need to scale each component of this project.
+ For example: If I want to have a new bullet, I can create a new bullet script that heritage the BulletControler, then I can implement new mechanism in this new script, without worrying about the other component. What I need to care is in the new script, I must override the Initialize (for the new bullet setting), AutoMove (new way to move), TriggerDeathProcess (new way to explode), SetDeath (new way to turn back to the pool).

#############################

CREDIT

All Assets were downloaded from the internet

#############################

