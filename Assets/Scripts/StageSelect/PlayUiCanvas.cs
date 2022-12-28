using UnityEngine;
using UnityEngine.UI;

public class PlayUiCanvas : MonoBehaviour
{
    [SerializeField, Tooltip("遊び方を表示するパネルの親オブジェクト")]
    Transform _pages;

    [ElementNames(new string[] {"1枚目", "2枚目", "3枚目", "4枚目" })]
    [SerializeField, Tooltip("現在のページを表すオブジェクト")]
    Image[] _pageCountImages = default;

    GameObject[] _playUiPanels;

    Image _currentPageCountImage = default;
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
        _currentPageCountImage = _pageCountImages[0];   //現在のページを一枚目に変更する
        _currentPageCountImage.color = Color.yellow;
    }

    /// <summary>次のページを表示</summary>
    public void NextPage()
    {
        if (_currentPageNumber < _playUiPanels.Length - 1)
        {
            _pageCountImages[_currentPageNumber].color = Color.white;
            DisableContents();
            _currentPageNumber++;
            ChangeContents();
            _pageCountImages[_currentPageNumber].color = Color.yellow;
        }
    }

    /// <summary>前のページを表示</summary>
    public void PeachPage()
    {
        if (_currentPageNumber > 0)
        {
            _pageCountImages[_currentPageNumber].color = Color.white;
            DisableContents();
            _currentPageNumber--;
            ChangeContents();
            _pageCountImages[_currentPageNumber].color = Color.yellow;
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
