namespace Insta.Portable.Models
{
    public class InstaResponse<T> where T : class
    {
        public T Response { get; set; }
        public Error Error { get; set; }
    }
}
