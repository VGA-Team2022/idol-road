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
    /// �t�F�[�h�p��ImageColor(�A���t�@�l���܂�)���Z�b�g���郁�\�b�h
    /// </summary>
    public void SettingFadeColor()
    {
        _fadeObj.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    }

    /// <summary>
    /// �t�F�[�h�A�E�g�����̃��\�b�h
    /// </summary>
    public void FadeOutMethod()
    {
        // deltaTime�ŏ��X�ɉ��Z
        _fadeSpeed += Time.deltaTime * _fadeSpeed;

        // �V���A���C�Y�����l�����X�ɉ��Z
       // _fadeSpeed++;
       
        // �A���t�@�l�ɉ��Z�����l����
        _fadeObj.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, _fadeSpeed);
    }

    /// <summary>
    /// �t�F�[�h�C�������̃��\�b�h
    /// </summary>
    public void FadeInMethod()
    {
        // deltaTime�ŏ��X�Ɍ��Z
        _fadeSpeed -= Time.deltaTime;

        // �A���t�@�l��deltaTime�Ō��Z�����l����
        _fadeObj.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, _fadeSpeed);
    }

    /// <summary>
    ///  �V�[���`�F���W�����̃��\�b�h
    /// </summary>
    /// <param name="sceneName">�J�ڂ���V�[����</param>
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
