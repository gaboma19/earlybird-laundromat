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
        READY, LOADED, DONE
    }
    public TYPE type { get; }
    public COLOR color { get; }
    public STATE state;

    public Clothes()
    {
        type = GetRandomEnum<TYPE>();
        color = GetRandomEnum<COLOR>();
        state = STATE.READY;
    }

    static T GetRandomEnum<T>()
    {
        System.Array A = System.Enum.GetValues(typeof(T));
        T V = (T)A.GetValue(UnityEngine.Random.Range(0, A.Length));
        return V;
    }

    public Clothes(TYPE _type, COLOR _color)
    {
        type = _type;
        color = _color;
    }

}

