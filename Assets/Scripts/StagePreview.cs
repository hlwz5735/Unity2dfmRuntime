using System;
using _2dfmFile;
using Attributes;
using Data;
using Data.ScriptItem;
using Game;
using UnityEngine;
using UnityEngine.Serialization;

public class StagePreview : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    [CustomLabel("脚本表ID")]
    public int scriptIdx = 0;

    private int _lastScriptIdx = 0;
    
    private GameStage _stage;

    [SerializeField]
    [CustomLabel("脚本项的索引")]
    private int runningScriptIdx = -1;

    private float _timeWaiting = 0;

    private ScriptData nowScript
    {
        get => _stage.stageData.scripts[this.scriptIdx];
    }

    private void Start()
    {
        var stageFilePath = Application.streamingAssetsPath + "/Stages/11 GUANG XI.stage";
        var stageData = StageFileReader.read2dfmStageFile(stageFilePath);
        this._stage = new GameStage(stageData);
    }

    public void Update()
    {
        if (scriptIdx != _lastScriptIdx)
        {
            Reset();
            this._lastScriptIdx = this.scriptIdx;
        }
        if (this.spriteRenderer == null || this._stage == null)
        {
            return;
        }

        if (this.scriptIdx < 0 || this.scriptIdx > _stage.stageData.scripts.Count)
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
            var sprite = this._stage.spriteAt(item.picIndex);
            if (sprite != null)
            {
                this.spriteRenderer.sprite = sprite;
                this.spriteRenderer.flipX = item.flipX;
                this.spriteRenderer.flipY = item.flipY;
                var trans = this.spriteRenderer.GetComponent<Transform>();
                trans.position = new Vector3(
                    item.offset.x / 100f, 
                    (-item.offset.y + (item.flipY ? sprite.rect.height : 0)) / 100f,
                    trans.position.z);
            }
        }
        else
        {
            this._timeWaiting -= Time.deltaTime;
        }
    }

    private bool hasNoFrameItem()
    {
        return Array.TrueForAll(nowScript.items, item => item.type != (int)ScriptItemTypes.AnimationFrame);
    }

    private PlayerShowPicture getNextFrameItem()
    {
        if (nowScript.itemCount == 1)
        {
            var theItem = nowScript.items[0];
            if (theItem.type != (int)ScriptItemTypes.AnimationFrame)
            {
                return null;
            }

            return ScriptItemReader.readPlayerShowPicture(theItem.parameters);
        }

        if (this.runningScriptIdx < 0 || this.runningScriptIdx >= nowScript.itemCount)
        {
            this.runningScriptIdx = 0;
        }

        var nowItem = nowScript.items[this.runningScriptIdx];
        if (nowItem.type != (int)ScriptItemTypes.AnimationFrame)
        {
            this.runningScriptIdx += 1;
            return this.getNextFrameItem();
        }
        return ScriptItemReader.readPlayerShowPicture(nowItem.parameters);
    }

    private void Reset()
    {
        this._timeWaiting = 0;
        this.runningScriptIdx = -1;
    }
}
