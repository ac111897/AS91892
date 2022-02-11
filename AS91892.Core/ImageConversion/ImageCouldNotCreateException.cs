namespace AS91892.Core.ImageConversion;


[Serializable]
public class ImageCouldNotBeCreatedException : Exception
{
    public ImageCouldNotBeCreatedException() { }
    public ImageCouldNotBeCreatedException(string message) : base(message) { }
    public ImageCouldNotBeCreatedException(string message, Exception inner) : base(message, inner) { }
    protected ImageCouldNotBeCreatedException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
