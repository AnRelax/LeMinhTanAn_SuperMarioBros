using TMPro;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup), typeof(TextMeshProUGUI))]
public class FadeOutTextTween : PoolableObject
{
    private CanvasGroup CanvasGroup;
    [HideInInspector]
    public TextMeshProUGUI Text;

    public Vector3 BigScale = new Vector3(1.2f, 1.2f, 1.2f);
    public float ScaleDuration = 0.2f;
    public float ScaleDelay = 0.4f;
    public float FadeOutDelay = 0.4f;
    public float FadeOutDuration = 0.4f;
    public float MoveAmount = 35;

    private void Awake()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
        Text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        FadeOut(false);
    }

    public void FadeOut(bool isCritical)
    {
        if (isCritical)
        {
            Text.color = Color.red;
            Text.rectTransform.localPosition = new Vector2(0, 40);
        }
        else
        {
            Text.color = Color.white;
            Text.rectTransform.localPosition = new Vector2(0, 25);
        }

        CanvasGroup.alpha = 1;

        LeanTween.scale(gameObject, BigScale, ScaleDuration).setOnComplete(ScaleToNormal);
    }

    private void ScaleToNormal()
    {
        LeanTween.scale(gameObject, Vector3.one, ScaleDuration).setDelay(ScaleDelay).setOnComplete(FadeOutText);
    }

    private void FadeOutText()
    {
        LeanTween.alphaCanvas(CanvasGroup, 0, FadeOutDuration).setDelay(FadeOutDelay);
        LeanTween.moveLocalY(gameObject, 25 + MoveAmount, FadeOutDuration).setDelay(FadeOutDelay).setOnComplete(Disable);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
