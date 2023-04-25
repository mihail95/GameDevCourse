# Animal Disco

## Design the Disco
1. Choose background, make it brighter or darker -- DONE
2. Create disco light, spawn lights with F, change colors on delay -- DONE

## Player and NPCs
1. Choose player, implement movement -- DONE
2. Create Dance Move, use number keys to activate -- DONE
    * left, right, left, right -- DONE
    * Do a barrel roll! -- DONE
    * scale up, up, up, scale down (to starting scale) -- DONE
3. Player can not do anything while dancing -- DONE
4. NPCs that dance -- DONE
5. Cool dance move - DONE
    * at least 3 seconds long -- DONE
    * background becomes completely black -- DONE
    * all disco lights stop changing color -- DONE

## Cheat codes
1. Implement some sort of buffer -- DONE
2. NINJA - transparent player; -50% movement speed -- DONE
3. DOGE - changes all NPCs to Doge -- DONE
4. SQUIDGAME
    * Find all lamps (by Tag), change green/red on a timer
    * If NPC dances while red - destroy
    * if player moves/dances while red - reload scene


5. Additional cheat - complexity between 3 and 4 -- DONE

## Extras
1. Music -- DONE
2. Volume Slider -- DONE
3. Sync Dances to Music -- DONE (reused Conductor.cs from old project)
    * 126 BPM
        * 1 beat = 476ms = 2.08Hz
        * 2 beats = 952ms = 1.05Hz
        * 3 beats = 1.43sec = 0.7Hz
        * 4 beats = 1.9sec = 0.53Hz