# Turn-Based Console Battle

## Initial Battle System (DiceGameInitial.cs)
* First impression of the system - if the computer was able to play perfectly, it would only attack. The player has a worse damage output and the heal is only able to stall in the best case (and loses HP in the rest 2/3). So in theory I should lose.
* First Playtests - Player feels pretty strong
    * The ability to negate all damage is very powerful, because it also blocks the following attack
    * This leads to the game being a bit RNG-heavy, because as the player you want the computer to use Charge as much as possible (to get in free attacks)
* If the computer high-rolls you are dead, because of the difference in damage output.
* Almost every game ends up in a state where the player has around 1-3HP.
* Healing is a bit underpowered, since it can only heal you for the minimum amount of damage, that the enemy deals


## Improvement Ideas
* Player Action - Parry - Blocks and Returns incoming damage from Normal Attacks (or a chance to take 50% of the damage)
* Improve Defend - Make it only block the next attack, so you don't block charge + normal attack
* Improve Heal - Set it to the median value of the enemy attack

<b>THIS MIGHT MAKE THE PLAYER A BIT OP...</b>

<img src="ComputerRNG.png" alt="isolated" width="400"/>


