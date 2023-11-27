using UnityEngine;

public class FillBar : MonoBehaviour
{
    public enum DIRECTION
    {
        X, Y
    }
    public DIRECTION direction;

    public Transform fillImage;
    void Start()
    {
        // Default to an empty bar
        var newScale = fillImage.localScale;
        switch (direction)
        {
            case DIRECTION.Y:
                newScale.y = 0;
                break;
            case DIRECTION.X:
                newScale.x = 0;
                break;
        }

        fillImage.localScale = newScale;
    }

    public void SetFillBar(float fillAmount)
    {
        // Make sure value is between 0 and 1
        fillAmount = Mathf.Clamp01(fillAmount);
        // Scale the fillImage accordingly
        var newScale = fillImage.localScale;

        switch (direction)
        {
            case DIRECTION.Y:
                newScale.y = fillAmount;
                break;
            case DIRECTION.X:
                newScale.x = fillAmount;
                break;
        }

        fillImage.localScale = newScale;
    }
}
