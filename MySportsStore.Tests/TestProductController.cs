using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MySportsStore.IBLL;
using MySportsStore.Model;
using MySportsStore.WebUI.Controllers;

namespace MySportsStore.Tests
{
    [TestClass]
    public class TestProductController
    {
        //[TestMethod]
        //public void Can_Paginate()
        //{
        //    //Arrange
        //    Mock<IProductService> mock = new Mock<IProductService>();

        //    int totalCount = 0;
        //    mock.Setup(m => m.LoadPageEntities(p => true,p => p.Id,4,1,out totalCount,true)).Returns(new Product[]
        //        {
        //            new Product() {Id = 1, Name = "P1", Price = 10M},
        //            new Product() {Id = 2, Name = "P2", Price = 15M},
        //            new Product() {Id = 3, Name = "P3", Price = 20M},
        //            new Product() {Id = 4, Name = "P4", Price = 25M},
        //            new Product() {Id = 5, Name = "P5", Price = 30M}
        //        }.AsQueryable());

        //    ProductController controller = new ProductController();
        //    controller.PageSize = 3;
        //    controller.ProductService = mock.Object;

        //    //Action
        //    IEnumerable<Product> result = (IEnumerable<Product>)controller.List(2).Model;

        //    //Assert
        //    Product[] prodArray = result.ToArray();
        //    Assert.IsTrue(prodArray.Length == 2);
        //    Assert.AreEqual(prodArray[0].Name, "4");
        //}
    }
}
