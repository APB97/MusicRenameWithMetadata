using UnityEngine;

namespace Actions
{
    public class ParameterReorder : MonoBehaviour
    {
        [SerializeField] private Transform parametersParent; 
        
        public void MoveUp(Transform parameterVisual)
        {
            int siblingIndex = parameterVisual.GetSiblingIndex() - 1;
            if (siblingIndex < 0)
                return;
            parameterVisual.SetSiblingIndex(siblingIndex);
        }

        public void MoveDown(Transform parameterVisual)
        {
            int siblingIndex = parameterVisual.GetSiblingIndex() + 1;
            if (siblingIndex >= parametersParent.childCount)
                return;
            parameterVisual.SetSiblingIndex(siblingIndex);
        }
    }
}