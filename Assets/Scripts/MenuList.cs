using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuList : MonoBehaviour
{
    public GameObject _menuList;//菜单列表
    [SerializeField] private bool isShow;
    [SerializeField] private AudioSource _bgmSound;// 背景音乐
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("按下ESC");
            isShow = !isShow;
            if (isShow)
            {
                Time.timeScale = 0f;
                _bgmSound.Pause();//暂停音乐
            }
            else
            {
                Time.timeScale = 1f;
                _bgmSound.Play();//音乐播放
            }
            _menuList.SetActive(isShow);
        }
    }

    public void Return()
    {//返回游戏
        isShow = !isShow;
        Time.timeScale = 1f;
        _bgmSound.Play();//音乐播放
        _menuList.SetActive(isShow);
    }
    public void Restart()
    {//重新开始
        SceneManager.LoadScene(0);
        Time.timeScale= 1f;
    }
    public void Exit()
    {//退出游戏
        Application.Quit();
    }
}
