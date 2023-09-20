using Moq;
using System.Linq.Expressions;

using TP24.Api.Invoices;
using TP24.Api.Invoices.DTO;
using TP24.Domain.Entities;
using TP24.Domain.Interfaces;


namespace TP24.Tests.InvoiceTests
{
    public class InvoiceServiceTests
    {
        [Fact]
        public async Task AddInvoiceAsync_Invoice_Exists_Throws_Exception()
        {
            // Arrange
            var invoice = new AddInvoiceRequest();

            var mockUnitOfWork = new Mock<IUnitOfWork>();

            //For AddInvoiceAsync to throw an exception we need GetAsync to return an invoice object with Reference set;
            var stubInvoice = new Invoice
            {
                Reference = "123"
            };

            mockUnitOfWork.Setup(x => 
                x.AsyncRepository<Invoice>()
                    .GetAsync(It.IsAny<Expression<Func<Invoice, bool>>>()))
                    .ReturnsAsync(stubInvoice);

            var invoiceService = new InvoiceService(mockUnitOfWork.Object);

            // Act
            try
            {
                await invoiceService.AddNewAsync(invoice);

                //Fail test if it reaches this point
                Assert.Fail("Exception not thrown");
            }
            catch (ArgumentException ex)
            {
                Assert.Equal("Invoice already exists", ex.Message);
            }
        }

        [Fact]
        public async Task AddInvoiceAsync_Invoice_Does_Not_Exist()
        {
            // Arrange
            var invoice = new AddInvoiceRequest
            {
                Reference = "123",
            };

            var mockUnitOfWork = new Mock<IUnitOfWork>();

            //For AddInvoiceAsync to throw an exception we need GetAsync to return an invoice object with Reference set;
            var stubInvoice = new Invoice();

            mockUnitOfWork.Setup(x => 
                x.AsyncRepository<Invoice>()
                    .GetAsync(It.IsAny<Expression<Func<Invoice, bool>>>()))
                    .ReturnsAsync(stubInvoice);

            var invoiceService = new InvoiceService(mockUnitOfWork.Object);

            // Act
            try
            {
                var result = await invoiceService.AddNewAsync(invoice);

                Assert.Equal(invoice.Reference, result.Reference);
            }
            catch (ArgumentException)
            {
                // Assert
                Assert.Fail("Threw Exception: Invoice already exists");
            }
        }

        [Fact]
        public async Task GetAsync_Returns_Response()
        {
            // Arrange
            var reference = "123";

            var mockUnitOfWork = new Mock<IUnitOfWork>();

            //For AddInvoiceAsync to throw an exception we need GetAsync to return an invoice object
            var stubInvoice = new Invoice
            {
                Reference = reference
            };

            mockUnitOfWork.Setup(x => 
                x.AsyncRepository<Invoice>()
                    .GetAsync(It.IsAny<Expression<Func<Invoice, bool>>>()))
                    .ReturnsAsync(stubInvoice);

            var invoiceService = new InvoiceService(mockUnitOfWork.Object);

            // Act
            var result = await invoiceService.GetAsync(reference);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(reference, result.Reference);
        }

        [Fact]
        public async Task GetAsync_Returns_Empty_Object()
        {
            // Arrange
            var reference = "123";

            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var stubInvoice = new Invoice();

            mockUnitOfWork.Setup(x => 
                x.AsyncRepository<Invoice>()
                    .GetAsync(It.IsAny<Expression<Func<Invoice, bool>>>()))
                    .ReturnsAsync(stubInvoice);

            var invoiceService = new InvoiceService(mockUnitOfWork.Object);

            // Act
            var result = await invoiceService.GetAsync(reference);
            var isEmpty = string.IsNullOrEmpty(result.Reference);
            // Assert
            Assert.True(isEmpty);
        }

        [Fact]
        public async Task GetAllAsync_Returns_EmptyList()
        {
            // Arrange
            var stubInvoiceList = new List<Invoice>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(x => 
                x.AsyncRepository<Invoice>()
                    .ListAsync(It.IsAny<Expression<Func<Invoice, bool>>>()))
                    .ReturnsAsync(stubInvoiceList);

            var invoiceService = new InvoiceService(mockUnitOfWork.Object);

            // Act
            var result = await invoiceService.GetAllAsync();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllAsync_Returns_List()
        {
            // Arrange
            var stubInvoiceList = new List<Invoice>
            {
                new Invoice(),
                new Invoice()
            };
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(x => 
                x.AsyncRepository<Invoice>()
                    .ListAsync(It.IsAny<Expression<Func<Invoice, bool>>>()))
                    .ReturnsAsync(stubInvoiceList);

            var invoiceService = new InvoiceService(mockUnitOfWork.Object);

            // Act
            var result = await invoiceService.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count);
        }

    }
}