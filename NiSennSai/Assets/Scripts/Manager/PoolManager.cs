using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

//オブジェクトプール管理
public class PoolManager : MonoBehaviour
{
    //オブジェクトプール内のオブジェクト
    public List<GameObject> poolPrefabs;
    private List<ObjectPool<GameObject>> poolEffectList = new List<ObjectPool<GameObject>>();
    //サウンドスタック
    private Queue<GameObject> soundQueue = new Queue<GameObject>();

    private void OnEnable()
    {
        EventHandler.InitSoundEffect += InitSoundEffect;
    }
    private void OnDisable()
    {
        EventHandler.InitSoundEffect -= InitSoundEffect;
    }

    /// <summary>
    /// 対応するオブジェクトの生成
    /// </summary>
    private void CreateSoundPool()
    {
        var parent = new GameObject(poolPrefabs[0].name).transform;
        parent.SetParent(transform);
        //20+個の空のオブジェクトを設定して
        for (int i = 0; i < 20; i++)
        {
            GameObject newObj = Instantiate(poolPrefabs[0], parent);
            newObj.SetActive(false);
            soundQueue.Enqueue(newObj);
        }
    }
    /// <summary>
    /// オブジェクトコンポーネントの取得
    /// </summary>
    /// <returns></returns>
    private GameObject GetPoolObject()
    {
        if (soundQueue.Count < 2)
            CreateSoundPool();
        return soundQueue.Dequeue();
    }

    /// <summary>
    /// サウンド初期化の設定
    /// </summary>
    /// <param name="soundDetails"></param>
    private void InitSoundEffect(SoundDetails soundDetails)
    {
        var obj = GetPoolObject();
        obj.GetComponent<Sound>().SetSound(soundDetails);
        obj.SetActive(true);
        StartCoroutine(DisableSound(obj, soundDetails.soundClip.length));
    }

    /// <summary>
    /// サウンドをオフにする
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    private IEnumerator DisableSound(GameObject obj,float duration)
    {
        yield return new WaitForSeconds(duration);
        obj.SetActive(false);
        soundQueue.Enqueue(obj);
    }

    /// <summary>
    /// サウンドを再生するサブオブジェクトコードは、再生が必要なときに内容を書くことができます
    /// </summary>
    private void CallMusic()
    {
        //使用するときは適切なサウンド名に変更してください
        EventHandler.CallPlaySoundEvent(SoundName.none);
    }
}
