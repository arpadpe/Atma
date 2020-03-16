using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AtmaAPI.Models;
using AtmaAPI.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AtmaAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    public class SalesController : ControllerBase
    {

        private readonly ISalesService _salesService;
        private readonly ILogger<SalesController> _logger;

        public SalesController(ISalesService salesService, ILogger<SalesController> logger)
        {
            _salesService = salesService;
            _logger = logger;
        }

        // GET: SoldArticlesPerDay
        /// <summary>
        /// Returns the number of articles sold per day.
        /// </summary>
        /// <returns>List of the number of articles sold per day.</returns>
        /// <response code="200">List of the total revenues per day in the form ' "Date"  : Number of sales '.</response>
        /// <response code="400">Error message in case of an error.</response> 
        [HttpGet("/SoldArticlesPerDay")]
        [ProducesResponseType(typeof(Dictionary<DateTime, int>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult SoldArticlesPerDay()
        {
            try { 
                
                var result = _salesService.GetSoldArticlesPerDay();

                return new JsonResult(JsonConvert.SerializeObject(result));

            } catch (Exception e)
            {
                _logger.LogError("Error in SoldArticlesPerDay", e);
                return new BadRequestObjectResult("Error occured, please try again later.");
            }
        }

        // GET: TotalRevenuePerDay
        /// <summary>
        /// Returns the total revenue per day.
        /// </summary>
        /// <returns>List of the total revenues per day.</returns>
        /// <response code="200">List of the total revenues per day in the form ' "Date"  : Total revenue '.</response>
        /// <response code="400">Error message in case of an error.</response> 
        [HttpGet("/TotalRevenuePerDay")]
        [ProducesResponseType(typeof(Dictionary<DateTime, double>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult TotalRevenuePerDay()
        {
            try { 

                var result = _salesService.GetTotalRevenuePerDay();

                return new JsonResult(JsonConvert.SerializeObject(result));

            } catch (Exception e)
            {
                _logger.LogError("Error in TotalRevenuePerDay", e);
                return new BadRequestObjectResult("Error occured, please try again later.");
            }
        }

        // GET: Statistics
        /// <summary>
        /// Returns a list of the total revenues per article.
        /// </summary>
        /// <returns>List of the total revenues per article.</returns>
        /// <response code="200">List of the total revenues per article in the form ' "Article name" : Total revenue '.</response>
        /// <response code="400">Error message in case of an error.</response>  
        [HttpGet("/Statistics")]
        [ProducesResponseType(typeof(Dictionary<string, double>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult Statistics()
        {
            try
            {

                var result = _salesService.GetTotalRevenuePerArticle();

                return new JsonResult(JsonConvert.SerializeObject(result));

            } catch (Exception e)
            {
                _logger.LogError("Error in Statistics", e);
                return new BadRequestObjectResult("Error occured, please try again later.");
            }
        }

        // POST: Sale
        /// <summary>
        /// Record a sale.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Sale
        ///     {
        ///        "ArticleNumber": abc123,
        ///        "SalesPrice": 12.5,
        ///        "Date": "2020-03-15"
        ///     }
        ///
        /// </remarks>
        /// <param name="sale"></param>
        /// <returns>Ok Result if successful, Bad Request in case of an error.</returns>
        /// <response code="200">If successful</response>
        /// <response code="400">In case of an error</response>  
        [HttpPost("/Sale")]
        public IActionResult Sale([FromBody] SalePost sale)
        {
            try
            {
                if (sale.ArticleNumber == null)
                {
                    return new BadRequestObjectResult("Missing article number.");
                }

                if (sale.Date == null)
                {
                    return new BadRequestObjectResult("Missing date.");
                }

                if (sale.SalesPrice == null)
                {
                    return new BadRequestObjectResult("Missing sales price.");
                }

                if (sale.ArticleNumber.Length > 32)
                {
                    return new BadRequestObjectResult("Article number too long.");
                }

                _salesService.RecordSale(new Sale
                {
                    ArticleNumber = sale.ArticleNumber,
                    SalesPrice = (double)sale.SalesPrice,
                    Date = sale.Date
                });
                return new OkResult();

            } catch (Exception e)
            {
                _logger.LogError("Error in POST Sale", e);
                return new BadRequestObjectResult("Error occured, please try again later.");
            }
        }
    }
}
