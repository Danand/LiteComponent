using System;
using System.Reflection;

using UnityEditor;

namespace LiteComponent.Reflected
{
    public delegate FieldInfo GetFieldInfoAndStaticTypeFromPropertyDelegate(SerializedProperty property, out Type type);

    public static class ScriptAttributeUtility
    {
        public static readonly Type OriginalType = Type.GetType("UnityEditor.ScriptAttributeUtility, UnityEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", true);

        public static GetFieldInfoAndStaticTypeFromPropertyDelegate GetFieldInfoAndStaticTypeFromProperty =
            (GetFieldInfoAndStaticTypeFromPropertyDelegate)OriginalType?.GetMethod(nameof(GetFieldInfoAndStaticTypeFromProperty), BindingFlags.Static | BindingFlags.NonPublic)?
                                                                        .CreateDelegate(typeof(GetFieldInfoAndStaticTypeFromPropertyDelegate));
    }
}