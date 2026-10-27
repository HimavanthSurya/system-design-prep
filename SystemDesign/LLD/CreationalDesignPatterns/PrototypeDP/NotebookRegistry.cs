namespace SystemDesign.LLD.CreationalDesignPatterns.PrototypeDP
{
    public class NotebookRegistry
    {
        Dictionary<string, Notebook> notebookRegistry = new Dictionary<string, Notebook>();

        public void registerNotebook(string key, Notebook notebook)
        {
            notebookRegistry.Add(key, notebook);
        }

        public Notebook getNotebook(string key)
        {
            return notebookRegistry[key].Clone();
        }
    }
}
