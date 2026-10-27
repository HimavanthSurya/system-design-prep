namespace SystemDesign.LLD.CreationalDesignPatterns.FactoryDP.AbstractFactoryDP
{
    public class AndroidFLutterExecutorUIComponentFactory : FlutterExecutorUIComponentFactory
    {
        public Button CreateButton()
        {
            return new AndroidButton();
        }
        public Dropdown CreateDropdown()
        {
            return new AndroidDropdown();
        }
    }
}
