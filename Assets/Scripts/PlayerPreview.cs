using _2dfmFile;
using Attributes;
using Data;
using Game;
using Game.ScriptItem;
using UnityEngine;

public class PlayerPreview : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public int ScriptIdx = 0;

    private int lastScriptIdx = 0;
    
    [SerializeField]
    private GamePlayer player;

    [SerializeField]
    [CustomLabel("当前执行的脚本索引")]
    private int runningScriptIdx = -1;

    private float timeWaiting = 0;

    private ScriptData nowScript
    {
        get => player.PlayerData.Scripts[this.ScriptIdx];
    }

    private void Start()
    {
        var playerFilePath = Application.streamingAssetsPath + "/Players/DONGDONG.player";
        var playerData = PlayerFileReader.Read2dfmPlayerFile(playerFilePath);
        
        this.player = new GamePlayer(playerData);
        
        this.player.Load();
    }

    public void Update()
    {
        if (ScriptIdx != lastScriptIdx)
        {
            Reset();
            this.lastScriptIdx = this.ScriptIdx;
        }
        if (this.spriteRenderer == null || this.player == null)
        {
            return;
        }

        if (this.ScriptIdx < 0 || this.ScriptIdx > player.PlayerData.Scripts.Count)
        {
            return;
        }

        if (float.IsInfinity(this.timeWaiting))
        {
            return;
        }
        if (hasNoFrameItem())
        {
            return;
        }

        if (this.timeWaiting <= 0)
        {
            this.runningScriptIdx += 1;
            var item = this.getNextFrameItem();
            if (item == null)
            {
                return;
            }

            this.timeWaiting = item.FreezeTime == 0 ? float.PositiveInfinity : item.FreezeTime / 100f;
            var sprite = this.player.SpriteAt(item.PicIndex);
            if (sprite != null)
            {
                this.spriteRenderer.sprite = sprite;
            }
        }
        else
        {
            this.timeWaiting -= Time.deltaTime;
        }
    }

    private bool hasNoFrameItem()
    {
        return nowScript.Items.TrueForAll(item => item.Type != (int)ScriptItemTypes.AnimationFrame);
    }

    private AnimationFrameScript getNextFrameItem()
    {
        if (nowScript.ItemCount == 1)
        {
            var theItem = nowScript.Items[0];
            if (theItem.Type != (int)ScriptItemTypes.AnimationFrame)
            {
                return null;
            }

            return AnimationFrameScriptTranslator.Instance.Decode(theItem.Parameters);
        }

        if (this.runningScriptIdx < 0 || this.runningScriptIdx >= nowScript.ItemCount)
        {
            this.runningScriptIdx = 0;
        }

        var nowItem = nowScript.Items[this.runningScriptIdx];
        if (nowItem.Type != (int)ScriptItemTypes.AnimationFrame)
        {
            this.runningScriptIdx += 1;
            return this.getNextFrameItem();
        }
        return AnimationFrameScriptTranslator.Instance.Decode(nowItem.Parameters);
    }

    private void Reset()
    {
        this.timeWaiting = 0;
        this.runningScriptIdx = -1;
    }
}
