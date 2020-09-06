using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Classes
{
    public class ZipCodeRepository : GenericRepository<ZipCode>, IZipCodeRepository
    {
        private readonly DataContext _context;

        public ZipCodeRepository(DataContext context) : base(context)
        {
            _context = context;
        }



        public async Task<ZipCode> GetZipCodeAsync(string zipCode4, string zipCode3)
        {
            var zipCode = await _context.ZipCodes.FirstOrDefaultAsync(z => z.ZipCode4 == zipCode4 && z.ZipCode3 == zipCode3);

            return zipCode;
        }




        public async Task<ZipCode> GetZipCodeByIdAsync(int zipCodeId)
        {
            return await _context.ZipCodes.FindAsync(zipCodeId);
        }



        public async Task<bool> ZipCodeExistsAsync(string zip4, string zip3)
        {
            return await _context.ZipCodes.AnyAsync(z => z.ZipCode4 == zip4 && z.ZipCode3 == zip3);
        }



        public async Task<ZipCode> GetCityIdFromZip4(string zip4)
        {
            return await _context.ZipCodes.FirstOrDefaultAsync(z => z.ZipCode4 == zip4);


        }

    }
}
