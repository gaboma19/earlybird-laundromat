using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClothesToken : MonoBehaviour
{
    [SerializeField] private Image shirtImage;
    [SerializeField] private Image pantsImage;
    [SerializeField] private Image pantiesImage;
    [SerializeField] private Image boxersImage;
    [SerializeField] private Image sockImage;
    [SerializeField] private Transform tokenContainer;
    Animator animator;

    public void AnimateLoad()
    {
        animator.SetTrigger("Up");
    }

    public void AnimateKeep()
    {
        animator.SetTrigger("Down");
    }

    public void SetClothes(Clothes clothes)
    {
        switch (clothes.type)
        {
            case Clothes.TYPE.SHIRT:
                InstantiateShirt(clothes);
                break;
            case Clothes.TYPE.PANTS:
                InstantiatePants(clothes);
                break;
            case Clothes.TYPE.PANTIES:
                InstantiatePanties(clothes);
                break;
            case Clothes.TYPE.BOXERS:
                InstantiateBoxers(clothes);
                break;
            case Clothes.TYPE.SOCKS:
                InstantiateSocks(clothes);
                break;
        }
    }
    private void InstantiateShirt(Clothes clothes)
    {
        switch (clothes.color)
        {
            case Clothes.COLOR.LIGHT:
                Instantiate(shirtImage, tokenContainer);
                break;
            case Clothes.COLOR.DARK:
                Instantiate(shirtImage, tokenContainer);
                break;
        }
    }
    private void InstantiatePants(Clothes clothes)
    {
        switch (clothes.color)
        {
            case Clothes.COLOR.LIGHT:
                Instantiate(pantsImage, tokenContainer);
                break;
            case Clothes.COLOR.DARK:
                Instantiate(pantsImage, tokenContainer);
                break;
        }
    }

    private void InstantiatePanties(Clothes clothes)
    {
        switch (clothes.color)
        {
            case Clothes.COLOR.LIGHT:
                Instantiate(pantiesImage, tokenContainer);
                break;
            case Clothes.COLOR.DARK:
                Instantiate(pantiesImage, tokenContainer);
                break;
        }
    }

    private void InstantiateBoxers(Clothes clothes)
    {
        switch (clothes.color)
        {
            case Clothes.COLOR.LIGHT:
                Instantiate(boxersImage, tokenContainer);
                break;
            case Clothes.COLOR.DARK:
                Instantiate(boxersImage, tokenContainer);
                break;
        }
    }

    private void InstantiateSocks(Clothes clothes)
    {
        switch (clothes.color)
        {
            case Clothes.COLOR.LIGHT:
                Instantiate(sockImage, tokenContainer);
                break;
            case Clothes.COLOR.DARK:
                Instantiate(sockImage, tokenContainer);
                break;
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
}
