# <center>Plane Glider


## Player Controller (4 Points)
* Choose one of the planes for the player and add a rigidbody and a fitting collider. When doing nothing, the plane should automatically fly to the right and slightly downwards. -- DONE
* Holding Space boosts the player upwards and repeatedly plays a sound effect (the example game uses a pitched-up version of SFXJump) . Make sure that the sprite can't leave the screen. -- DONE
* The player has limited fuel and can only boost if there's still some left. The remaining fuel is shown by an UI Image of type Filled with a text that goes up to 100. At 0 remaining fuel the text says 'Empty' instead. -- DONE
* The plane is animated by flipping through the individual sprites. Furthermore, the plane is always rotated in its moving direction (see code from exercise 3). If you use your own sprite, it also has to have some kind of animation. -- DONE

## Obstacle Course (5 Points)
* Build one level per group member that contains the elements described in the next few tasks and a goal at the end. The camera follows the player on the x-axis throughout each level. -- DONE
* The player can collect items that restore some of the plane's fuel. Collecting an item plays a fitting sound effect. -- DONE
* Create a ground and at least two basic obstacles. When the plane collides with either of them, a sound effect is played and the controls are deactivated. The player can then restart the level by pressing Space. -- DONE
* [Hard] Create two different moving obstacles per group member and add them to your own level. If you want, you can also use the other group members' obstacles in your own course. -- DONE
* [Hard] When the plane crashes, instead of just falling down, it will now bounce on the ground indefinitely. While bouncing around, collisions with items and every obstacle other than the ground are deactivated. -- DONE

## Menu (1 Point)
* Create a main menu that is shown at the start of the game. The level can be selected by clicking on a screenshot of your course. If the player presses R while in a level, or reaches the goal, they are brought back to the menu.
(In case you don't implement a menu, please provide some other way of reaching all of your levels, so that we can grade your submission.) -- DONE


## WHY DOES IT NOT WORK LIKE IN THE EDITOR WTF?!
## Write Feedback for A1 and A2
* Deadline May 21st