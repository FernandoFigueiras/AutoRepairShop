using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Classes
{
    public class ColorRepository : GenericRepository<Color>, IColorRepository
    {

        public ColorRepository(DataContext context) : base(context)
        {

        }


    }
}
