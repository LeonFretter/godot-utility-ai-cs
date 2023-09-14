using Godot;

[Tool]
[GlobalClass]
public partial class UtilityAIAction : Resource {
    [Export]
    public string ActionName = "idle";

    [Export]
    public Godot.Collections.Dictionary Params = new();
}