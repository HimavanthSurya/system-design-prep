namespace SystemDesign.LLD.CreationalDesignPatterns.FactoryDP.AbstractFactoryDP
{
    public interface FlutterExecutorUIComponentFactory
    {
        public Button CreateButton();
        public Dropdown CreateDropdown();
    }
}
