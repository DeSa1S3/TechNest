namespace Backend.Middleware_Components.DTO
{
    public class TokenValidateResultDTO
    {
        public bool TokenHasError() => !string.IsNullOrEmpty(error_message);
        public bool TokenHasSuccess() => token_success != null;

        public string? error_message { get; set; }
        public TokenSuccessDTO? token_success { get; set; }
    }
}
