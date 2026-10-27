namespace SystemDesign.LLD.CreationalDesignPatterns.FactoryDP.FactoryMethodDP
{
    public class IOSFlutterExecutor : FlutterExecutor
    {
        public void StartUp()
        {
            Console.WriteLine("IOSFlutterExecutor StartUp");
        }
        public Button CreateButton()
        {
            return new IOSButton();
        }
        public void Shutdown()
        {
            Console.WriteLine("IOSFlutterExecutor Shutdown");
        }
    
    }
}
