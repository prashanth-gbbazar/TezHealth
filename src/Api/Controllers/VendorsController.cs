using TezHealth.Application.DTOs;
using TezHealth.Application.Interfaces;
using TezHealth.Api.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace TezHealth.Api.Controllers;

/// <summary>
/// Vendor Management API
/// Provides CRUD operations and search functionality for managing vendors
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Tags("Vendors")]
public class VendorsController : ControllerBase
{
    private readonly IVendorService _vendorService;
    private readonly ILogger<VendorsController> _logger;

    /// <summary>
    /// Initializes a new instance of the VendorsController class
    /// </summary>
    /// <param name="vendorService">The vendor service for business logic</param>
    /// <param name="logger">The logger for logging operations</param>
    public VendorsController(IVendorService vendorService, ILogger<VendorsController> logger)
    {
        _vendorService = vendorService;
        _logger = logger;
    }

    /// <summary>
    /// Retrieves all vendors from the system
    /// </summary>
    /// <returns>A list of all vendors</returns>
    /// <response code="200">Successfully retrieved all vendors</response>
    /// <response code="500">Internal server error occurred</response>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<VendorDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<IEnumerable<VendorDto>>>> GetAllVendors()
    {
        _logger.LogInformation("Getting all vendors");
        var vendors = await _vendorService.GetAllVendorsAsync();
        return Ok(new ApiResponse<IEnumerable<VendorDto>>(
            vendors,
            "Vendors retrieved successfully",
            HttpContext.TraceIdentifier
        ));
    }

    /// <summary>
    /// Retrieves a specific vendor by its internal database ID
    /// </summary>
    /// <param name="id">The internal database ID of the vendor</param>
    /// <returns>The requested vendor object</returns>
    /// <response code="200">Vendor found and returned successfully</response>
    /// <response code="404">Vendor not found with the provided ID</response>
    /// <response code="500">Internal server error occurred</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<VendorDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<VendorDto>>> GetVendorById(int id)
    {
        _logger.LogInformation("Getting vendor with id {VendorId}", id);
        var vendor = await _vendorService.GetVendorByIdAsync(id);
        return Ok(new ApiResponse<VendorDto>(
            vendor!,
            "Vendor retrieved successfully",
            HttpContext.TraceIdentifier
        ));
    }

    /// <summary>
    /// Retrieves a specific vendor by its unique UUID (vendor_id)
    /// </summary>
    /// <param name="vendorId">The unique UUID of the vendor</param>
    /// <returns>The requested vendor object</returns>
    /// <response code="200">Vendor found and returned successfully</response>
    /// <response code="404">Vendor not found with the provided UUID</response>
    /// <response code="500">Internal server error occurred</response>
    [HttpGet("vendor-id/{vendorId}")]
    [ProducesResponseType(typeof(ApiResponse<VendorDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<VendorDto>>> GetVendorByVendorId(Guid vendorId)
    {
        _logger.LogInformation("Getting vendor with vendor_id {VendorId}", vendorId);
        var vendor = await _vendorService.GetVendorByVendorIdAsync(vendorId);
        return Ok(new ApiResponse<VendorDto>(
            vendor!,
            "Vendor retrieved successfully",
            HttpContext.TraceIdentifier
        ));
    }

    /// <summary>
    /// Retrieves all vendors located in a specific city
    /// </summary>
    /// <param name="city">The city name to filter by</param>
    /// <returns>A list of vendors in the specified city</returns>
    /// <response code="200">Successfully retrieved vendors for the city</response>
    /// <response code="400">Invalid city provided</response>
    /// <response code="500">Internal server error occurred</response>
    [HttpGet("city/{city}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<VendorDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<IEnumerable<VendorDto>>>> GetVendorsByCity(string city)
    {
        _logger.LogInformation("Getting vendors for city {City}", city);
        var vendors = await _vendorService.GetVendorsByCityAsync(city);
        return Ok(new ApiResponse<IEnumerable<VendorDto>>(
            vendors,
            "Vendors retrieved successfully",
            HttpContext.TraceIdentifier
        ));
    }

    /// <summary>
    /// Retrieves all vendors located in a specific state
    /// </summary>
    /// <param name="state">The state name or code to filter by</param>
    /// <returns>A list of vendors in the specified state</returns>
    /// <response code="200">Successfully retrieved vendors for the state</response>
    /// <response code="400">Invalid state provided</response>
    /// <response code="500">Internal server error occurred</response>
    [HttpGet("state/{state}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<VendorDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<IEnumerable<VendorDto>>>> GetVendorsByState(string state)
    {
        _logger.LogInformation("Getting vendors for state {State}", state);
        var vendors = await _vendorService.GetVendorsByStateAsync(state);
        return Ok(new ApiResponse<IEnumerable<VendorDto>>(
            vendors,
            "Vendors retrieved successfully",
            HttpContext.TraceIdentifier
        ));
    }

    /// <summary>
    /// Retrieves all active vendors
    /// </summary>
    /// <returns>A list of vendors with active status</returns>
    /// <response code="200">Successfully retrieved active vendors</response>
    /// <response code="500">Internal server error occurred</response>
    [HttpGet("active")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<VendorDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<IEnumerable<VendorDto>>>> GetActiveVendors()
    {
        _logger.LogInformation("Getting active vendors");
        var vendors = await _vendorService.GetActiveVendorsAsync();
        return Ok(new ApiResponse<IEnumerable<VendorDto>>(
            vendors,
            "Active vendors retrieved successfully",
            HttpContext.TraceIdentifier
        ));
    }

    /// <summary>
    /// Retrieves a vendor by email address
    /// </summary>
    /// <param name="email">The email address to search for</param>
    /// <returns>The vendor with the specified email</returns>
    /// <response code="200">Vendor found and returned successfully</response>
    /// <response code="400">Invalid email provided</response>
    /// <response code="404">Vendor not found with the provided email</response>
    /// <response code="500">Internal server error occurred</response>
    [HttpGet("email/{email}")]
    [ProducesResponseType(typeof(ApiResponse<VendorDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<VendorDto>>> GetVendorByEmail(string email)
    {
        _logger.LogInformation("Getting vendor with email {Email}", email);
        var vendor = await _vendorService.GetVendorByEmailAsync(email);
        return Ok(new ApiResponse<VendorDto>(
            vendor!,
            "Vendor retrieved successfully",
            HttpContext.TraceIdentifier
        ));
    }

    /// <summary>
    /// Retrieves a vendor by phone number
    /// </summary>
    /// <param name="phoneNumber">The phone number to search for</param>
    /// <returns>The vendor with the specified phone number</returns>
    /// <response code="200">Vendor found and returned successfully</response>
    /// <response code="400">Invalid phone number provided</response>
    /// <response code="404">Vendor not found with the provided phone number</response>
    /// <response code="500">Internal server error occurred</response>
    [HttpGet("phone/{phoneNumber}")]
    [ProducesResponseType(typeof(ApiResponse<VendorDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<VendorDto>>> GetVendorByPhoneNumber(string phoneNumber)
    {
        _logger.LogInformation("Getting vendor with phone {PhoneNumber}", phoneNumber);
        var vendor = await _vendorService.GetVendorByPhoneNumberAsync(phoneNumber);
        return Ok(new ApiResponse<VendorDto>(
            vendor!,
            "Vendor retrieved successfully",
            HttpContext.TraceIdentifier
        ));
    }

    /// <summary>
    /// Creates a new vendor in the system
    /// </summary>
    /// <param name="vendorDto">The vendor data to create</param>
    /// <returns>The newly created vendor object with assigned ID</returns>
    /// <response code="201">Vendor created successfully</response>
    /// <response code="400">Invalid vendor data or validation failed</response>
    /// <response code="409">Vendor already exists with the provided email or phone number</response>
    /// <response code="500">Internal server error occurred</response>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<VendorDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<VendorDto>>> CreateVendor([FromBody] VendorDto vendorDto)
    {
        _logger.LogInformation("Creating new vendor: {VendorName}", vendorDto.VendorName);

        var createdVendor = await _vendorService.CreateVendorAsync(vendorDto);
        return CreatedAtAction(
            nameof(GetVendorById),
            new { vendorid = createdVendor.VendorId },
            new ApiResponse<VendorDto>(
                createdVendor,
                "Vendor created successfully",
                HttpContext.TraceIdentifier
            )
        );
    }

    /// <summary>
    /// Updates an existing vendor in the system
    /// </summary>
    /// <param name="id">The internal ID of the vendor to update</param>
    /// <param name="vendorDto">The updated vendor data</param>
    /// <returns>The updated vendor object</returns>
    /// <response code="200">Vendor updated successfully</response>
    /// <response code="400">Invalid vendor data or validation failed</response>
    /// <response code="404">Vendor not found with the provided ID</response>
    /// <response code="409">Another vendor already exists with the provided email or phone number</response>
    /// <response code="500">Internal server error occurred</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<VendorDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<VendorDto>>> UpdateVendor(int id, [FromBody] VendorDto vendorDto)
    {
        _logger.LogInformation("Updating vendor with id {VendorId}", id);

        var updatedVendor = await _vendorService.UpdateVendorAsync(id, vendorDto);
        return Ok(new ApiResponse<VendorDto>(
            updatedVendor,
            "Vendor updated successfully",
            HttpContext.TraceIdentifier
        ));
    }

    /// <summary>
    /// Deletes a vendor from the system by its internal ID
    /// </summary>
    /// <param name="id">The internal ID of the vendor to delete</param>
    /// <returns>Success message</returns>
    /// <response code="200">Vendor deleted successfully</response>
    /// <response code="404">Vendor not found with the provided ID</response>
    /// <response code="500">Internal server error occurred</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse>> DeleteVendor(int id)
    {
        _logger.LogInformation("Deleting vendor with id {VendorId}", id);

        await _vendorService.DeleteVendorAsync(id);
        return Ok(new ApiResponse(
            "Vendor deleted successfully",
            HttpContext.TraceIdentifier
        ));
    }

    /// <summary>
    /// Deletes a vendor from the system by its UUID
    /// </summary>
    /// <param name="vendorId">The unique UUID of the vendor to delete</param>
    /// <returns>Success message</returns>
    /// <response code="200">Vendor deleted successfully</response>
    /// <response code="400">Invalid vendor UUID provided</response>
    /// <response code="404">Vendor not found with the provided UUID</response>
    /// <response code="500">Internal server error occurred</response>
    [HttpDelete("vendor-id/{vendorId}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse>> DeleteVendorByVendorId(Guid vendorId)
    {
        _logger.LogInformation("Deleting vendor with vendor_id {VendorId}", vendorId);

        await _vendorService.DeleteVendorByVendorIdAsync(vendorId);
        return Ok(new ApiResponse(
            "Vendor deleted successfully",
            HttpContext.TraceIdentifier
        ));
    }
}
