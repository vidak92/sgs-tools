# MijanTools

A collection of utility-style scripts and shaders for Unity.

## Features

- Extension methods for [Transform](Common/TransformExtensions.cs) and [other](Common/CommonExtensions.cs) components.
- [DebugDraw](DebugDraw.cs) - An immediate-mode API for easily drawing basic primitive shapes. Supports drawing lines & circles. Uses **LineRenderer** components for drawing.
- [CoroutineUtils](CoroutineUtils.cs) - Coroutine helper methods that make it easy to make delayed calls from any script(e.g. call a function after one frame, X frames or X seconds).
- [Shaders](Shaders) - Shaders for some basic effects.
- Components:
	- [ObjectPool](Components/ObjectPool.cs) - A generic object pooling class. Works with any **MonoBehaviour**.
	- [ObjectShaker](Components/ObjectShaker.cs) - Can be used for a screenshake effect or shaking any object.
	- [FPSCounter](FPSCounter.cs) - An FPS counter for calculating and displaying min, max, and average FPS. Based on Catlike Coding's [Frames Per Second tutorial](https://catlikecoding.com/unity/tutorials/frames-per-second/).
	- [Other](Components) reusable components.

## Usage

Clone the repo or copy it's contents anywhere inside the 'Assets' folder of your Unity project. If you're using Git, you can add this repo as a submodule. 

## Dependencies

- Tested with Unity 2019 LTS and Unity 2020 LTS. Might not work well with other versions.
- [TextMesh Pro](https://docs.unity3d.com/Manual/com.unity.textmeshpro.html).

## Notes
- Only tested with Unity's built-in/legacy systems(e.g. renderer or input system).
