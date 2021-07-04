# Compute Shaders

This is an implementation of various simulations through Unity and their compute shaders. Compute shaders allow calculations to be run in parallel on the GPU, written in DirectX 11 [HLSL](http://msdn.microsoft.com/en-us/library/windows/desktop/bb509561.aspx). This is mainly used to decrease rendering times for complex simulations or textures, and is supported by the following API's

- macOS and iOS: [Metal Graphics](https://developer.apple.com/metal/)
- Android, Linux, and Windows: [Vulkan](https://www.khronos.org/vulkan/)
- [OpenGL](https://www.opengl.org/) 4.3^ for Windows and

## Slime Molds

In this project, I use compute shaders to allow for fast computation of individual simulated "agents". For exact information about how this project works, see [this video](https://www.youtube.com/watch?v=X-iSQQgOd1A&ab_channel=SebastianLague), which the project was based off of.

## Primordial Particle System

This one is an implementation of a primordial particle system (PPS), where complex relationships can arise from very simple rules. I was attempting to recreate a simulation like [this one](https://www.youtube.com/watch?v=-c5XaC5-DXg&ab_channel=ScienceMathematics), but you're able to tune the parameters yourself and see if you can find some cool results.

## Installation and Running

Just opening `builds/Slime Molds/Compute Shaders.exe` will open the Slime Mold simulation for you, while `builds/Primordial Particle System/Compute Shaders.exe` will open the PPS simulation. Keep in mind that, since this uses GPU specific technology, you must have the most recent drivers and appropriate API installed for your system.

> NOTE: I have yet to get this running on Linux, development was in Windows.

To install this project into your Unity Hub, simply clone this repository into wherever you keep your Unity Projects (usually `Documents/Unity Projects`). This will allow you to launch through Unity Hub.
