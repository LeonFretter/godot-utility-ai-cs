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

    public float Evaluate(Godot.Collections.Dictionary context) {
        float x = context[InputKey].As<float>();
        if (Invert) {
            x = 1 - x;
        }
        return ResponseCurve.Evaluate(x);
    }
}