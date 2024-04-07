namespace Ecommerce.Domain.Const
{
    public class ApiResponse<T> where T : class
    {
        public List<string> Messages { get; set; } = new List<string>();
        public bool Successed {  get; set; }
        public T? Response {  get; set; }
    }
}
