namespace CR.Common.DTOs
{

    public class ResultDto<T>
    {
        public bool IsSuccess { get; set; }
        public AlertType Type { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public string dateTime { get; set; }
    }

    public class ResultDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public AlertType Type { get; set; }
    }

    public enum AlertType
    {
        success = 0,
        warning = 1,
        danger = 2,
        info = 3
    }
}
