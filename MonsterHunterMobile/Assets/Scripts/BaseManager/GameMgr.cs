using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMgr : SingletonBase<GameMgr>
{
    [SerializeField]
    private GameObject gameSettingPanel;
    [SerializeField]
    private Slider BKMusicSlider;//Slider 对象
    [SerializeField]
    private Slider SoundMusicSlider;
    private Animator panelAnim;

    void Start()
    {
        gameSettingPanel = GameObject.Find("GameSettingPanel");
        BKMusicSlider = GameObject.Find("BKMusicSlider").GetComponent<Slider>();
        SoundMusicSlider = GameObject.Find("SoundMusicSlider").GetComponent<Slider>();
        panelAnim = gameSettingPanel.GetComponent<Animator>();
    }

    public void OpenGameSetting()
    {
        Time.timeScale = 0;
        panelAnim.SetBool("isRise", false);
        panelAnim.SetTrigger("Move");
    }

    public void CloseGameSetting()
    {
        Time.timeScale = 1f;
        panelAnim.SetBool("isRise", true);
    }

    public void OnRestart()//点击“重新开始”时执行此方法
    {
        //Loading Scene0
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }


    //public void BKMusicSetting()
    //{
    //    //Debug.Log(BKMusicSlider.value / 1);
    //    MusicMgr.GetInstance().changeValue(BKMusicSlider.value / 1);
    //}

    //public void SoundMusicSetting()
    //{
    //    //Debug.Log(SoundMusicSlider.value / 1);
    //    MusicMgr.GetInstance().changeSoundValue(SoundMusicSlider.value / 1);
    //}


    /// <summary>
    /// 游戏结束
    /// </summary>
    public void GameOver()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#else
         Application.Quit();
#endif

    }

}
