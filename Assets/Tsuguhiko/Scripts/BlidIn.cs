using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BlidIn : MonoBehaviour
{
    /// <summary>�t�F�[�h�C�������邽�߂̃t�F�[�h�}�l�[�W���[</summary>
    FadeManager2 _fadeManager2;

    /// <summary>
    /// �t�F�[�h�}�l�[�W���[���擾
    /// </summary>
    void Start()
    {
        _fadeManager2 = GetComponent<FadeManager2>();
    }

    /// <summary>
    /// �t�F�[�h�C����Update�ŉ�
    /// </summary>
    void Update()
    {
        _fadeManager2.FadeInMethod();

        
    }

    
}
