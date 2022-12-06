using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �t���b�N�̕�����^�b�v�����擾�ł���
/// </summary>

public class ScreenInput : MonoBehaviour
{
    /// <summary>���C�̍ő�͈� </summary>
    const float MAX_RAY_RANGE = 30f;

    [SerializeField, Tooltip("�t���b�N�̔���ɕK�v�ȑ��싗��")]
    Vector2 _flickMinRange = new Vector2(0f, 0f);
    [SerializeField, Tooltip("�A�C�e���̃��C���[")]
    LayerMask _itemHitLayer = default;
    [SerializeField, Tooltip("���͕�����\������e�L�X�g")]
    Text _inputText = default;
    [SerializeField]
    GameManager _manager = default;
    [SerializeField]
    ResultManager _resultManager = default;
    [SerializeField]
    SuperIdolTime _superIdolTime = default;
 
    /// <summary>
    /// ���݂̃t���b�N�����Ȃǂ�ۑ�����
    /// </summary>
    FlickType _flickType = FlickType.None;

    /// <summary>
    /// ���߂ɐG�ꂽ�A�������̓X���C�v�������ۑ�����
    /// </summary>
    Vector2 _StartPosition = new Vector2();

    /// <summary>
    /// �w�𗣂����ꏊ��ۑ�����
    /// </summary>
    Vector2 _EndPosition = new Vector2();

    void Update()
    {
        InputFrickGet();
    }

    /// <summary>
    /// ��ʂ�G���Ă��邩���擾����
    /// </summary>
    private void InputFrickGet()
    {
        //Unity��ł̃f�o�b�N�p
        if (UnityEngine.Application.isEditor)
        {
            //�������ꏊ��ۑ�
            if (Input.GetMouseButtonDown(0))
            {
                ResetParameter();
                _StartPosition = Input.mousePosition;
            }//�X���C�v�����ꏊ��ۑ�
            else if (Input.GetMouseButton(0))
            {
                 _StartPosition = Input.mousePosition;
            }
            //�������ꏊ��ۑ�
            else if (Input.GetMouseButtonUp(0))
            {
                _EndPosition = Input.mousePosition;
                Frickdirection();
            }
            //���͂��Ȃ��ꍇ�̓��Z�b�g����
            else if (_flickType != FlickType.None || _flickType != FlickType.None)
            {
                ResetParameter();
            }
        }
        else //�X�}�z�p
        {   
            //��{�ȏ�w���G�ꂽ��
            if (Input.touchCount > 0)
            {
                Touch touch = Input.touches[0];

                switch (touch.phase)
                {
                    //�������A�������̓X���C�v�����ꏊ��ۑ�
                    case TouchPhase.Began:
                        ResetParameter();
                        _StartPosition = touch.position;
                        break;
                    case TouchPhase.Moved:
                        _StartPosition = touch.position;
                        break;
                    //�������ꏊ��ۑ�
                    case TouchPhase.Ended:
                        _EndPosition = touch.position;
                        Frickdirection();
                        break;
                }
            }//���͂��Ȃ��ꍇ�̓��Z�b�g����
            else if (_flickType != FlickType.None || _flickType != FlickType.None)
            {
                ResetParameter();
            }
        }
    }

    /// <summary>
    /// �G�ꂽ�ʒu�֌W����^�b�v���t���b�N�����肷��
    /// </summary>
    private void Frickdirection()
    {
        Vector2 flickVector = new Vector2((new Vector3(_EndPosition.x, 0 , 0) - new Vector3(_StartPosition.x, 0 , 0)).magnitude,
                                          (new Vector3(0 , _EndPosition.y, 0) - new Vector3(0, _StartPosition.y , 0)).magnitude);

        if (flickVector.x <= _flickMinRange.x && flickVector.y <= _flickMinRange.y)
        {
            _flickType = FlickType.Tap;

            //�A�C�e���擾����
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, MAX_RAY_RANGE, _itemHitLayer);

            if (hit.collider != null)   //�A�C�e�����擾
            {   
                var item = hit.collider.GetComponent<IdolPowerItem>();
                item.GetItem();
            }
        }
        else if (flickVector.x > flickVector.y)
        {
            if (Mathf.Sign(_EndPosition.x - _StartPosition.x) > 0)
            {
                _flickType = FlickType.Right;
            }
            else { _flickType = FlickType.Left; }
        }
        else
        {
            if (Mathf.Sign(_EndPosition.y - _StartPosition.y) > 0)
            {
                _flickType = FlickType.Up;
            }
            else { _flickType = FlickType.Down; }
        }


        //�^�b�v�̎��͔�΂Ȃ�
        if (_manager.CurrentEnemy != null && _flickType != FlickType.Tap)
        {
            if(_flickType == _manager.CurrentEnemy._flickTypeEnemy) ///�t���b�N�����������t�@���T�Ɠ��l�Ȃ琁�����
            {
                AudioManager.Instance.PlayRequestSE(_flickType);
                _manager.CurrentEnemy.JugeTime();//��񂾂Ƃ��̕b���Ɣ�������߂����
                _manager.CurrentEnemy.Dead();
            }
        }

        //if (_manager.IsIdleTime == true && _flickType == FlickType.Tap)
        //{
        //    _resultManager.CountPerfect++;
        //    _superIdolTime.GaugeCount++;
        //}

        _inputText.text = _flickType.ToString();
    }
    /// <summary>
    /// �ϐ��̏������p
    /// </summary>
    private void ResetParameter()
    {
        _flickType = FlickType.None;
    }

    public FlickType GetFlickType() 
    {
        return _flickType;
    }
}

public enum FlickType
{
    None = 0,
    Up = 1,
    Down = 2,
    Right = 3,
    Left = 4,
    Tap = 5
}
