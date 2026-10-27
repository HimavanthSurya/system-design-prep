namespace SystemDesign.LLD.CreationalDesignPatterns.FactoryDP.AbstractFactoryDP
{
    public class FlutterExecutorClient
    {
        private FlutterExecutor _flutterExecutor;
        private FlutterExecutorUIComponentFactory _uiComponentFactory;
        public FlutterExecutorClient(string platform)
        {
            _flutterExecutor = FlutterExecutorFactory.GetFlutterExecutor(platform);
            _uiComponentFactory = platform.ToLower() == "android" ? 
                new AndroidFLutterExecutorUIComponentFactory() : 
                new IOSFlutterExecutorUIComponentFactory();
        }
        public void StartUp()
        {
            _flutterExecutor.StartUp();
            Button button = _uiComponentFactory.CreateButton();
            Dropdown dropdown = _uiComponentFactory.CreateDropdown();
            Console.WriteLine($"Created {button.GetType().Name} and {dropdown.GetType().Name}");
        }
        public void Shutdown()
        {
            _flutterExecutor.Shutdown();
        }
    }
}
