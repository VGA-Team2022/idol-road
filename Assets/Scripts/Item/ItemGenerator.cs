using UnityEngine;

/// <summary>�A�C�e���𐶐�����N���X</summary>
public class ItemGenerator : MonoBehaviour
{
    [SerializeField, Header("�E�����ʒu�E�����ʒu")]
    Transform[] _rightPoints = default;
    [SerializeField, Header("�������ʒu�E�����ʒu")]
    Transform[] _leftPoints = default;
    [SerializeField, Header("�����Ԋu")]
    float _generateTime = 5f;
    [SerializeField, Header("�A�C�e����e�I�u�W�F�N�g")]
    Transform _itemParentObj = default;
    [SerializeField, Header("�������v���n�u")] 
    IdolPowerItem _itemPrefab = default;
    [SerializeField, Header("�Q�[���}�l�[�W���[")]
    GameManager _gameManager = default;
    /// <summary>�������ꂽ�A�C�e�� </summary>
    IdolPowerItem _generateItem = default;
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
            _generateItem = Instantiate(_itemPrefab, _rightPoints[0].position, Quaternion.identity);
            _generateItem.GameManager = _gameManager;
            _generateItem.Move(_rightPoints[1].position, SetItemParent);
        }
        else 
        {
            _generateItem = Instantiate(_itemPrefab, _leftPoints[0].position, Quaternion.identity);
            _generateItem.GameManager = _gameManager;
            _generateItem.Move(_leftPoints[1].position, SetItemParent);
        }
    }

    /// <summary>�A�C�e�����X�N���[�������邽�߂ɃX�e�[�W�̎q�I�u�W�F�N�g�ɂ��� </summary>
    void SetItemParent()
    {
        _generateItem.transform.SetParent(_itemParentObj, true);
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
