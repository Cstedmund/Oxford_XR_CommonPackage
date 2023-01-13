using UnityEngine;

public static class StaticManager
{
    public static readonly string MATCH_NAME = "OxfordGeogAR";

    public static readonly Color[] PLAYERS_COLORS = {
        new Color(146f / 255f, 0f / 255f, 0f / 255f),
        new Color(255f / 255f, 149f / 255f, 0f / 255f),
        new Color(255f / 255f, 255f / 255f, 0f / 255f),
        new Color(26f / 255f, 255f / 255f, 0f / 255f),
        new Color(0f / 255f, 255f / 255f, 217f / 255f),
        new Color(12f / 255f, 107f / 255f, 0f / 255f),
        new Color(0f / 255f, 51f / 255f, 255f / 255f),
        new Color(61f / 255f, 0f / 255f, 81f / 255f),
    };

    public static readonly Color NORMAL_COLOR = new Color(0f / 255f, 51f / 255f, 255f / 255f);
    public static readonly Color IN_PROGRESS_COLOR = new Color(0f / 255f, 255f / 255f, 217f / 255f);
    public static readonly Color DISABLED_COLOR = new Color(82f / 255f, 82f / 255f, 82f / 255f);
    public static readonly Color NOT_PRESSED_COLOR = new Color(255f / 255f, 0f / 255f, 255f / 255f);
    public static readonly Color PRESSED_COLOR = new Color(125f / 255f, 0f / 255f, 125f / 255f);
    public static readonly Color CONNECTED_COLOR = new Color(48f / 255f, 243f / 255f, 0f / 255f);
    public static readonly Color FAILED_COLOR = new Color(255f / 255f, 0f / 255f, 0f / 255f);
    public static readonly Color SUCCEEDED_COLOR = new Color(26f / 255f, 255f / 255f, 0f / 255f);
    public static readonly Color CORRECT_COLOR = new Color(99f / 255f, 224f / 255f, 48f / 255f);
    public static readonly Color WRONG_COLOR = new Color(255f / 255f, 32f / 255f, 0f / 255f);
    public static readonly Color SELECTED_MC_COLOR = new Color(255f / 255f, 106f / 255f, 255f / 255f);
    public static readonly Color TEXT_COLOR = new Color(255f / 255f, 237f / 255f, 207f / 255f);

    public static readonly int MIN_PLAYERS = 2;
    public static readonly int MAX_PLAYERS = 8;

    public static readonly int NUM_OF_PUZZLES = 8;
    public static readonly int NUM_OF_CHEMS = 3;
    public static readonly int NUM_OF_VOS = 5;
    public static readonly int NUM_OF_TEMPS = 4;
    public static readonly int NUM_OF_MCS = 5;
}
