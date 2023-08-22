using System;
using System.Collections.Generic;
using _2dfmFile;
using Attributes;
using Data;
using Data.ScriptItem;
using UnityEngine;

namespace Game.Stage
{
    class GameStageEntity : MonoBehaviour
    {
        public List<ScriptItemData> scriptItems;
        public string name;
        
        private Transform _transform;
        private GameStage _stage;
        private SpriteRenderer _spriteRenderer;

        /* 以下为实体初期设置 */
        private bool _horizontalLoop = false;
        private bool _verticalLoop = false;
        private int _horizontalScroll;
        private int _verticalScroll;
        
        private Stack<ScriptRuntimeContext> _scriptCallStack;
        
        
        [SerializeField]
        [CustomLabel("脚本项的索引")]
        private int runningScriptIdx = 0;
        private float _timeWaiting = 0;


        public GameStage stage
        {
            set => _stage = value;
        }
        
        private void Start()
        {
            _transform = GetComponent<Transform>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            if (scriptItems.Count > 0)
            {
                var head = StageScriptHead.read(scriptItems[0]);
                _horizontalLoop = head.horizontalLoop;
                _verticalLoop = head.verticalLoop;
                _horizontalScroll = head.horizontalScroll;
                _verticalScroll = head.verticalScroll;
            }
        }

        private void Update()
        {
            if (_spriteRenderer == null || this._stage == null)
            {
                return;
            }

            if (float.IsInfinity(this._timeWaiting))
            {
                return;
            }
            if (hasNoFrameItem())
            {
                return;
            }

            if (this._timeWaiting <= 0)
            {
                this.runningScriptIdx += 1;
                var item = this.getNextFrameItem();
                if (item == null)
                {
                    return;
                }

                this._timeWaiting = item.freezeTime == 0 ? float.PositiveInfinity : item.freezeTime / 100f;
                var sprite = _stage.spriteAt(item.picIndex);
                if (sprite != null)
                {
                    _spriteRenderer.sprite = sprite;
                    _spriteRenderer.flipX = item.flipX;
                    _spriteRenderer.flipY = item.flipY;
                    _transform.localPosition = new Vector3(
                        (item.offset.x + sprite.rect.width / 2) / 100f, 
                        (960 - item.offset.y - (item.flipY ? 0 : sprite.rect.height)) / 100f,
                        _transform.position.z
                    );
                }
            }
            else
            {
                this._timeWaiting -= Time.deltaTime;
            }
        }
        
        private bool hasNoFrameItem()
        {
            return scriptItems.TrueForAll(item => item.type != (int)StageScriptItemTypes.AnimationFrame);
        }
        
        private PlayerShowPicture getNextFrameItem()
        {
            if (scriptItems.Count == 1)
            {
                var theItem = scriptItems[0];
                if (theItem.type != (int)PlayerScriptItemTypes.AnimationFrame)
                {
                    return null;
                }

                return ScriptItemReader.readPlayerShowPicture(theItem.parameters);
            }

            if (this.runningScriptIdx < 0 || this.runningScriptIdx >= scriptItems.Count)
            {
                runningScriptIdx = 0;
            }

            var nowItem = scriptItems[this.runningScriptIdx];
            if (nowItem.type != (int)PlayerScriptItemTypes.AnimationFrame)
            {
                this.runningScriptIdx += 1;
                return this.getNextFrameItem();
            }
            return ScriptItemReader.readPlayerShowPicture(nowItem.parameters);
        }
    }
}
