namespace SpaceEngineers.Persistence.Errors;
internal class SpaceEngineersPersistenceException(string? message, Exception? innerException) : Exception(message, innerException);
