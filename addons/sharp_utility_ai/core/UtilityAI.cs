using Godot;

public static class UtilityAI {
    public static UtilityAIOption ChooseHighest(
        Godot.Collections.Array<UtilityAIOption> options
    ) {
        var bestOptionScore = .0f;
        UtilityAIOption bestOption = null;

        foreach (var option in options) {
            var score = option.Evaluate();
            if(score > bestOptionScore) {
                bestOptionScore = score;
                bestOption = option;
            }
        }

        return bestOption;
    }
}