# Dungeon RPG (10 Points) 

## Building the Dungeon (3 Points) 
* Create a new Tile Palette with the dungeon tileset from the package. These sprites use pixel art, so you have to adjust the import settings accordingly. -- DONE
* Create a Grid and draw a map that consists of [2/4/6] interconnected rooms. The example game uses one single Scene that has four rooms in total. Add the proper colliders to make sure that the player can't leave the map. -- DONE
* Add some 2D Lights to all of your rooms to make them stand out. The player should be able to move between your rooms at clearly marked places (e.g. doors, stairs, holes). -- DONE

## Battle System (5 Points) 
* Add buttons for the player actions (Attack, Defend, Heal, Run) and a text to print the battle messages. -- DONE
* Implement the battle system. -- DONE
    * Design Details in next section
* Choose at least one enemy per group member from the split spritesheets in the Enemies folder. Create an Idle animation for your enemy that plays continuously when no other animation is playing. -- DONE
* Create a Hit animation that plays when your enemy is hit by the player and an Attack animation that plays when your enemy attacks. -- DONE
* [Hard] Create a Special animation for your enemy's charged attack. This should be an elaborate animation that also uses particle effects in some form.
* Add a few Slimes to your dungeon (the prefabs can be found in the package). When the player stands near a Slime and presses Space, a Battle Scene with a specific enemy is loaded. The enemies are not random - each Slime always loads a battle with the same enemy. If the player runs from a battle, they return to the same position they were at before. -- DONE


## Battle System Design
Player HP: 15\
Enemy HP: 20
### Player Actions
1. Attack (2-3)
2. Heal (4-5)
3. Parry/Riposte
    * Parrying a Charged attack negates all damage and stuns the opponent for a round
    * After exiting the stun, the enemy gets buffed for +1 Attack damage
    * Attacking a stunned enemy deals double damage
    * Riposting a stunned enemy decreases it's damage buff by 1, but deals no damage
4. Run

### Enemy Actions
1. Attack (3-5 DMG; +1 after getting parried)
2. Charge (Insta kill the player if they don't parry) - 25% chance to cast
