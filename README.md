# script-tools

This is an ongoing collection of useful one-off C# scripts that I have created for Unity, including example scenes.

-------------------

AnimSpriteController
=======

![gif](https://imgur.com/Ol4JuGC.gif)

*AnimSpriteController* allows the user to implement a sequence of any number of sprite textures as an animation for use within UI elements. 
Useful for custom animations exported from something like After Effects, which otherwise couldn't be done within Unity.

Includes the following features:
- Custom frame rate
- Play animation only once
- Play animation repeatedly
- Rewind animation
- Ping-pong animation

-------------------

AvatarControl
=======

![gif](https://imgur.com/JjD2MMs.gif)

*AvatarColorControl* allows for custom coloration of an instanced prefab. Note that it only works with Opaque materials right now.

*AvatarTextControl* allows for custom floating text (player names, for instance), always faces the viewer, and can be set to a fixed relative size so that it always remains readable at a distance.
Useful for very simple custom avatars in a multiplayer environment.

-------------------

CustomEditorCamera
=======

![gif](https://imgur.com/ADkJXaN.gif)

*CustomEditorCamera* is designed to be placed on the Main Camera, allowing for control similar to a 3D editing package.
Additionally, the view will center on any objects selected with a mouse click. Click any non-collision area to de-select.
Note that currently this requires the added step of manually creating a "Selected" Tag in the project.

Controls:
- Rotate/Orbit: Drag right-mouse button
- Translate: Drag middle-mouse button
- Zoom: Mouse scroll wheel
- Zoom (smoothly): Ctrl + Alt + middle-mouse

-------------------

DrawLine
=======

![gif](https://imgur.com/ZGyqU73.gif)

*DrawLine* is a very simple script which creates a line between any two transform locations in the scene, whether they are in motion or not. Useful for floating labels or connection indicators. The line renderer will use a default material unless otherwise specified.

-------------------

LightTrigger
=======

![gif](https://imgur.com/kFUpzft.gif)

*LightTrigger* works with a trigger volume around an object of interest, activating a camera light only when it enters that volume.
The example scene should show how this simple script can be set up and used.

-------------------

SurfaceNormalPlace
=======

![gif](https://imgur.com/xd4DwnZ.gif)

*SurfaceNormalPlace* Allows the user to place an array of objects on the surface of a collision object, aligned to its surface normal direction. Allows for custom offset and random rotation.
Useful for quickly placing environment objects such as flora or buildings.

-------------------

TriggerToClick
=======

![gif](https://imgur.com/IeYO9lD.gif)

*TriggerToClick* is specifically for activating On Click events in a World Space Unity UI button with a trigger volume attached.
Useful for creating VR/AR user interfaces.


