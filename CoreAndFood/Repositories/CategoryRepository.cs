using CoreAndFood.Data;
using CoreAndFood.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace CoreAndFood.Repositories
{
    public class CategoryRepository : GenericRepository<Category>
    {
        //Generic repository'den kalıtım alınarak metodların bu sınıf altında da kullanılmasına olanak sağlandı.
    }
}
