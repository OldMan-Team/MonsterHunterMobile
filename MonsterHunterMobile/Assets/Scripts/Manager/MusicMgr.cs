//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Events;

//public class MusicMgr : BaseManager<MusicMgr>
//{
//    private float bkValue = 1;      //背景音乐大小
//    private float soundValue = 0.5f;   //音效大小

//    private AudioSource bkMusic;    //唯一背景音乐对象
//    private GameObject soundObj = null; //音效存放对象
//    private List<AudioSource> soundList = new List<AudioSource>();  //音效列表

//    public MusicMgr()
//    {
//        MonoMgr.GetInstance().AddUpdateListener(Update);
//    }
//    private void Update()
//    {
//        //自动清理播放完毕的音效
//        for(int i=soundList.Count-1;i>=0;i--)
//        {
//            if(!soundList[i].isPlaying)
//            {
//                GameObject.Destroy(soundList[i]);
//                soundList.RemoveAt(i);
//            }
//        }
//    }
//    /// <summary>
//    /// 播放背景音乐
//    /// </summary>
//    /// <param name="name"></param>
//    public void PlayBkMusic(string name)
//    {
//        if(bkMusic==null)
//        {
//            GameObject obj = new GameObject("bkMusic");
//            bkMusic = obj.AddComponent<AudioSource>();
//        }
//        ResMgr.GetInstance().LoadAsync<AudioClip>("Music/BK/" + name, (clip) =>
//         {
//             bkMusic.clip = clip;
//             bkMusic.volume = bkValue;
//             bkMusic.loop = true;
//             bkMusic.Play();
//             //Debug.Log("play bkmusic");

//         });
//    }

//    /// <summary>
//    /// 改变背景音乐大小
//    /// </summary>
//    /// <param name="v"></param>
//    public void changeValue(float v)
//    {
//        bkValue = v;
//        if(bkMusic==null)
//        {
//            return;
//        }
//        else
//        {
//            bkMusic.volume = bkValue;
//        }
//    }

//    /// <summary>
//    /// 暂停背景音乐
//    /// </summary>
//    /// <param name="name"></param>
//    public void PauseBkMusic(string name)
//    {
//        if (bkMusic == null)
//        {
//            return;
//        }
//        else
//        {
//            bkMusic.Pause();
//        }
//    }

//    /// <summary>
//    /// 停止背景音乐
//    /// </summary>
//    /// <param name="name"></param>
//    public void StopBkMusic(string name)
//    {
//        if(bkMusic==null)
//        {
//            return;
//        }
//        else
//        {
//            bkMusic.Stop();
//        }
//    }

//    /// <summary>
//    /// 播放音效
//    /// </summary>
//    /// <param name="name"></param>
//    /// <param name="isLoop">是否循环</param>
//    /// <param name="callBack">返回AudioSource用于控制</param>
//    public void PlaySoundMusic(string name,bool isLoop,UnityAction<AudioSource>callBack=null)
//    {
//        if(soundObj==null)
//        {
//            soundObj = new GameObject("Sound");
//        }
//        ResMgr.GetInstance().LoadAsync<AudioClip>("Music/Sound/" + name, (clip) =>
//        {
//            AudioSource source = soundObj.AddComponent<AudioSource>();
//            source.clip = clip;
//            source.volume = soundValue;
//            source.loop = isLoop;
//            source.Play();
//            soundList.Add(source);
//            if (callBack != null)
//                callBack(source);
//        });

//    }

//    /// <summary>
//    /// 停止音效
//    /// </summary>
//    public void StopSoundMusic(AudioSource source)
//    {
//        if(soundList.Contains(source))
//        {
//            soundList.Remove(source);
//            source.Stop();
//            //Debug.Log("stop sound");
//            GameObject.Destroy(source);
//        }
//    }

//    /// <summary>
//    /// 修改音效大小
//    /// </summary>
//    /// <param name="value"></param>
//    public void changeSoundValue(float value)
//    {
//        soundValue = value;
//        foreach(var source in soundList)
//        {
//            source.volume = value;
//        }
//    }

    
//}
