using System;
using _2dfmFile;
using Attributes;
using Data;
using Data.ScriptItem;
using Game;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerPreview : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    [CustomLabel("脚本表ID")]
    public int scriptIdx = 0;

    private int _lastScriptIdx = 0;
    
    private GamePlayer _player;

    [SerializeField]
    [CustomLabel("脚本项的索引")]
    private int runningScriptIdx = -1;

    private float _timeWaiting = 0;

    private ScriptData nowScript
    {
        get => _player.playerData.scripts[this.scriptIdx];
    }

    private void Start()
    {
        var playerFilePath = Application.streamingAssetsPath + "/Players/DONGDONG.player";
        var playerData = PlayerFileReader.read2dfmPlayerFile(playerFilePath);
        this._player = new GamePlayer(playerData);
        this._player.load();
    }

    public void Update()
    {
        if (scriptIdx != _lastScriptIdx)
        {
            Reset();
            this._lastScriptIdx = this.scriptIdx;
        }
        if (this.spriteRenderer == null || this._player == null)
        {
            return;
        }

        if (this.scriptIdx < 0 || this.scriptIdx > _player.playerData.scripts.Count)
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
            var sprite = this._player.spriteAt(item.picIndex);
            if (sprite != null)
            {
                this.spriteRenderer.sprite = sprite;
                this.spriteRenderer.flipX = item.flipX;
                this.spriteRenderer.flipY = item.flipY;
                var trans = this.spriteRenderer.GetComponent<Transform>();
                trans.localPosition = new Vector3(
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
        return Array.TrueForAll(nowScript.items, item => item.type != (int)PlayerScriptItemTypes.AnimationFrame);
    }

    private PlayerShowPicture getNextFrameItem()
    {
        if (nowScript.itemCount == 1)
        {
            var theItem = nowScript.items[0];
            if (theItem.type != (int)PlayerScriptItemTypes.AnimationFrame)
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
        if (nowItem.type != (int)PlayerScriptItemTypes.AnimationFrame)
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
