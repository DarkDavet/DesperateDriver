using DG.Tweening;
using UnityEngine;

public class UIMoneyRemovePopupAnim: MonoBehaviour
{
    private CanvasGroup canvasGroup;

    public float moveUpDistance = 5f;
    public float moveDuration = 2f;
    public float fadeInDuration = 1f;
    public float fadeOutDuration = 1f;
    public float scaleDuration = 1f;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0;
    }


    public void PlayPopupAnimation()
    {
        canvasGroup.alpha = 0;

        Sequence sequence = DOTween.Sequence();

        sequence.Append(canvasGroup.DOFade(1, fadeInDuration))
                .Join(transform.DOLocalMoveY(transform.localPosition.y - moveUpDistance, moveDuration).SetEase(Ease.Linear))
                .Join(transform.DOScale(transform.localScale + new Vector3(0.1f, 0.1f, 0f), scaleDuration).SetEase(Ease.Linear))
                .Append(transform.DOLocalMoveY(transform.localPosition.y - moveUpDistance, moveDuration).SetEase(Ease.Linear))
                .Join(transform.DOScale(transform.localScale - new Vector3(0.1f, 0.1f, 0f), scaleDuration).SetEase(Ease.Linear))
                .Join(canvasGroup.DOFade(0, fadeOutDuration).SetDelay(moveDuration - fadeOutDuration));

        sequence.Play();
    }
}
