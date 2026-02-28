using EventManagementAPI.Dtos;
namespace EventManagementAPI.Services;

public enum RegistrationErrorType {
    None = 0,
    EventNotFound = 1,
    UserNotFound = 2,
    DuplicateRegistration = 3
}

public sealed class RegistrationResult{
    private RegistrationResult(RegistrationResponse? registration, RegistrationErrorType errorType, string? errorMessage) {
        Registration = registration;
        ErrorType = errorType;
        ErrorMessage = errorMessage;
    }

    public RegistrationResponse? Registration { get; }
    public RegistrationErrorType ErrorType { get; }
    public string? ErrorMessage { get; }

    public static RegistrationResult Created(RegistrationResponse registration) {
        return new RegistrationResult(registration, RegistrationErrorType.None, null);
    }

    public static RegistrationResult Failure(RegistrationErrorType errorType, string errorMessage) {
        return new RegistrationResult(null, errorType, errorMessage);
    }
}
