using System.Collections.Generic;

public class Clothes
{
    public enum TYPE
    {
        SHIRT, PANTS, PANTIES, BOXERS, SOCKS
    }
    public enum COLOR
    {
        DARK, LIGHT
    }
    public enum STATE
    {
        READY, LOADED
    }
    public TYPE type { get; }
    public COLOR color { get; }
    public STATE state;
    public List<Instruction> foldingInstructions;

    public Clothes()
    {
        type = GetRandomEnum<TYPE>();
        color = GetRandomEnum<COLOR>();
        state = STATE.READY;
        SetFoldingInstructions(type);
    }

    public static T GetRandomEnum<T>()
    {
        System.Array A = System.Enum.GetValues(typeof(T));
        T V = (T)A.GetValue(UnityEngine.Random.Range(0, A.Length));
        return V;
    }

    private void SetFoldingInstructions(TYPE type)
    {
        switch (type)
        {
            case TYPE.SHIRT:
                foldingInstructions.Add(new Instruction(Instruction.DIRECTION.RIGHT));
                foldingInstructions.Add(new Instruction(Instruction.DIRECTION.LEFT));
                foldingInstructions.Add(new Instruction(Instruction.DIRECTION.UP));
                break;
            case TYPE.PANTS:
                foldingInstructions.Add(new Instruction(Instruction.DIRECTION.LEFT));
                foldingInstructions.Add(new Instruction(Instruction.DIRECTION.UP));
                foldingInstructions.Add(new Instruction(Instruction.DIRECTION.UP));
                break;
            case TYPE.PANTIES:
                foldingInstructions.Add(new Instruction(Instruction.DIRECTION.UP));
                foldingInstructions.Add(new Instruction(Instruction.DIRECTION.LEFT));
                foldingInstructions.Add(new Instruction(Instruction.DIRECTION.RIGHT));
                break;
            case TYPE.BOXERS:
                foldingInstructions.Add(new Instruction(Instruction.DIRECTION.RIGHT));
                foldingInstructions.Add(new Instruction(Instruction.DIRECTION.LEFT));
                foldingInstructions.Add(new Instruction(Instruction.DIRECTION.UP));
                break;
            case TYPE.SOCKS:
                foldingInstructions.Add(new Instruction(Instruction.DIRECTION.RIGHT));
                foldingInstructions.Add(new Instruction(Instruction.DIRECTION.ROTATE));
                foldingInstructions.Add(new Instruction(Instruction.DIRECTION.DOWN));
                break;
        }
    }
}

