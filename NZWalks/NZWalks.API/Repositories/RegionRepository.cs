using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalkDbContext db;

        public RegionRepository(NZWalkDbContext db)
        {
            this.db = db;
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id= Guid.NewGuid();
            await db.Regions.AddAsync(region);
            await db.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region =await db.Regions.FirstOrDefaultAsync(x=>x.Id==id);
            if(region == null)
            {
                return null;
            }
            db.Regions.Remove(region);
            await db.SaveChangesAsync();
            return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await db.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
            return await db.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await db.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Lat = region.Lat;
            existingRegion.Long = region.Long;
            existingRegion.Population = region.Population;

            await db.SaveChangesAsync();

            return existingRegion;
        }
    }
}
