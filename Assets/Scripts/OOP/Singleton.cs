using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OOP
{
    public abstract class Singleton<T> where T : Singleton<T>, new()
    {
        private static T _Instance = null;

        protected Singleton()
        {

        }

        static Singleton()
        {
            _Instance = new T();
        }

        //private static object _SyncObj = new object();
        public static T Instance
        {
            get { return _Instance; }
        }
    }
}
