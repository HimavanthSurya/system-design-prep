namespace SystemDesign.LLD.StructuralDesignPatterns.DecoratorDP
{
    public class Client
    {
        public static void main(string[] args)
        {
            Cone cone = new ChocoWafer();
            cone = new ChocoScoope(cone);
            cone = new StrawberryScoope(cone);
            cone.printComposition();
            Console.WriteLine("Total Price: " + cone.getPrice());
        }
    }



}
