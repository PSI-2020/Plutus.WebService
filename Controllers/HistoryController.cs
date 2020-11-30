using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Plutus.Services;

namespace Plutus.WebService
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly FileManager _fileManager = new FileManager();
        private readonly HistoryService _historyService = new HistoryService();

        [HttpGet]
        public ActionResult<DataTable> Get() => _historyService.LoadDataGrid(_fileManager, 0);

        [HttpGet("{selectedIndex}")]
        public ActionResult<DataTable> Get(int selectedIndex) => _historyService.LoadDataGrid(_fileManager, selectedIndex);
    }
}