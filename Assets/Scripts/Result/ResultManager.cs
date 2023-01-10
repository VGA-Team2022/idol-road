using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>���U���g�V�[�����Ǘ�����N���X </summary>
public class ResultManager : MonoBehaviour
{
    #region
    /// <summary>��ʂɔ��f������v���C���ʂ̐� </summary>
    const int RESULT_COUNT = 4;
    /// <summary>���v�X�R�A���i�[����Ă���Y���� </summary>
    const int TOTAL_SCORE_INDEX = 3;

    [SerializeField, Header("���U���gUI���X�V����N���X")]
    ResultUIController _resultUIController = default;
    [SerializeField, Header("�t�F�[�h���s���N���X")]
    FadeController _fadeController = default;


    /// <summary>���U���g�֘A�̃p�����[�^�[�̃N���X </summary>
    ResultParameter _resultParameter => LevelManager.Instance.CurrentLevel.Result;
    /// <summary>�v���C���� </summary>
    PlayResult _playResult => PlayResult.Instance;
    /// <summary>�P�v���C�̌���</summary>
    Result _currentResult = Result.Good;
    /// <summary>�e�X�R�A 0=bad 1=good 2=perfect 3=���v�X�R�A</summary>
    int[] _scores = default;
    /// <summary>�Đ����Ă���BGM��ID </summary>
    int _playBgmID = 0;
    /// <summary>�J�ڂ����Ă��邩 true=�J�n���Ă���</summary>
    bool _isTransition = false;


    public FadeController FadeController { get => _fadeController; }

    /// <summary>���� </summary>
    public Result CurrentResult { get => _currentResult; }

    /// <summary>�e�X�R�A 0=bad 1=good 2=perfect 3=���v�X�R�A</summary>
    public int[] Scores { get => _scores; }
    #endregion

    void Start()
    {
        _scores = ScoreCalculation();
        JudgeResult();

        _fadeController.FadeIn(() =>
        {
            _resultUIController.StartCommentAnime();
            PlayIdolVoice();
        });
    }

    /// <summary>�X�R�A���v�Z����</summary>
    /// <returns>�v�Z���� 0=bad 1=good 2=perfect 3=���v�X�R�A</returns>
    public int[] ScoreCalculation()
    {
        //���肻�ꂼ��̃X�R�A�l�~���肵����
        var perfectValue = _resultParameter._addParfectScoreValue * _playResult.CountPerfect;
        var goodValue = _resultParameter._addGoodScoreValue * _playResult.CountGood;
        var badValue = _resultParameter._addBadScoreValue * _playResult.CountBad;
        var totalScore = 0;

        totalScore += perfectValue + goodValue + badValue + _playResult.ValueSuperIdleTimePerfect;  //�X�R�A�v�Z
        var result = new int[RESULT_COUNT] { badValue, goodValue, perfectValue, totalScore };  //��ʂɔ��f������ׂ̔z��

        return result;
    }

    /// <summary>
    /// ���U���g��\������A�j���[�V�������Đ�����
    /// �{�^������Ăяo��
    /// </summary>
    void StartShowResultAnim()
    {
        StartCoroutine(_resultUIController.ShowResult(_scores));
    }

    /// <summary>
    /// �X�R�A�̒l���猋�ʂ𔻒肷��
    /// </summary>
    private void JudgeResult()
    {
        if (LevelManager.Instance.CurrentLevel.InGame.PlayerHp <= _playResult.CountMiss)    //���s
        {
            ResultBad();
            return;
        }

        if (_scores[TOTAL_SCORE_INDEX] >= _resultParameter._superPerfectLine)
        {
            ResultSuperPerfect();
        }
        else if (_scores[TOTAL_SCORE_INDEX] >= _resultParameter._perfectLine)
        {
            ResultPerfect();
        }
        else
        {
            ResultGood();
        }
    }

    /// <summary>���ʂɂ���čĐ�����{�C�X��ύX���� </summary>
    public void PlayIdolVoice()
    {
        switch (_currentResult)
        {
            case Result.Bad:
                AudioManager.Instance.PlayVoice(15);
                AudioManager.Instance.PlayBGM(12);
                _playBgmID = 12;
                break;
            case Result.Good:
                AudioManager.Instance.PlayVoice(19);
                AudioManager.Instance.PlayBGM(13);
                _playBgmID = 13;
                break;
            case Result.Perfect:
                AudioManager.Instance.PlayVoice(18);
                AudioManager.Instance.PlayBGM(13);
                _playBgmID = 13;
                break;
            case Result.SuperPerfect:
                AudioManager.Instance.PlayVoice(17);
                AudioManager.Instance.PlayBGM(13);
                _playBgmID = 13;
                break;
        }
    }


    //Game Over
    public void ResultBad()
    {
        _currentResult = Result.Bad;
        _resultUIController.SetupUI(_currentResult);
    }

    //����
    public void ResultGood()
    {
        _currentResult = Result.Good;
        _resultUIController.SetupUI(_currentResult);

    }

    //��
    public void ResultPerfect()
    {
        _currentResult = Result.Perfect;
        _resultUIController.SetupUI(_currentResult);

    }

    //�_�@�S��
    public void ResultSuperPerfect()
    {
        _currentResult = Result.SuperPerfect;
        _resultUIController.SetupUI(_currentResult);
    }

    /// <summary>
    /// ��Փx�Z���N�g�V�[���ɖ߂邩���g���C���邩
    /// �{�^������Ăяo��
    /// </summary>
    /// <param name="index">�V�[���ԍ�</param>
    public void ReturnModeSelectAndRetry(int index)
    {
        if (_isTransition) { return; }

        _isTransition = true;
        _fadeController.FadeOut(() => { SceneManager.LoadScene(index); });

        if (_currentResult == Result.Bad) //���g���C�p�{�C�X���Đ�����
        {
            AudioManager.Instance.PlayVoice(16);
        }

        AudioManager.Instance.PlaySE(7);
        AudioManager.Instance.StopBGM(_playBgmID);
    }

}

public enum Result
{
    SuperPerfect,
    Perfect,
    Good,
    Bad
}

