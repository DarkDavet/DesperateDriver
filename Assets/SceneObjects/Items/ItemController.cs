using UnityEngine;

public class ItemController: MonoBehaviour
{
    public void ResetItems()
    {
        Transform parentTransform = this.transform;

        for (int i = 0; i < parentTransform.childCount; i++)
        {
            Transform childTransform = parentTransform.GetChild(i);
            GameObject childObject = childTransform.gameObject;

            if (!childObject.activeSelf && childObject.GetComponent<MoneyItem>() != null)
            {
                childObject.SetActive(true);
            }
        }
    }
}