/**
* MIT License
* 
* Copyright (c) 2024 Manuel Bottini
*
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:

* The above copyright notice and this permission notice shall be included in all
* copies or substantial portions of the Software.

* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
* SOFTWARE.
*/

using UnityEngine;

public class EvoslashCostants
{

    public readonly static string PLAYER_PREFERENCE_HAPTIC_ENABLED = "PLAYER_PREFERENCE_HAPTIC_ENABLED";
    public readonly static string PLAYER_PREFERENCE_FORCE_BODYPART_PLAYER = "PLAYER_PREFERENCE_FORCE_BODYPART_PLAYER";
    public readonly static string PLAYER_PREFERENCE_PLAYER_NUM = "PLAYER_PREFERENCE_PLAYER_NUM";
    public readonly static string PLAYER_PREFERENCE_GAME_NAME = "PLAYER_PREFERENCE_GAME_NAME";
    public readonly static string PLAYER_PREFERENCE_HIGH_SCORE = "PLAYER_PREFERENCE_HIGH_SCORE";
    public readonly static string PLAYER_PREFERENCE_HARDCORE_MODE = "PLAYER_PREFERENCE_HARDCORE_MODE";
    public readonly static string PLAYER_PREFERENCE_PLAYING_MUSIC = "PLAYER_PREFERENCE_PLAYING_MUSIC";

    public readonly static int PLAYER_PREFERENCE_HAPTIC_ENABLED_DEFAULT = 1;
    public readonly static int PLAYER_PREFERENCE_PLAYER_NUM_DEFAULT = 1;
    public readonly static int PLAYER_PREFERENCE_HARDCORE_MODE_DEFAULT = 0;
    public readonly static int PLAYER_PREFERENCE_PLAYING_MUSIC_DEFAULT = 1;
    public readonly static int PLAYER_PREFERENCE_FORCE_BODYPART_PLAYER_ENABLED_DEFAULT = 1;

    public readonly static int[] DifficultyScoreLimits = new int[] { 0, 1000, 3000, 5000, 7000, 10000, 13000, 16000, 19000 };
    public readonly static int[] DifficultySpawnTimesMs = new int[] { 2000, 1500, 1000, 800, 700, 600, 500, 450, 400 };
    public readonly static float[] DifficultyRedimensions = new float[] { 1.0f, 0.9f, 0.8f, 0.7f, 0.6f, 0.5f, 0.45f, 0.42f, 0.4f };
    public readonly static float[] BasketSizes = new float[] { 1f, 1.1f, 1.2f, 1.3f, 1.4f, 1.5f, 1.7f, 1.9f, 2.0f };

    public static string GenerateGameName()
    {
        return "BN";
    }

    public static bool CheckLevel(int newLevel, int currentLevel)
    {
        if (newLevel == currentLevel)
        {
            Debug.Log("Same current level");
            return false;
        }
        if (newLevel > DifficultyScoreLimits.Length - 1)
        {
            Debug.Log("Level not valid");
            return false;
        }
        if (newLevel < 0)
        {
            Debug.Log("Level not valid");
            return false;
        }
        return true;
    }
}

