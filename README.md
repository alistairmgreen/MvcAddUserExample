# Minimal ASP.NET MVC Example: Adding a User to a Database

[![Build status](https://ci.appveyor.com/api/projects/status/j9b2x9m447ne3pst?svg=true)](https://ci.appveyor.com/project/alistairmgreen/mvcadduserexample)

This is a simple example application demonstrating how to use ASP.NET MVC.

## Prerequisites

**Important:** The [Visual C++ Redistributable for Visual Studio 2015](https://www.microsoft.com/en-us/download/details.aspx?id=48145) is required. You can either install this manually or use [Chocolatey](https://chocolatey.org):
```
choco install vcredist2015 -y
```

The Visual C++ Redistributable is needed by the [libsodium-net](https://github.com/adamcaudill/libsodium-net)
cryptography library, which is used for password hashing.
