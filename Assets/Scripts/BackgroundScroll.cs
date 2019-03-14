﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour {

    Material material;
    Vector2 offset;

    public float xVelocity, yVelocity;

    private void Awake() {
        material = GetComponent<Renderer>().material;
    }

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        offset = new Vector2(xVelocity, yVelocity);
        material.mainTextureOffset += offset * Time.deltaTime;
	}
}
