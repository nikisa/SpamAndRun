using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour {

    public bool isDead = false;
    public int points;
    public Text pointsText;
    GameManager _gm;

    private void Awake() {
        _gm = Object.FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }
    
	void Update () {
        foreach (char c in Input.inputString) {
            if (c.ToString() == _gm.getLetter().ToLower() || c.ToString() == _gm.getLetter().ToUpper()) {
                Debug.Log(points++);
                pointsText.text = points.ToString();
            }
            else {
                isDead = true;
            }
        }	
	}
}
