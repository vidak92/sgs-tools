# MijanTools

A collection of utility-style scripts and shaders for Unity.

## Features

- Extension methods for [Transform](Assets/Scripts/Common/TransformExtensions.cs) and other [common components](Assets/Scripts/Common/CommonExtensions.cs).
- [Utility](Assets/Scripts/Util) scripts for **Coroutines**, **MenuItems**, **strings**, **time scale** and other stuff.
- [Shaders](Assets/Shaders) for some basic effects(for Unity's built-in renderer).
- [DebugDraw](Assets/Scripts/Util/DebugDraw.cs): An immediate-mode API for easily drawing basic primitive shapes. 
	- Supports drawing lines & circles. Uses **LineRenderer** components for drawing.
- [Components](Assets/Scripts/Components):
	- [ObjectPool](Assets/Scripts/Components/ObjectPool.cs): A generic object pooling class. Works with any **MonoBehaviour**.
	- [ObjectShaker](Assets/Scripts/Components/ObjectShaker.cs): Can be used for a screenshake effect or shaking any object.
	- [FPSCounter](Assets/Scripts/Components/FPSCounter.cs): An FPS counter for calculating and displaying min, max, and average FPS. Based on Catlike Coding's [Frames Per Second tutorial](https://catlikecoding.com/unity/tutorials/frames-per-second/).
	- Other reusable components.

## Usage

This library is available as a package for [Unity's package manager](https://docs.unity3d.com/Manual/Packages.html). You can either clone the repo and use it as a [local package](https://docs.unity3d.com/Manual/upm-ui-local.html), or add it as a [git package](https://docs.unity3d.com/Manual/upm-git.html) to your Unity project.

## Notes

- Should work well with Unity 2020 LTS and Unity 2021 LTS versions.
