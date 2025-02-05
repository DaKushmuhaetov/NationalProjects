﻿using System;

namespace Chuvashia.NationalProjects.Model.News
{
    public sealed class NewsPost
    {
        public Guid Id { get; set; }
        public NationalProjectType Type { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsArchived { get; set; }
    }
}
