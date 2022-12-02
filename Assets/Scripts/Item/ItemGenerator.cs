using UnityEngine;

/// <summary>�A�C�e���𐶐�����N���X</summary>
public class ItemGenerator : MonoBehaviour
{
    [Tooltip("�l��������΍����قǏo�����܂�")]
    [SerializeField, Header("�e�A�C�e���̏o���m��"), ElementNames(new string[] { "�ʂ������", "�ԑ�", "�v���[���g", "����" }), Range(0f, 10f)]
    float[] _itemWeights = default;
    [SerializeField, Header("�����Ԋu")]
    float _generateTime = 5f;
    [SerializeField, Header("�E�����ʒu�E�����ʒu")]
    Transform[] _rightPoints = default;
    [SerializeField, Header("�������ʒu�E�����ʒu")]
    Transform[] _leftPoints = default;
    [SerializeField, Header("�Q�[���}�l�[�W���[")]
    GameManager _gameManager = default;
    [SerializeField, Header("��������A�C�e��"), ElementNames(new string[] { "�ʂ������", "�ԑ�", "�v���[���g", "����" })]
    IdolPowerItem[] _items = default;

    /// <summary>�d�݂̑��a </summary>
    float _totalWeight = 0f;

    float _timer = 0f;
    /// <summary>���������邩�ǂ��� </summary>
    bool _isGenerate = false;

    private void Start()
    {   
        foreach (var weight in _itemWeights)    //�d�݂̑��a���v�Z����
        {
            _totalWeight += weight;
        }
    }

    private void Update()
    {
        if (_isGenerate)
        {
            _timer += Time.deltaTime;

            if (_generateTime <= _timer)
            {
                Generate();
                _timer = 0f;
            }
        }
    }

    /// <summary>�A�C�e���𐶐����� </summary>
    void Generate()
    {
        var direction = (GenerateDirection)Random.Range(0, System.Enum.GetNames(typeof(GenerateDirection)).Length);

        if (direction == GenerateDirection.Right)
        {
            var item = Instantiate(_items[GetSelectItemIndex()], _rightPoints[0].position, Quaternion.identity);
            item.GameManager = _gameManager;
            item.Move(_rightPoints[1].position);
        }
        else
        {
            var item = Instantiate(_items[GetSelectItemIndex()], _leftPoints[0].position, Quaternion.identity);
            item.GameManager = _gameManager;
            item.Move(_leftPoints[1].position);
        }

        AudioManager.Instance.PlaySE(15, 0.5f);
    }

    /// <summary>��������A�C�e���̓Y�������擾���� </summary>
    /// <returns>��������A�C�e���̓Y����</returns>
    int GetSelectItemIndex()
    {
        var rand = Random.Range(0, _totalWeight); 
        var currnetWeight = 0f;     //���݂̏d��

        for (var i = 0; i < _itemWeights.Length; i++)   // �����l��������v�f��擪���珇�ɑI��
        {
            currnetWeight += _itemWeights[i];   // ���ݗv�f�܂ł̏d�݂̑��a�����߂�

            if (rand < currnetWeight)   // �����l�����ݗv�f�͈͓̔����`�F�b�N
            {
                return i;
            }
        }

        return _itemWeights.Length - 1;     // �����l���d�݂̑��a�ȏ�Ȃ疖���v�f�Ƃ���
    }

    /// <summary>�W�F�l���[�^�[���N���A��~������֐� </summary>
    public void GeneratorOperation()
    {
        _isGenerate = !_isGenerate;
    }

    public enum GenerateDirection
    {
        /// <summary>�E�ɐ��� </summary>
        Right = 0,
        /// <summary>���ɐ��� </summary>
        Left = 1,
    }
}
