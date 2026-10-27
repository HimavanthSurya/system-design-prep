namespace SystemDesign.LLD.CreationalDesignPatterns.FactoryDP.FactoryMethodDP
{
    public interface FlutterExecutor
    {
        public void StartUp();

        Button CreateButton();

        public void Shutdown();
    }
}
