using FirstWebApplication.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FirstWebApplication.Binders
{
    public class BookBinder : IModelBinder
    {
        // custom validations will work as is -> next step after this one
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            int bookId = Convert.ToInt32(bindingContext.ValueProvider.GetValue(nameof(Book.BookId)).FirstValue);
            ModelBindingResult.Success(new Book());
            return Task.CompletedTask;
        }
    }
}
