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

using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsBehaviour : MonoBehaviour
{
    public Toggle HapticToggle;
    public Toggle ForceBodypartPlayerToggle;
    public InputField GameNameInputText;
    public InputField HighScoreInputText;
    public Toggle HardcoreToggle;
    public Toggle PlayingMusicToggle;


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt(EvoslashCostants.PLAYER_PREFERENCE_HAPTIC_ENABLED, EvoslashCostants.PLAYER_PREFERENCE_HAPTIC_ENABLED_DEFAULT) == 1)
        {
            HapticToggle.isOn = true;
        }
        else
        {
            HapticToggle.isOn = false;
        }

        if (PlayerPrefs.GetInt(EvoslashCostants.PLAYER_PREFERENCE_FORCE_BODYPART_PLAYER, EvoslashCostants.PLAYER_PREFERENCE_FORCE_BODYPART_PLAYER_ENABLED_DEFAULT) == 1)
        {
            ForceBodypartPlayerToggle.isOn = true;
        }
        else
        {
            ForceBodypartPlayerToggle.isOn = false;
        }

        String gameName = PlayerPrefs.GetString(EvoslashCostants.PLAYER_PREFERENCE_GAME_NAME, "");
        if (gameName == "")
        {
            gameName = EvoslashCostants.GenerateGameName();
            PlayerPrefs.SetString(
                EvoslashCostants.PLAYER_PREFERENCE_GAME_NAME,
                gameName);
        }
        GameNameInputText.text = gameName;

        int recordedHighScore = PlayerPrefs.GetInt(EvoslashCostants.PLAYER_PREFERENCE_HIGH_SCORE, 0);
        HighScoreInputText.text = recordedHighScore + "";

        if (PlayerPrefs.GetInt(EvoslashCostants.PLAYER_PREFERENCE_HARDCORE_MODE, EvoslashCostants.PLAYER_PREFERENCE_HARDCORE_MODE_DEFAULT) == 1)
        {
            HardcoreToggle.isOn = true;
        }
        else
        {
            HardcoreToggle.isOn = false;
        }

        if (PlayerPrefs.GetInt(EvoslashCostants.PLAYER_PREFERENCE_PLAYING_MUSIC, EvoslashCostants.PLAYER_PREFERENCE_PLAYING_MUSIC_DEFAULT) == 1)
        {
            PlayingMusicToggle.isOn = true;
        }
        else
        {
            PlayingMusicToggle.isOn = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void HapticToggleChanged() {
        if (HapticToggle.isOn)
        {
            Debug.Log("Haptic is ON");
            PlayerPrefs.SetInt(EvoslashCostants.PLAYER_PREFERENCE_HAPTIC_ENABLED, 1);
        }
        else
        {
            Debug.Log("Haptic is OFF");
            PlayerPrefs.SetInt(EvoslashCostants.PLAYER_PREFERENCE_HAPTIC_ENABLED, 0);
        }
    }

    public void ForceBodypartPlayerToggleChanged()
    {
        if (ForceBodypartPlayerToggle.isOn)
        {
            Debug.Log("ForceBodypartPlayer is ON");
            PlayerPrefs.SetInt(EvoslashCostants.PLAYER_PREFERENCE_FORCE_BODYPART_PLAYER, 1);
        }
        else
        {
            Debug.Log("ForceBodypartPlayer is OFF");
            PlayerPrefs.SetInt(EvoslashCostants.PLAYER_PREFERENCE_FORCE_BODYPART_PLAYER, 0);
        }
    }

    public void  SaveGameName() {
        string gameName = GameNameInputText.text;
        if (string.IsNullOrEmpty(gameName)) {
            GameNameInputText.text = PlayerPrefs.GetString(EvoslashCostants.PLAYER_PREFERENCE_GAME_NAME, "");
        } else {
            PlayerPrefs.SetString(EvoslashCostants.PLAYER_PREFERENCE_GAME_NAME, gameName);
        }
    }

    public void ResetHighScore()
    {
        PlayerPrefs.SetInt(EvoslashCostants.PLAYER_PREFERENCE_HIGH_SCORE, 0);
        HighScoreInputText.text = "0";
    }


    public void HardcoreToggleChanged()
    {
        if (HardcoreToggle.isOn)
        {
            Debug.Log("HardcoreToggle is ON");
            PlayerPrefs.SetInt(EvoslashCostants.PLAYER_PREFERENCE_HARDCORE_MODE, 1);
        }
        else
        {
            Debug.Log("HardcoreToggle is OFF");
            PlayerPrefs.SetInt(EvoslashCostants.PLAYER_PREFERENCE_HARDCORE_MODE, 0);
        }
    }

    public void PlayingMusicToggleChanged()
    {
        if (PlayingMusicToggle.isOn)
        {
            Debug.Log("PlayingMusicToggle is ON");
            PlayerPrefs.SetInt(EvoslashCostants.PLAYER_PREFERENCE_PLAYING_MUSIC, 1);
        }
        else
        {
            Debug.Log("PlayingMusicToggle is OFF");
            PlayerPrefs.SetInt(EvoslashCostants.PLAYER_PREFERENCE_PLAYING_MUSIC, 0);
        }
    }
}

