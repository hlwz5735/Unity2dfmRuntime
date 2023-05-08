namespace OOP
{
    public abstract class Singleton<T> where T : Singleton<T>, new()
    {
        private static readonly T INSTANCE = null;

        protected Singleton()
        {

        }

        static Singleton()
        {
            INSTANCE = new T();
        }

        //private static object _SyncObj = new object();
        public static T Instance => INSTANCE;
    }
}
