namespace SystemDesign.LLD.CreationalDesignPatterns.FactoryDP.AbstractFactoryDP
{
    public class IOSFlutterExecutor : FlutterExecutor
    {
        public void StartUp()
        {
            Console.WriteLine("IOS Flutter Executor Started");
        }
        public void Shutdown()
        {
            Console.WriteLine("IOS Flutter Executor Shutdown");
        }

        public IOSFlutterExecutorUIComponentFactory GetUIComponentFactory()
        {
            return new IOSFlutterExecutorUIComponentFactory();
        }
    }
}
