namespace Nocturne.BL.Helpers
{
    public class ValidationErrorMessage : IValidationMessage
    {
        public string Message { get; private set; }

        public ValidationErrorMessage(string message)
        {
            Message = message;
        }
    }
}
