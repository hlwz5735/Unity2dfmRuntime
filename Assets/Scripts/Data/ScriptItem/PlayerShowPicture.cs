using UnityEngine;

namespace Data.ScriptItem
{
    public class PlayerShowPicture : BaseScriptItem
    {
        /// <summary>
        /// 停留时间
        /// </summary>
        public int freezeTime = 0;

        /// <summary>
        /// 图号
        /// </summary>
        public int picIndex = 0;

        /// <summary>
        /// 偏移量
        /// </summary>
        public Vector2 offset = Vector2.zero;

        /// <summary>
        /// 是否X轴反转
        /// </summary>
        public bool flipX = false;

        /// <summary>
        /// 是否Y轴反转
        /// </summary>
        public bool flipY = false;

        /// <summary>
        /// 是否固定朝向
        /// </summary>
        public bool fixDir = false;

        public PlayerShowPicture() : base((int)PlayerScriptItemTypes.AnimationFrame)
        {

        }
    }
}
