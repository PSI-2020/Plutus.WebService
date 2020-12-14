﻿using Microsoft.AspNetCore.Mvc;
using Plutus.WebService.IRepos;
using System.Collections.Generic;
using System.Linq;

namespace Plutus.WebService
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulerController : ControllerBase
    {
        private readonly IFileManagerRepository _fileManager;
        private readonly ISchedulerService _schedulerRepository;
        public SchedulerController(IFileManagerRepository fileManagerRepository, ISchedulerService schedulerRepository)
        {
            _fileManager = fileManagerRepository;
            _schedulerRepository = schedulerRepository;
        }
        private List<ScheduledPayment> ReadExpenses() => _fileManager.ReadFromFile<ScheduledPayment>(DataType.MonthlyExpenses);
        private List<ScheduledPayment> ReadIncomes() => _fileManager.ReadFromFile<ScheduledPayment>(DataType.MonthlyIncome);

        [HttpGet]
        public string Get()
        {
            var result = "";
            var expenses = ReadExpenses();
            var incomes = ReadIncomes();
            if (expenses.Any())
            {
                for (var x = 0; x < expenses.Count; x++)
                {
                    result = result + _schedulerRepository.ShowPayment(x, DataType.MonthlyExpenses) + "\r\n" + "\r\n";
                }
            }
            if (incomes.Any())
            {
                for (var x = 0; x < incomes.Count; x++)
                {
                    result = result + _schedulerRepository.ShowPayment(x, DataType.MonthlyIncome) + "\r\n" + "\r\n";
                }
            }
            return result;
        }
        [HttpGet("{type}")]
        public List<ScheduledPayment> Get(DataType type) => type == DataType.MonthlyExpenses ? ReadExpenses() : type == DataType.MonthlyIncome ? ReadIncomes() : null;

        [HttpGet("{id}/{type}")]
        public string Get(int id, DataType type) => _schedulerRepository.ShowPayment(id, type);

        [HttpPost("{type}")]
        public void Post([FromBody] ScheduledPayment payment, DataType type) => _fileManager.AddScheduledPayment(payment, type);

        [HttpPut("{id}/{type}/{status}")]
        public void Put(int id, DataType type, bool status) => _schedulerRepository.ChangeStatus(id, type, status);

        [HttpDelete("{id}/{type}")]
        public void Delete(int id, DataType type) => _schedulerRepository.DeletePayment(id, type);

        [HttpPatch]
        public void CheckPayments() => _schedulerRepository.CheckPayments();
    }
}
