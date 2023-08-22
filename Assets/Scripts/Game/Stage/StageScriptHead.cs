using System;
using Data;

namespace Game.Stage
{
    public class StageScriptHead
    {
        /** 脚本头 */
        public int type { get; set; }
        /** 横向循环 */
        public bool horizontalLoop { get; set; }
        /** 纵向循环 */
        public bool verticalLoop { get; set; }
        /** 横向滚轴（-9999 - 9999），其中100代表正常滚轴 */
        public int horizontalScroll { get; set; }
        /** 纵向滚轴（-9999 - 9999），其中100代表正常滚轴 */
        public int verticalScroll { get; set; }

        public static StageScriptHead read(ScriptItemData data)
        {
            return new StageScriptHead()
            {
                type = data.type,
                horizontalLoop = (data.parameters[0] & 0x02) > 0,
                verticalLoop = (data.parameters[0] & 0x04) > 0,
                horizontalScroll = (data.parameters[0] & 0x08) > 0 ? BitConverter.ToInt16(data.parameters, 1) : 0,
                verticalScroll = (data.parameters[0] & 0x10) > 0 ? BitConverter.ToInt16(data.parameters, 3) : 0,
            };
        }
    }
}
