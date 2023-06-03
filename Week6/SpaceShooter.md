# Space Shooter

## Player Controller (4 Points) 
1. [x] Choose a spaceship and implement the basic movement using the physics system. The only requirement is that the player can not leave the screen. 
2. [x] The player can shoot lasers with Space or the Left Mouse Button. Lasers are shot continously while either button is held down. Each shot plays a sound effect. 
3. [x] The player can shoot three lasers at once with 1 or the Right Mouse Button. This attack has a short cooldown window, which is shown as an Image of type Filled in the UI.
4. [x] [Hard] The player has one special attack per group member that can be shot with 2, 3 or 4. This attack also has a cooldown that is shown on the screen. The effect should be more complex than the previous two, e.g. a bomb that explodes into smaller bombs, multiple bullets that move in a sine wave, or a projectile that bounces between enemies. Try to be creative! (Adding another laser to the previous attack or just changing its sprite doesn't count).


## Enemies (6 Points) 
1. [x] The player is attacked by enemies that move around continuously and shoot lasers every few seconds. Create at least four different enemies with a different amount of hit points (HP).
2. [x] When the player is hit, an explosion sound effect is played, the music fades out over a second, and the player is returned to the menu from 3a).
3. [x] Pressing G toggles God Mode on and off. In God Mode the player cannot be hit be enemy attacks. Activating God Mode plays a sound effect and shows a text or some other indicator on the screen. 
4. [x] When an enemy is hit by the player, it takes damage and a sound effect is played. An enemy is destroyed once their HP reach zero. The damage is shown as a floating text above their sprite. 
5. [x] Enemies spawn in waves. A wave consists of one or multiple enemies that have to be destroyed before the next wave can spawn.
6. [x] [Hard] Build one boss per group member out of multiple sprites from the Boss Parts folder. This enemy has more HP and uses attacks that are much harder to avoid than the regular lasers. Every 5th wave contains a boss.

## Menu and Effects (3 Points) 
1. [x] Create a main menu that is shown at the start of the game. The menu contains a text that shows the highest wave the player has reached so far. This highscore is saved persistently in PlayerPrefs. 
2. [x] Create a star field using particle systems and add it to your game and your menu. (Hint: the example game uses two systems that are moved from right to left. The second system has reduced particle size and speed.) 
3. [x] Add an explosion particle effect whenever an enemy or the player is destroyed. Make sure that the enemies can't be hit anymore while the explosion effect is playing. 
