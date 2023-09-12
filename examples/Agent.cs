using Godot;

[GlobalClass]
public partial class Agent : Node {
    [Export]
    public Godot.Collections.Array<UtilityAIOption> Options = new();
}