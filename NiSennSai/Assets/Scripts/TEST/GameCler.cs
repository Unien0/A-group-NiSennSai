using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �{�^�����͂ɉ����ăV�[���J��
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(0);
        }
    }
}
