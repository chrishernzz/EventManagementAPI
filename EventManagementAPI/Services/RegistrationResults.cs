using EventManagementAPI.Dtos;
namespace EventManagementAPI.Services;

//enum that shows different categories of errors that can happen whn registering
public enum RegistrationErrorType {
    None = 0,
    EventNotFound = 1,
    UserNotFound = 2,
    DuplicateRegistration = 3
}

//this class is like a throw exception but this will not do that it will just return the result if success or fail
public sealed class RegistrationResult {

    //creating a private constructor and only access here and takes in three parameters
    private RegistrationResult(RegistrationResponse? registration, RegistrationErrorType errorType, string? errorMessage) {
        Registration = registration;
        ErrorType = errorType;
        ErrorMessage = errorMessage;
    }


    //getters only that can only read the value
    public RegistrationResponse? Registration { get; }
    public RegistrationErrorType ErrorType { get; }
    public string? ErrorMessage { get; }

    //this will pass a success object and not pass the error message since it was success
    public static RegistrationResult Created(RegistrationResponse registration) {
        return new RegistrationResult(registration, RegistrationErrorType.None, null);
    }

    //this will pass an error and not pass the Response since it'll be null
    public static RegistrationResult Failure(RegistrationErrorType errorType, string errorMessage) {
        return new RegistrationResult(null, errorType, errorMessage);
    }
}
