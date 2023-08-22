namespace Data.ScriptItem
{
    public enum PlayerScriptItemTypes
    {
        /** 脚本头 */
        ScriptHead = 0x0,
        /** 移动 */
        Movement = 0x1,
        /** 播放声音 */
        Sound = 0x3,
        /** 物体 */
        Item = 0x4,
        /** 动画帧显示 */
        AnimationFrame = 0xC,
        /** 调色盘/屏幕摇晃效果 */
        Effect = 0xE,
        /** 防御范围 */
        DefenseRange = 0x19,
        /** 取消条件 */
        CancelCondition = 0x1E,
        /** 改变颜色 */
        ColorChanging = 0x23,
        /** 残影效果 */
        StickingEffect = 0x25
    }

    public enum StageScriptItemTypes
    {
        /** 场景脚本头项 */
        ScriptHead = 0x0,
        /** 播放声音 */
        Sound = 0x3,
        /** 动画帧显示 */
        AnimationFrame = 0xC,
        /** 变量分歧 */
        Variable = 0x1F,
    }
}
