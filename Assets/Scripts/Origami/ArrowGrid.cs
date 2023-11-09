using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGrid : MonoBehaviour
{
    [SerializeField] private Transform grid;
    [SerializeField] private Transform arrowTemplate;
    [SerializeField] private Origami origami;

    private void Awake()
    {
        arrowTemplate.gameObject.SetActive(false);
    }

    void Start()
    {
        UpdateVisual();

        Origami.OnOrigamiStarted += UpdateVisual;
        Origami.OnInstructionCompleted += UpdateVisual;
        Origami.OnSequenceCompleted += UpdateVisual;
    }

    void UpdateVisual()
    {
        foreach (Transform child in grid)
        {
            if (child == arrowTemplate) continue;
            Destroy(child.gameObject);
        }

        List<Instruction> instructionList = origami.GetInstructionList();

        if (instructionList != null)
        {
            for (int i = 0; i < instructionList.Count; i++)
            {
                Transform arrowTransform = Instantiate(arrowTemplate, grid);
                arrowTransform.gameObject.SetActive(true);
                arrowTransform.GetComponent<Arrow>().SetArrow(instructionList[i]);
            }
        }
    }
}
