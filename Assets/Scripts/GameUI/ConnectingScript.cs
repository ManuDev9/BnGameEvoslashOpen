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
using UnityEngine.UI;

public class ConnectingScript : MonoBehaviour
{

    public Microlight.MicroBar.MicroBar ProgressBar;
    public GameUIManagerScript GameUIManager;
    public Text GameNameText;

    public float TotTimeToWaitInSeconds = 10;
    private float mProgressBarValue;
    private bool mIsRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        mProgressBarValue = TotTimeToWaitInSeconds * 10;
        ProgressBar.Initialize((int)mProgressBarValue);
    }

    // Update is called once per frame
    void Update()
    {
        if (mIsRunning) { 
            mProgressBarValue -= Time.deltaTime*10;
            ProgressBar.UpdateHealthBar((int)mProgressBarValue);
            if (mProgressBarValue <= 0) {
                GameUIManager.noNodeConnected();
                mIsRunning = false;
            }
        }
    }

    public void startAnimation() {
        GameNameText.text = PlayerPrefs.GetString(EvoslashCostants.PLAYER_PREFERENCE_GAME_NAME, "TEST");
        mProgressBarValue = TotTimeToWaitInSeconds * 10;
        ProgressBar.UpdateHealthBar((int)mProgressBarValue);
        mIsRunning = true;
    }
    public void stopAnimation()
    {
        mIsRunning = false;
    }
}
