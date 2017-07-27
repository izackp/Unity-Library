using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSText : MonoBehaviour {
    float _totalDelta = 0.0f;
    Text _lblFPS;
    float[] _deltaTimes = new float[60];
    int index = 0;
    
    void Start () {
        _lblFPS = GetComponent<Text>();
	}
	
	void Update () {
        float oldDelta = _deltaTimes[index];
        float newDelta = Time.deltaTime;
        _deltaTimes[index] = newDelta;
        _totalDelta += newDelta;
        _totalDelta -= oldDelta;
        index += 1;
        if (index >= 60)
            index = 0;
        
        float deltaTime = _totalDelta / 60;
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        _lblFPS.text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
    }
    
}
