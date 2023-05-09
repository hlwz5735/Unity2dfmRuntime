typedef struct 
{
    CHAR  scriptName[32] <comment="脚本名", bgcolor=cGreen>;
    SHORT scriptIndex    <comment="脚本格子起始索引", bgcolor=0xFFAAAA>;
    // BYTE unknownFlags[5] <comment="未知内容", bgcolor=cRed>;
    BYTE  unknownFlag1;
    /* 
     * KGT中存在0x01、0x21、0x39、0x81等特殊值
     * stage中0x03对应角色位置脚本项
     * player中0x01对应的都是游戏默认动作项
     * demo中，0x01对应基本背景，0x03对应系统图像位置
     */
    int  specialFlag <comment="特殊脚本项标记", bgcolor=cRed>;
} SCRIPT <comment=GetScriptLineName>;

char[] GetScriptLineName(SCRIPT &s)
{
    return s.scriptName;
}

typedef struct
{
    BYTE scriptType <bgcolor=cGreen>;
    BYTE bytes[15] <fgcolor=0x00AA00>;
} SCRIPT_ITEM <comment="脚本项">;

typedef struct {
    int unknownFlag1;
    int width;
    int height;
    int hasPrivatePalettle;
    int size;
    // 如果帧大小为0，则判断精灵帧的宽高，如果也为0，则这条精灵帧是空的，否则
    // 按照每个像素占1字节主动计算帧大小（即未压缩的图片），如果精灵使用了私有
    // 调色盘，则追加1kb的私有调色盘内容
    // 对于被压缩的图片，直接取此处定义的size大小即可
    if (size == 0)
    {
        local int tSize = width * height;
        if (tSize > 0)
        {
            byte frameContent[tSize + (hasPrivatePalettle ? 1024 : 0)];
        }
    }
    else
    {
        byte frameContent[size];
    }
} SPRITE_FRAME <name="精灵帧", read=printSpriteFrame>;

string printSpriteFrame(SPRITE_FRAME &sf)
{
    string s;   
    SPrintf(s, "%d x %d", sf.width, sf.height);
    if (sf.size == 0)
    {
        s = s + "(origin)";
    }
    else
    {
        s = s + "(compressed)";
    }
    return s;
}

typedef struct {
    uchar blue;
    uchar green;
    uchar red;
    uchar alpha;    
} ColorBGRA <read=getColorValue>;

string getColorValue(ColorBGRA& color)
{
    string s;
    local uchar alpha = color.alpha == 0 ? 0 : 255;
    return SPrintf(s, "0x%02x%02x%02x%02x", color.red, color.green, color.blue, alpha);
}

typedef struct {
    ColorBGRA colors[256];
    // 共享调色盘之后，是32字节的空行
    int palettleGap[8] <bgcolor=cYellow>;
} Palettle <name="共享调色盘">;

typedef struct {
    int unknown1 <name="未知整型数据1">;
    char name[32];
    int size;
    byte soundType <name="声音类型", comment="1.WAV;2.MIDI;3.CDDA">;
    byte soundTrack <name="音轨", comment="仅在CDDA下有效">;
    if (size > 0)
    {
        byte data[size] <name="声音数据">;
    }
} SoundData <name="声音", comment=GetSoundName>;

char[] GetSoundName(SoundData &s)
{
    return s.name;
}
