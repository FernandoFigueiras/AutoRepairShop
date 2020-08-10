using AutoRepairShop.Web.Data.Entities;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories
{
    public class ColorRepository : GenericRepository<Color>, IColorRepository
    {

        public ColorRepository(DataContext context) : base(context)
        {

        }


    }
}
