# SimplePathSelector
Use the `SimplePathSelector` class if you have multiple sources for a path, e.g. user input, a configuration file and a default value. It allows you to collect all values at a central place and define the order in which they are eventually choosen. Use the examples below to get a quick start. 

The nuget package can be found [here](https://www.nuget.org/packages/zahnom.SimplePathSelector/). You can install the latest version by typing `Install-Package zahnom.SimplePathSelector` in the Package Manager Console in Visual Studio. Note that `SimplePathSelector` is currently using .NET framework 4.6.1.

# Examples
## Simple use case
In this example we create a new path selector that takes some user input when available and otherwise a default value. `UserInput` and `DefaultValue` are part of the SimplePathSelector library and can be found at [`SimplePathSelector/SimplePathSelector/PathProviders/`](https://github.com/zahnom/SimplePathSelector/tree/master/SimplePathSelector/PathProviders).
```c#
// Create selector and set the desired provider order. Here, the user input
// is prefered over the default value.
var myPathSelector =  new SimplePathSelectorBuilder()
    .FirstChoice(typeof(UserInput))
    .Otherwise(new DefaultValue(@"C:\some\dir\"))
    .Create();

myPathSelector.AddPathProvider(new UserInput(inputOfUser));

// Get the path: Will be the value of inputOfUser.
var pathToFile = myPathSelector.SelectPath();
```
## Current working directory as last choice
When no user input is given, the current working directory shall be used. The `CurrentDirectory` path provider is part of the SimplePathSelector library and can be found at [`SimplePathSelector/SimplePathSelector/PathProviders/`](https://github.com/zahnom/SimplePathSelector/tree/master/SimplePathSelector/PathProviders).
```c#
var myPathSelector = new SimplePathSelectorBuilder()
    .FirstChoice(typeof(UserInput))
    .Otherwise(new CurrentDirectory())
    .Create();

// Get the path: Will be the value of the current working directory.
var pathToFile = myPathSelector.SelectPath();
```
## Using a path selector as provider
It is also possible to use a path selector as path provider for another path selector.
```c#
var myFirstPathSelector = new SimplePathSelectorBuilder() 
   .FirstChoice(typeof(UserInput))
   .Otherwise(new DefaultValue(@"C:\some\default\path"))
   .Create();

var myOtherPathSelector = new SimplePathSelectorBuilder() 
   .FirstChoice(myFirstPathSelector)
   .Otherwise(new DefaultValue(@"some\default\path"))
   .Create();
```
