# Project Tetrad

## Description

Project Tetrad is a party based dungeon crawler roguelike.  Every dungeon requires four heroes to explore and conqure.  These heroes are controlled either by multiple players or AI bots.

### Dungeon

* The dungeons are a 9x9 grid that expands from the center.
* Each dungeon contains locks and obstacles that require specific heroes to overcome them, making the entire party relient on the other members.
    * Enemies
        1. Goblin- can only be defeated after its attack is parried by the Knight's shield.
        2. Ghost- can only be damaged by the mage's magic based attacks.
        3. Ninja- warps away from heroes to avoid melee damage.  Must be shot from a distance by the Ranger.
        4. Tortoise- has a hard shell that protects itself from all damage.  Must be lifted and thrown by the barbarian to flip it upside down and reveal its soft underside.
    * Locks
        1. Overgrowth Door- vines that cover doors and prevent them from opening.  Must be cut by the Knight's sword.
        2. Cursed Door- a mysterious door that warps heroes to the other end of the room when they enter it.  Must be purified by the mage.
        3. Watching Door- a lock with an eye in it that watches the heroes and can only be removed using physical damage.  The eye closes if the heroes get too close, so the Ranger has to shoot it from a distance.
        4. Blocked Door- a door blocked by a massive rock.  The barbarian must move the rock.
* The goal of each dungeon (at least for prototyping purposes) is the same for every run:
    1. Find and defeat the Mini-Boss to collect the Boss Key.
    2. Use the Boss Key to open the Boss Room.
    3. Defeat the Boss to complete the dungeon.
* Players can find items and cosmetics in the dungeons that can be used in future runs.

### AI Overview

The AI bots explore the dungeon with the players, but they don't simply follow the players around.  Rather, they use a simple state machine to logically explore the dungeon on their own.

**Regroup**: The standard state of the AIs.  When there's nothing for the AI to accomplish, it returns to the host player's location.
**Explore**: If the AI is in the same room as the host player and there is an adjacent room that hasn't been explored yet, the AI will target that room and make its way there to explore it.  If there's nothing for it to do in this new room, it returns to the Regroup State.
**Solve**: If there is an obstacle linked to the AI's hero, the AI will target that room to solve the puzzle.  It continues this procedure until all of its locks are open, then returns to the Regroup State.
**Hunt**: If the Mini-Boss has been Discovered, all AI characters prioritize defeating it over all other actions.  They make their way to the Mini-Boss and defeat it.  Afterward, they also target the Boss Key dropped by the Mini-Boss.
**Rally**: If the Boss Key is collected and the location of the Boss Room is discovered, the AIs perform one of two actions in preparation of the boss fight.
* If the AI is the hero that collected the key, it makes its way to the closest Boss Door to the host player.
* If the AI is not the hero that collected the key, it will track and follow the hero that has the key.
* If an AI has the Boss Key, it will not open the Boss Door until the host player is present in the same room as it.
**Final**: If the Boss Door is open, the AI will target the Boss.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Requirements

**Unity Hub**
**Unity Editor Version:** '6000.4.0b2' (or minimum recommended patch version)
**Target Platform:** 'Windows' (Requires the Windows Build Support module installed via Unity Hub)

### Installation

1. **Clone the repository:** 
'''bash
git clone https://github.com/3Dever96/ProjectTetrad.git
'''

2. **Open in Unity**
* Open ** Unity Hub**.
* Click **"Add Project from Disk"**.
* Navigate to the cloned directory and select the root folder.
* Ensure the correct Unity Editor Version ('6000.4.0b2') is selected in the Hub and open the project.

3. **Run the Project:**
* Once the Editor loads, navigate to the primary scene file (usually in 'Assets/Scenes/').
* Press the **Play** button in the Unity Editor to begin.


## Contributing

Contributions are what make the open-source community such an amazing place to learn, inspire, and create.  Any contributions you make are **greatly appreciated**.

*   Please adhere to the **https://leotgo.github.io/unity-coding-standards/** before submitting code.

1. Fork the Project
2. Create your Feature Branch ('git checkout -b feature/AmazingFeature')
3. Commit your Changes ('git commit -m 'Add some AmazaingFeature'')
4. Push to the Branch ('git push origin feature/AmazingFeature')
5. Open a Pull Request

## License

This project is licensed under the MIT License- see the LICENSE file for details.

## Acknowledgments
