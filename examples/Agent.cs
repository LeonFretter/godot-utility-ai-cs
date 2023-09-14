using Godot;
using System;

[GlobalClass]
public partial class Agent : Node {
    [Export]
    public Godot.Collections.Array<UtilityAIOption> Options;

    [Export]
    public Godot.Collections.Dictionary Context = new();

    private Label _actionLabel;

    public override void _Ready() {
        _actionLabel = GetNode<Label>("CanvasLayer/HBoxContainer/Sliders/CurrentActionLabel");
        foreach (var option in Options) {
            option.Context = Context;
        }
        Context["nutrition"] = .5f;
        Context["hydration"] = .5f;
        Context["energy"] = .5f;
    }

    public override void _Process(double delta) {
        string bestAction = "idle";
        var bestOption = UtilityAI.ChooseHighest(Options);
        if(bestOption != null) {
            bestAction = bestOption.Action.ActionName;
        }
        _actionLabel.Text = bestAction;
    }

    public void SetNutrition(float value) {
        Context["nutrition"] = value / 100f;
    }

    public void SetHydration(float value) {
        Context["hydration"] = value / 100f;
    }

    public void SetEnergy(float value) {
        Context["energy"] = value / 100f;
    }
}