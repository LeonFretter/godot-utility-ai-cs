using Godot;

[Tool]
[GlobalClass]
public partial class UtilityAIContext : Resource {
    [Export]
    public Godot.Collections.Dictionary<string, float> Dictionary = new();

    public float Get(string propertyKey) {
        return Dictionary[propertyKey];
    }

    public bool Set(string propertyKey, float value) {
        Dictionary[propertyKey] = value;

        return true;
    }
}