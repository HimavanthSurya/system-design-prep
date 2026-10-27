namespace SystemDesign.LLD.CreationalDesignPatterns.FactoryDP.AbstractFactoryDP
{
    public class IOSFlutterExecutorUIComponentFactory : FlutterExecutorUIComponentFactory
    {
        public Button CreateButton()
        {
            return new IOSButton();
        }
        public Dropdown CreateDropdown()
        {
            return new IOSDropdown();
        }
    }
}
