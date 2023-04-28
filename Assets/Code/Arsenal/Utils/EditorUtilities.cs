#if UNITY_EDITOR

using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Arsenal.Utils
{
    public static class EditorUtilities
    {
        public static T DoGenericField<T>(Rect area, string label, T value)
        {
            if (typeof(Object).IsAssignableFrom(typeof(T)))
            {
                return (T)(object)EditorGUI.ObjectField(area, label, value as Object, typeof(T), true);
            }

            var state_name = null != value ? value.ToString() : "Null";
            EditorGUI.LabelField(area, label, state_name);

            return value;
        }

        public static void NextVerticalArea(ref Rect area)
        {
            if (area.height > 0)
                area.y += area.height + EditorGUIUtility.standardVerticalSpacing;
        }


        public static void RemoveElement(this SerializedProperty list_property, int index)
        {
            if (null == list_property)
                throw new System.ArgumentNullException(nameof(list_property));

            if (!list_property.isArray)
                throw new System.ArgumentException("Property is not an array", nameof(list_property));

            if (index < 0 || index >= list_property.arraySize)
                throw new IndexOutOfRangeException(nameof(list_property));

            list_property.DeleteArrayElementAtIndex(index);
            list_property.serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif