using MySportsStore.IBLL;
using MySportsStore.Model;

namespace MySportsStore.BLL
{
    public class ProductService : BaseService<Product>, IProductService
    {
        public ProductService():base(){}

        public override bool SetCurrentRepository()
        {
            this.CurrentRepository = DbSessionContext.ProductRepository;
            this.AddDisposableObject(this.CurrentRepository);
            return true;
        }
    }
}