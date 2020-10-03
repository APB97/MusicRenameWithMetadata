using UnityEngine;

public class ModifyHeightBy : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    
    public void Modify(float deltaHeight)
    {
        Vector2 sizeDelta = rectTransform.sizeDelta;
        sizeDelta.y += deltaHeight;
        rectTransform.sizeDelta = sizeDelta;
    }
}
