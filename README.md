# Exploding Zombies

**Exploding Zombies** is a fast-paced **3D first-person shooter (FPS) mobile game** where players battle waves of zombies across multiple worlds. Each world presents **progressively harder challenges**, unique zombie types with **special abilities**, and a **boss fight at the end of each level**. Players can collect and use **special powers** dropped by zombies, unlock and upgrade a wide arsenal of weapons, and survive increasingly difficult waves.

## Features

- **Wave-Based Gameplay** - Survive increasing waves of zombies with unique abilities.
- **Diverse Zombies** - Each zombie has a **special power** and a **different amount of health**.
- **Boss Battles** - Each level ends with a **powerful boss** fight.
- **Multiple Worlds** - Progress through different worlds, each with unique challenges.
- **Weapon System** - Earn points to **purchase and upgrade** a variety of weapons.
- **Special Powers** - Collect power-ups dropped by zombies to use against enemies.
- **Increasing Difficulty** - The game gets harder as more levels and worlds are completed.

## Game Mechanics

### Enemy System
- **Zombie Waves** - The game spawns waves of zombies with varying difficulty.
- **Boss Fights** - After surviving waves, a **boss fight is triggered** before progressing to the next level.
- **Enemy Special Abilities** - Zombies have unique powers that affect gameplay.
- **Spawn System** - Enemy difficulty increases dynamically based on progress.

### Weapons System
- **Weapon Unlocking** - Players earn points to unlock and upgrade weapons.
- **Upgrade System** - Weapons can be enhanced with damage boosts, faster reloads, and larger magazines.
- **Ammo Management** - Reload mechanics and weapon switching for tactical combat.

### Player Progression
- **Points System** - Earn points by killing zombies and surviving waves.
- **Special Powers** - Collect and activate special abilities dropped by zombies.
- **Level Advancement** - Progress through levels and unlock new worlds.

## Core Components

### Enemy & Wave Management
- `EnemyManager.cs` - Handles zombie waves, enemy spawning, and difficulty scaling.
- `BossFightsManager.cs` - Manages boss fights at the end of levels.
- `ChanceToMakeHarderEnemySO.cs` - Controls the probability of spawning tougher zombies.

### Player & Combat Mechanics
- `WeaponsManager.cs` - Manages all weapons, shooting mechanics, and reload systems.
- `PointsManager.cs` - Tracks earned points and applies scoring multipliers.
- `ActivateSpecialPower.cs` - Enables special powers collected from zombies.
- `ShopManager.cs` - Allows players to buy and upgrade weapons.

### Game Flow & Progression
- `GameManager.cs` - Controls overall game state, level transitions, and world progression.
- `AmountOfEnemiesAddedToWaveSO.cs` - Determines the number of zombies in each wave.
- `Level6EnemySpecialPower.cs` - Handles special abilities for zombies in advanced levels.

## Future Improvements
- **New zombie types** with more complex behaviors.
- **Additional weapon upgrades** and new firearms.
- **More special abilities** for both the player and zombies.
- **Multiplayer mode** for cooperative gameplay.

## About the Developer
This project showcases **strong Unity development skills**, including **OOP design, event-driven systems, AI management, and FPS mechanics**. It demonstrates expertise in **game system architecture, player progression, and mobile game optimization**.

**Looking to hire a Unity Developer?** Feel free to reach out!

---

**Built for Mobile | Optimized for Performance | Designed for Action**

