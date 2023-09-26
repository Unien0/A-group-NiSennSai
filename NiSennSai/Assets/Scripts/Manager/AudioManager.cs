using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class AudioManager : Singleton<AudioManager>
{
    [Header("音楽データベース")]
    public SoundDetailsList_SO soundDetailsData;
    public SceneSoundList_SO sceneSoundData;
    [Header("Audio Source")]
    public AudioSource ambientSource;
    public AudioSource gameSource;

    //ランダム再生時間
    //public float MusicStartSecond => Random.Range(5f,15f);
    //private Coroutine soundRoutine;

    [Header("Audio Mixer")]
    public AudioMixer audioMixer;

    //AudioMixerの状態
    [Header("Snapshots")]
    public AudioMixerSnapshot normalSnapShot;
    public AudioMixerSnapshot muteSnapShot;

    //音楽オーバータイムの設定
    private float musicTransitionSecond = 3f;


    private void OnEnable()
    {
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
        EventHandler.PlaySoundEvent += OnPlaySoundEvent;
    }

    private void OnDisable()
    {
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
        EventHandler.PlaySoundEvent -= OnPlaySoundEvent;
    }

    /// <summary>
    /// 再生イベントの設定
    /// </summary>
    /// <param name="soundName"></param>
    private void OnPlaySoundEvent(SoundName soundName)
    {
        var soundDetails = soundDetailsData.GetSoundDetails(soundName);
        if (soundDetails != null)
            EventHandler.CallInitSoundEffect(soundDetails);
    }

    /// <summary>
    /// シーンBGM再生イベントの設定
    /// </summary>
    private void OnAfterSceneLoadedEvent()
    {
        //関連シーンが開始されたとき
        string currentScene = SceneManager.GetActiveScene().name;

        //対応するシーン音楽の取得
        SceneSoundItem sceneSound = sceneSoundData.GetSceneSoundItem(currentScene);

        //むだ出し防止
        if (sceneSound == null)
            return;

        //対応する環境音とBGMを読み込む
        SoundDetails ambient = soundDetailsData.GetSoundDetails(sceneSound.ambient);
        SoundDetails music = soundDetailsData.GetSoundDetails(sceneSound.music);

        //音楽インボリュート効果の設定
        PlayerAmbientClip(ambient,0.5f);
        PlayerMusicClip(music,musicTransitionSecond);

        ////遅延再生関連（今要らないけど削除しない
        //if (soundRoutine != null)
        //{
        //    StopCoroutine(soundRoutine);
        //}
        //else
        //{
        //    soundRoutine = StartCoroutine(PlaySoundRoutine(music, ambient));
        //}
    }

    /// <summary>
    /// BGMを再生
    /// </summary>
    /// <param name="soundDetails"></param>
    private void PlayerMusicClip(SoundDetails soundDetails , float transitionTime)
    {
        //AudioMixerの対応するトラックを取得する
        audioMixer.SetFloat("MusicVolume", ConertSoundVolume(soundDetails.soundVolume));
        gameSource.clip = soundDetails.soundClip;
        if (gameSource.isActiveAndEnabled)
            gameSource.Play();

        normalSnapShot.TransitionTo(transitionTime);
    }

    /// <summary>
    /// 環境音を再生する
    /// </summary>
    /// <param name="soundDetails"></param>
    private void PlayerAmbientClip(SoundDetails soundDetails, float transitionTime)
    {
        audioMixer.SetFloat("AmbientVolume", ConertSoundVolume(soundDetails.soundVolume));
        ambientSource.clip = soundDetails.soundClip;
        if (gameSource.isActiveAndEnabled)
            gameSource.Play();

        normalSnapShot.TransitionTo(transitionTime);
    }

    //遅延再生
    //private IEnumerator PlaySoundRoutine(SoundDetails music, SoundDetails ambient)
    //{
    //    if (music != null && ambient != null)
    //    {
    //        PlayerAmbientClip(ambient);
    //        yield return new WaitForSeconds(MusicStartSecond);
    //        PlayerMusicClip(music);
    //    }
    //}

    //音量を(-80,20)の範囲区間に調整する
    private float ConertSoundVolume(float amount)
    {
        return (amount * 100 - 80);
    }
}
