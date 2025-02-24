﻿using MidTerm_Exm.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MidTerm_Exm.ViewModels
{
    public class CountryInputModel
    {
        public int Id { get; set; }
        [Required, StringLength(40)]
        public string CountryName { get; set; }
        [Required, StringLength(30)]
        public string Capital { get; set; }
        [Required, StringLength(50)]
        public string Currency { get; set; }
        [Required, StringLength(50)]
        public string Symbol { get; set; }
        [Required]
        public HttpPostedFileBase Picture { get; set; }
        public bool IsDeveloped { get; set; }
        public virtual List<University> Universities { get; set; } = new List<University>();
    }
}