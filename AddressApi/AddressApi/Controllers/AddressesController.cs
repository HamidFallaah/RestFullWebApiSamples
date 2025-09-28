using AddressApi.Contracts;
using AddressApi.Dtos;
using AddressApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AddressApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AddressesController : ControllerBase
{
    private readonly IAddressRepository _repo;
    public AddressesController(IAddressRepository repo) => _repo = repo;
    private static AddressReadDto ToReadDto(Address a) => new()
    {
        AdrId = a.AdrId,
        adrPostalCode = a.adrPostalCode,
        adrBuildingName = a.adrBuildingName,
        adrFloor = a.adrFloor,
        adrHouseNumber = a.adrHouseNumber,
        adrLocalityCode = a.adrLocalityCode,
        adrLocalityName = a.adrLocalityName,
        adrLocalityType = a.adrLocalityType,
        adrMainAvenue = a.adrMainAvenue,
        adrProvince = a.adrProvince,
        adrSideFloor = a.adrSideFloor,
        adrStreet = a.adrStreet,
        adrSubLocality = a.adrSubLocality,
        adrTownShip = a.adrTownShip,
        adrLatitude = a.adrLatitude,
        adrLongitude = a.adrLongitude
    };

    private static Address FromCreateDto(AddressCreateDto d) => new()
    {
      
        adrPostalCode = d.adrPostalCode,
        adrBuildingName = d.adrBuildingName,
        adrFloor = d.adrFloor,
        adrHouseNumber = d.adrHouseNumber,
        adrLocalityCode = d.adrLocalityCode,
        adrLocalityName = d.adrLocalityName,
        adrLocalityType = d.adrLocalityType,
        adrMainAvenue = d.adrMainAvenue,
        adrProvince = d.adrProvince,
        adrSideFloor = d.adrSideFloor,
        adrStreet = d.adrStreet,
        adrSubLocality = d.adrSubLocality,
        adrTownShip = d.adrTownShip,
        adrLatitude = d.adrLatitude,
        adrLongitude = d.adrLongitude
    };

    private static void ApplyUpdate(Address entity, AddressUpdateDto d)
    {
        entity.adrPostalCode = d.adrPostalCode;
        entity.adrBuildingName = d.adrBuildingName;
        entity.adrFloor = d.adrFloor;
        entity.adrHouseNumber = d.adrHouseNumber;
        entity.adrLocalityCode = d.adrLocalityCode;
        entity.adrLocalityName = d.adrLocalityName;
        entity.adrLocalityType = d.adrLocalityType;
        entity.adrMainAvenue = d.adrMainAvenue;
        entity.adrProvince = d.adrProvince;
        entity.adrSideFloor = d.adrSideFloor;
        entity.adrStreet = d.adrStreet;
        entity.adrSubLocality = d.adrSubLocality;
        entity.adrTownShip = d.adrTownShip;
        entity.adrLatitude = d.adrLatitude;
        entity.adrLongitude = d.adrLongitude;
    }

    // GET: api/addresses?province=Texas&postalCode=73302&page=1&pageSize=25
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? province, [FromQuery] string? postalCode,
                                            [FromQuery] int page = 1, [FromQuery] int pageSize = 25)
    {
        var (items, total) = await _repo.GetAllAsync(province, postalCode, page, pageSize);
        var dtos = items.Select(ToReadDto);
        return Ok(new { total, page, pageSize, items = dtos });
    }

    // GET: api/addresses/5
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _repo.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(ToReadDto(item));
    }

    // GET: api/addresses/postal/90001
    [HttpGet("postal/{postal}")]
    public async Task<IActionResult> GetByPostal(string postal)
    {
        var item = await _repo.GetByPostalAsync(postal);
        return item is null ? NotFound() : Ok(ToReadDto(item));
    }

    // POST: api/addresses
    [HttpPost]
    public async Task<IActionResult> Create(AddressCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var entity = FromCreateDto(dto);
        var id = await _repo.CreateAsync(entity);
        var created = await _repo.GetByIdAsync(id);
        return CreatedAtAction(nameof(GetById), new { id }, ToReadDto(created!));
    }

    // PUT: api/addresses/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, AddressUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existing = await _repo.GetByIdAsync(id);
        if (existing is null) return NotFound();

        ApplyUpdate(existing, dto);
        var ok = await _repo.UpdateAsync(id, existing);
        return ok ? NoContent() : NotFound();
    }

    // DELETE: api/addresses/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var ok = await _repo.DeleteAsync(id);
        return ok ? NoContent() : NotFound();
    }
}
