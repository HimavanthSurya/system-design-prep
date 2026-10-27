namespace SystemDesign.LLD.CreationalDesignPatterns.FactoryDP.NormalFactoryDP
{
    public class PaymentAPIFactory
    {
        public static IPaymentAPI GetPaymentAPI(string paymentType)
        {
            switch(paymentType.ToLower())
            {
                case "paypal":
                    return new PayPalPaymentAPI();
                case "stripe":
                    return new StripePaymentAPI();
                case "square":
                    return new SquarePaymentAPI();
                default:
                    throw new ArgumentException("Invalid payment type");
            }
        }
    }

    public class ClientClass
    {
        public void ProcesssPayment(string paymentType)
        {
            IPaymentAPI paymentAPI = PaymentAPIFactory.GetPaymentAPI(paymentType);
            //paymentAPI.ProcessPayment();
        }
    }
}
