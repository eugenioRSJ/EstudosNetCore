using Flunt.Validations;

namespace Store.Domain.Entities
{
    public class OrderItem : Entity
    {
        public OrderItem(Product product, decimal quantity)
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .IsNotNull(product, "Product", "Produto inválido")
                    .IsGreaterThan(quantity, 0, "Quantity", "A quantidade deve ser maior que Zero")
            );
            Product = product;
            Price = Product != null ? Product.Price : 0;
            Quantity = quantity;
        }

        public Product Product { get; private set;} 
        public decimal Price { get; private set; }
        public decimal Quantity { get; private set; }  

        public decimal Total()
        {
            return Price * Quantity;
        }
    }
}