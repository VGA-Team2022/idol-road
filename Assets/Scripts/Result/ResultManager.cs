using UnityEngine;

/// <summary>���U���g�V�[�����Ǘ�����N���X </summary>
public class ResultManager : MonoBehaviour
{
    /// <summary>��ʂɔ��f������v���C���ʂ̐� </summary>
    const int RESULT_COUNT = 4;

    [SerializeField, Header("���U���gUI���X�V����N���X")]
    ResultUIController _resultUIController = default;

    /// <summary>���U���g�֘A�̃p�����[�^�[�̃N���X </summary>
    ResultParameter _resultParameter => LevelManager.Instance.CurrentLevel.Result;
    /// <summary>�v���C���� </summary>
    PlayResult _playResult => PlayResult.Instance;
    /// <summary>���v�X�R�A </summary>
    int _score = 0;
    /// <summary>���v�X�R�A </summary>
    public int Score { get => _score; }

    void Start()
    {
        JudgeResult();
        StartCoroutine(_resultUIController.ShowResult(ScoreCalculation()));
    }

    /// <summary>�X�R�A���v�Z����</summary>
    /// <returns>�v�Z���� 0=bad 1=good 2=perfect 4=score</returns>
    public int[] ScoreCalculation()
    {
        //���肻�ꂼ��̃X�R�A�l�~���肵����
        var perfectValue = _resultParameter._addParfectScoreValue * _playResult.CountPerfect;
        var goodValue = _resultParameter._addGoodScoreValue * _playResult.CountGood;
        var badValue = _resultParameter._addBadScoreValue * _playResult.CountBad;

        _score += perfectValue + goodValue + badValue;  //�X�R�A�v�Z

        var result = new int[RESULT_COUNT] { badValue, goodValue, perfectValue, _score };  //��ʂɔ��f������ׂ̔z��

        return result;
    }

    /// <summary>
    /// �X�R�A�̒l���猋�ʂ𔻒肷��
    /// </summary>
    private void JudgeResult()
    {
        if (LevelManager.Instance.CurrentLevel.InGame.PlayerHp <= _playResult.CountMiss)    //���s
        {
            ResultBad();
        }

        if (_score >= _resultParameter._superPerfectLine)
        {
            ResultSuperPerfect();
        }
        else if (_score >= _resultParameter._perfectLine)
        {
            ResultPerfect();
        }
        else
        {
            ResultGood();
        }
    }

    //Game Over
    public void ResultBad()
    {
        Debug.Log("Bad ��");
        _resultUIController.ChangeResultImage(Result.Bad);
    }
    //����
    public void ResultGood()
    {
        Debug.Log("Good ����");
        _resultUIController.ChangeResultImage(Result.Good);
    }
    //��
    public void ResultPerfect()
    {
        Debug.Log("Perfect �_");
        _resultUIController.ChangeResultImage(Result.Perfect);
    }
    //�_�@�S��
    public void ResultSuperPerfect()
    {
        Debug.Log("SuperPerfect �S�\�_");
        _resultUIController.ChangeResultImage(Result.SuperPerfect);
    }
}

public enum Result
{
    SuperPerfect,
    Perfect,
    Good,
    Bad
}

