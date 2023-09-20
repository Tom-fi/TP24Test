using System.Globalization;

using TP24.Api.Common;
using TP24.Domain.Entities;
using TP24.Domain.Interfaces;

namespace TP24.Api.Summaries
{
    public struct InvoiceValues
    {
        public string CurrencyCode { get; set; }
        public decimal Paid { get; set; }
        public decimal Opening { get; set; }
        public decimal Remaining { get => Opening - Paid;}
    }
    public class SummaryService : BaseService
    {
        public SummaryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            ///
        }

        //The return types here needs to be heavily refactored, but I'm a bit out of time.
        public async Task<Dictionary<string, Dictionary<string, List<Invoice>>>> GetSummaryAsync()
        {
            var invoiceRepository = UnitOfWork.AsyncRepository<Invoice>();
            var invoices = await invoiceRepository.ListAsync(x => true);

            var response = GroupInvoicesByCloseDateAndCurrency(invoices);

            return response;
        }

        public async Task<Dictionary<string, List<InvoiceValues>>> GetSumsAsync()
        {
            var invoiceRepository = UnitOfWork.AsyncRepository<Invoice>();
            var invoices = await invoiceRepository.ListAsync(x => true);

            var response = SumByOpenClosedAndCurrency(invoices);

            return response;
        }

        public Dictionary<string, List<Invoice>> GroupByClosedDate(List<Invoice> invoices)
        {
            var openClosedInvoices = invoices.GroupBy(x => isOpenOrClosedInvoice(x.ClosedDate)).ToDictionary(g => g.Key, g => g.ToList());
            return openClosedInvoices;
        }

        public Dictionary<string, List<Invoice>> GroupByCurrencyCode(List<Invoice> invoices)
        {
            var currencyCodeInvoices = invoices.GroupBy(x => x.CurrencyCode).ToDictionary(g => g.Key, g => g.ToList());
            return currencyCodeInvoices;
        }

        public List<InvoiceValues> SumByCurrencyCode(List<Invoice> invoices)
        {
            var invoiceSums = invoices.GroupBy(x => x.CurrencyCode).Select(g => new InvoiceValues
            {
                CurrencyCode = g.Key,
                Paid = g.Sum(c => c.PaidValue),
                Opening = g.Sum(c => c.OpeningValue)
            });
            return invoiceSums.ToList();
        }

        public Dictionary<string, Dictionary<string, List<Invoice>>> GroupInvoicesByCloseDateAndCurrency(List<Invoice> invoices)
        {
            var openClosedInvoices = GroupByClosedDate(invoices);
            //var currencyCodeInvoices = GroupByCurrencyCode(invoices);

            var openClosedCurrencyCodeInvoices = new Dictionary<string, Dictionary<string, List<Invoice>>>();
            foreach (var openClosedInvoice in openClosedInvoices)
            {
                var currencyCodeInvoices = GroupByCurrencyCode(openClosedInvoice.Value);
                openClosedCurrencyCodeInvoices.Add(openClosedInvoice.Key, currencyCodeInvoices);
            }

            return openClosedCurrencyCodeInvoices;
        }

        public Dictionary<string, List<InvoiceValues>> SumByOpenClosedAndCurrency(List<Invoice> invoices) 
        {
            var openClosedInvoices = GroupByClosedDate(invoices);
            //var currencyCodeInvoices = GroupByCurrencyCode(invoices);

            var openClosedCurrencyCodeInvoices = new Dictionary<string, List<InvoiceValues>>();
            foreach (var openClosedInvoice in openClosedInvoices)
            {
                var currencyCodeInvoices = SumByCurrencyCode(openClosedInvoice.Value);
                openClosedCurrencyCodeInvoices.Add(openClosedInvoice.Key, currencyCodeInvoices);
            }

            return openClosedCurrencyCodeInvoices;
        }

        public string isOpenOrClosedInvoice(string closedDate)
        {
            if (string.IsNullOrEmpty(closedDate))
            {
                return "Open";
            }
            else
            {
                return "Closed";
            }
        }

        public bool isOverdue(string dueDate)
        {
            if (DateTime.ParseExact(dueDate,"yyyy-MM-dd", CultureInfo.InvariantCulture) < DateTime.Now)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
