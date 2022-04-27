using Microsoft.AspNetCore.Mvc;

namespace RegistrationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly DbTransactionStore _dbTransactionStore;

        public TransactionsController(DbTransactionStore transactionStore)
        {
            _dbTransactionStore = transactionStore;
        }

        [HttpGet("/transactions/all")]
        public IActionResult GetAllTransactions()
        {
            return Ok(_dbTransactionStore.GetAllTransactions());
        }
    }
}