﻿using v_wallet_api.Models;

namespace v_wallet_api.Repositories
{
    public interface IFinancialAccountRepository
    {
        Task<List<FinancialAccount>> GetFinancialAccountsById(List<string> accountIds);
        Task<List<FinancialAccount>> GetFinancialAccountsByUserProfileId(string userId);
        Task<FinancialTransaction?> GetFinancialTransaction(Guid id);
        Task<List<FinancialTransaction>> GetFinancialTransactions(List<string> accountIds);
        Task<FinancialTransaction> CreateFinancialTransaction(FinancialTransaction transaction);
        Task<FinancialAccount?> GetFinancialAccount(Guid id);
        Task<FinancialAccount> CreateFinancialAccount(FinancialAccount account);
        Task UpdateFinancialAccount(FinancialAccount account);
        Task DeleteFinancialAccount(FinancialAccount account);

        Task DeleteFinancialTransaction(IEnumerable<FinancialTransaction> transactions);
    }
}
