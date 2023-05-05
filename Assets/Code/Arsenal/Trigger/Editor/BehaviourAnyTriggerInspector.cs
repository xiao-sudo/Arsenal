using UnityEditor;

namespace Code.Arsenal.Trigger.Editor
{
    [CustomEditor(typeof(BehaviourAnyTrigger))]
    public class BehaviourAnyTriggerInspector : UnityEditor.Editor
    {
        private SerializedProperty m_ChildTriggerProperty;

        private void OnEnable()
        {
            m_ChildTriggerProperty = serializedObject.FindProperty("m_ChildTriggers");
            RemoveEmptyOrParentTriggers();
        }

        private void OnDisable()
        {
            RemoveEmptyOrParentTriggers();
        }

        private void RemoveEmptyOrParentTriggers()
        {
            if (null == serializedObject.targetObject)
                return;

            if (null == m_ChildTriggerProperty)
                return;

            serializedObject.Update();

            for (var i = m_ChildTriggerProperty.arraySize - 1; i >= 0; --i)
            {
                var element = m_ChildTriggerProperty.GetArrayElementAtIndex(i);

                if (null == element.objectReferenceValue)
                {
                    m_ChildTriggerProperty.DeleteArrayElementAtIndex(i);
                }
                else
                {
                    var trigger = element.objectReferenceValue as BehaviourTrigger;
                    if (null == trigger || trigger == target)
                    {
                        m_ChildTriggerProperty.DeleteArrayElementAtIndex(i);
                    }
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}