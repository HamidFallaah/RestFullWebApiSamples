using AddressApi.Contracts;             
using AddressApi.Models;
using Microsoft.Data.SqlClient; 
using System.Data;

namespace AddressApi.Data
{
    public class AddressRepository : IAddressRepository
    {
        private readonly string _connStr;
        public AddressRepository(IConfiguration config)
        {
            _connStr = config.GetConnectionString("SqlServer")
                       ?? throw new InvalidOperationException("SqlServer connection string not found!");
        }

        private static Address Map(IDataRecord r) => new()
        {
            AdrId = r.GetInt32(r.GetOrdinal("AdrId")),
            adrPostalCode = r.GetString(r.GetOrdinal("adrPostalCode")),
            adrBuildingName = r["adrBuildingName"] as string,
            adrFloor = r["adrFloor"] as string,
            adrHouseNumber = r["adrHouseNumber"] as string,
            adrLocalityCode = r["adrLocalityCode"] as string,
            adrLocalityName = r["adrLocalityName"] as string,
            adrLocalityType = r["adrLocalityType"] as string,
            adrMainAvenue = r["adrMainAvenue"] as string,
            adrProvince = r["adrProvince"] as string,
            adrSideFloor = r["adrSideFloor"] as string,
            adrStreet = r["adrStreet"] as string,
            adrSubLocality = r["adrSubLocality"] as string,
            adrTownShip = r["adrTownShip"] as string,
            adrLatitude = r["adrLatitude"] as decimal?,
            adrLongitude = r["adrLongitude"] as decimal?
        };

        public async Task<(IEnumerable<Address> items, int total)> GetAllAsync(
            string? province, string? postalCode, int page = 1, int pageSize = 25, CancellationToken ct = default)
        {
            var items = new List<Address>();
            int total = 0;

            await using var cn = new SqlConnection(_connStr);
            await cn.OpenAsync(ct);

            await using var cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "dbo.usp_Addresses_Search";

            cmd.Parameters.Add(new SqlParameter("@Province", SqlDbType.NVarChar, 100)
            { Value = (object?)province ?? DBNull.Value });

            cmd.Parameters.Add(new SqlParameter("@PostalCode", SqlDbType.NVarChar, 10)
            { Value = (object?)postalCode ?? DBNull.Value });

            cmd.Parameters.Add(new SqlParameter("@Page", SqlDbType.Int) { Value = page });
            cmd.Parameters.Add(new SqlParameter("@PageSize", SqlDbType.Int) { Value = pageSize });

            await using var reader = await cmd.ExecuteReaderAsync(ct);

            if (await reader.ReadAsync(ct))
                total = reader.GetInt32(reader.GetOrdinal("Total"));

            if (await reader.NextResultAsync(ct))
            {
                while (await reader.ReadAsync(ct))
                    items.Add(Map(reader));
            }

            return (items, total);
        }

        public async Task<Address?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            await using var cn = new SqlConnection(_connStr);
            await cn.OpenAsync(ct);

            await using var cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "dbo.usp_Addresses_GetById";
            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

            await using var reader = await cmd.ExecuteReaderAsync(ct);
            return await reader.ReadAsync(ct) ? Map(reader) : null;
        }

        public async Task<Address?> GetByPostalAsync(string postal, CancellationToken ct = default)
        {
            await using var cn = new SqlConnection(_connStr);
            await cn.OpenAsync(ct);

            await using var cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "dbo.usp_Addresses_GetByPostal";
            cmd.Parameters.Add(new SqlParameter("@Postal", SqlDbType.NVarChar, 10) { Value = postal });

            await using var reader = await cmd.ExecuteReaderAsync(ct);
            return await reader.ReadAsync(ct) ? Map(reader) : null;
        }

        public async Task<int> CreateAsync(Address a, CancellationToken ct = default)
        {
            await using var cn = new SqlConnection(_connStr);
            await cn.OpenAsync(ct);

            await using var cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "dbo.usp_Addresses_Create";
            AddParams(cmd, a);

            var newIdObj = await cmd.ExecuteScalarAsync(ct);
            return Convert.ToInt32(newIdObj);
        }

        public async Task<bool> UpdateAsync(int id, Address a, CancellationToken ct = default)
        {
            await using var cn = new SqlConnection(_connStr);
            await cn.OpenAsync(ct);

            await using var cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "dbo.usp_Addresses_Update";
            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });
            AddParams(cmd, a);

            await using var reader = await cmd.ExecuteReaderAsync(ct);
            int affected = 0;
            if (await reader.ReadAsync(ct))
                affected = Convert.ToInt32(reader["Affected"]);

            return affected > 0;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            await using var cn = new SqlConnection(_connStr);
            await cn.OpenAsync(ct);

            await using var cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "dbo.usp_Addresses_Delete";

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

            await using var reader = await cmd.ExecuteReaderAsync(ct);
            int affected = 0;
            if (await reader.ReadAsync(ct))
                affected = Convert.ToInt32(reader["Affected"]);

            return affected > 0;
        }

        private static void AddParams(SqlCommand cmd, Address a)
        {
            cmd.Parameters.Add(new SqlParameter("@adrPostalCode", SqlDbType.NVarChar, 10) { Value = (object?)a.adrPostalCode ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@adrBuildingName", SqlDbType.NVarChar, 100) { Value = (object?)a.adrBuildingName ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@adrFloor", SqlDbType.NVarChar, 10) { Value = (object?)a.adrFloor ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@adrHouseNumber", SqlDbType.NVarChar, 10) { Value = (object?)a.adrHouseNumber ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@adrLocalityCode", SqlDbType.NVarChar, 20) { Value = (object?)a.adrLocalityCode ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@adrLocalityName", SqlDbType.NVarChar, 100) { Value = (object?)a.adrLocalityName ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@adrLocalityType", SqlDbType.NVarChar, 50) { Value = (object?)a.adrLocalityType ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@adrMainAvenue", SqlDbType.NVarChar, 100) { Value = (object?)a.adrMainAvenue ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@adrProvince", SqlDbType.NVarChar, 100) { Value = (object?)a.adrProvince ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@adrSideFloor", SqlDbType.NVarChar, 10) { Value = (object?)a.adrSideFloor ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@adrStreet", SqlDbType.NVarChar, 100) { Value = (object?)a.adrStreet ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@adrSubLocality", SqlDbType.NVarChar, 100) { Value = (object?)a.adrSubLocality ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@adrTownShip", SqlDbType.NVarChar, 100) { Value = (object?)a.adrTownShip ?? DBNull.Value });

            var pLat = new SqlParameter("@adrLatitude", SqlDbType.Decimal) { Precision = 9, Scale = 6, Value = (object?)a.adrLatitude ?? DBNull.Value };
            var pLon = new SqlParameter("@adrLongitude", SqlDbType.Decimal) { Precision = 9, Scale = 6, Value = (object?)a.adrLongitude ?? DBNull.Value };
            cmd.Parameters.Add(pLat);
            cmd.Parameters.Add(pLon);
        }
    }
}
