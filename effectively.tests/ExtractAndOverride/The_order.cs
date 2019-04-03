namespace effectively.tests.ExtractAndOverride
{
    using NUnit.Framework;
    using effectively.ExtractAndOverride;

    [TestFixture]
    public class The_order
    {
        protected Order Order { get; set; }

        [TestFixture]
        public class Given_The_Order_Amount_Is_Negative : And_UserService_User_Is_Valid
        {
            private readonly int NegativeAmount = -5;

            [SetUp]
            public void ArrangeOrderWithNegativeAmount()
            {
                this.OrderService = new TestOrderServiceWithValidUser();
                this.Order = new Order()
                {
                    Amount = this.NegativeAmount
                };
            }

            [Test]
            public void Then_The_Order_Amount_Is_Negative()
            {
                Assert.IsTrue(this.Order.Amount < 0);
            }

            [Test]
            public void Then_It_Will_Be_Rejected()
            {
                var orderResult = this.OrderService.Add(this.Order);

                Assert.False(orderResult);
            }
        }

        [TestFixture]
        public class Given_The_Order_Amount_Is_Positive : And_UserService_User_Is_Valid
        {
            private readonly int PositiveAmount = 5;

            [SetUp]
            public void ArrangeOrderWithPositiveAmount()
            {
                this.Order = new Order() { Amount = this.PositiveAmount };
            }

            [Test]
            public void Then_The_Order_Amount_Is_Positive()
            {
                Assert.IsTrue(this.Order.Amount > 0);
            }

            [Test]
            public void Then_The_Order_Will_Be_Accepted()
            {
                var orderResult = this.OrderService.Add(this.Order);
                Assert.IsTrue(orderResult); 
            }
        }

        #region Test Objects

        public abstract class And_UserService_User_Is_Valid : The_order
        {
            protected TestOrderServiceWithValidUser OrderService {get; set;}

            [SetUp]
            public void SetupUserService()
            {
                this.OrderService = new TestOrderServiceWithValidUser();
            }

            [Test]
            public void Then_The_User_Is_Vald()
            {
                var userServiceResult = this.OrderService.ExposeGetUserServiceCall();
                Assert.IsTrue(userServiceResult.IsValidUser);
            }

        }

        public class TestOrderServiceWithValidUser : OrderService
        {
            public UserService ExposeGetUserServiceCall() => this.GetUserService();

            protected override UserService GetUserService()
            {
                return new UserService() { IsValidUser = true };
            }
        }
        #endregion
    }
}