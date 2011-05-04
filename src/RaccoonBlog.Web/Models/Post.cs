using System;
using System.Collections.Generic;
using RaccoonBlog.Web.Common;

namespace RaccoonBlog.Web.Models
{
	public class Post
	{
		public string Id { get; set; }
		public string Title { get; set; }
		public string LegacySlug { get; set; }
		public string Body { get; set; }
		public string[] Tags { get; set; }
		public AuthorReference Author { get; set; }
		public AuthorReference LastEditedBy { get; set; }

		private Guid _showPostEvenIfPrivate;
		public Guid ShowPostEvenIfPrivate
		{
			get
			{
				if (_showPostEvenIfPrivate == Guid.Empty)
					_showPostEvenIfPrivate = Guid.NewGuid();
				return _showPostEvenIfPrivate;
			}
			set { _showPostEvenIfPrivate = value; }
		}


		public DateTimeOffset CreatedAt { get; set; }
		public DateTimeOffset? LastEditedAt { get; set; }
		public bool SkipAutoReschedule { get; set; }
		public DateTimeOffset PublishAt { get; set; }
		public bool IsDeleted { get; set; }
		public bool AllowComments { get; set; }

		public int CommentsCount { get; set; }

		public string CommentsId { get; set; }

		public IEnumerable<string> TagsAsSlugs
		{
			get
			{
				if (Tags == null)
					yield break;
				foreach (var tag in Tags)
				{
					yield return SlugConverter.TitleToSlag(tag);
				}
			}
		}

		public bool IsPublicPost(string key)
		{
			if (PublishAt <= DateTimeOffset.Now && IsDeleted == false)
				return true;

			Guid maybeKey;
			if (key == null || Guid.TryParse(key, out maybeKey) == false)
				return false;

			return maybeKey == ShowPostEvenIfPrivate;
		}

		public class AuthorReference
		{
			public string Id { get; set; }
			public string FullName { get; set; }
		}
	}
}