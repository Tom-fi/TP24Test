using Moq;
using System.Linq.Expressions;

using TP24.Api.Summaries;
using TP24.Api.Invoices.DTO;
using TP24.Domain.Entities;
using TP24.Domain.Interfaces;


namespace TP24.Tests.SummaryTests
{
    public class SummaryServiceTests
    {
        [Theory]
        [InlineData("2020-10-10")]
        [InlineData("2022-01-01")]
        [InlineData("2020-02-29")]
        public async Task isOverDue(string overdueDate)
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var service = new SummaryService(mockUnitOfWork.Object);

            // Act
            var result = service.isOverdue(overdueDate);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(180)]
        [InlineData(365)]
        public async Task isNotOverDue(int addDays)
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var service = new SummaryService(mockUnitOfWork.Object);

            // Act
            var result = service.isOverdue(DateTime.Now.AddDays(addDays).ToString("yyyy-MM-dd"));

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task isOpenInvoice()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var service = new SummaryService(mockUnitOfWork.Object);

            // Act
            var result = service.isOpenOrClosedInvoice("");

            // Assert
            Assert.Equal("Open", result);
        }

        [Fact]
        public async Task isClosedInvoice()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var service = new SummaryService(mockUnitOfWork.Object);

            // Act
            var result = service.isOpenOrClosedInvoice("2020-10-10");

            // Assert
            Assert.Equal("Closed", result);
        }

        [Fact]
        public async Task SumInvoicesWithOneCurrency()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var service = new SummaryService(mockUnitOfWork.Object);

            var stubInvoiceList = new List<Invoice>
            {
                new Invoice
                {
                    OpeningValue = 100.50M,
                    PaidValue = 50.50M,
                    CurrencyCode = "EUR"
                },
                new Invoice
                {
                    OpeningValue = 100.49M,
                    PaidValue = 50.49M,
                    CurrencyCode = "EUR"
                }
            };

            // Act
            var result = service.SumByCurrencyCode(stubInvoiceList);

            // Assert
            Assert.Equal(200.99M, result[0].Opening);
            Assert.Equal(100.99M, result[0].Paid);
            Assert.Equal("EUR", result[0].CurrencyCode);
        }

        //[Fact]
        //TODO:: Sum multiple invoices with different currencies

        [Fact]
        public async Task GroupByCurrencies()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var service = new SummaryService(mockUnitOfWork.Object);

            var stubInvoiceList = new List<Invoice>
            {
                new Invoice
                {
                    OpeningValue = 100.50M,
                    PaidValue = 50.50M,
                    CurrencyCode = "EUR"
                },
                new Invoice
                {
                    OpeningValue = 100.49M,
                    PaidValue = 50.49M,
                    CurrencyCode = "EUR"
                },
                new Invoice
                {
                    OpeningValue = 1.5M,
                    PaidValue = 5.5M,
                    CurrencyCode = "USD"
                }
            };

            // Act
            var result = service.GroupByCurrencyCode(stubInvoiceList);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(2, result["EUR"].Count);
            Assert.Single(result["USD"]);
        }

        [Fact]
        public async Task GroupByClosedDate()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var service = new SummaryService(mockUnitOfWork.Object);

            //ClosedDate if empty or null is considered as Open
            var stubInvoiceList = new List<Invoice>
            {
                new Invoice
                {
                    ClosedDate = "2020-10-10"
                },
                new Invoice
                {
                    ClosedDate = "2020-15-10"
                },
                new Invoice
                {
                    ClosedDate = ""
                }
            };

            // Act
            var result = service.GroupByClosedDate(stubInvoiceList);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(2, result["Closed"].Count);
            Assert.Single(result["Open"]);
        }

        //[Fact]
        //TODO:: GroupInvoicesByCloseDateAndCurrency
        //[Fact]
        //TODO:: SumByOpenClosedAndCurrency
    }
}
