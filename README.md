# 🧬 Viral Nebula

**Viral Nebula** is a third-person survival sci-fi game created in Unity. You play as a biomedical engineer sent aboard a space research vessel to contain a virus outbreak, repair the central control system, and avoid deadly biomechanical threats.

---

## 🎮 Gameplay Overview

You must:

- Collect **5 code digits** scattered throughout the ship.
- **Avoid the infected zones** and hostile biomechanical robots.
- **Reach the control room** and repair the central computer before time runs out.

### ✅ Win Conditions

- Collect all 5 code digits.
- Repair the central computer.

### ❌ Loss Conditions

- Die from virus exposure or enemy attacks.
- Fail to complete the mission before the time limit.

---

## 🛠️ Technologies Used

- **Engine**: Unity (C#)
- **Navigation**: NavMeshAgent for AI pathfinding
- **UI**: Unity UI Toolkit
- **Audio/Animation**: Animator Controllers and AudioSources

---

## 🧩 Features

- Inventory and item collection (health kits, mask recharges, codes)
- Countdown timer for infected areas
- Dynamic AI: enemy patrol, chase and attack
- Interactive environment: automatic doors, animated NPCs
- Win/Game Over menus with sound and animation
- In-game dialogue system

---

## 📦 3D Assets

3D models were sourced from free public Unity Asset Store packs.  
*(Assets are not included in this repository due to licensing restrictions.)*

---

## 📜 Script Summary

| Script                  | Description                                                                 |
|-------------------------|-----------------------------------------------------------------------------|
| `Inventaire.cs`         | Manages the player's items (kits, recharges, codes).                        |
| `AnimationPP.cs`        | Controls the player's animation and sounds (walking, damage, death).        |
| `CollecterItem.cs`      | Handles item pickups and audio feedback.                                    |
| `Deplacement.cs`        | Implements player movement with CharacterController and gravity.            |
| `Ennemi.cs`             | AI for the initial enemy who kills the NPC and activates patrols.           |
| `EnnemiPatrouille.cs`   | Patrol, chase, and attack logic for biomechanical enemies.                  |
| `GestionJoueur.cs`      | Tracks health, handles damage/death, and checks safe zone state.            |
| `GestionPortes.cs`      | Opens and closes doors based on player proximity.                           |
| `Menus.cs`              | Controls the title, win, and game over menus and their sound transitions.   |
| `OrdinateurControle.cs` | Triggers win state when player reaches the central computer.                |
| `PNJ.cs`                | Controls NPC behavior: dialogue, animations, movement, death.               |
| `PorteSalleControle.cs` | Verifies code digits before unlocking the control room.                     |
| `ZoneInfectee.cs`       | Manages infection zones, damage over time, and game over if timer runs out. |

---

## 🗺️ Level Flow

1. **Start**: Get the protective mask from the NPC.
2. **Explore**: Search rooms for code digits and resources.
3. **Survive**: Avoid robots and manage time in infected areas.
4. **Complete**: Reach the control room and repair the central computer.

---

## ⏱️ Game Duration

- Estimated time: **3 minutes**
- Time pressure increases with virus spread and zone exposure

---

## 📁 Project Files

You can download the full Unity project [here](https://drive.google.com/file/d/1hAX7oSkxe9RjjcI05kqNDeZa5NdJbpPy/view?usp=sharing).  
*(Includes all C# scripts, scenes, prefabs, and UI assets. 3D and audio assets have been taken from royalty-free asset banks for non-commercial use.)*

---

## 👩‍💻 Developer

Project by **Inès Bouakkaoui**  
Course: **360-2PR-BB – Programming II**  
Collège de Bois-de-Boulogne  
**May 2024**

---
