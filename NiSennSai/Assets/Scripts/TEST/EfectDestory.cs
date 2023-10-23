using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectDestory : MonoBehaviour
{
    private float DestoryNowTime;
    [SerializeField]private float DestoryTime;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DestoryNowTime += Time.deltaTime;
        if (DestoryNowTime >= DestoryTime)
        {
            Destroy(this.gameObject);
        }
    }
}
