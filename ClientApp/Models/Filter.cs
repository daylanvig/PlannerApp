namespace ClientApp.Models
{
    public class Filter<T>
    {
        public T Model { get; set; }
        public bool IsVisible { get; set; }
    }
}
