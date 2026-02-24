
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMaker : MonoBehaviour
{

    public float noiseVolume = 0.0f;
    public float decayRate = 1.0f;


    // Update is called once per frame
    void Update()
    {

        
    }
   public void makeNoise(float noise) {
        noiseVolume=noise;
    }
}
