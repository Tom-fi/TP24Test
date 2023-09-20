using TP24.Api.Common;
using TP24.Api.Invoices.DTO;
using TP24.Domain.Entities;
using TP24.Domain.Interfaces;

namespace TP24.Api.Invoices
{
    public class InvoiceService : BaseService
    {
        public InvoiceService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            ///
        }
        public async Task<AddInvoiceResponse> AddNewAsync(AddInvoiceRequest model)
        {
            var invoice = new Invoice
            {
                Reference = model.Reference,
                CurrencyCode = model.CurrencyCode,
                IssueDate = model.IssueDate,
                OpeningValue = model.OpeningValue,
                PaidValue = model.PaidValue,
                DueDate = model.DueDate,
                ClosedDate = model.ClosedDate,
                Cancelled = model.Cancelled,
                DebtorName = model.DebtorName,
                DebtorReference = model.DebtorReference,
                DebtorAddress1 = model.DebtorAddress1,
                DebtorAddress2 = model.DebtorAddress2,
                DebtorTown = model.DebtorTown,
                DebtorState = model.DebtorState,
                DebtorZip = model.DebtorZip,
                DebtorCountryCode = model.DebtorCountryCode,
                DebtorRegistrationNumber = model.DebtorRegistrationNumber
            };
            var repository = UnitOfWork.AsyncRepository<Invoice>();

            var existingInvoice = await repository.GetAsync(x => x.Reference == model.Reference);
            if (!string.IsNullOrEmpty(existingInvoice.Reference))
            {
                throw new ArgumentException("Invoice already exists");
            }

            await repository.AddAsync(invoice);
            await UnitOfWork.SaveChangesAsync();

            var response = new AddInvoiceResponse()
            {
                Reference = invoice.Reference
            };

            return response;
        }

        public async Task<GetInvoiceResponse> GetAsync(string reference)
        {
            var repository = UnitOfWork.AsyncRepository<Invoice>();
            var invoice = await repository.GetAsync(x => x.Reference == reference);

            if(string.IsNullOrEmpty(invoice.Reference))
            {
                return new GetInvoiceResponse();
            }

            var response = new GetInvoiceResponse()
            {
                Reference = invoice.Reference,
                CurrencyCode = invoice.CurrencyCode,
                IssueDate = invoice.IssueDate,
                OpeningValue = invoice.OpeningValue,
                PaidValue = invoice.PaidValue,
                DueDate = invoice.DueDate,
                ClosedDate = invoice.ClosedDate,
                Cancelled = invoice.Cancelled,
                DebtorName = invoice.DebtorName,
                DebtorReference = invoice.DebtorReference,
                DebtorAddress1 = invoice.DebtorAddress1,
                DebtorAddress2 = invoice.DebtorAddress2,
                DebtorTown = invoice.DebtorTown,
                DebtorState = invoice.DebtorState,
                DebtorZip = invoice.DebtorZip,
                DebtorCountryCode = invoice.DebtorCountryCode,
                DebtorRegistrationNumber = invoice.DebtorRegistrationNumber
            };

            return response;
        }

        public async Task<List<GetInvoiceResponse>> GetAllAsync()
        {
            var repository = UnitOfWork.AsyncRepository<Invoice>();
            List<Invoice> invoices = await repository.ListAsync(x => true);

            if (invoices.Count == 0)
            {
                return new List<GetInvoiceResponse>();
            }

            var response = invoices.Select(x => new GetInvoiceResponse()
            {
                Reference = x.Reference,
                CurrencyCode = x.CurrencyCode,
                IssueDate = x.IssueDate,
                OpeningValue = x.OpeningValue,
                PaidValue = x.PaidValue,
                DueDate = x.DueDate,
                ClosedDate = x.ClosedDate,
                Cancelled = x.Cancelled,
                DebtorName = x.DebtorName,
                DebtorReference = x.DebtorReference,
                DebtorAddress1 = x.DebtorAddress1,
                DebtorAddress2 = x.DebtorAddress2,
                DebtorTown = x.DebtorTown,
                DebtorState = x.DebtorState,
                DebtorZip = x.DebtorZip,
                DebtorCountryCode = x.DebtorCountryCode,
                DebtorRegistrationNumber = x.DebtorRegistrationNumber
            }).ToList();

            return response;
        }

        public async Task<UpdateInvoiceResponse> UpdateAsync(UpdateInvoiceRequest model)
        {
            var repository = UnitOfWork.AsyncRepository<Invoice>();
            var invoice = await repository.GetAsync(x => x.Reference == model.Reference);

            if (invoice == null)
            {
                return null;
            }

            invoice.Reference = model.Reference;
            invoice.CurrencyCode = model.CurrencyCode;
            invoice.IssueDate = model.IssueDate;
            invoice.OpeningValue = model.OpeningValue;
            invoice.PaidValue = model.PaidValue;
            invoice.DueDate = model.DueDate;
            invoice.ClosedDate = model.ClosedDate;
            invoice.Cancelled = model.Cancelled;
            invoice.DebtorName = model.DebtorName;
            invoice.DebtorReference = model.DebtorReference;
            invoice.DebtorAddress1 = model.DebtorAddress1;
            invoice.DebtorAddress2 = model.DebtorAddress2;
            invoice.DebtorTown = model.DebtorTown;
            invoice.DebtorState = model.DebtorState;
            invoice.DebtorZip = model.DebtorZip;
            invoice.DebtorCountryCode = model.DebtorCountryCode;
            invoice.DebtorRegistrationNumber = model.DebtorRegistrationNumber;

            var result = await repository.UpdateAsync(invoice);
            await UnitOfWork.SaveChangesAsync();

            var response = new UpdateInvoiceResponse()
            {
                Reference = invoice.Reference,
                CurrencyCode = invoice.CurrencyCode,
                IssueDate = invoice.IssueDate,
                OpeningValue = invoice.OpeningValue,
                PaidValue = invoice.PaidValue,
                DueDate = invoice.DueDate,
                ClosedDate = invoice.ClosedDate,
                Cancelled = invoice.Cancelled,
                DebtorName = invoice.DebtorName,
                DebtorReference = invoice.DebtorReference,
                DebtorAddress1 = invoice.DebtorAddress1,
                DebtorAddress2 = invoice.DebtorAddress2,
                DebtorTown = invoice.DebtorTown,
                DebtorState = invoice.DebtorState,
                DebtorZip = invoice.DebtorZip,
                DebtorCountryCode = invoice.DebtorCountryCode,
                DebtorRegistrationNumber = invoice.DebtorRegistrationNumber
            };

            return response;
        }

        public async Task<bool> DeleteAsync(string reference)
        {
            var repository = UnitOfWork.AsyncRepository<Invoice>();
            var invoice = await repository.GetAsync(x => x.Reference == reference);

            if (invoice == null)
            {
                return false;
            }

            var result = await repository.DeleteAsync(invoice);
            await UnitOfWork.SaveChangesAsync();

            return result;
        }

    }
}
