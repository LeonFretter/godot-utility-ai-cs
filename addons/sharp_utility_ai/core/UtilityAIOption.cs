using Godot;

[Tool]
[GlobalClass]
public partial class UtilityAIOption : Resource {
    [Export]
    public UtilityAIBehavior Behavior;

    [Export]
    public UtilityAIContext Context;

    [Export]
    public Variant Action;

    public float Evaluate() {
        return Behavior.Evaluate(Context);
    }
}