namespace Interfaces
{
    public interface IVisitable
    {
        public void Accept(IVisitor visitor);
        public void Accept(IStatsVisitor visitor);
    }
}