﻿using v_wallet_api.Models;

namespace v_wallet_api.Repositories
{
    public interface IFinancialAccountRepository
    {
        Task<FinancialTransaction?> GetFinancialTransaction(Guid id);
        Task<List<FinancialTransaction>> GetFinancialTransactions(Guid accountId);
    }
}
