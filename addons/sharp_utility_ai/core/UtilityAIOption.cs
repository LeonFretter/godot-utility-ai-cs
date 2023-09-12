using Godot;

[Tool]
[GlobalClass]
public partial class UtilityAIOption : Resource {
    [Export]
    public UtilityAIBehavior Behavior;

    [Export]
    public Godot.Collections.Dictionary Context;

    [Export]
    public Godot.Collections.Dictionary Action;

    public float Evaluate() {
        return Behavior.Evaluate(Context);
    }
}