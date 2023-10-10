using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimatorPlayer : MonoBehaviour
{
    public Animator ani;
    public void Test()
    {
        this.ani.SetTrigger("Play");
    }
}
