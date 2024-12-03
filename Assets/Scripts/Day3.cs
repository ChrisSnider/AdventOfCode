using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Day3 : MonoBehaviour
{

    string rawString;

    // Start is called before the first frame update
    void Start()
    {
        foreach (char letter in rawString)
        {

        }    
    }

    int leftPointer = 0;
    

    int? CheckAndCalculate(string rawString, int leftPointer)
    {
        int charCounter = 0;

        string targetString = "mul(";

        for (int i = 0; i < targetString.Count(); i++)
        {
            if (rawString[leftPointer + i] != targetString[i])
            {
                charCounter += 1;
            }
        }

        while ()
        



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
