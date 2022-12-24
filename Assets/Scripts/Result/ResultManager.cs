using UnityEngine;

/// <summary>���U���g�V�[�����Ǘ�����N���X </summary>
public class ResultManager : MonoBehaviour
{
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
    /// <summary>�e�X�R�A </summary>
    int[] _scores = default;
   
    public FadeController FadeController { get => _fadeController; }


    public Result CurrentResult { get => _currentResult; set => _currentResult = value; }

    void Start()
    {
        _scores = ScoreCalculation();
        JudgeResult();
        _fadeController.FadeIn(StartShowResultAnim);
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

        totalScore += perfectValue + goodValue + badValue;  //�X�R�A�v�Z

        var result = new int[RESULT_COUNT] { badValue, goodValue, perfectValue, totalScore };  //��ʂɔ��f������ׂ̔z��

        return result;
    }

    /// <summary>���U���g��\������A�j���[�V�������Đ�����</summary>
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
            ResultBad();
        }
    }

    //Game Over
    public void ResultBad()
    {
        _currentResult = Result.Bad;
        _resultUIController.ChangeResultImage(Result.Bad);
        _resultUIController.ReflectFansComment(Result.Bad);
        AudioManager.Instance.PlayVoice(15);
        AudioManager.Instance.PlaySE(31, 0.5f);
    }
    //����
    public void ResultGood()
    {
        _currentResult = Result.Good;
        _resultUIController.ChangeResultImage(Result.Good);
        _resultUIController.ReflectFansComment(Result.Good);
        AudioManager.Instance.PlayVoice(19);
    }
    //��
    public void ResultPerfect()
    {
        _currentResult = Result.Perfect;
        _resultUIController.ChangeResultImage(Result.Perfect);
        _resultUIController.ReflectFansComment(Result.Perfect);
        AudioManager.Instance.PlayVoice(18);
    }
    //�_�@�S��
    public void ResultSuperPerfect()
    {
        _currentResult = Result.SuperPerfect;
        _resultUIController.ChangeResultImage(Result.SuperPerfect);
        _resultUIController.ReflectFansComment(Result.SuperPerfect);
        AudioManager.Instance.PlayVoice(17);
    }
}

public enum Result
{
    SuperPerfect,
    Perfect,
    Good,
    Bad
}

