using System;
using System.Collections.Generic;
using Data;
using SpriteFrame;
using UnityEngine;

namespace Game
{
    
    [Serializable]
    public class GamePlayer
    {
        public readonly PlayerData playerData;
        private Sprite[] _spritePool;

        public GamePlayer(PlayerData playerData)
        {
            this.playerData = playerData;
            this._spritePool = new Sprite[playerData.pictureCount];
        }

        /// <summary>
        /// 加载
        /// </summary>
        public void load() {
            // TODO: 之前一启动就会把所有图片加载到精灵池，改成惰性加载
            // for (int i = 0; i < this.playerData.pictureCount; i++)
            // {
            //     loadSpriteFrame(i);
            // }
        }
        
        public void loadSpriteFrame(int idx)
        {
            if (idx < 0 || idx >= this.playerData.pictureCount)
            {
                return;
            }
            var spriteFrame = playerData.pictures[idx];
            PaletteData palette = null;
            if (!spriteFrame.hasPrivatePalette)
            {
                palette = playerData.publicPalettes[0];
            }

            var sprite = SpriteFrameUtil.genSprite(spriteFrame, palette);
            sprite.name = this.playerData.name + "_" + idx;
            this._spritePool[idx] = sprite;
        }

        public Sprite spriteAt(int idx)
        {
            if (idx < 0 || idx >= this.playerData.pictureCount)
            {
                return null;
            }

            if (_spritePool[idx] == null) {
                loadSpriteFrame(idx);
            }
            
            return _spritePool[idx];
        }
    }
}