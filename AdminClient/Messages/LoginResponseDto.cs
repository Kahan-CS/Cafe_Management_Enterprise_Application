namespace AdminClient.Messages
{
    public class LoginResponseDto
    {
        public bool Success { get; set; }
        public string? Token { get; internal set; }
        public string Message { get; set; } = string.Empty;
    }
}
