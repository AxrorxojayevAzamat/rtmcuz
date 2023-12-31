﻿using rtmcuz.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace rtmcuz.ViewModels
{
    public class Leadership : BaseViewModel
    {
        [Display(Name = "Subtitle")]
        public string Subtitle { get; set; }

        [Display(Name = "Content")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "ImageId")]
        public int? ImageId { get; set; }
        public Attachment? Image { get; set; }

        public static Leadership FromSection(Section section)
        {
            return new Leadership()
            {
                Id = section.Id,
                Title = section.Title,
                Subtitle = section.Subtitle,
                Content = section.Content,
                ImageId = section.ImageId,
                Image = section.Image,
                GroupId = section.GroupId,
                Lang = section.Lang,
            };
        }

    }
}
