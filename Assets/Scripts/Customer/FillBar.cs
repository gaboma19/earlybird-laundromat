using UnityEngine;

public class FillBar : MonoBehaviour
{
    public Transform fillImage;
    void Start()
    {
        // Default to an empty bar
        var newScale = fillImage.localScale;
        newScale.y = 0;
        fillImage.localScale = newScale;
    }

    public void SetFillBar(float fillAmount)
    {
        // Make sure value is between 0 and 1
        fillAmount = Mathf.Clamp01(fillAmount);
        // Scale the fillImage accordingly
        var newScale = fillImage.localScale;
        newScale.y = fillAmount;
        fillImage.localScale = newScale;
    }
}
