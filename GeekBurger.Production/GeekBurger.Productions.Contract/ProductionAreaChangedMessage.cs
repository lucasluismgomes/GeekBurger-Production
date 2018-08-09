namespace GeekBurger.Productions.Contract
{
    public class ProductionAreaChangedMessage
    {
        public ProductionAreaState State { get; set; }
        public ProductionAreaToGet ProductionArea { get; set; }
    }

    public enum ProductionAreaState
    {
        Deleted = 2,
        Modified = 3,
        Added = 4
    }
}
