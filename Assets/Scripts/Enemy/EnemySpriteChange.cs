using UnityEngine;

/// <summary>�G�̃C���X�g��ύX����</summary>
[RequireComponent(typeof(SpriteRenderer))]
public class EnemySpriteChange : MonoBehaviour
{
    [ElementNames(new string[] {"�n��", "JK", "�C�P����", "���K�l"})]
    [SerializeField, Tooltip("�ʏ�t�@���̃X�v���C�g���Ǘ�����ϐ�")]
    Sprite[] _nomalEnemySprites = default;
    [SerializeField, Tooltip("�{�X��ł̃t�@���̃X�v���C�g���Ǘ�����ϐ�")]
    Sprite _bossEnemy = default;

    /// <summary>sprite�\���p</summary>
    SpriteRenderer _spriteRenderer => GetComponent<SpriteRenderer>();

    /// <summary>�ʏ�t�@���̃C���X�g��؂�ւ���</summary>
    public EnemyNomalSprites ChangeNomalEnemySprite()
    {
        var rand = Random.Range(0, _nomalEnemySprites.Length);
        _spriteRenderer.sprite = _nomalEnemySprites[rand];

        return (EnemyNomalSprites)rand;
    }

    /// <summary>�t�@���̃X�v���C�g���{�X��p�ɕύX������֐�</summary>
    public void EnemyBossMethod()
    {
        _spriteRenderer.sprite = _bossEnemy;
    }
}

/// <summary>�G�̃C���X�g(�ʏ�t�@��)</summary>
public enum EnemyNomalSprites
{
    /// <summary>�n���t�@�� </summary>
    Mine = 0,
    /// <summary>JK�t�@��</summary>
    JK = 1,
    /// <summary>�C�P�����t�@�� </summary>
    Handsome = 2,
    /// <summary>���K�l�t�@�� </summary>
    Glasses = 3,
}
