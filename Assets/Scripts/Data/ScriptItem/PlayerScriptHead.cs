namespace Data.ScriptItem
{
    public class PlayerScriptHead : BaseScriptItem
    {
        public int level;

        public PlayerScriptHead(int level = 0) : base((int)ScriptItemTypes.ScriptHead)
        {
            this.level = level;
        }
    }
}