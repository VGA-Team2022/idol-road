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
    /// �t�F�[�h�p��ImageColor(�A���t�@�l���܂�)���Z�b�g���郁�\�b�h
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
    /// �t�F�[�h�A�E�g�����̃��\�b�h
    /// </summary>
    public IEnumerator FadeOutMethod()
    {
        for (int i = 0; i < 255; i++)
        {
            // �A���t�@�l��deltaTime�Ō��Z�����l����
            _fadeObj.GetComponent<Image>().color = _fadeObj.GetComponent<Image>().color + new Color32(0, 0, 0, 1);

            yield return new WaitForSeconds(0.001f);
        }

        yield return ChangeScene(_buttonFuncs.NextScene);
    }

    /// <summary>
    /// �t�F�[�h�C�������̃��\�b�h
    /// </summary>
    public IEnumerator FadeInMethod()
    {
        for (int i = 0; i < 255 ; i++)
        {
            // �A���t�@�l��deltaTime�Ō��Z�����l����
            _fadeObj.GetComponent<Image>().color = _fadeObj.GetComponent<Image>().color - new Color32(0, 0, 0, 1);

            yield return new WaitForSeconds(0.001f);
        }

            
    }


    /// <summary>
    ///  �V�[���`�F���W�����̃��\�b�h
    /// </summary>
    /// <param name="sceneName">�J�ڂ���V�[����</param>
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
