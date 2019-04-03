namespace effectively.tests.ExtractInterface {
    using effectively.ExtractInterface;
    using FakeItEasy;
    using NUnit.Framework;

    [TestFixture]
    public class The_invoice_service 
    {
        [TestFixture]
        public class when_getting_an_invoice 
        {
            private ISetResponseCode setterMock;
            private InvoiceService invoiceService;
            private InvoiceDto invoice;

            [SetUp]
            public void Setup()
            {
                this.Arrange();
                this.Act();
            }

            [Test]
            public void Then_the_id_is_assigned_to_the_invoice()
            {
                Assert.AreEqual(5, this.invoice.Id);
            }

            public void Then_ok_status_was_assigned()
            {
                A.CallTo(() => this.setterMock.SetStatusCode(200)).MustHaveHappenedOnceExactly();

            }

            private void Arrange()
            {
                this.setterMock = A.Fake<ISetResponseCode>();
                this.invoiceService = new InvoiceService(setterMock);
            }

            private void Act()
            {
                this.invoice = this.invoiceService.GetInvoice(5);
            }
        }
    }
}