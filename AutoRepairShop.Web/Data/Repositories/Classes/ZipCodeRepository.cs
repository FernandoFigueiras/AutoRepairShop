using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Pages.Internal.Account;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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


    
        public async Task<int> GetZipCodeIdAsync(string zipCode4, string zipCode3)
        {
            var zipCode = await _context.ZipCodes.FirstOrDefaultAsync(z => z.ZipCode4 == zipCode4 && z.ZipCode3 == zipCode3);

            return zipCode.Id;
        } 




        public async Task<ZipCode> GetZipCodeByIdAsync(int zipCodeId)
        {
            return await _context.ZipCodes.FindAsync(zipCodeId);
        }
        



    }
}
