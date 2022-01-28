using _2dfmFile;
using Game;
using UnityEngine;

public class PlayerPreview : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public int spriteIdx = 0;
    
    [SerializeField]
    private GamePlayer player;
    
    private void Start()
    {
        var playerFilePath = Application.streamingAssetsPath + "/Players/DONGDONG.player";
        var playerData = PlayerFileReader.Read2dfmPlayerFile(playerFilePath);
        
        this.player = new GamePlayer(playerData);
        
        this.player.LoadSpriteFrames();
    }

    public void Update()
    {
        if (this.spriteRenderer != null && this.player != null)
        {
            var sprite = this.player.SpriteAt(this.spriteIdx);
            if (this.spriteRenderer.sprite != sprite)
            {
                this.spriteRenderer.sprite = sprite;
            }
        }
    }
}
