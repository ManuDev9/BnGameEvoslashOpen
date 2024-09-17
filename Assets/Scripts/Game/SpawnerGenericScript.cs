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
public class SpawnerGenericScript : MonoBehaviour
{

    public float RotationMin = -500f;
    public float RotationMax = 500f;

    public float ForceXmin = -250f;
    public float ForceXmax = 250f;
    public float ForceYmin = 300f;
    public float ForceYmax = 400f;
    public float ForceZmin = -1500f;
    public float ForceZmax = -1200f;

    // Difficulty parameters
    private long mSpawnInterval_MS = 2000;
    private float mPossibleReduceDimensionsLimit = 1.0f;

    public void setSpawnInternvalMs(long val) {
        if( val < (long)(Time.deltaTime*1000))
        {
            Debug.Log("Reached the timing limit for this app");
            return;
        }
        mSpawnInterval_MS = val;
    }
    public void setPossibleReduceDimensionsLimit(float val)
    {
        if (val <= 0) {
            Debug.Log("Not a valid reduce dimension");
            return;
        }
        if (val > 1.0)
        {
            Debug.Log("Not a valid reduce dimension");
            return;
        }
        mPossibleReduceDimensionsLimit = val;
    }

    private bool mSpawn = false;

    private List<GameObject> mSpawnableObjs;

    private float mLastTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        mSpawn = true;
        mSpawnableObjs = new List<GameObject>();
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform childTransform = transform.GetChild(i);
            mSpawnableObjs.Add(childTransform.gameObject);
        }

        foreach (GameObject obj in mSpawnableObjs)
        {
            Debug.Log("Spawnable obj name = " + obj.name);
        }
        mLastTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (mSpawn)
        {
            if (Time.fixedTime - mLastTime > mSpawnInterval_MS/1000.0f)
            {
                mLastTime = Time.fixedTime;
                int objIndex = UnityEngine.Random.Range(0, mSpawnableObjs.Count);
                GameObject go = Instantiate(mSpawnableObjs[objIndex], transform.position, mSpawnableObjs[objIndex].transform.rotation);

                if (go != null)
                {
                    go.SetActive(true);

                    float rescale = UnityEngine.Random.Range(mPossibleReduceDimensionsLimit, 1.0f);
                    Vector3 scaleFactor = new Vector3(rescale, rescale, rescale);
                    go.transform.localScale = Vector3.Scale(go.transform.localScale, scaleFactor);

                    Rigidbody rb = go.AddComponent<Rigidbody>();
                    rb.useGravity = true;
                    float xdiff = UnityEngine.Random.Range(ForceXmin, ForceXmax);
                    float ydiff = UnityEngine.Random.Range(ForceYmin, ForceYmax);
                    float zdiff = UnityEngine.Random.Range(ForceZmin, ForceZmax);
                    rb.AddForce(new Vector3(xdiff, ydiff, zdiff));

                    float xtor = UnityEngine.Random.Range(RotationMin, RotationMin);
                    float ytor = UnityEngine.Random.Range(RotationMin, RotationMin);
                    float ztor = UnityEngine.Random.Range(RotationMin, RotationMin);
                    rb.AddTorque(new Vector3(xtor, ytor, ztor));
                }
            }
        }
    }

    private static WaitForSeconds wait3secs = new WaitForSeconds(3);
    // Start the spawning after a few seconds
    public void startSpawning()
    {
        StartCoroutine(startSpawningCoroutine());
    }

    IEnumerator startSpawningCoroutine()
    {
        yield return wait3secs;
        mSpawn = true;
    }

    public void stopSpawning()
    {
        mSpawn = false;
    }

}
