using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChange : MonoBehaviour
{
    [SerializeField, Tooltip("�ʏ�t�@���̃X�v���C�g���Ǘ�����ϐ�")]
    Sprite[]_normalEnemys = default;
    [Tooltip("�t�@���������_���ɐ��������邽�߂̕ϐ�")]
    int _rand = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>�G�̃X�v���C�g�������_���ɕύX������֐�</summary>
    public void EnemyRandomMethod()
    {
        _rand = Random.Range(0, _normalEnemys.Length);
        GetComponent<SpriteRenderer>().sprite = _normalEnemys[_rand];
        Debug.Log(_normalEnemys[_rand]);
    }
}
