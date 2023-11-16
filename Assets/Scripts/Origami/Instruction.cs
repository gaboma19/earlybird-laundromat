public class Instruction
{
    public enum DIRECTION
    {
        UP, DOWN, LEFT, RIGHT
    }
    public DIRECTION direction { get; }
    public bool isCompleted = false;

    public Instruction(DIRECTION _direction)
    {
        direction = _direction;
    }
}
