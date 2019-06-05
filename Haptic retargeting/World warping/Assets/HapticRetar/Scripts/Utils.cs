using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public bool debug;
    float deltaTime = 0.0f;

    void Start()
    {
        
    }

    void Update()
    {
        if (debug) {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
            print(1.0f / deltaTime);
        }
    }
}
