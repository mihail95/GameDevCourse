# Dungeon RPG (10 Points) 

## Building the Dungeon (3 Points) 
* Create a new Tile Palette with the dungeon tileset from the package. These sprites use pixel art, so you have to adjust the import settings accordingly.
* Create a Grid and draw a map that consists of [2/4/6] interconnected rooms. The example game uses one single Scene that has four rooms in total. Add the proper colliders to make sure that the player can't leave the map.
* Add some 2D Lights to all of your rooms to make them stand out. The player should be able to move between your rooms at clearly marked places (e.g. doors, stairs, holes).

## Battle System (5 Points) 
* The battle system is a re-balanced implementation of the Intro homework with animated sprites. If you want, you can use your own mechanics instead, as long as all the animations are there.
* You can use your own animated sprites, but they have to consist of multiple individual images, so that you can animate them similar to the example game.
* Add buttons for the player actions (Attack, Defend, Heal, Run) and a text to print the battle messages. Then implement (or reuse) the battle system from the Intro exercise. As a reminder:
* Player and AI each start with a certain number of hit points (HP). The player's HP should be visible somewhere on the screen.
* Attack deals damage to the enemy. Defend negates all incoming damage. Heal increases your own HP.
The enemy can also Attack, or it can Charge. This prints a message that the enemy is charging energy. In the following turn a special attack is unleashed.
* Choose at least one enemy per group member from the split spritesheets in the Enemies folder. Create an Idle animation for your enemy that plays continuously when no other animation is playing.
* Create a Hit animation that plays when your enemy is hit by the player and an Attack animation that plays when your enemy attacks.
* [Hard] Create a Special animation for your enemy's charged attack. This should be an elaborate animation that also uses particle effects in some form.
* Add a few Slimes to your dungeon (the prefabs can be found in the package). When the player stands near a Slime and presses Space, a Battle Scene with a specific enemy is loaded. The enemies are not random - each Slime always loads a battle with the same enemy. If the player runs from a battle, they return to the same position they were at before.