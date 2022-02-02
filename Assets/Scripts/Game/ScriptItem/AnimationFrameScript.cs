using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.ScriptItem
{
    class AnimationFrameScript : BaseScriptItem
    {
        /// <summary>
        /// 停留时间
        /// </summary>
        public int FreezeTime = 0;

        /// <summary>
        /// 图号
        /// </summary>
        public int PicIndex = 0;

        /// <summary>
        /// 偏移量
        /// </summary>
        public Vector2 Offset = Vector2.zero;

        /// <summary>
        /// 是否X轴反转
        /// </summary>
        public bool FlipX = false;

        /// <summary>
        /// 是否Y轴反转
        /// </summary>
        public bool FlipY = false;

        /// <summary>
        /// 是否固定朝向
        /// </summary>
        public bool FixDir = false;


        public AnimationFrameScript() : base((int)ScriptItemTypes.AnimationFrame)
        {

        }
    }
}
