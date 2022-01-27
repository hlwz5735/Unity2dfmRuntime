using UnityEngine;

namespace Attributes
{
    public class CustomLabelAttribute : PropertyAttribute
    {
        public string Name { get; }

        public CustomLabelAttribute(string name)
        {
            Name = name;
        }
    }
}
