namespace Interfaces
{
    public interface IInteractableObject
    {
        public string Name { get; set; }
        public void OnInteraction();
        public void OnApproaching();
        public void OnLeave();
    }
}