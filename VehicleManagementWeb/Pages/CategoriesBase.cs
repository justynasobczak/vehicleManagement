using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;

using Microsoft.AspNetCore.Components.Forms;
using VehicleManagementModels;
using VehicleManagementWeb.Services;

namespace VehicleManagementWeb.Pages
{
    public class CategoriesBase : ComponentBase
    {
        [Inject]
        public ICategoryService CategoryService { get; set; }
        
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected IList<Category> Categories { get; set; }
        private IList<int> CategoryToRemoveIds { get; set; }
        protected bool IsModified { get; set; }

        protected List<string> Icons {get; } = new List<string> {
            "fas fa-truck",
            "fas fa-car",
            "fas fa-motorcycle",
            "fas fa-shopping-cart",
            "fas fa-baby-carriage"
        };

        protected override async Task OnInitializedAsync()
        {
            Categories = (await CategoryService.GetCategories()).ToList();
            CategoryToRemoveIds = new List<int>();
            SortAndUpdateCategories();
            IsModified = false;
        }

        protected void OnChangeWeightFrom(decimal value, int index)
        {
            Categories[index].WeightFrom = decimal.Round(value, 2);
            SortAndUpdateCategories();
            IsModified = true;
        }

        private void SortAndUpdateCategories()
        {
            // Clone here to work on Categories shallow copy.
            // It is needed to evaluate ranges
            var categories = Categories.Select(item => item).ToList();
            categories.Sort((left, right) => decimal.Compare(left.WeightFrom, right.WeightFrom));
            categories[0].WeightFrom = 0;
            for (var index = 0; index < Categories.Count - 1; index++)
            {
                categories[index].WeightUpTo = categories[index + 1].WeightFrom;
            }
            categories[^1].WeightUpTo = Category.MaxWeight;
        }

        protected void OnSelectIcon(EventArgs e, string icon, Category category) {
            category.Icon = icon;
            IsModified = true;
        }

        protected void OnAdd(MouseEventArgs e)
        {
            var heaviest = Categories.Max((category) => category.WeightFrom);
            Categories.Add(new Category
            {
                CategoryId = 0,
                CategoryName = $"From {heaviest + 1}",
                WeightFrom = heaviest + 1,
                Icon = "fas fa-car"
            });
            SortAndUpdateCategories();
            IsModified = true;
        }

        protected void OnRemove(MouseEventArgs e, int index, int categoryId)
        {
            // Don't remove the first category
            if (Categories.Count == 1) {
                return;
            }
            
            // Remove from categories and to the list of removed ids
            Categories.RemoveAt(index);
            if (categoryId > 0)
            {
                CategoryToRemoveIds.Add(categoryId);
            }

            SortAndUpdateCategories();
            IsModified = true;
        }

        protected void OnChangeCategoryName(string categoryName, Category category)
        {
            category.CategoryName = categoryName;
            IsModified = true;
        }

        protected async void OnSubmit(EditContext editContext) {
            if (!editContext.Validate() || !AllValid()) return;
            
            // Add/Update
            foreach (var category in Categories)
            {
                if (category.CategoryId > 0)
                {
                    await CategoryService.UpdateCategory(category);
                }
                else
                {
                    await CategoryService.AddCategory(category);
                }
            }

            // Delete
            foreach (var id in CategoryToRemoveIds)
            {
                await CategoryService.DeleteCategory(id);
            }

            NavigationManager.NavigateTo("/", true);
        }

        protected bool AllValid() {
            return !(
                Categories.Any((category) => category.WeightFrom == category.WeightUpTo) ||
                Categories.Any((category) => category.WeightFrom > Category.MaxWeight || category.WeightUpTo > Category.MaxWeight)
            );
        }
    }
}