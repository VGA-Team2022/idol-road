using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayUiCanvas : MonoBehaviour
{
    [SerializeField , Tooltip("遊び方を表示するパネルの親オブジェクト")]
    Transform _pages;

    GameObject[] _playUiPanels;

    //現在開いているページ数
    int _currentPageNumber = 0;

    private void Start()
    {
        int _pageNumber = _pages.childCount;

        _playUiPanels = new GameObject[_pageNumber];

        for (int i = 0; i < _pages.childCount; i++)
        {
            _playUiPanels[i] = _pages.GetChild(i).gameObject;
            _playUiPanels[i].SetActive(false);
        }

        ChangeContents();
    }

    /// <summary>次のページを表示</summary>
    public void NextPage() 
    {
        if (_currentPageNumber < _playUiPanels.Length - 1) 
        {
            DisableContents();
            _currentPageNumber++;
            ChangeContents();
        }
    }

    /// <summary>前のページを表示</summary>
    public void PeachPage() 
    {
        if (_currentPageNumber > 0) 
        {
            DisableContents();
            _currentPageNumber--;
            ChangeContents();
        }
    }

    private void DisableContents() 
    {
        _playUiPanels[_currentPageNumber].gameObject.SetActive(false);
    }

    private void ChangeContents()
    {
        _playUiPanels[_currentPageNumber].gameObject.SetActive(true);
    }
}
