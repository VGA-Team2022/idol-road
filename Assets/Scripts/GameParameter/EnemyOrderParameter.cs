using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>�t�@���̐������Ԃ�ݒ肷��N���X</summary>
[CreateAssetMenu]
public class EnemyOrderParameter : ScriptableObject
{
    [SerializeField, Header("��������")]
    List<EnemyInfo> _enemyOrder = new List<EnemyInfo>();

    /// <summary>��������</summary>
    public List<EnemyInfo> EnemyOrder { get => _enemyOrder; }
}

/// <summary>��������G�̏���ێ�����N���X</summary>
[Serializable]
public class EnemyInfo
{
    [SerializeField, Header("�G�̎��")]
    public EnemyType _enemyType = EnemyType.Nomal;
    [SerializeField, Header("�����Ԋu"), Tooltip("1=7.68�b, 2=5.76�b, 3=3.84�b, 4=1.92�b")]
    public GenerateInterval _generateInterval = GenerateInterval.Interval1;
    [SerializeField, Header("�t�@���T�v��")]
    public List<RequestType> requestTypes = new List<RequestType>();
}

/// <summary>�t�@���̎�� </summary>
public enum EnemyType
{
    /// <summary>�ʏ� </summary>
    Nomal = 0,
    /// <summary>�ǃt�@�� �t�@���T�� 2 </summary>
    Wall2 = 1,
    /// <summary>�ǃt�@�� �t�@���T�� 3 </summary>
    Wall3 = 2,
    /// <summary>�{�X </summary>
    Boss = 3,
    /// <summary>�G�Ȃ�</summary>
    Wait = 4,
}

/// <summary>�v���̎�� </summary>
public enum RequestType
{
    Random = 0,

    Pose = 1,
    Kiss = 2,
    Wink = 3,
    Sign = 4,
}

/// <summary>�����Ԋu</summary>
public enum GenerateInterval
{
    /// <summary>7.68f </summary>
    Interval1 = 0,
    /// <summary>5.76f </summary>
    Interval2 = 1,
    /// <summary>3.84f </summary>
    Interval3 = 2,
    /// <summary>1.92f </summary>
    Interval4 = 3,
}


