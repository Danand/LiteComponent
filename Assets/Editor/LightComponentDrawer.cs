using System;
using System.Collections.Generic;
using System.Linq;

using UnityEditor;
using UnityEngine;

using LiteComponent.Reflected;

namespace LiteComponent.Editor
{
    [CustomPropertyDrawer(typeof(LiteComponentAttribute))]
    public sealed class LightComponentDrawer : PropertyDrawer
    {
        private int selectedIndex = -1;
        private Type[] derivedTypes;
        private List<string> names;
        private Type managedReferenceType;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (managedReferenceType == null)
                ScriptAttributeUtility.GetFieldInfoAndStaticTypeFromProperty(property, out managedReferenceType);

            derivedTypes = derivedTypes ?? (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                                            from assemblyType in domainAssembly.GetTypes()
                                            where managedReferenceType.IsAssignableFrom(assemblyType)
                                            where !assemblyType.IsAbstract && !assemblyType.IsInterface
                                            select assemblyType).ToArray();

            names = names ?? derivedTypes.Select(derievedType => derievedType.Name).ToList();

            if (selectedIndex == -1)
            {
                var newIndex = FindIndex(names, name => property.managedReferenceFullTypename.EndsWith(name, StringComparison.Ordinal));
                selectedIndex = newIndex == -1 ? 0 : newIndex;
            }

            label = EditorGUI.BeginProperty(position, label, property);

            EditorGUI.BeginChangeCheck();

            selectedIndex = EditorGUI.Popup(position, label, selectedIndex, names.Select(name => new GUIContent(name)).ToArray());

            if (EditorGUI.EndChangeCheck())
            {
                var instance = Activator.CreateInstance(derivedTypes[selectedIndex]);
                property.managedReferenceValue = instance;
            }

            EditorGUI.EndProperty();

            EditorGUI.PropertyField(position, property, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) * property.CountInProperty();
        }

        private int FindIndex<T>(IList<T> source, Func<T, bool> selector)
        {
            for (int i = 0; i < source.Count; i++)
            {
                if (selector.Invoke(source[i]))
                    return i;
            }

            return -1;
        }
    }
}