namespace SchoolSystem.DTOs
{

    public class ResponseModel<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<T> Data { get; set; }
        public long TotalCount { get; set; }
        public int Status { get; set; }
    }
}
