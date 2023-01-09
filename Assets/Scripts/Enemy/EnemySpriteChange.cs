using System;
using UnityEngine;

/// <summary>�G�̃C���X�g��ύX����</summary>
[RequireComponent(typeof(SpriteRenderer))]
public class EnemySpriteChange : MonoBehaviour
{
    [ElementNames(new string[] { "�n��", "JK", "�C�P����", "���K�l" })]
    [SerializeField, Tooltip("�ʏ�t�@���̃X�v���C�g���Ǘ�����ϐ�")]
    Sprite[] _nomalEnemySprites = default;
    [SerializeField, Tooltip("�{�X��ł̃t�@���̃X�v���C�g���Ǘ�����ϐ�")]
    Sprite _bossEnemy = default;

    [ElementNames(new string[] { "�n��", "JK", "�C�P����", "���K�l", "�ǂQ��", "�ǂQ�j", "�ǂR��", "�ǂR�j", "���~" })]
    [Tooltip("0.�n��, 1.JK, 2.�C�P����, 3.���K�l, 4.��2�j, 5.��2��, 6.��3�j, 7.��3��, 8.���~")]
    [SerializeField, Header("���]�p�C���X�g")]
    Sprite[] _reversalSprites = default;

    /// <summary>sprite�\���p</summary>
    SpriteRenderer _spriteRenderer => GetComponent<SpriteRenderer>();

    /// <summary>�ʏ�t�@���̃C���X�g��؂�ւ���</summary>
    public EnemySprites ChangeNomalEnemySprite()
    {
        var rand = UnityEngine.Random.Range(0, _nomalEnemySprites.Length);
        _spriteRenderer.sprite = _nomalEnemySprites[rand];

        return (EnemySprites)rand;
    }

    /// <summary>�t�@���̃X�v���C�g���{�X��p�ɕύX������֐�</summary>
    public void EnemyBossMethod()
    {
        _spriteRenderer.sprite = _bossEnemy;
    }

    /// <summary>�X�|�[���|�C���g�������ł���΃C���X�g�𔽓]������ </summary>
    public void ReverseSprite(IState state)
    {
        if (state is BossTime)
        {
            _spriteRenderer.sprite = _reversalSprites[(int)EnemySprites.Boss];
            return;
        }

        foreach (EnemySprites type in Enum.GetValues(typeof(EnemySprites)))
        {
            if (ChangeReverseSprite(type))  //���]�����烋�[�v����ʂ���
            {
                break;
            }
        }
    }

    /// <summary>�����̃C���X�g���m�F���A�����Ă���C���X�g�i���]�j�ɐ؂�ւ��� </summary>
    /// <param name="type">�G�̎��</param>
    /// <returns>true=���� false=���s</returns>
    bool ChangeReverseSprite(EnemySprites type)
    {
        if (type.ToString() == _spriteRenderer.sprite.name)
        {
            _spriteRenderer.sprite = _reversalSprites[(int)type];
            return true;
        }

        return false;
    }
}

/// <summary>�S�Ă̓G�̃C���X�g</summary>
public enum EnemySprites
{
    /// <summary>�n���t�@�� </summary>
    Mine = 0,
    /// <summary>JK�t�@��</summary>
    JK = 1,
    /// <summary>�C�P�����t�@�� </summary>
    Handsome = 2,
    /// <summary>���K�l�t�@�� </summary>
    Glasses = 3,
    /// <summary>��2�j </summary>
    Wall2Man = 4,
    /// <summary>��2�� </summary>
    Wall2Woman = 5,
    /// <summary>��3�j </summary>
    Wall3Man = 6,
    /// <summary>��3�� </summary>
    Wall3Woman = 7,
    /// <summary>���~ </summary>
    Boss = 8,
}
