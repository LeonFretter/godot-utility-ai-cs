using Godot;
using System;

[GlobalClass]
public partial class Agent : Node {
    [Export]
    public Godot.Collections.Array<UtilityAIOption> Options;

    [Export]
    public UtilityAIContext Context;

    private Label _actionLabel;

    public override void _Ready() {
        _actionLabel = GetNode<Label>("CanvasLayer/HBoxContainer/Sliders/CurrentActionLabel");
        foreach (var option in Options) {
            option.Context = Context;
        }
    }

    public override void _Process(double delta) {
        var bestOptionScore = .0f;
        string bestAction = "idle";

        foreach (var option in Options) {
            var score = option.Evaluate();
            if(score > bestOptionScore) {
                bestOptionScore = score;
                // In this example, the action is a dict with a single key "name"
                bestAction = option.Action["Name"].As<string>();
            }
        }

        _actionLabel.Text = bestAction;
    }

    public void SetNutrition(float value) {
        Context.Set("nutrition", value / 100f);
    }

    public void SetHydration(float value) {
        Context.Set("hydration", value / 100f);
    }

    public void SetEnergy(float value) {
        Context.Set("energy", value / 100f);
    }
}