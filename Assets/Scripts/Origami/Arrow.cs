using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    [SerializeField] private Image instructionArrow;
    [SerializeField] private Image instructionRotateArrow;
    [SerializeField] private Transform arrowContainer;

    public void SetArrow(Instruction instruction)
    {
        Image arrow;

        if (!instruction.isCompleted)
        {
            switch (instruction.direction)
            {
                case Instruction.DIRECTION.UP:
                    Instantiate(instructionArrow, arrowContainer);
                    break;
                case Instruction.DIRECTION.DOWN:
                    arrow = Instantiate(instructionArrow, arrowContainer);
                    arrow.transform.Rotate(0, 0, 180f);
                    break;
                case Instruction.DIRECTION.LEFT:
                    arrow = Instantiate(instructionArrow, arrowContainer);
                    arrow.transform.Rotate(0, 0, 90f);
                    break;
                case Instruction.DIRECTION.RIGHT:
                    arrow = Instantiate(instructionArrow, arrowContainer);
                    arrow.transform.Rotate(0, 0, -90f);
                    break;
                case Instruction.DIRECTION.ROTATE:
                    Instantiate(instructionRotateArrow, arrowContainer);
                    break;
            }
        }
    }
}
