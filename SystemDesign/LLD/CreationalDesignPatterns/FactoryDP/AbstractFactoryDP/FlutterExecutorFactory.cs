namespace SystemDesign.LLD.CreationalDesignPatterns.FactoryDP.AbstractFactoryDP
{
    public class FlutterExecutorFactory
    {
        public static FlutterExecutor GetFlutterExecutor(string platform)
        {
            switch(platform.ToLower())
            {
                case "android":
                    return new AndroidFlutterExecutor();
                case "ios":
                    return new IOSFlutterExecutor();
                default:
                    throw new ArgumentException("Invalid platform");
            }
        }
    }
}
