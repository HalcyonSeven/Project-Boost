using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;
    float movementFactor;
    Vector3 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        GenerateOscillation();
    }

    private void GenerateOscillation()
    {
        // smallest point floating point value, 
        // used instead of zero to compare two floats
        if (period <= Mathf.Epsilon)
        {
            return;
        }
        // increasing with time
        float cycles = Time.time / period;
        // tau is a constant = 2PI rads
        const float tau = Mathf.PI * 2;
        // generates sin wave range  -1 to 1
        float rawSinWave = Mathf.Sin(cycles * tau);
        // recalculated to 0 to 1
        movementFactor = (rawSinWave + 1f) / 2f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
