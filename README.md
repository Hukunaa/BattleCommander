![](https://github.com/Hukunaa/BattleCommander/blob/main/battlecommandergif.gif)
# Battle Commander

This project is a one week technical test for the Vodoo company to apply as a casual game developer.
A technical test PDF was sent to me by the company and acts as a "specification" for the base of an Army Clash like fight simulator for mobile.



## What is Battle Commander?

Battle Commander is a small and short game that gives the player the ability to play with little armies of people and make them fight with a simulation.
They can also influence the course of the fight by doing some simple tap interactions on the army units to heal or damage them. 
## Installation

### Windows

#### Prerequisites

- The game was tested in the editor with Unity version 2019.4.40 LTS. I recommend that you use the same version to prevent any unwanted issues when opening the project.

#### Setup

- Setup should be as simple as hitting the Play button in the editor. If the Demo scene is not opening itself and you get an empty scene, simply go to **Assets -> Scenes** and open **DemoBattleCommander.unity**.

### Android

#### Prerequisites

- The game was tested on a Xiaomi 11T (Android). Just make sure that you have all the Android SDK, NDK and JDK installed properly to be able to build the app correctly.
- Simply go to **Build Settings** -> **Build** / **Build and Run**
## Important

### Universal Render Pipeline Error

There is a known error in the console when you launch the project on Unity. This error is from the Universal Render Pipeline and doesn't not affect the project's compilation and/or the build process for mobile platform.
Clearing the console will remove this error.

Error:
```NullReferenceException: Object reference not set to an instance of an object
UnityEditor.Rendering.Universal.MaterialPostprocessor.OnPostprocessAllAssets (System.String[] importedAssets, System.String[] deletedAssets, System.String[] movedAssets, System.String[] movedFromAssetPaths) (at Library/PackageCache/com.unity.render-pipelines.universal@7.7.1/Editor/AssetPostProcessors/MaterialPostprocessor.cs:140)
```
## How to Play

### In the Unity Editor:

- The mouse / your finger is your controller. You can simply click on the *Play* button, then, if you want to , you can randomize your army with the *Randomize* button, and then press *Battle* to see your army fight the enemy!
- Once the battle start, you will see a little button in the bottom left corner of you screen. This button gives you the possibility to change the ability you want to use on your units. There are two abilities for now:
- the **Damage Units** ability gives you the power of hitting enemy units with your finger and weaken them.
- the **Heal Units** ability gives you the power of healing your units with a simple tap of a finger, making them last longer in the battle.
## Code Architecture

### Main scripts architecture

### **GameManager.cs**

The GameManager.cs script contains the gamemanager of the game with a simple Singleton pattern, making it accessible from anywhere so that everyone can listen to the game state changes.
Other scripts can listen to any game state update by listening to the **OnGameStateChanged** event in the GameManager.
There are 4 game states possible:

- Menu
- BattlePreparation
- Battle
- PostBattle

**Main Functions**

- SetGameState(GameStateType _state)

### **BattleManager.cs**

The BattleManager.cs script manages the Battle system in the game. Some objects need to listen to what the battle manager does, this is why I made it a singleton as there is only one in the game, and events are needed by other objects. It will load units, randomize armies, start and end battles.
The battle manager uses two GridManagers (details about this script below). One grid is used for the player, and another one for the enemy's army.

Once the player enters the BattlePreparation stage, the battle manager will place units on their respective slots, retrieved from their grid.
After that, the player will be able to randomize his units.

The units are managed in an object pool limited by the **maxPoolSize** variable to optimize the runtime performance of the game and to make the randomization process faster. (GameObject.Instantiate method costs a lot of performances, so we want to avoid using it as much as possible as we target the mobile platform).

The battle manager listens to the game manager to know when to start the battle. Once the battles ends, the battle manager will tell the gamemanager to update its state to the PostBattle state.

**Main Functions**

- GenerateArmies()
- RandomizeUnits()
- StartBattle()
- EndBattle()

### **GridManager.cs**

The GridManager.cs script manages the player and enemy grids. In the unity inspector and by script, you can define the grid dimensions in **X** and **Y**. A **Scale** value is also available to scale the grid and so the position slots of the armies.
The grids are enabled when in battle preparation state, and disabled when outside of this game state.
As units are stored in a one dimension array, we get the unit's position in the grid with its index in the army list.

**Main Functions**

- GenerateGrid()
- ShowHideGrid(GameManager.GameStateType _state)
- GetPosition(int _index)

### **ResourcesManager.cs**

This script is needed to facilitate the addition of new units in the game. If you want to add a new unit in the game and it requires a new mesh to be imported, you will need to place this mesh in **Assets -> Resources -> Models** folder.
(More informations on how to add a new unit below).

### **Unit.cs**

The Unit.cs script is the main script attached to the Unit prefab. It manages the UnitController, UnitDisplay, UnitSettings and UnitAnimator scripts attached to the same GameObject *(adding the Unit script to a GameObject will automatically add those needed scripts too)*.
This script is the main script that other scripts will interact whith when they need to do something with a Unit.
Once a Unit is created, it initiliazes its settings *(UnitSettings)* with random values. Those random values will determine the statistics of this Unit *(Attack Speed, Attack etc...)*.
The Unit.cs script has the main features it needs to interact with the world and other Units, like **Fight()**, **Die()**, **Heal()** etc...

The unit will listen to the BattleManager to know when to start and stop fighting:

- BattleManager.OnBattleStart
- BattleManager.OnBattleEnd

**Main Functions**

- GenerateUnit()
- Fight()
- Stop()
- ChooseEnemy()
- ApplyDamage()
- ReceiveDamage()
- Heal()
- Die()

### **UnitController.cs**

The UnitController.cs script manages the NavMeshAgent of the Unit. It tells the Unit where to go and who to look for when selecting a target.
This script contains the logic needed to sleect a target depending on the targetMode in the UnitSettings.
Once a unit kills an enemy or at the start of the battle, the unit will ask the controller to find a suitable target to go and attack to.

**Main Functions**

- SelectTarget()
- MoveToTarget()

### **UnitSettings.cs**

The UnitSettings.cs script contains all the statistics that the Unit can have when created. *(Settings imported from the technical test PDF)*
To add a new Unit configuration in the game, simply add a new switch statement entry in the corresponding category with the values desired. *(If needed, add the new enum types in their respective categories)*.

Example:
```
switch (shape)
    {
        case ...

        case ShapeType.CYLINDER:
            _health += 150;
            _attack += 15;
            _targetMode = 1;
            break;

        case ...
    }
```

If you want to alter the way units settings are generated, you can do so by modifying the Unit.cs script and change the values in the **GenerateUnit** function at:
```
_settings = new UnitSettings(ShapeType shape, SizeType size, ColorType color);
```

As the time frame for the test was limited, the different statistics are stored in enums, and applied in switch statements, but an ideal solution to facilitate the addition of Units is to load them from a JSON file with a simple serializer / deserializer.
With that solution, we would be able to load everything from the JSON, and the mesh path needed for that new unit too.

The UnitSettings script contains the following statistics:

- ShapeType (CUBE, SPHERE)
- SizeType (SMALL, BIG)
- ColorType (BLUE, GREEN, RED)
- Health
- Attack
- AttackSpeed
- Speed
- AttackRange
- TargetMode (Closest enemy / Lowest health enemy)

### **UnitDisplay.cs**

The UnitDisplay.cs script manages the display of the Unit in game. It gives the user all the visual feedback needed to the player.
The **UpdateUnitDisplay** function will update the Unit's mesh, color, and size according to the unit settings.
The script also executes a simple scaling animation when a unit receives a hit.

If you want to add a specific mesh to any new unit configuration, here is how you can easily do that:
Let's say I want to add a **Cylinder** configuration to the game.

- Drag and drop your **"Cylinder.fbx / Cylinder.obj"** model in the **Assets -> Resources -> Models** folder.
- In the UnitSettings.cs script, add the new shape in the **ShapeType** enum ***(here "CYLINDER")***. After that, add that ShapeType entry in the switch statement that gives the unit its statistics depending on its shape.
- In the UnitDisplay.cs script, in the **switch(settings.Shape)** statement, add your newly created ShapeType entry called **"CYLINDER"** and like for the CUBE and the SPHERE case, write:
```
_meshFilter.mesh = ResourcesManager.Instance.Meshes[ResourcesManager.Instance.Meshes.Keys.Single(s => s.Contains("Cylinder"))];;
```

You are all set with your new Shape!

## Additional feature

### The Player Cards

#### What is a Player Card?

During the battle, the player can use one of the two player cards implemented for this technical test, the **Damage Unit** card and the **Heal Unit** card.
You can switch between the two by simply clicking on the bottom left card button during a battle.

#### Why?

Simulated battles in short mobile games often give players a sense of "automated gameplay" where the player mostly watches the game play itself.
While this is a good thing to have in casual games, we don't want too much of that feeling and to loose any sense of gameplay.
The Player Card feature is a good solution for that.
With a simple tap, players have the ability to interact with the armies during the battle, and influence its outcome by penalizing the enemy or giving yourself advantages.
With a bit more development time, The Player Card feature can expand to more creative bonuses like little bombs, weapon drops for both armies and much more!
While this version of the game contains only two cards, I can easily see a close future where a set of Player Cards would be placed in the bottom of the player's screen, and usable at any time during the battle by the player.

#### Code Architecture

The code architecture of this feature is made to be implemented easily with a simple Inheritance Pattern.
The PlayerInteractions.cs script is a singleton that contains all the Interactions possible for the player to use.
In code, we can set the selected interaction (Player Card) with a simple call of the function **SetInteraction**.

For example, the Damage Unit card is a **Interaction_PlayerDamage** type object that inherits from the **Interaction** class.
The **Interaction** class contains one virtual method called **Interact** that is called when the user taps the screen during battle.

The **Interaction_PlayerDamage** script overrides this method to give the player the ability to damage enemy units when he taps on the screen.
To add a new card to the set, it is as easy as creating a new script **Interaction_NAME** that inherits from **Interaction** and overriding the **Interact** function from this class.
Once that is done, simply add this object to `List<Interaction> InteractionsCards;` and select that card with `PlayerInteractions.Instance.SetInteraction(int _index)`.

#### Interaction.cs
```
public class Interaction : MonoBehaviour
{
    public virtual void Interact() { }
}
```
Example:

```
public class Interaction_Some : Interaction
{
    //This Interaction Module allows the player to interact with armies via tap interactions on their screens

    public override void Interact()
    {
        //Execute Some Interaction
    }
}
```
## Authors

Victor Jore
- [@hukuna](https://www.github.com/Hukunaa)


## Support

For support, email victorjorepro@gmail.com.

