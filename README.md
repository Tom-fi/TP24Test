# TP24 Test

## How to Run

1. Edit **TP24Test** connection string in appsettings.json
2. In Visual Studio go to **Tools –> Nuget Package Manager –> Package Manager Console**
3. Run **Update-Database** command in Package Manager Console to run DB migrations.
4. Run the solution.

---
Time taken: ~3.5-4h. Would have been bit faster if I had a boilerplate template handy.
There is currently no seeders, as such there is no initial data. (I'm sorry)
After running the solution, Swagger UI will open and new data can be easily added.


Example payload
[
  {
    "reference": "string",
    "currencyCode": "string",
    "issueDate": "string",
    "openingValue": 1234.56,
    "paidValue": 1234.56,
    "dueDate": "string",
    "closedDate": "string", // optional
    "cancelled": true|false, // optional
    "debtorName": "string",
    "debtorReference": "string",
    "debtorAddress1": "string", // optional
    "debtorAddress2": "string", // optional
    "debtorTown": "string",  // optional
    "debtorState": "string", // optional
    "debtorZip": "string", // optional
    "debtorCountryCode": "string",
    "debtorRegistrationNumber": "string", // optional
  }
]