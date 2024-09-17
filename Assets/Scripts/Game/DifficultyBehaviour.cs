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

public class DifficultyBehaviour : MonoBehaviour
{

    public SpawnerGenericScript Spawner;
    public GameObject SkyDome;
    public GameManagerScript GameManager;
    public int MaxScoreForSky = 10000;

    private int mCurrentLevel = -1;
    private bool mLevelForced = false;

    // Start is called before the first frame update
    void Start()
    {
        mCurrentLevel = -1;
        mLevelForced = false;
        if (PlayerPrefs.GetInt(EvoslashCostants.PLAYER_PREFERENCE_HARDCORE_MODE) == 1)
        {
            forceLevel( EvoslashCostants.DifficultyScoreLimits.Length-1 );
        }
    }

    private void forceLevel(int level) {
        if (!EvoslashCostants.CheckLevel(level, mCurrentLevel))
        {
            return;
        }
        mLevelForced = true;
        setLevel(level);
    }

    private void setLevel(int level) {
        if (!EvoslashCostants.CheckLevel(level, mCurrentLevel))
        {
            return;
        }
        mCurrentLevel = level;
        Spawner.setSpawnInternvalMs(EvoslashCostants.DifficultySpawnTimesMs[level]);
        Spawner.setPossibleReduceDimensionsLimit(EvoslashCostants.DifficultyRedimensions[level]);
        GameManager.newLevel(level);
    }

    // Update is called once per frame
    void Update()
    {
        int currentScore = GameManager.getCurrentScore();
        // I will try to change the sky to get darker as the number of points increases
        float offsetX = (float)(currentScore) / (float)MaxScoreForSky;
        if (offsetX > 1.0f)
        {
            offsetX = 1.0f;
        }
        else if (offsetX < 0.0f)
        {
            offsetX = 0.0f;
        }
        // I am multiplying the offsetX by 0.5 because night is in the middle of 0 and 1
        // Still I like the idea of computing an offset between 0 and 1.
        SkyDome.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offsetX*0.5f, 0);

        // If level was forced, we won't change it
        if (mLevelForced) {
            return;
        }

        for( int levelIndex = EvoslashCostants.DifficultyScoreLimits.Length - 1; levelIndex > 0; --levelIndex)
        {
            if (currentScore >= EvoslashCostants.DifficultyScoreLimits[levelIndex])
            {
                setLevel(levelIndex);
                break;
            }
        }

        // You can force a level from here, example
        // setLevel(3);


    }
}
