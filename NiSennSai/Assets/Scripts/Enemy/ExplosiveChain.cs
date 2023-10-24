using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveChain : MonoBehaviour
{
    public GameObject Explosive;
        public void Chain()
    {
        Explosive.SetActive(true);
    }
}
