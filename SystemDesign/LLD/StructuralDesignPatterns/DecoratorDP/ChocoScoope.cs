namespace SystemDesign.LLD.StructuralDesignPatterns.DecoratorDP
{
    public class ChocoScoope : Cone
    {
        private readonly Cone cone;

        public ChocoScoope(Cone cone)
        {
            if(cone == null)
            {
                throw new NullReferenceException(nameof(cone));
            }
            this.cone = cone;
        }

        public int getPrice()
        {
            return this.cone.getPrice() + 50;
        }

        public void printComposition()
        {
            this.cone.printComposition();
            Console.WriteLine("Choco Scoope");
        }
    }



}
