    using DG.Tweening;
    using System;
    using UnityEngine;

    /// <summary>エネミーを管理するクラス </summary>
    public class Enemy : MonoBehaviour
    {
        /// <summary>吹き飛ぶまでの遅延 </summary>
        const float EXPLOSION_DELAY = 0.3f;

        [SerializeField, Header("現在の移動方法")]
        MoveMethod _currentMoveMethod = MoveMethod.Path;
        [SerializeField, Tooltip("ファンが動く方向"), Header("ファン関係")]
        Vector3 _enemySpped;
        [SerializeField, Tooltip("飛ぶ方向")]
        Transform[] _trajectoryParent = default;
        [SerializeField, Tooltip("ファンサを要求する数")]
        int _fansaNum = 1;
        [SerializeField, Tooltip("判定によって飛び方を変えるための変数")]
        int _jugdeLength = 1;
        [SerializeField, Tooltip("ファンがoutになったときの透明になる速度")]
        float _fadedSpeed = 0.01f;
        [SerializeField, Tooltip("リズム判定をするための時間(デバッグ用)"), Header("リズム関係")]
        float _time = default;
        [SerializeField, Tooltip("リズム判定の秒数")]
        float _perfect, _good, _bad, _out;
        [SerializeField, Header("倒した時のスコア")]
        int _addScoreValue = 1;
        [SerializeField, Header("移動にかかる時間"), Range(0.1f, 10f)]
        float _moveTime = 1f;
        [SerializeField, Header("吹き飛んだ時のサイズ"), Range(0.1f, 1f)]
        float _minScale = 0.3f;
        [SerializeField, Tooltip("爆発エフェクト")]
        GameObject _explosionEffect = default;
        [SerializeField, Tooltip("ファンサ")]
        RequestUIController _requestUIController = null;
        [SerializeField, Tooltip("敵のスプライトを管理するクラスの変数")]
        SpriteChange _spriteChange = default;
        /// <summary>倒された時の吹き飛ぶ軌道を構成するポイントの配列 </summary>
        Vector3[] _deadMovePoints = default;
        /// <summary>敵の死亡フラグ</summary>
        bool _isdead = default;
        /// <summary>FlickTypeを保存させておく変数 </summary>
        public FlickType _flickTypeEnemy;
        /// <summary>アウトがどうかの判定をするフラグ</summary>
        public bool _isOut;
        /// <summary>スコアを増やすAction </summary>
        event Action<int> _addScore = default;
        /// <summary>倒されたらステージスクロールを開始する </summary>
        event Action _stageScroll = default;
        /// <summary>ダメージを与える（プレイヤーの体力を減らす）</summary>
        event Action<int> _giveDamage = default;

        Rigidbody _rb;
        SpriteRenderer _sr;
        /// <summary>敵が倒れた際にかかるDoTweenでの動きの時間のプロパティ</summary>
        public float MoveTime { get => _moveTime; set => _moveTime = value; }
        /// <summary>スコアを増やすAction </summary>
        public event Action<int> AddScore
        {
            add { _addScore += value; }
            remove { _addScore -= value; }
        }

        /// <summary>倒されたらステージスクロールを開始する </summary>
        public event Action StageScroll
        {
            add { _stageScroll += value; }
            remove { _stageScroll -= value; }
        }

        /// <summary>ダメージを与える（プレイヤーの体力を減らす）</summary>
        public event Action<int> GiveDamage
        {
            add { _giveDamage += value; }
            remove { _giveDamage -= value; }
        }


        private void Start()
        {
            _sr = GetComponent<SpriteRenderer>();
            _rb = GetComponent<Rigidbody>();
            _rb.AddForce(_enemySpped); //ファンを前に動かす（仮)
            _spriteChange.EnemyRandomMethod();
            _isOut = false;
            _isdead = false;
        }
        private void Update()
        {

            _time -= Time.deltaTime;// リズム判定用       
           if(Input.GetKeyDown(KeyCode.Space))
           {
            BadMove();
           }
            if (_time <= _out && !_isdead) //_outを超えたら飛ばないようにboolで管理
            {
                _isOut = true;
                StartFade();
            }
            if (!_isdead)
            {
                UpdateRequestWindow();  //吹き出しを更新
            }
        }

        /// <summary> 吹き飛ぶ演出（移動） </summary>
        void DeadMove()
        {
            if (_currentMoveMethod == MoveMethod.Path)
            {
                //移動処理
                transform.DOPath(path: _deadMovePoints, duration: _moveTime, pathType: PathType.CatmullRom)
                    .SetDelay(EXPLOSION_DELAY)
                    .OnComplete(() => Destroy(gameObject));
            }
            else if (_currentMoveMethod == MoveMethod.Jump)
            {
                transform.DOJump(_deadMovePoints[_deadMovePoints.Length - 1], jumpPower: 1f, numJumps: 1, duration: _moveTime)
                    .SetDelay(EXPLOSION_DELAY)
                    .OnComplete(() => _addScore.Invoke(_addScoreValue))
                    .OnComplete(() => Destroy(gameObject));
            }

            //大きさ調整
            transform.DOScale(new Vector3(_minScale, _minScale, _minScale), _moveTime)
                .SetDelay(EXPLOSION_DELAY)
                .OnComplete(() => _stageScroll?.Invoke());

            //回転
            transform.DOLocalRotate(new Vector3(0, 0, 360f), 0.1f, RotateMode.FastBeyond360)
                .SetDelay(EXPLOSION_DELAY)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);
        }
        /// <summary>評価BadのときのEnemyの動き</summary>
       void BadMove()
        {
            //横移動
            transform.DOMoveX(-3, _moveTime)
                .SetDelay(EXPLOSION_DELAY)
                //動ききってから消えるパターン
                //.OnComplete(()=>_sr.DOFade(endValue:0,duration:2.0f))
                .OnComplete(() => _stageScroll?.Invoke())
                .OnComplete(() => Destroy(gameObject));       

        //透明になりながら消えていくパターン
        //_sr.DOFade(endValue: 0, duration: 2.0f);
            _isdead = true;

        //スケールをいじる
        /*transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), _moveTime)
            .SetDelay(EXPLOSION_DELAY)
            .OnComplete(() => _stageScroll?.Invoke());*/
    }
    /// <summary>時間によって吹き出しのイラストを変更する </summary>
    private void UpdateRequestWindow()
        {
            if (_time <= _perfect)
            {
                _requestUIController.ChangeRequestWindow(TimingResult.Perfect);

            }
            else if (_time <= _good)
            {
                _requestUIController.ChangeRequestWindow(TimingResult.Good);
            }
            else if (_time <= _bad)
            {
                _requestUIController.ChangeRequestWindow(TimingResult.Bad);
            }
        }
        /// <summary>outの時の動き</summary>
        void StartFade()
        {
            _sr.color -= new Color(0, 0, 0, _fadedSpeed); //徐々に透明度を下げていく
            if (_sr.color.a <= 0)//透明になったら消す
            {
                _giveDamage?.Invoke(_fansaNum);
                _stageScroll?.Invoke();
                Destroy(gameObject);
            }
        }
        /// <summary>倒された時の処理 </summary>
        public void Dead()
        {
            Instantiate(_explosionEffect, transform.position, Quaternion.identity);     //爆発エフェクトを生成
            DeadMove();
            _isdead = true;
            AudioManager.Instance.PlaySE(6, 0.1f);
        }

        /// <summary>Flickする方向をランダムに取得する</summary>
        public void FlickNum()
        {
            var rnd = new System.Random();
            _flickTypeEnemy = (FlickType)rnd.Next(2, 5);
            _requestUIController.ChangeRequestImage(_flickTypeEnemy);
            Debug.Log(_flickTypeEnemy);
        }
        /// <summary>リズム判定用</summary>
        public void JugeTime()
        {
            if (!_isdead)
            {
                if (_time <= _out)
                {
                    Debug.Log("out");
                }
                else if (_time <= _perfect)
                {
                    Debug.Log($"perfect {_time:F1}");
                    ResultManager.Instance.CountPerfect++;
                }
                else if (_time <= _good)
                {
                    Debug.Log($"good {_time:F1}");
                    ResultManager.Instance.CountGood++;
                }
                else if (_time <= _bad)
                {
                    Debug.Log($"bad {_time:F1}");
                    ResultManager.Instance.CountBad++;
                }
            }
        }
        /// <summary>倒された時の軌道のポイントを取得する </summary>
        /// <param name="pointParent">軌道のポイントを子オブジェクトに持つオブジェクト(親オブジェ)</param>
        public void SetUp()
        {
         var points = transform;
            for (int i = 0; i < _fansaNum; i++)
            {
                if (!_isdead)
                {
                    FlickNum(); //ランダムでフリック方向を取得する

                }
            }

            if (_flickTypeEnemy == FlickType.Left)
            {
            points = _trajectoryParent[0];
            }
            else if (_flickTypeEnemy == FlickType.Right)
            {
            points = _trajectoryParent[1];
            }
            else if (_flickTypeEnemy == FlickType.Up || _flickTypeEnemy == FlickType.Down)
            {
            points = _trajectoryParent[2];
            }

            _deadMovePoints = new Vector3[points.childCount - 1];

            for (var i = 0; i < _deadMovePoints.Length; i++)
            {
                _deadMovePoints[i] = points.GetChild(i).position;
            }

        }
        /// <summary>移動方法 </summary>
        public enum MoveMethod
        {
            /// <summary>移動位置を指定する方法 </summary>
            Path,
            /// <summary>最終的な位置にジャンプする方法 </summary>
            Jump,
        }
    }