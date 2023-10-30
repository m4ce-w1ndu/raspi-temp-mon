# Raspberry Pi Temperature Monitor

# 1. Features
The Raspberry Pi 4 Temperature Monitor provides a simple set of utilities to easily read the CPU temperature on Raspberry Pi 4 systems (possibly others).

This small set of packages provides the following features:
* Temperature reader: .NET library to **read temperatures** from standard Linux facilities.
* Temperature monitor: a .NET **service** that will provide temperature **registration** and other features such as graphing.
* Temperature viewer: small **CLI interface** that allows the user to query the temperature with a single command.

# 2. Compatibility
All packages are written using .NET 6.0 and C# 10.0, using a partial SOLID approach, to allow the best modularity and adaptability of the code.
It is, therefore, possible to use the **TempMonitor** and **TempViewer** projects as they are currenty, provided that the established Interface
**IReader** is properly implemented.

As of right now, the package offers full compatibility with **Raspberry Pi 4**
