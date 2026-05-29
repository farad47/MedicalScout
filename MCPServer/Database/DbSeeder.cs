using MCPServer.Entities;
using MCPServer.Model;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace MCPServer.Database
{
    public static class DbSeeder
    {
        private record BloodTestSeedDto(
            string Code,
            string Name,
            string Description,
            string[] Indications,
            string Category,
            decimal PriceGross,
            string PreparationInfo);

        public static async Task SeedAsync(AppDbContext db, IWebHostEnvironment env)
        {
            if (await db.BloodTests.AnyAsync())
                return;   

            var path = Path.Combine(env.ContentRootPath, "Database", "Init.json");
            var json = await File.ReadAllTextAsync(path);

            var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var dtos = JsonSerializer.Deserialize<List<BloodTestSeedDto>>(json, opts)
                       ?? throw new InvalidOperationException("Error occured during init database");

            var allIndicationNames = dtos
                .SelectMany(d => d.Indications)
                .Select(name => name.Trim().ToLowerInvariant())
                .Distinct()
                .ToList();

            var indicationEntities = allIndicationNames
                .Select(name => new Indication { Name = name })
                .ToList();

            await db.Indications.AddRangeAsync(indicationEntities);
            await db.SaveChangesAsync();

            var indicationByName = indicationEntities.ToDictionary(i => i.Name, i => i);

            var bloodTests = dtos.Select(d => new BloodTest
            {
                Code = d.Code,
                Name = d.Name,
                Description = d.Description,
                Category = d.Category,
                PriceGross = d.PriceGross,
                PreparationInfo = d.PreparationInfo,
                Indications = d.Indications
                    .Select(n => indicationByName[n.Trim().ToLowerInvariant()])
                    .ToList()
            }).ToList();

            await db.BloodTests.AddRangeAsync(bloodTests);
            await db.SaveChangesAsync();
        }
    }
}
