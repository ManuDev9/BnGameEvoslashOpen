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
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManagerScript : MonoBehaviour
{
    private GameObject YouLostCanvas;
    private GameObject ConnectNodeCanvas;
    private GameObject ConnectBodynodesCanvas;
    private GameObject ConnectingCanvas;
    private GameObject ExitCanvas;
    private GameObject NodeFoundCanvas;
    private GameObject LetsStartCanvas;
    private GameObject SettingUpNodeCanvas;
    private bool mUIRunning = false;

    public bool isUIRunning() { 
        return mUIRunning;
    }

    public bool isLostUIActive()
    {
        return YouLostCanvas.activeSelf;
    }

    // Start is called before the first frame update
    void Start()
    {
        YouLostCanvas = transform.Find("YouLostCanvas").gameObject;
        ConnectNodeCanvas = transform.Find("ConnectNodeCanvas").gameObject;
        ConnectBodynodesCanvas = transform.Find("ConnectBodynodesCanvasDev").gameObject;
        ConnectingCanvas = transform.Find("ConnectingCanvas").gameObject;
        NodeFoundCanvas = transform.Find("NodeFoundCanvas").gameObject;
        SettingUpNodeCanvas = transform.Find("SettingUpNodeCanvas").gameObject;
        LetsStartCanvas = transform.Find("LetsStartCanvas").gameObject;
        ExitCanvas = transform.Find("ExitCanvas").gameObject;
        mUIRunning = false;
    }

    private static WaitForSeconds wait3secs = new WaitForSeconds(3);

    IEnumerator letsStartCoroutine()
    {
        yield return wait3secs;
        LetsStartCanvas.SetActive(false);
    }

    public void noNodeConnected()
    {
        YouLostCanvas.SetActive(false);
        ConnectNodeCanvas.SetActive(true);
        ConnectBodynodesCanvas.SetActive(false);
        ConnectingCanvas.SetActive(false);
        NodeFoundCanvas.SetActive(false);
        SettingUpNodeCanvas.SetActive(false);
        LetsStartCanvas.SetActive(false);
        ExitCanvas.SetActive(false);
        mUIRunning = true;
    }

    public void tryToConnectABodynode()
    {
        YouLostCanvas.SetActive(false);
        ConnectNodeCanvas.SetActive(false);
        ConnectBodynodesCanvas.SetActive(true);
        ConnectingCanvas.SetActive(false);
        NodeFoundCanvas.SetActive(false);
        SettingUpNodeCanvas.SetActive(false);
        LetsStartCanvas.SetActive(false);
        ExitCanvas.SetActive(false);
        mUIRunning = true;
    }

    public void connectingUI()
    {
        YouLostCanvas.SetActive(false);
        ConnectNodeCanvas.SetActive(false);
        ConnectBodynodesCanvas.SetActive(false);
        ConnectingCanvas.SetActive(true);
        ConnectingCanvas.GetComponent<ConnectingScript>().startAnimation();
        NodeFoundCanvas.SetActive(false);
        SettingUpNodeCanvas.SetActive(false);
        LetsStartCanvas.SetActive(false);
        ExitCanvas.SetActive(false);
        mUIRunning = true;
    }

    public void nodeFound()
    {
        YouLostCanvas.SetActive(false);
        ConnectNodeCanvas.SetActive(false);
        ConnectBodynodesCanvas.SetActive(false);
        ConnectingCanvas.SetActive(false);
        ConnectingCanvas.GetComponent<ConnectingScript>().stopAnimation();
        NodeFoundCanvas.SetActive(true);
        SettingUpNodeCanvas.SetActive(false);
        LetsStartCanvas.SetActive(false);
        ExitCanvas.SetActive(false);
        mUIRunning = true;
    }

    public void nodeSetup()
    {
        YouLostCanvas.SetActive(false);
        ConnectNodeCanvas.SetActive(false);
        ConnectBodynodesCanvas.SetActive(false);
        ConnectingCanvas.SetActive(false);
        NodeFoundCanvas.SetActive(false);
        SettingUpNodeCanvas.SetActive(true);
        LetsStartCanvas.SetActive(false);
        ExitCanvas.SetActive(false);
        mUIRunning = true;
    }

    public void restartGame()
    {
        YouLostCanvas.SetActive(false);
        ConnectNodeCanvas.SetActive(false);
        ConnectBodynodesCanvas.SetActive(false);
        ConnectingCanvas.SetActive(false);
        ConnectingCanvas.GetComponent<ConnectingScript>().stopAnimation();
        NodeFoundCanvas.SetActive(false);
        SettingUpNodeCanvas.SetActive(false);
        LetsStartCanvas.SetActive(true);
        ExitCanvas.SetActive(false);
        StartCoroutine(letsStartCoroutine());
        mUIRunning = false;
    }

    public void youLost(bool isNewHighScore)
    {
        ConnectNodeCanvas.SetActive(false);
        ConnectBodynodesCanvas.SetActive(false);
        ConnectingCanvas.SetActive(false);
        SettingUpNodeCanvas.SetActive(false);
        NodeFoundCanvas.SetActive(false);
        SettingUpNodeCanvas.SetActive(false);
        LetsStartCanvas.SetActive(false);
        YouLostCanvas.SetActive(true);
        YouLostCanvas.GetComponent<YouLostBehaviour>().setHighScore(isNewHighScore);
        ExitCanvas.SetActive(false);
        mUIRunning = true;
    }

    public void doYouWantToExit() {
        YouLostCanvas.SetActive(false);
        ConnectNodeCanvas.SetActive(false);
        ConnectBodynodesCanvas.SetActive(false);
        ConnectingCanvas.SetActive(false);
        NodeFoundCanvas.SetActive(false);
        SettingUpNodeCanvas.SetActive(false);
        LetsStartCanvas.SetActive(false);
        ExitCanvas.SetActive(true);
        mUIRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

}
