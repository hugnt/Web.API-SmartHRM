﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHRM.Repository;
using SmartHRM.Repository.Models;
using SmartHRM.Services;

namespace SmartHRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        //sample
        private readonly ContractService _ContractService;
        public ContractController(ContractService ContractService)
        {
            _ContractService = ContractService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Contract>))]
        public IActionResult GetContracts()
        {
            var Contracts = _ContractService.GetContracts();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(Contracts);
        }

        [HttpGet("{ContractId}")]
        [ProducesResponseType(200, Type = typeof(Contract))]
        [ProducesResponseType(400)]
        public IActionResult GetContract(int ContractId)
        {
            var Contract = _ContractService.GetContract(ContractId);
            if (!ModelState.IsValid) return BadRequest();
            if (Contract == null) return NotFound();
            return Ok(Contract);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateContract([FromBody] Contract ContractCreate)
        {
            if (ContractCreate == null) return BadRequest(ModelState);

            var res = _ContractService.CreateContract(ContractCreate);

            if (res.Status != 201)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return StatusCode(res.Status, res.StatusMessage);
        }

        [HttpPut("{ContractId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateContract(int ContractId, [FromBody] Contract updatedContract)
        {
            if (updatedContract == null) return BadRequest(ModelState);
            if (ContractId != updatedContract.Id) return BadRequest(ModelState);


            var res = _ContractService.UpdateContract(ContractId, updatedContract);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);

            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return NoContent();
        }

        [HttpDelete("{ContractId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteContract(int ContractId)
        {
            var res = _ContractService.DeleteContract(ContractId);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return NoContent();
        }
        [HttpPut("DeletedStatus/{contractId}/{status}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDeleteStatus(int contractId, bool status)
        {

            var res = _ContractService.UpdateDeleteStatus(contractId, status);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);

            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return NoContent();
        }

        [HttpGet("Search/{field}/{keyWords}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Contract>))]
        public IActionResult Search(string field, string keyWords)
        {
            var contracts = _ContractService.Search(field, keyWords);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(contracts);
        }
        [HttpGet("Statistic/Total")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(400)]
        public IActionResult GetTotalEmployee()
        {
            var res = _ContractService.GetTotal();
            if (!ModelState.IsValid) return BadRequest();
            if (res == 0) return NotFound();
            return Ok(res);
        }
    }
}
