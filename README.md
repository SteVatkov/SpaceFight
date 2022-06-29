 ![alt](docs/images/Geese03a.png) 
 
# Dune Sea Base Code 

Base code and assets for Dune Sea. 
This is Rev3, started on October 2019.

### Unity version: 2018.4.10
### Last Update: October 2019
Branch: dev

## Game Hot Keys
* The game features a built in console, accessible with ` [BackSlash]
* Pause menu with Escape key or gamepad 'start'

![alt](docs/images/keys-desc.png) 

## Gamepad Controls
See in game chart.

## Keyboard Controls
* Directions: WASD or arrow keys
* Hold K to flap
* Hold L while flying close to the ground to land
* Special Moves:
    - Tap 1 to Honk
	- Tap 2 to Roll
	- Hold 3 to Dive, release to pull up

## Scenes

There are 3 screens in the game.  Each screen is contained in a Unity scene, and each can be loaded and unloaded from memory. The 3 scenes are included in the build:


- Title Screen (TitleScreen)
- Replay and game screens (StatsReplay)
- Game (GameScreen)


---------------| ![alt](docs/images/screens-01.png) 

Above we can see the Game state contains both the replay and game scenes, and that the 2 scenes will be loaded (and unloded) at the same time.

To start the game, run the Title scene and select start from the menu.

## Builds
The project is setup in Unity Cloud build. Builds can be found here:
https://developer.cloud.unity3d.com/build/orgs/frolic-labs/projects/dunesea/


### How do I get set up? ###

This project requires git 2.x with git LFS.
* Install/Update Git   https://git-for-windows.github.io/ 
* Make sure LFS option is checked for install options

## Useful links
LeanTween: 
http://dentedpixel.com/LeanTweenDocumentation/classes/LeanTween.html

Example
```
// move through bezier path
LeanTween.move(gameObject, new Vector3[]{p0, c0, c1, p1, c2, c3, p2 ..}, 1.5f)
	.setEase(LeanTweenType.easeOutQuad)
	.setOrientToPath(true);
```