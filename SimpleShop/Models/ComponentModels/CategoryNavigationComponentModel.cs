using SimpleShop.Application.Categories.Queries.GetCategoriesList;

namespace SimpleShop.Web.Models.ComponentModels
{
    public class CategoryNavigationComponentModel
    {
        public List<CategoryListDTO> Categories { get; set; } = new List<CategoryListDTO>();
        public int? CurrentCategoryId { get; set; }
    }
}
