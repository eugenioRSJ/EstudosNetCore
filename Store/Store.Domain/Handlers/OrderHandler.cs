using System.Linq;
using Flunt.Notifications;
using Store.Domain.Commands;
using Store.Domain.Commands.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Handlers.Interfaces;
using Store.Domain.Repositories;

namespace Store.Domain.Handlers
{
    public class OrderHandler : Notifiable, IHandler<CreateOrderCommand>
    {

        private readonly ICustomerRepository _customerRepository;
        private readonly IDeliveryFeeRepository _deliveryFeeRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderHandler(ICustomerRepository customerRepository, IDeliveryFeeRepository deliveryFeeRepository, IDiscountRepository discountRepository, IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _deliveryFeeRepository = deliveryFeeRepository;
            _discountRepository = discountRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public ICommandResult Handle(CreateOrderCommand command)
        {
            command.Validate();
            if(command.Invalid)
                return new GenericCommandResult(false, "Invalid", command.Notifications);
            //Recuperar cliente
            var customer = _customerRepository.Get(command.Customer);
            
            //Calcular taxa de entrega
            var deliveryFee = _deliveryFeeRepository.Get(command.ZipCode);
            
            //Obter cupom de descontos
            var discount = _discountRepository.Get(command.PromoCode);

            //gerarPedido
            var products = _productRepository.Get(ExtractGuids.Extract(command.Items)).ToList();
            var order = new Order(customer, deliveryFee, discount);
            foreach(var item in command.Items)
            {
                var product = products.Where(x=> x.Id == item.Product).FirstOrDefault();
                order.AddItem(product, item.Quantity);
            }

            //Agrupar as notificações
            AddNotifications(order.Notifications);

            if(Invalid)
                return new GenericCommandResult(false, "Falha ao gerar pedido", Notifications);
            
            _orderRepository.Save(order);
            return new GenericCommandResult(true, $"Pedido {order.Number} gerado com sucesso", order);
        }
    }
}