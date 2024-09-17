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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketRescalerScript : MonoBehaviour
{

    public float RescaleDuration = 0.3f;

    private Vector3 mInitialScale;
    private Vector3 mTargetScale;
    private Coroutine mScaleCoroutine;
    private int mCurrentLevel = -1;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void animateRescale(int level) {
        if (!EvoslashCostants.CheckLevel(level, mCurrentLevel))
        {
            return;
        }
        mCurrentLevel = level;
        mInitialScale = transform.localScale;
        float scaleVal = EvoslashCostants.BasketSizes[mCurrentLevel] * 1.3f;
        mTargetScale = new Vector3(scaleVal, scaleVal, scaleVal);
        if (mScaleCoroutine != null)
        {
            StopCoroutine(mScaleCoroutine);
        }
        mScaleCoroutine = StartCoroutine(ScaleOverTimeBig());
    }

    IEnumerator ScaleOverTimeBig()
    {
        float timer = 0f;
        while (timer < RescaleDuration)
        {
            float scaleFactor = timer / RescaleDuration;
            transform.localScale = Vector3.Lerp(mInitialScale, mTargetScale, scaleFactor);
            timer += Time.deltaTime;
            // It essentially waits for the next frame update before continuing execution of the coroutine.
            yield return null;
        }
        transform.localScale = mTargetScale;
        mInitialScale = mTargetScale;
        float scaleVal = EvoslashCostants.BasketSizes[mCurrentLevel];
        mTargetScale = new Vector3(scaleVal, scaleVal, scaleVal);
        mScaleCoroutine = StartCoroutine(ScaleOverTimeNormal());
    }

    IEnumerator ScaleOverTimeNormal()
    {
        float timer = 0f;
        while (timer < RescaleDuration)
        {
            float scaleFactor = timer / RescaleDuration;
            transform.localScale = Vector3.Lerp(mInitialScale, mTargetScale, scaleFactor);
            timer += Time.deltaTime;
            // It essentially waits for the next frame update before continuing execution of the coroutine.
            yield return null;
        }
        transform.localScale = mTargetScale;
        mScaleCoroutine = null;
    }
}
