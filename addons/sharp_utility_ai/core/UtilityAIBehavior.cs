using Godot;
using System.Linq;

[Tool]
[GlobalClass]
public partial class UtilityAIBehavior : Resource {
    public enum AggregationType {
        PRODUCT,
        AVERAGE,
        MAXIMUM,
        MINIMUM
    }

    [Export]
    public string Name;

    [Export]
    public AggregationType Aggregation = AggregationType.PRODUCT;

    [Export]
    public Godot.Collections.Array<UtilityAIConsideration> Considerations = new ();

    public float Evaluate(UtilityAIContext context) {
        var scores = new Godot.Collections.Array<float>();
        foreach (var consideration in Considerations) {
            scores.Add(consideration.Evaluate(context));
        }
        return Aggregate(scores);
    }

    private float Aggregate(Godot.Collections.Array<float> scores) {
        switch (Aggregation) {
            case AggregationType.PRODUCT:
                return scores.Aggregate(1.0f, (acc, x) => acc * x);
            case AggregationType.AVERAGE:
                return scores.Aggregate(.0f, (acc, x) => acc + x) / scores.Count;
            case AggregationType.MAXIMUM:
                return scores.Max();
            case AggregationType.MINIMUM:
                return scores.Min();
            default:
                return scores.Aggregate((acc, x) => acc * x);
        }
    }
}