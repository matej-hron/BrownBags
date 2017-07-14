using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace BBUnitTesting
{
    [TestFixture]
    public class Tests
    {
        // why
        // ----- 
        // allows refactoring
        // regression
        // document code, example of usage
        // developers ensure functionality
        // helps to design prod code


        // good unit tests should:
        // structure AAA (Arrange Act Assert)
        // test one thing
        // readable
        // well named - clear
        // easy to write no logic
        // first class code
        // decoupled to prod code
        // independent on each other
        // fast to run
        // test coverage (app logic)
            // code coverage
            // case coverage
        // reliable

        // how to write tests
        //---------------------

        [Test]
        public void ShouldSumOrdersAmount()
        {
            var repositoryMock = new Mock<IOrderRepository>();
            repositoryMock.Setup(m => m.GetList()).Returns(new[] {50, 50}.Select(x => new Order(x)));
            var notifier = Mock.Of<INotifier>();

            var sut = new OrdersStatisticCalculator(repositoryMock.Object, notifier);
            var res = sut.Sum();

            Assert.That(res, Is.EqualTo(100));
        }

        [Test]
        public void ShouldSumOrdersAmount2()
        {
            var repository = Mock.Of<IOrderRepository>(r => r.GetList() == GetOrders());
            var notifier = Mock.Of<INotifier>();

            var sut = new OrdersStatisticCalculator(repository, notifier);
            var res = sut.Sum();

            Assert.That(res, Is.EqualTo(100));
        }

        [Test]
        public void ShouldSumOrdersAmountWhenThereAreNoOrders()
        {
            var repository = Mock.Of<IOrderRepository>(r => r.GetList() == GetOrdersEmpty());
            var notifier = Mock.Of<INotifier>();

            var sut = new OrdersStatisticCalculator(repository, notifier);
            var res = sut.Sum();

            Assert.That(res, Is.EqualTo(0));
        }

        [Test]
        public void UserIsNotifiedWhenSumIsCalculated()
        {
            var repository = Mock.Of<IOrderRepository>(r => r.GetList() == GetOrdersEmpty());
            var notifier = Mock.Of<INotifier>();

            var sut = new OrdersStatisticCalculator(repository, notifier);
            var res = sut.Sum();

            Mock.Get(notifier).Verify(m => m.Notify(res), Times.Once);
        }

        [Test]
        public void TestAF()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var orders = fixture.Create<List<Order>>();

            var orderRepoMock = fixture.Freeze<Mock<IOrderRepository>>();
            orderRepoMock.Setup(m => m.GetList()).Returns(orders);

            var sut = fixture.Create<OrdersStatisticCalculator>();

            var res = sut.Sum();

            Assert.That(res, Is.EqualTo(100));
        }

        private IEnumerable<Order> GetOrders() => new[] {50, 50}.Select(x => new Order(x));
        private IEnumerable<Order> GetOrdersEmpty() => Enumerable.Empty<Order>();
    }




    public class OrdersStatisticCalculator
    {
        private readonly IOrderRepository repository;
        private readonly INotifier notifier;

        public OrdersStatisticCalculator(IOrderRepository repository, INotifier notifier)
        {
            this.repository = repository;
            this.notifier = notifier;
        }

        public int Sum()
        {
            var res = repository.GetList().Sum(x => x.Amount);
            notifier.Notify(res);
            return res;
        }
    }

    public interface IOrderRepository
    {
        IEnumerable<Order> GetList();
    }

    public interface INotifier
    {
        void Notify(int result);
    }

    public class Order
    {
        public int Amount { get; set; }

        public Order(int amt)
        {
            Amount = amt;
        }
    }
}
