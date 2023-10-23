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
        // ボタン入力に応じてシーン遷移
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(0);
        }
    }
}
