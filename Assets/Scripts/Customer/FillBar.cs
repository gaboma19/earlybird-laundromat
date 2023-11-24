using UnityEngine;

public class FillBar : MonoBehaviour
{
    public Transform fillImage;
    void Start()
    {
        // Default to an empty bar
        var newScale = this.fillImage.localScale;
        newScale.y = 0;
        this.fillImage.localScale = newScale;
    }

    public void SetFillBar(float fillAmount)
    {
        // Make sure value is between 0 and 1
        fillAmount = Mathf.Clamp01(fillAmount);
        // Scale the fillImage accordingly
        var newScale = this.fillImage.localScale;
        newScale.y = fillAmount;
        this.fillImage.localScale = newScale;
    }
}
