﻿using System.ComponentModel.DataAnnotations;

namespace DotNetMVC_GithubRepos.Domain.Entities
{
    public class GitRepository
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ForksCount { get; set; }
    }
}