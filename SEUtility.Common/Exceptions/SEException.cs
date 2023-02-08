namespace SEUtility.Common.Exceptions;

[Serializable]
public class SEException : Exception
{
    public SEException() { }
    public SEException(string message) : base(message) { }
    public SEException(string message, Exception inner) : base(message, inner) { }
    protected SEException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
