using Godot;

[Tool]
[GlobalClass]
public partial class UtilityAIConsideration : Resource {
    [Export]
    public string InputKey;

    [Export]
    public bool Invert;

    [Export]
    public UtilityAIResponseCurve ResponseCurve;

    public float Evaluate(UtilityAIContext context) {
        float x = context.Get(InputKey);
        if (Invert) {
            x = 1 - x;
        }
        return ResponseCurve.Evaluate(x);
    }
}