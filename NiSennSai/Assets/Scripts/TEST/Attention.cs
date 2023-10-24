using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Attention : MonoBehaviour
{
    private float InputTime;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            InputTime += Time.deltaTime;
        }

        if(InputTime > 2)
        {
            SceneManager.LoadScene(2);
        }
    } 
}
