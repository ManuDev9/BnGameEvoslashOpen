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
public class SwordCutBehaviour : MonoBehaviour
{
    public GameManagerScript GameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Floor") {
            Debug.Log("Not cutting the floor");
            return;
        }
        Debug.Log("Name of object to be cut is: "+ other.gameObject.name);
        GameManager.swordCutThisThing(other.gameObject);
        Material capMaterial = other.gameObject.GetComponent<Renderer>().material;
        // Apparently Cut changes the other object and make it as "left side"
        GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(other.gameObject, transform.position, transform.up, capMaterial);

        if (!pieces[0].GetComponent<Rigidbody>())
            pieces[0].AddComponent<Rigidbody>();

        if (!pieces[1].GetComponent<Rigidbody>())
            pieces[1].AddComponent<Rigidbody>();

        pieces[0].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        pieces[1].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

        pieces[0].GetComponent<Rigidbody>().AddForce(new Vector3(-100, 10, 0));
        pieces[1].GetComponent<Rigidbody>().AddForce(new Vector3(100, -10, 0));

        pieces[0].GetComponent<Rigidbody>().useGravity = true;
        pieces[1].GetComponent<Rigidbody>().useGravity = true;

        pieces[0].AddComponent<AutodestroyScript>().start();
        pieces[1].AddComponent<AutodestroyScript>().start();

        //GetComponent<AudioSource>().Play();
    }

}
