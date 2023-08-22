namespace Data.ScriptItem
{
    public class PlayerScriptHead : BaseScriptItem
    {
        public int level;

        public PlayerScriptHead(int level = 0) : base((int)PlayerScriptItemTypes.ScriptHead)
        {
            this.level = level;
        }
    }
}