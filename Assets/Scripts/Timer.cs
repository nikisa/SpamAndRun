using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Text timerText;
    private float startTime;
    public bool started = false;
    public bool finished = false;
    public float t;
    
    

    // Update is called once per frame
    void Update() {
        if (started) {
            t -= Time.deltaTime;
        }
        if (finished) {
            return;
        }
        else {
            string seconds = (t % 60).ToString("f2");
            timerText.text = seconds;
        }
    }

    public void StopTimer() {
        finished = true;
        timerText.color = Color.green;
    }
}
