using Microsoft.AspNetCore.Mvc;

namespace RegistrationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly DbAccountStore _accountStore;

        public AccountsController(DbAccountStore accountStore)
        {
            _accountStore = accountStore;
        }

        [HttpPost("/accounts/register")]
        public IActionResult RegisterAcc(AccountRequest accountRequest)
        {
           _accountStore.RegisterAcc(accountRequest);

           return Ok(new StatusModel {Status = "success"});
        }

        [HttpGet("/accounts/all")]
        public IActionResult GetAllAccounts()
        {
            return Ok(_accountStore.GetAllAccounts());
        }

        [HttpGet("/accounts/getbyid/{id}")]
        public IActionResult GetAccountById(string id)
        {
            return id == null ? Ok(new StatusModel{Status = "account is not found."}) : Ok(_accountStore.GetAccountById(id));
        }

        [HttpGet("/accounts/deposit/{accountId}/{sumOfDeposit}")]
        public IActionResult Deposit(string accountId, double sumOfDeposit)
        {
            if (sumOfDeposit >= 0)
            {
                _accountStore.Deposit(accountId, sumOfDeposit);
                return Ok(new StatusModel{Status = "success"});
            }
            else
            {
                return Ok(new StatusModel {Status = "wrong sum of deposit."});
            }
        }

        [HttpGet("/accounts/withdrawal/{accountId}/{sumOfWithdrawal}")]
        public IActionResult Withdrawal(string accountId, double sumOfWithdrawal)
        {
            if (accountId == null)
            {
                return Ok(new StatusModel{Status = "account is not found."});
            }
            if (sumOfWithdrawal >= 0)
            {
                _accountStore.Withdrawal(accountId, sumOfWithdrawal);
                return Ok(new StatusModel{Status = "success"});
            }
            else
            {
                return Ok(new StatusModel {Status = "wrong sum of withdrawal."});
            }
        }

        [HttpGet("/accounts/transaction/{idOfSender}/{sumOfTransaction}/{idOfRecipient}")]
        public IActionResult Transaction(string idOfSender, double sumOfTransaction, string idOfRecipient)
        {
            if (sumOfTransaction >= 0)
            {
                _accountStore.Transaction(idOfSender, sumOfTransaction, idOfRecipient);
                return Ok(new StatusModel{Status = "success"});
            }
            else
            {
                return Ok(new StatusModel {Status = "wrong sum of transaction."});
            }
        }
    }
}