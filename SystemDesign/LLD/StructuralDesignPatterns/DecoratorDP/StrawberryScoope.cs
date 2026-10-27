namespace SystemDesign.LLD.StructuralDesignPatterns.DecoratorDP
{
    public class StrawberryScoope : Cone
    {
        private readonly Cone cone;

        public StrawberryScoope(Cone cone)
        {
            if (cone == null)
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
            Console.WriteLine("Strawberry Scoope");
        }
    }



}
