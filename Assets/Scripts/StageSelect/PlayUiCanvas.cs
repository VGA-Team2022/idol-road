using UnityEngine;
using UnityEngine.UI;

public class PlayUiCanvas : MonoBehaviour
{
    [SerializeField, Tooltip("�V�ѕ���\������p�l���̐e�I�u�W�F�N�g")]
    Transform _pages;

    [ElementNames(new string[] {"1����", "2����", "3����", "4����" })]
    [SerializeField, Tooltip("���݂̃y�[�W��\���I�u�W�F�N�g")]
    Image[] _pageCountImages = default;

    GameObject[] _playUiPanels;

    Image _currentPageCountImage = default;
    //���݊J���Ă���y�[�W��
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
        _currentPageCountImage = _pageCountImages[0];   //���݂̃y�[�W���ꖇ�ڂɕύX����
        _currentPageCountImage.color = Color.yellow;
    }

    /// <summary>���̃y�[�W��\��</summary>
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

    /// <summary>�O�̃y�[�W��\��</summary>
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
