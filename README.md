# SimplePathSelector
Use the `SimplePathSelector` class if you have multiple sources for a path, e.g. user input, a configuration file and a default value. You can add all these sources to the path selector and define the order in which the values should be choosen. Use the examples below to get a quick start.

# Example: Simple use case
In this example we create a new path selector that takes some user input when available and otherwise a default value. `UserInput` and `DefaultValue` are part of the SimplePathSelector library and can be found at [`SimplePathSelector/SimplePathSelector/PathProviders/`](https://github.com/zahnom/SimplePathSelector/tree/master/SimplePathSelector/PathProviders).
```c#
// Create selector and set the desired provider order. Here, the user input
// is prefered over the default value.
var myPathSelector = new SimplePathSelector(
  typeof(UserInput), typeof(DefaultValue));

// Add some paths.
myPathSelector.AddPathProviderFor("path-to-a-file", new UserInput(inputOfUser));
myPathSelector.AddPathProviderFor("path-to-a-file", new DefaultValue(@"C:\some\dir\");

// Get the path.
var pathToFile = myPathSelector.SelectPathFor("path-to-a-file");
```
# Example: Current working directory as last choice
When no user input is given for the entry called `"path-to-a-file"`, the current working directory is used. The current directory path provider is part of the SimplePathSelector library and can be found at [`SimplePathSelector/SimplePathSelector/PathProviders/`](https://github.com/zahnom/SimplePathSelector/tree/master/SimplePathSelector/PathProviders).
```c#
var myPathSelector = new SimplePathSelector(
  typeof(UserInput), typeof(CurrentDirectory));

myPathSelector.AddPathProviderFor("path-to-a-file", new CurrentDirectory());

var pathToFile = myPathSelector.SelectPathFor("path-to-a-file");
```
