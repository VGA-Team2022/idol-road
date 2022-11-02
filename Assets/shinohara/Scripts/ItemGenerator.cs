using UnityEngine;

/// <summary>�A�C�e���𐶐�����N���X</summary>
public class ItemGenerator : MonoBehaviour
{
    [SerializeField, Header("�E�����ʒu�E�����ʒu")]
    Transform[] _rightPoints = default;
    [SerializeField, Header("�������ʒu�E�����ʒu")]
    Transform[] _leftPoints = default;
    [SerializeField, Header("�������v���n�u")] 
    IdolPowerItem _itemPrefab = default;
    [SerializeField, Header("�����Ԋu")]
    float _generateTime = 5f;

    float _time = 0f;
    /// <summary>���������邩�ǂ��� </summary>
    bool _isGenerate = false;

    private void Update()
    {
        if (_isGenerate)
        {
            _time += Time.deltaTime;

            if (_generateTime <= _time)
            {
                Generate();
                _time = 0f;
            }
        }
    }

    /// <summary>�A�C�e���𐶐����� </summary>
    void Generate()
    {
        var direction = (GenerateDirection)Random.Range(0, 2);

        if (direction == GenerateDirection.Right)
        {
            var item = Instantiate(_itemPrefab, _rightPoints[0].position, Quaternion.identity);
            item.Move(_rightPoints[1].position);
        }
        else 
        {
            var item = Instantiate(_itemPrefab, _leftPoints[0].position, Quaternion.identity);
            item.Move(_leftPoints[1].position);
        }
    }

    /// <summary>�W�F�l���[�^�[���N���A��~������֐� </summary>
    public void GeneratorOperation()
    {
        _isGenerate = !_isGenerate;

        if (!_isGenerate)
        {
            _time = 0f;
        }
    }

    public enum GenerateDirection
    {
        /// <summary>�E�ɐ��� </summary>
        Right = 0,
        /// <summary>���ɐ��� </summary>
        Left = 1,
    }
}
