# Roller Ball
## Player Controller (5 Points) 
* Create a textured ball and a platform to roll on. W/S move the ball forward and back, while A/D move it to the left and the right. When the ball falls off the platform, the Scene is reloaded. -- DONE
* Pressing Space makes the ball jump. This is only possible when it is touching the ground. While in the air the ball can jump one additional time by pressing Space again. -- DONE
* The camera is positioned behind the ball and follows it on the x- and z-axis the entire time. The y-position does not change. -- DONE
* Pressing G toggles God Mode on and off. While in God Mode, the Rigidbody is set to Kinematic and the player can fly through the level with WASD, as well as press E and Q to move up and down. -- DONE
* [Hard] The ball emits dust particles when moving. These are only emitted when the ball is on the ground and the player is holding down WASD. The particles are emitted from the bottom of the ball, so that it looks like dust is building up where the ball is touching the ground. (You can use the default particle material for this effect and you don't have to make it work inside of God Mode.) -- DONE
## Obstacle Course (6 Points) 
* For the following tasks you are allowed to create the prefabs together as a group. Each member can then use them to build their own obstacle course.
Don't forget to generate the lighting for your level when you're done. Otherwise it will stop working correctly once you reload the Scene.
* Build one level per group member that contains the elements described in the next few tasks. The level should be at least 40 seconds long and reasonably challenging to complete.
* The player can collect items throughout each level. Collecting an item plays a sound effect and creates a small particle explosion. -- DONE
* To complete a level, the player has to collect every item and then reach a clearly marked goal. The number of collected items is shown as an UI text that turns green when all items have been found.
* Create two additional UI texts. One shows the current playtime in seconds and the other the current record, which is saved in PlayerPrefs. Each level has its own record.
* Add materials with different textures to all of your GameObjects and decorate your level with additional lights and/or particle effects.
* [Hard] The level contains at least three switches. These are objects that the player has to collide with to change something within the level. A switch is clearly recognizable as a switch and sticks out of the ground when it hasn't been pressed yet. When the player hits the switch, a sound effect is played and the switch is pushed into the ground. -- DONE
## Menu (2 Points) 
* Create a main menu for your game. The level can be selected by clicking on a screenshot of your course. If the player presses R while in a level, or reaches the goal, they are brought back to the menu.
* [Hard] Before starting a level, the player can select between different skins for the ball. This changes the texture it uses in the game.

## Deadline July 16th