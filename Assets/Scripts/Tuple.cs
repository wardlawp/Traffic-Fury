public class Tuple<T1, T2>
{
    public T1 left { get; private set; }
    public T2 right { get; private set; }

    internal Tuple(T1 left, T2 right)
    {
        this.left = left;
        this.right = right;
    }
}
