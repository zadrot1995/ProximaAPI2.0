using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            var checkBoxItems = new List<CheckBoxItem>
    {
        new CheckBoxItem { Id = 1, Name = "Item 1" },
        new CheckBoxItem { Id = 2, Name = "Item 2" },
        new CheckBoxItem { Id = 3, Name = "Item 3" }
    };

            return View(checkBoxItems);
        }

        [HttpPost]
        public IActionResult Submit(List<CheckBoxItem> checkBoxItems)
        {
            // Process the submitted checkbox items
            // checkBoxItems will contain the selected checkbox items

            return View();
        }
    }





    public class CheckBoxItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }

}
