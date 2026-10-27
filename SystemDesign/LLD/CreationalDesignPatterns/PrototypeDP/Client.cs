using System.Net.NetworkInformation;

namespace SystemDesign.LLD.CreationalDesignPatterns.PrototypeDP
{
    public class Client
    {

        public static void Main(string[] args)
        {
            NotebookRegistry notebookRegistry = new NotebookRegistry();
            notebookRegistry.registerNotebook("a4_rule_120", new Notebook(PageType.A4, 120, RuleType.RULED));
            notebookRegistry.registerNotebook("a4_plain_120", new Notebook(PageType.A4, 120, RuleType.PLAIN));
            notebookRegistry.registerNotebook("a3_rule_100", new Notebook(PageType.A3, 100, RuleType.RULED));

            List<String> input = new List<String>() { "a4_rule_120", "a4_plain_120", "a3_rule_100" };


            List<Notebook> notebooks = new List<Notebook>();

            foreach(String notebookType in input)
            {
                Notebook prototype = notebookRegistry.getNotebook(notebookType);

                if (prototype != null)
                    continue;

                prototype.setFactPage(new FactPage());
                prototype.setFrontPage(new FrontPage());

                notebooks.Add(prototype);

            }


            //Read a list of Strings
            // a4_rule_120
            // a4_plain_120
            // a3_rule_100
            // 2. Based upon the type of srings create a relevant object
            // 3. Add a random front page and fact page to it
        }





    }
}
