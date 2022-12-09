using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager2 : MonoBehaviour
{
    [SerializeField] GameObject _fadeObj;

    [SerializeField] float _fadeSpeed;

    

    [SerializeField] FadeMode _fadeMode;

    Color _color;

    ButtonFuncs _buttonFuncs;

    //  public GameObject FadeObj { get; private set; }

    //  public float FadeSpeed { get { return _fadeSpeed; } set { _fadeSpeed = value; } }

    public FadeMode Mode { get; set; }
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
        if (_fadeMode == FadeMode.OutMode)
        {
            _fadeObj.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
            StartCoroutine(FadeOutMethod());
        }
        if (_fadeMode == FadeMode.InMode)
        {
            _fadeObj.GetComponent<Image>().color = _fadeObj.GetComponent<Image>().color - new Color32(0, 0, 0, (byte)_fadeSpeed);
            StartCoroutine(FadeInMethod());
        }
       
    }

    /// <summary>
    /// フェードアウト処理のメソッド
    /// </summary>
    public IEnumerator FadeOutMethod()
    {
        for (int i = 0; i < 255; i++)
        {
            // アルファ値にdeltaTimeで減算した値を代入
            _fadeObj.GetComponent<Image>().color = _fadeObj.GetComponent<Image>().color + new Color32(0, 0, 0, 1);

            yield return new WaitForSeconds(0.001f);
        }

        yield return ChangeScene(_buttonFuncs.NextScene);
    }

    /// <summary>
    /// フェードイン処理のメソッド
    /// </summary>
    public IEnumerator FadeInMethod()
    {
        for (int i = 0; i < 255 ; i++)
        {
            // アルファ値にdeltaTimeで減算した値を代入
            _fadeObj.GetComponent<Image>().color = _fadeObj.GetComponent<Image>().color - new Color32(0, 0, 0, 1);

            yield return new WaitForSeconds(0.001f);
        }

            
    }


    /// <summary>
    ///  シーンチェンジ処理のメソッド
    /// </summary>
    /// <param name="sceneName">遷移するシーン名</param>
    public IEnumerator ChangeScene(string sceneName)
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene(sceneName);
    }
}

public enum FadeMode
{
    InMode,
    OutMode
}
