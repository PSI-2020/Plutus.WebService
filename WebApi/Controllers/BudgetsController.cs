﻿using Microsoft.AspNetCore.Mvc;
using Plutus.WebService.IRepos;
using System.Collections.Generic;

namespace Plutus.WebService
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetsController : ControllerBase
    {
        private readonly IBudgetService _budgetService;

        public BudgetsController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }


        [HttpGet("list")]
        public List<Budget> GetList() => _budgetService.GetBudgetsList();

        [HttpGet("{id}")]
        public string Get(int id) => _budgetService.GenerateBudget(id);

        [HttpGet("{id}/stats")]
        public List<Payment> GetStats(int id) => _budgetService.ShowStats(id);

        [HttpGet("{id}/spent")]
        public decimal GetSpent(int id) => _budgetService.Spent(id);

        [HttpGet("{id}/left")]
        public decimal GetLeftToSpend(int id) => _budgetService.LeftToSpend(id);

        [HttpPost]
        public void Post([FromBody] Budget budget) => _budgetService.AddBudget(budget);

        [HttpDelete("{id}")]
        public void Delete(int id) => _budgetService.DeleteBudget(id);
    }
}
