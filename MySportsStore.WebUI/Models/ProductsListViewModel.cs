using System.Collections.Generic;
using MySportsStore.Model;

namespace MySportsStore.WebUI.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product>  Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}