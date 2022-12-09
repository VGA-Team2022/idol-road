using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleManager : MonoBehaviour
{
    [SerializeField] 
    FadeController _fadeController = default;
    [SerializeField]
    string _sceneName = "";

    void Start()
    {
        _fadeController.gameObject.SetActive(false);
    }
    public void GameSceneState()
    {
        _fadeController.FadeIn(() => SceneManager.LoadScene(_sceneName));
    }
}
