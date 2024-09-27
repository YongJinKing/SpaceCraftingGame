using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Insert))
        {
            Time.timeScale += 0.2f;
        }
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            Time.timeScale -= 0.2f;
        }
    }
}
