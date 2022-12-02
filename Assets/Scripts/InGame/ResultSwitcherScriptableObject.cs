using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data",menuName ="ScriptableObjects/ResultSwitcherScriptableObject",order =1)]
public class ResultSwitcherScriptableObject : ScriptableObject
{
    [SerializeField, Tooltip("Excellent(��)�ȏ�Ȃ邽�߂�Bad����̐��̋��e�l")]
    public int _bad_to_Excellent = 0;
    [SerializeField, Tooltip("Perfect(�_)�ɏ��i���邽�߂ɕK�v��Perfect�̐�")]
    public int _perfect_to_Perfect = 10;
    [SerializeField, Tooltip("Good(����)����Excellent(��)�ɏ��i���邽�߂ɕK�v��Perfect�̐�")]
    public int _perfect_to_Excellent = 10;
}
