# BankAPI

📌 Beskrivning  
Detta är ett API utvecklat i C#/.NET som hanterar kund-, konto- och transaktionsdata.  
API:et är strukturerat enligt Repository Pattern för att separera affärslogik från dataåtkomst.  

🔹 Teknisk Struktur  
Projektet är organiserat i följande mappar/namespaces:    

Controllers – Hanterar inkommande HTTP-förfrågningar och returnerar svar.  
Services – Innehåller affärslogik och hanterar data från repositories.  
Repositories – Abstraherar dataåtkomst och hanterar databasoperationer.  
Models – Innehåller entiteter som speglar databasstrukturen.  
Dtos – Data Transfer Objects för att hantera in- och utgående data.  
Interfaces – Definierar kontrakt för repositories och services.  
Data – Innehåller en DbContext-klass för databasanslutning.  
Unittests – Innehåller tester för API:ets funktioner.  
     
📌 API Endpoints  

🔑 Login  
POST /api/Authorization/Login – Autentisering för användare (Admin eller Customer)  

🔧 Admin  
POST /api/Admin/RegisterCustomer – Registrera en ny kund.  
POST /api/AdminVerification/UpdateAdminPassword – Uppdatera admin-lösenord.  
POST /api/AdminVerification/GetAdminSecurityKey – Hämta admin-säkerhetsnyckel.  
POST /api/Loan/AddCustomerLoan – Lägg till kundlån.  

👤 Customer  
GET /api/CustomerAuth/SecurityKey – Hämta kundens säkerhetsnyckel.  

💰 Accounts  
GET /api/AccountTransactions/Account/{accountId}Transactions – Hämta transaktioner för ett konto.  
GET /api/AccountTypes/GetAccountTypes – Hämta alla kontotyper.  
GET /api/CustomerAccounts/AccountOverview – Hämta en sammanfattning av kundens konton.  
PUT /api/NewAccount/CreateNewAccount – Skapa ett nytt konto.  
POST /api/Transaction/TransferFunds – Överför pengar mellan konton.  
