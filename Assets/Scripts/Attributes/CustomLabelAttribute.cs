using UnityEngine;

namespace Attributes
{
    public class CustomLabelAttribute : PropertyAttribute
    {
        public string name { get; }

        public CustomLabelAttribute(string name)
        {
            this.name = name;
        }
    }
}
