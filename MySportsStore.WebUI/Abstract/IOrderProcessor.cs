using MySportsStore.Model;
using MySportsStore.WebUI.Models;

namespace MySportsStore.WebUI.Abstract
{
    public interface IOrderProcessor
    {
        void ProcessOrder(Cart cart, ShippingDetail shippingDetail);

    }
}