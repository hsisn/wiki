using System;
using System.ComponentModel.DataAnnotations;
using Wiki.Views.Shared;

namespace Wiki.Models.Biz
{
    public class Article
    {
        [Required]
        [Display(ResourceType =typeof(SiteResource),Name = "Title")]
        public string Titre { get; set; }

        [Required]
        [Display(ResourceType = typeof(SiteResource), Name = "Contents")]
        public string Contenu { get; set; }

        [Display(ResourceType = typeof(SiteResource), Name = "ChangementDate")]
        public DateTime DateModification { get; set; }

        public int Revision { get; set; }

        [Display(ResourceType = typeof(SiteResource), Name = "IdContributor")]

        public int IdContributeur { get; set; }

    }
}