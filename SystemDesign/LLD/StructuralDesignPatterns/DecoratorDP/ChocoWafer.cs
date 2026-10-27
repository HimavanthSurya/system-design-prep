namespace SystemDesign.LLD.StructuralDesignPatterns.DecoratorDP
{
    public class ChocoWafer : Cone
    {
        private readonly Cone cone;

        public ChocoWafer()
        {
            
        }

        public ChocoWafer(Cone cone)
        {
            if(cone == null)
            {
                throw new NullReferenceException(nameof(cone));
            }
            this.cone = cone;
        }

        public int getPrice()
        {
            int cost = Menu.ChocoWaferPrice;
            if(this.cone != null)
            {
                cost += this.cone.getPrice();
            }
            return cost;
        }
        public void printComposition()
        {
            if (this.cone != null)
            {
                this.cone.printComposition();
            }
            Console.WriteLine("Choco Wafer");
        }

        // When should we have constructor of Cone ?
        // When this particular class can add to some outer class, then we can have constructor of Cone in this class.
        // When not to have constructor of Cone ?
        // This itself is base class.
    }



}
