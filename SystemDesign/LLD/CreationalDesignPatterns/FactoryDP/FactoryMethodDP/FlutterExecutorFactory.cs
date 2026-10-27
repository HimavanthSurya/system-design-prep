namespace SystemDesign.LLD.CreationalDesignPatterns.FactoryDP.FactoryMethodDP
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
                    throw new ArgumentException("Invalid platform specified.");
            }
        }
    }



    public class ClientClass
    {
        public void Execute(string platform)
        {
            FlutterExecutor flutterExecutor = FlutterExecutorFactory.GetFlutterExecutor(platform);

            flutterExecutor.CreateButton();
        }
    }
}


// But if we add more create methods in the FlutterExecutor interface, we will have to modify the ClientClass as well. This violates the Open/Closed Principle. To fix this, we can use the Abstract Factory Design Pattern.