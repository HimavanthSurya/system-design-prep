namespace SystemDesign.LLD.CreationalDesignPatterns.PrototypeDP
{
    public class Notebook
    {
        private PageType pageType;
        private int pageCount;
        private RuleType ruleType;
        private FrontPage frontPage;
        private FactPage factPage;

        public Notebook(PageType pageType, int pageCount, RuleType ruleType)
        {
            this.pageType = pageType;
            this.pageCount = pageCount;
            this.ruleType = ruleType;
        }

        public Notebook Clone()
        {
            Notebook clonedNotebook = new Notebook(this.pageType, this.pageCount, this.ruleType);

            return clonedNotebook;
        }

        public FrontPage getFrontPage()
        {
            return frontPage;
        }

        public void setFrontPage(FrontPage frontPage)
        {
            this.frontPage = frontPage;
        }

        public FactPage getFactPage()
        {
            return factPage;
        }

        public void setFactPage(FactPage factPage)
        {
            this.factPage = factPage;
        }

    }
}
