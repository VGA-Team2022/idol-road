using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager2 : MonoBehaviour
{
    [SerializeField] GameObject _fadeObj;

    [SerializeField] float _fadeSpeed;

    Color _color;

    void Start()
    {
        SettingFadeColor();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    FadeOutMethod();
    //}

    /// <summary>
    /// フェード用のImageColor(アルファ値を含む)をセットするメソッド
    /// </summary>
    public void SettingFadeColor()
    {
        _fadeObj.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    }

    /// <summary>
    /// フェードアウト処理のメソッド
    /// </summary>
    public void FadeOutMethod()
    {
        // deltaTimeで徐々に加算
        _fadeSpeed += Time.deltaTime * _fadeSpeed;

        // シリアライズした値を徐々に加算
       // _fadeSpeed++;
       
        // アルファ値に加算した値を代入
        _fadeObj.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, _fadeSpeed);
    }

    /// <summary>
    /// フェードイン処理のメソッド
    /// </summary>
    public void FadeInMethod()
    {
        // deltaTimeで徐々に減算
        _fadeSpeed -= Time.deltaTime;

        // アルファ値にdeltaTimeで減算した値を代入
        _fadeObj.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, _fadeSpeed);
    }

    /// <summary>
    ///  シーンチェンジ処理のメソッド
    /// </summary>
    /// <param name="sceneName">遷移するシーン名</param>
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
