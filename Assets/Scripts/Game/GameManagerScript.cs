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
using System.Collections.Generic;
using UnityEngine;

using BodynodesDev.Common;

public class GameManagerScript : MonoBehaviour
{
    public Microlight.MicroBar.MicroBar HealthBar;
    public int InitialHealth = 100;

    public SpawnerGenericScript SpawnerLevel1;

    public GameUIManagerScript GameUIManager;
    public BodynodesPlayer BnPlayer;
    public BodynodesController BnSword;
    private BodynodesHostCommunicator HostCommunicator;

    public TextMesh ScoreText;
    public BasketRescalerScript BasketRescaler;

    private int mCurrentScore;

    private int mCurrentHealth = 100;
    private bool mGameRunning = false;
    private string mNodeRequesting = null;
    private List<string> mTempNodeBlacklist = null;

    private void Awake()
    {
        HostCommunicator = BodynodesHostCommunicator.Instance;

    }

    // Start is called before the first frame update
    void Start()
    {
        HealthBar.Initialize(InitialHealth);
        SpawnerLevel1.stopSpawning();

        BnDatatypes.BnName player = new BnDatatypes.BnName();
        player.value = "" + PlayerPrefs.GetInt(
                EvoslashCostants.PLAYER_PREFERENCE_PLAYER_NUM,
                EvoslashCostants.PLAYER_PREFERENCE_PLAYER_NUM_DEFAULT);
        BnPlayer.setPlayer(player);

        mTempNodeBlacklist = new List<string>();
        mNodeRequesting = null;
        String nodeRequest = HostCommunicator.anyNodeRequesting();
        if (nodeRequest != null)
        {
            mNodeRequesting = nodeRequest;
            GameUIManager.nodeFound();
        }
    }

    private void checkNodes() {
        if (mGameRunning) {
            return;
        }

        if (GameUIManager.isLostUIActive()) {
            return;
        }

        // Game not running
        if( BnSword.isReceiving() )
        {
            restartGame();
            return;
        }

        // Game not running and sword is not receiving
        if (mNodeRequesting != null)
        {
            // A request is in progress
            return;
        }
        String nodeRequest = HostCommunicator.anyNodeRequesting();
        if (mTempNodeBlacklist.Contains(nodeRequest))
        {
            // The requesting node was previously temporarily banned
            return;
        }

        if (nodeRequest != null)
        {
            mNodeRequesting = nodeRequest;
            GameUIManager.nodeFound();
        }
        else if (!GameUIManager.isUIRunning())
        {
            GameUIManager.noNodeConnected();
        }
    }

    public void acceptNode()
    {
        HostCommunicator.acceptNodeRequesting(mNodeRequesting);
        mNodeRequesting = null;

        if (PlayerPrefs.GetInt(EvoslashCostants.PLAYER_PREFERENCE_FORCE_BODYPART_PLAYER, EvoslashCostants.PLAYER_PREFERENCE_FORCE_BODYPART_PLAYER_ENABLED_DEFAULT) == 1)
        {
            Debug.Log("Forcing player and node");
            forcePlayerOnNode();
            forceBodypartOnNode();
        }
    }

    public void dontAcceptNode()
    {
        GameUIManager.noNodeConnected();
        mTempNodeBlacklist.Add(mNodeRequesting);
        mNodeRequesting = null;
    }

        // Update is called once per frame
    void Update()
    {
        checkNodes();
        // Check for the back button press on Android
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameUIManager.doYouWantToExit();
        }
    }

    private void gameFinished() {
        stopGame();
        int recordedHighScore = PlayerPrefs.GetInt(EvoslashCostants.PLAYER_PREFERENCE_HIGH_SCORE, 0);
        Debug.Log("Recorded score is = " + recordedHighScore + " and current score is " + mCurrentScore);
        bool isNewHighScore = false;
        if (mCurrentScore > recordedHighScore)
        {
            isNewHighScore = true;
            PlayerPrefs.SetInt(
                EvoslashCostants.PLAYER_PREFERENCE_HIGH_SCORE,
                mCurrentScore);
        }
        GameUIManager.youLost(isNewHighScore);
    }

    public void playerGotHit(int damage)
    {
        changeHealth(-damage);
        if (mCurrentHealth <= 0 && !GameUIManager.isLostUIActive())
        {
            gameFinished();
        }

        if (PlayerPrefs.GetInt(EvoslashCostants.PLAYER_PREFERENCE_HAPTIC_ENABLED, EvoslashCostants.PLAYER_PREFERENCE_HAPTIC_ENABLED_DEFAULT) == 1)
        { 
            BnSword.sendHapticAction(500, 200);
        }
    }

    public void swordCutThisThing(GameObject thing)
    {
        Debug.Log("swordCutThisThing thing name is = "+thing.name);

        int heal = thing.GetComponent<ThrownItemCharacteristics>().Heals;
        int points = thing.GetComponent<ThrownItemCharacteristics>().Points;

        if (heal > 0)
        {
            changeHealth(heal);
        }
        if (points > 0)
        { 
            changeScore(points);
            if (PlayerPrefs.GetInt(EvoslashCostants.PLAYER_PREFERENCE_HAPTIC_ENABLED, EvoslashCostants.PLAYER_PREFERENCE_HAPTIC_ENABLED_DEFAULT) == 1)
            {
                BnSword.sendHapticAction(200, 100);
            }
        }
    }

    private void changeHealth(int value)
    {
        mCurrentHealth += value;
        if (mCurrentHealth > 100)
        {
            mCurrentHealth = 100;
        }
        else if (mCurrentHealth < 0) 
        {
            mCurrentHealth = 0;
        }
        HealthBar.UpdateHealthBar(mCurrentHealth);
    }

    private void changeScore(int value) {
        mCurrentScore += value;
        ScoreText.text = mCurrentScore+"";
    }

    public void restartGame()
    {
        if (PlayerPrefs.GetInt(EvoslashCostants.PLAYER_PREFERENCE_PLAYING_MUSIC, EvoslashCostants.PLAYER_PREFERENCE_PLAYING_MUSIC_DEFAULT) == 1)
        {
            GetComponent<AudioSource>().Play();
        }
        mCurrentHealth = InitialHealth;
        GameUIManager.restartGame();
        HealthBar.UpdateHealthBar(mCurrentHealth);
        SpawnerLevel1.startSpawning();
        mGameRunning = true;
        mCurrentScore = 0;
        ScoreText.text = "0";
    }

    public void stopGame() {
        if (PlayerPrefs.GetInt(EvoslashCostants.PLAYER_PREFERENCE_PLAYING_MUSIC, EvoslashCostants.PLAYER_PREFERENCE_PLAYING_MUSIC_DEFAULT) == 1)
        {
            GetComponent<AudioSource>().Stop();
        }
        SpawnerLevel1.stopSpawning();
        mGameRunning = false;
    }

    private void forcePlayerOnNode() {
        BnDatatypes.BnName all_players = new BnDatatypes.BnName
        {
            value = BnConstants.PLAYER_ALL_TAG
        };
        BnDatatypes.BnBodypart all_bodyparts = new BnDatatypes.BnBodypart
        {
            value = BnConstants.BODYPART_ALL_TAG
        };


        BnDatatypes.BnName new_player = new BnDatatypes.BnName();
        new_player.value = "" + PlayerPrefs.GetInt(
                EvoslashCostants.PLAYER_PREFERENCE_PLAYER_NUM,
                EvoslashCostants.PLAYER_PREFERENCE_PLAYER_NUM_DEFAULT);
        BnDatatypes.BnAction player_action = new BnDatatypes.BnAction();
        player_action.createSetPlayer(all_players, all_bodyparts, new_player);
        HostCommunicator.addAction(player_action);
    }

    private void forceBodypartOnNode() {
        BnDatatypes.BnName all_players = new BnDatatypes.BnName
        {
            value = BnConstants.PLAYER_ALL_TAG
        };
        BnDatatypes.BnBodypart all_bodyparts = new BnDatatypes.BnBodypart
        {
            value = BnConstants.BODYPART_ALL_TAG
        };
        BnDatatypes.BnAction bodypart_action = new BnDatatypes.BnAction();
        BnDatatypes.BnBodypart new_bodypart = new BnDatatypes.BnBodypart
        {
            value = BnConstants.BODYPART_KATANA_TAG
        };
        bodypart_action.createSetBodypart(all_players, all_bodyparts, new_bodypart);
        HostCommunicator.addAction(bodypart_action);
    }

    public int getCurrentScore() {
        return mCurrentScore;
    }

    public void newLevel(int level) {
        Debug.Log("New level = "+level);
        BasketRescaler.animateRescale(level);
    }

    private void OnDestroy()
    {
        HostCommunicator.stop();
    }

}
