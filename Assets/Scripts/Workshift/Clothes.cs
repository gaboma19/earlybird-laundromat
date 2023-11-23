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
    public TYPE type { get; }
    public COLOR color { get; }
    public List<Instruction> foldingInstructions = new();

    public Clothes()
    {
        type = GetRandomEnum<TYPE>();
        color = GetRandomEnum<COLOR>();
        SetFoldingInstructions(type);
    }

    public Clothes(TYPE _type)
    {
        type = _type;
        color = GetRandomEnum<COLOR>();
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
        Instruction.DIRECTION previousDirection = Instruction.DIRECTION.UP;

        for (int i = 0; i < 4; i++)
        {
            Instruction.DIRECTION direction;

            // Keep generating a new random direction until it's different from the previous one
            do
            {
                direction = GetRandomEnum<Instruction.DIRECTION>();
            } while (direction == previousDirection);

            // Update the previous direction for the next iteration
            previousDirection = direction;

            foldingInstructions.Add(new Instruction(direction));
        }
    }
}
