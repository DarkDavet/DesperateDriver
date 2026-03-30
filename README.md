# Desperate Driver

<img width="534" height="300" alt="image" src="https://github.com/user-attachments/assets/7acac7f9-525c-42fe-aa6a-14d55e840664" />

It is a 3D racing and management game where players deliver various types of ice cream to clients. Manage your vehicle, avoid obstacles, and ensure product freshness to maximize rewards. Earn money to upgrade your car, customize its appearance and unlock new levels.

## Technical description
* Unity Version: 6000.0.36f1
* Render Pipeline: URP (Universal Render Pipeline)
  
## Key features
* Architecture:
<br>- implemented state pattern for decoupled systems, including car handling states, ice cream quality stages, and player reputation logic.
<br>- scriptableObject-based event system that allows creating and assigning game events directly within the project window for high flexibility.
<br>- transaction system that handling all in-game purchases, including upgrades, visual customization, and level pack unlocking.
<br>- custom level system integraded in a single gameplay scene
* Optimization:
<br>- using object pooling for frequently reused VFX, interactive game objects, and dynamic UI icons
<br>- utilized low-poly models and optimized mesh data
<br>- organized UI elements into separate canvas groups
* UI:
<br>- implementation independent UI systems: in-game shop, levels' panel with ability  to unlock access to parts of this panel, a level inventory widget for each level, an inventory for the game in general, player reputation widget, ice cream panel with expire indicators(amount of cells for ice creams depends from purchased upgades), fuel indicator   

## Project structure
* Assets/m_DesperateDriver/Gameplay/LevelSystem - custom level management logic
* Assets/m_DesperateDriver/Gameplay/Player/Car - car entities, including a custom suspension pack with realistic physics
* Assets/m_DesperateDriver/Gameplay/Scripts - core mechanics: items, obstacles, entry points, and configurations
* Assets/m_DesperateDriver/Services - different essential services like event system, transaction service, storage service, status state system, pool manager etc.
* Assets/m_DesperateDriver/UI - UI widgets and layouts

## How to launch?
1. Clone the repository
2. Open the project in Unity 6 (6000.0.36f1)
3. Open and run the scene Assets/Scenes/MainMenu.unity

