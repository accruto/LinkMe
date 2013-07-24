using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkMe.Domain.Resources
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public IList<Subcategory> Subcategories { get; set; }
    }

    public static class CategoryExtensions
    {
        public static Category GetCategory(this IEnumerable<Category> categories, Guid categoryId)
        {
            return (from c in categories
                    where c.Id == categoryId
                    select c).SingleOrDefault();
        }

        public static Category GetCategoryBySubcategory(this IEnumerable<Category> categories, Guid subcategoryId)
        {
            return (from c in categories
                    where (from s in c.Subcategories where s.Id == subcategoryId select s).Any()
                    select c).SingleOrDefault();
        }

        public static Subcategory GetSubcategory(this IEnumerable<Category> categories, Guid subcategoryId)
        {
            return (from s in categories.SelectMany(c => c.Subcategories)
                    where s.Id == subcategoryId
                    select s).SingleOrDefault();
        }
    }
}
