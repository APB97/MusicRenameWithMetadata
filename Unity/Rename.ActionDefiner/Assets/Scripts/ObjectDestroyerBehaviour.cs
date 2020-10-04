using UnityEngine;

public class ObjectDestroyerBehaviour : MonoBehaviour
{
    public void DestroyTarget(GameObject target)
    {
        Destroy(target);
    }
}