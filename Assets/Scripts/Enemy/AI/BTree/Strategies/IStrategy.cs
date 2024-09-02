namespace AI.Btree
{
    public interface IStrategy
    {
        Node.Status Process();

        void Reset()
        {
            //noop
        }
    }
}