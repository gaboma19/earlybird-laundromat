using UnityEngine;

public class Face : MonoBehaviour
{
    public Sprite normalFace;
    public Sprite smileFace;
    public Sprite crossFace;
    SpriteRenderer spriteRenderer;
    SpriteMask spriteMask;

    public void SetFace(float fillAmount)
    {
        fillAmount = Mathf.Clamp01(fillAmount);

        if (fillAmount < 0.33333)
        {
            ChangeSprite(TYPE.SMILE);
        }
        else if (fillAmount < 0.66666 && fillAmount > 0.33333)
        {
            ChangeSprite(TYPE.NORMAL);
        }
        else
        {
            ChangeSprite(TYPE.CROSS);
        }
    }

    enum TYPE
    {
        NORMAL, SMILE, CROSS
    }

    private void ChangeSprite(TYPE type)
    {
        switch (type)
        {
            case TYPE.NORMAL:
                spriteRenderer.sprite = normalFace;
                spriteMask.sprite = normalFace;
                break;
            case TYPE.SMILE:
                spriteRenderer.sprite = smileFace;
                spriteMask.sprite = smileFace;
                break;
            case TYPE.CROSS:
                spriteRenderer.sprite = crossFace;
                spriteMask.sprite = crossFace;
                break;
        }
    }

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteMask = gameObject.GetComponent<SpriteMask>();
    }
}
