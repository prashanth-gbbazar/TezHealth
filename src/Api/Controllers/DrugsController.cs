using TezHealth.Application.DTOs;
using TezHealth.Application.Interfaces;
using TezHealth.Api.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace TezHealth.Api.Controllers;

/// <summary>
/// Drug Inventory Management API
/// Provides CRUD operations for managing drugs in the inventory system
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Tags("Drugs")]
public class DrugsController : ControllerBase
{
    private readonly IDrugService _drugService;
    private readonly ILogger<DrugsController> _logger;

    /// <summary>
    /// Initializes a new instance of the DrugsController class
    /// </summary>
    /// <param name="drugService">The drug service for business logic</param>
    /// <param name="logger">The logger for logging operations</param>
    public DrugsController(IDrugService drugService, ILogger<DrugsController> logger)
    {
        _drugService = drugService;
        _logger = logger;
    }

    /// <summary>
    /// Retrieves all drugs from the inventory
    /// </summary>
    /// <returns>A list of all drugs</returns>
    /// <response code="200">Successfully retrieved all drugs</response>
    /// <response code="500">Internal server error occurred</response>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<DrugDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<IEnumerable<DrugDto>>>> GetAllDrugs()
    {
        _logger.LogInformation("Getting all drugs");
        var drugs = await _drugService.GetAllDrugsAsync();
        return Ok(new ApiResponse<IEnumerable<DrugDto>>(
            drugs,
            "Drugs retrieved successfully",
            HttpContext.TraceIdentifier
        ));
    }

    /// <summary>
    /// Retrieves a specific drug by its internal database ID
    /// </summary>
    /// <param name="id">The internal database ID of the drug</param>
    /// <returns>The requested drug object</returns>
    /// <response code="200">Drug found and returned successfully</response>
    /// <response code="404">Drug not found with the provided ID</response>
    /// <response code="500">Internal server error occurred</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<DrugDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<DrugDto>>> GetDrugById(int id)
    {
        _logger.LogInformation("Getting drug with id {DrugId}", id);
        var drug = await _drugService.GetDrugByIdAsync(id);
        return Ok(new ApiResponse<DrugDto>(
            drug!,
            "Drug retrieved successfully",
            HttpContext.TraceIdentifier
        ));
    }

    /// <summary>
    /// Retrieves a specific drug by its unique UUID (drug_id)
    /// </summary>
    /// <param name="drugId">The unique UUID of the drug</param>
    /// <returns>The requested drug object</returns>
    /// <response code="200">Drug found and returned successfully</response>
    /// <response code="404">Drug not found with the provided UUID</response>
    /// <response code="500">Internal server error occurred</response>
    [HttpGet("drug-id/{drugId}")]
    [ProducesResponseType(typeof(ApiResponse<DrugDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<DrugDto>>> GetDrugByDrugId(Guid drugId)
    {
        _logger.LogInformation("Getting drug with drug_id {DrugId}", drugId);
        var drug = await _drugService.GetDrugByDrugIdAsync(drugId);
        return Ok(new ApiResponse<DrugDto>(
            drug!,
            "Drug retrieved successfully",
            HttpContext.TraceIdentifier
        ));
    }

    /// <summary>
    /// Retrieves all drugs that belong to a specific category
    /// </summary>
    /// <param name="categoryId">The UUID of the category to filter by</param>
    /// <returns>A list of drugs in the specified category</returns>
    /// <response code="200">Successfully retrieved drugs in the category</response>
    /// <response code="400">Invalid category ID provided</response>
    /// <response code="500">Internal server error occurred</response>
    [HttpGet("category/{categoryId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<DrugDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<IEnumerable<DrugDto>>>> GetDrugsByCategory(Guid categoryId)
    {
        _logger.LogInformation("Getting drugs for category {CategoryId}", categoryId);
        var drugs = await _drugService.GetDrugsByCategoryAsync(categoryId);
        return Ok(new ApiResponse<IEnumerable<DrugDto>>(
            drugs,
            "Drugs retrieved successfully",
            HttpContext.TraceIdentifier
        ));
    }

    /// <summary>
    /// Creates a new drug in the inventory
    /// </summary>
    /// <param name="drugDto">The drug data to create</param>
    /// <returns>The newly created drug object with assigned ID</returns>
    /// <response code="201">Drug created successfully</response>
    /// <response code="400">Invalid drug data or validation failed</response>
    /// <response code="500">Internal server error occurred</response>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<DrugDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<DrugDto>>> CreateDrug([FromBody] DrugDto drugDto)
    {
        _logger.LogInformation("Creating new drug: {DrugName}", drugDto.DrugName);

        var createdDrug = await _drugService.CreateDrugAsync(drugDto);
        return CreatedAtAction(
            nameof(GetDrugById),
            new { id = createdDrug.Id },
            new ApiResponse<DrugDto>(
                createdDrug,
                "Drug created successfully",
                HttpContext.TraceIdentifier
            )
        );
    }

    /// <summary>
    /// Updates an existing drug in the inventory
    /// </summary>
    /// <param name="id">The internal ID of the drug to update</param>
    /// <param name="drugDto">The updated drug data</param>
    /// <returns>The updated drug object</returns>
    /// <response code="200">Drug updated successfully</response>
    /// <response code="400">Invalid drug data or validation failed</response>
    /// <response code="404">Drug not found with the provided ID</response>
    /// <response code="500">Internal server error occurred</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<DrugDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<DrugDto>>> UpdateDrug(int id, [FromBody] DrugDto drugDto)
    {
        _logger.LogInformation("Updating drug with id {DrugId}", id);

        var updatedDrug = await _drugService.UpdateDrugAsync(id, drugDto);
        return Ok(new ApiResponse<DrugDto>(
            updatedDrug,
            "Drug updated successfully",
            HttpContext.TraceIdentifier
        ));
    }

    /// <summary>
    /// Deletes a drug from the inventory by its internal ID
    /// </summary>
    /// <param name="id">The internal ID of the drug to delete</param>
    /// <returns>Success message</returns>
    /// <response code="200">Drug deleted successfully</response>
    /// <response code="404">Drug not found with the provided ID</response>
    /// <response code="500">Internal server error occurred</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse>> DeleteDrug(int id)
    {
        _logger.LogInformation("Deleting drug with id {DrugId}", id);

        await _drugService.DeleteDrugAsync(id);
        return Ok(new ApiResponse(
            "Drug deleted successfully",
            HttpContext.TraceIdentifier
        ));
    }

    /// <summary>
    /// Deletes a drug from the inventory by its UUID
    /// </summary>
    /// <param name="drugId">The unique UUID of the drug to delete</param>
    /// <returns>Success message</returns>
    /// <response code="200">Drug deleted successfully</response>
    /// <response code="400">Invalid drug UUID provided</response>
    /// <response code="404">Drug not found with the provided UUID</response>
    /// <response code="500">Internal server error occurred</response>
    [HttpDelete("drug-id/{drugId}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse>> DeleteDrugByDrugId(Guid drugId)
    {
        _logger.LogInformation("Deleting drug with drug_id {DrugId}", drugId);

        await _drugService.DeleteDrugByDrugIdAsync(drugId);
        return Ok(new ApiResponse(
            "Drug deleted successfully",
            HttpContext.TraceIdentifier
        ));
    }
}
