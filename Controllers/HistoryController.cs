﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Plutus.WebService.IRepos;

namespace Plutus.WebService
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryRepository _historyRepository;
        public HistoryController(IHistoryRepository historyRepository)
        {
            _historyRepository = historyRepository;
        }

        [HttpGet]
        public ActionResult<List<All>> Get()
        {
            return _historyRepository.LoadDataGrid(0);
        }

        [HttpGet("{index}")]
        public ActionResult<List<All>> Get(int index) =>
            _historyRepository.LoadDataGrid(index);

    }
}