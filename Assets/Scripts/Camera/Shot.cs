using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour
{
    public void CutToShot()
    {
        Camera.main.transform.localPosition = transform.position;
        Camera.main.transform.localRotation = transform.rotation;
    }
}
