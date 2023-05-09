namespace Data.ScriptItem
{
    public abstract class BaseScriptItem
    {
        public int type;

        protected BaseScriptItem(int type)
        {
            this.type = type;
        }
    }
}
