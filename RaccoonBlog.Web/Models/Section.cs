using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using RaccoonBlog.Web.Helpers.Validation;

namespace RaccoonBlog.Web.Models
{
    /*Section can contains:
     * 1. Body = "Any html text"
     * 2. Can point to any internal action.
     */
    public class Section
    {
		[HiddenInput]
        public string Id { get; set; }

		[Required]
		[Display(Name = "Title")]
        public string Title { get; set; }

		[Display(Name = "Active?")]
        public bool IsActive { get; set; }

        public int Position { get; set; }

		[AllowHtml]
		[Display(Name = "Body")]
		[DataType(DataType.MultilineText)]
        public string Body { get; set; }

		[RequiredIf("Body", "")]
		[Display(Name = "Controller Name")]
        public string ControllerName { get; set; }

		[RequiredIf("Body", "")]
		[Display(Name = "Action Name")]
        public string ActionName { get; set; }

		public bool IsNewSection()
		{
			return string.IsNullOrEmpty(Id);
		}
    }
}
