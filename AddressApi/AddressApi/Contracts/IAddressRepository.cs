using AddressApi.Models;

namespace AddressApi.Contracts;

public interface IAddressRepository
{
    Task<(IEnumerable<Address> items, int total)> GetAllAsync(
        string? province, string? postalCode, int page = 1, int pageSize = 25,
        CancellationToken ct = default);

    Task<Address?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Address?> GetByPostalAsync(string postal, CancellationToken ct = default);

    Task<int> CreateAsync(Address entity, CancellationToken ct = default);
    Task<bool> UpdateAsync(int id, Address entity, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}
