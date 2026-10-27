namespace SystemDesign.LLD.CreationalDesignPatterns.FactoryDP.AbstractFactoryDP
{
    public class AndroidFlutterExecutor : FlutterExecutor
    {
        public void StartUp()
        {
            Console.WriteLine("Android Flutter Executor Started");
        }
        public void Shutdown()
        {
            Console.WriteLine("Android Flutter Executor Shutdown");
        }

        public AndroidFLutterExecutorUIComponentFactory GetUIComponentFactory()
        {
            return new AndroidFLutterExecutorUIComponentFactory();
        }
    }
}
