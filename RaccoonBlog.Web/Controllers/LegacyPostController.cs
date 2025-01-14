using System.Linq;
using RaccoonBlog.Web.Infrastructure.AutoMapper;
using RaccoonBlog.Web.Infrastructure.Common;
using RaccoonBlog.Web.Models;
using RaccoonBlog.Web.ViewModels;
using System.Web.Mvc;

namespace RaccoonBlog.Web.Controllers
{
    public class LegacyPostController : RaccoonController
    {
        public ActionResult RedirectLegacyPost(int year, int month, int day, string slug)
        {
			// attempt to find a post with match slug in the given date, but will back off the exact date if we can't find it
			var post = RavenSession.Query<Post>()
        	           	.WhereIsPublicPost()
        	           	.Where(p => p.LegacySlug == slug && (p.PublishAt.Year == year && p.PublishAt.Month == month && p.PublishAt.Day == day))
        	           	.FirstOrDefault() ??
					  RavenSession.Query<Post>()
        	           	.WhereIsPublicPost()
        	           	.Where(p => p.LegacySlug == slug && p.PublishAt.Year == year && p.PublishAt.Month == month)
        	           	.FirstOrDefault() ??
					 RavenSession.Query<Post>()
        	           	.WhereIsPublicPost()
        	           	.Where(p => p.LegacySlug == slug && p.PublishAt.Year == year)
        	           	.FirstOrDefault() ??
					 RavenSession.Query<Post>()
        	           	.WhereIsPublicPost()
        	           	.Where(p => p.LegacySlug == slug)
        	           	.FirstOrDefault();

            if (post == null) 
            {
				return HttpNotFound();
            }

            var postReference = post.MapTo<PostReference>();
            return RedirectToActionPermanent("Details", "PostDetails", new { Id = postReference.DomainId, postReference.Slug });
        }

        public ActionResult RedirectLegacyArchive(int year, int month, int day)
        {
            return RedirectToActionPermanent("Archive", "Posts", new { year, month, day });
        }
    }
}
