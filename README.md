# Slime Mold Simulations

This is an implementation of Slime Mold simulations through Unity and their compute shaders. Compute shaders allow calculations to be run in parallel on the GPU, written in DirectX 11 [HLSL](http://msdn.microsoft.com/en-us/library/windows/desktop/bb509561.aspx). This is mainly used to decrease rendering times for complex simulations or textures, and is supported by the following API's

- macOS and iOS: [Metal Graphics](https://developer.apple.com/metal/)
- Android, Linux, and Windows: [Vulkan](https://www.khronos.org/vulkan/)
- [OpenGL](https://www.opengl.org/) 4.3^ for Windows and

In this project, I use compute shaders to allow for fast computation of individual slime mold "agents". For exact information about how this project works, see [this video](https://www.youtube.com/watch?v=X-iSQQgOd1A&ab_channel=SebastianLague), which the project was based off of.

# Installation and Running

Just opening `builds/1.1/Compute Shaders.exe` will open the game for you. Keep in mind that, since this uses GPU specific technology, you must have the most recent drivers and appropriate API installed for your system.

> NOTE: I have yet to get this running on Linux, development was in Windows.

To install this project into your Unity Hub, simply clone this repository into wherever you keep your Unity Projects (usually `Documents/Unity Projects`). This will allow you to launch through Unity Hub.
