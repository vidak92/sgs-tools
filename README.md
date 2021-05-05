# MijanTools

A collection of utility-style scripts for Unity.

## Features

- [DebugDraw](DebugDraw.cs) - An immediate-mode API for easily drawing basic primitive shapes. Supports drawing lines, rects & circles. Uses **LineRenderer** components for drawing.
- [SpriteShadow](SpriteShadow.cs) - Make one **SpriteRenderer** act as a shadow of another **SpriteRenderer**.
- [FPSCounter](FPSCounter.cs) - An FPS counter for calculating and displaying min, max, and average FPS. Based on Catlike Coding's [Frames Per Second tutorial](https://catlikecoding.com/unity/tutorials/frames-per-second/).
- Helper methods & components for easily manipulating transforms, colors, strings, sprites, cameras, time scale etc.

## Usage

Clone the repo or copy it's contents anywhere inside the 'Assets' folder of your Unity project. If you're using Git, you can add this repo as a submodule. 

## Dependencies

- Tested with Unity 2019 LTS. Might not work well with other versions.
- [TextMesh Pro](https://docs.unity3d.com/Manual/com.unity.textmeshpro.html)
