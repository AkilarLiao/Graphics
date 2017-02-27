using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace UnityEditor.VFX
{
    struct VFXProperty
    {
        public System.Type type;
        public string name;

        public VFXProperty(Type type, string name)
        {
            this.type = type;
            this.name = name;
        }

        public IEnumerable<VFXProperty> SubProperties()
        {
            if (IsExpandable())
            {
                FieldInfo[] infos = type.GetFields(BindingFlags.Public|BindingFlags.Instance);
                return infos.Select(info => new VFXProperty(info.FieldType,info.Name));
            }
            else
                return Enumerable.Empty<VFXProperty>();
        }

        public bool IsExpandable()
        {
            return !type.IsPrimitive && !typeof(UnityEngine.Object).IsAssignableFrom(type) && type != typeof(AnimationCurve);
        }
    }
}