# Purpose

This is a C# port of John Pennycook's [godot-utility-ai](https://github.com/Pennycook/godot-utility-ai) which is written in GDScript.

The original GDScript addon may be used in combination with C# via 
[cross-language-scripting](https://docs.godotengine.org/en/stable/tutorials/scripting/cross_language_scripting.html), but in some cases it might be beneficial to have the addon available directly in C#.

# Differences to original version
The `UtilityAIContext` is strongly typed to be a small wrapper around a `<string,float>` dictionary, instead of being `Variant`.