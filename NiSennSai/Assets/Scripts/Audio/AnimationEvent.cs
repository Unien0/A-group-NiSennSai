using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    //メソッド名の前に対応するアニメーション再生コンテンツを付けることができます
    //例：アニメ名+AnimationSoundPlay
    //その後、対応する（SoundName.サウンド名）を調整するだけで、アニメーションに対応するサウンドを再生することができます
    public void AnimationSoundPlay()
    {
        //ここで再生されるサウンドをアニメーションに合わせて調整します（注：AnimationEventでトリガーされます）
        EventHandler.CallPlaySoundEvent(SoundName.none);
    }

}
