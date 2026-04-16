using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DomainDraft.WebApi.Controllers;

[ApiController]
[Route("Recommended")]
public class RecommendedController(IRecommendedService recommendedService) : ControllerBase
{
    [HttpGet("Merchant")]
    public IActionResult GetMerchant()
    {
        return Ok(recommendedService.GetRecommendedMerchant());
    }

    [HttpGet("Bundles")]
    public IActionResult GetBundles()
    {
        return Ok(recommendedService.GetRecommendedBundles());
    }

    [HttpGet("Product")]
    public IActionResult GetProduct()
    {
        return Ok(recommendedService.GetRecommendedProduct());
    }
}