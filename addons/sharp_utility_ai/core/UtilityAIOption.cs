using Godot;

[Tool]
[GlobalClass]
public partial class UtilityAIOption : Resource {
    [Export]
    public UtilityAIBehavior Behavior;

    [Export]
    public Godot.Collections.Dictionary Context;

    [Export]
    public UtilityAIAction Action = new();

    public float Evaluate() {
        return Behavior.Evaluate(Context);
    }
}