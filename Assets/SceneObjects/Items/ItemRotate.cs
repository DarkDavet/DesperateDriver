using UnityEngine;

using DG.Tweening;

public class ItemRotate : MonoBehaviour
{
    void Start()
    {
        transform.rotation = Quaternion.identity;

        transform.DORotate(new Vector3(0f, 360f, 0f), 2f, RotateMode.FastBeyond360)
                 .SetEase(Ease.Linear)
                 .SetLoops(-1, LoopType.Restart);
    }
}
