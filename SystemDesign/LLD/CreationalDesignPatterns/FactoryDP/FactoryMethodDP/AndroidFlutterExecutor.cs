namespace SystemDesign.LLD.CreationalDesignPatterns.FactoryDP.FactoryMethodDP
{
    public class AndroidFlutterExecutor : FlutterExecutor
    {
        public void StartUp()
        {
            Console.WriteLine("Android Flutter Executor Starting Up");
        }
        public Button CreateButton()
        {
            return new AndroidButton();
        }
        public void Shutdown()
        {
            Console.WriteLine("Android Flutter Executor Shutting Down");
        }
    }
}
