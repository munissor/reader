﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Model
{
    /// <summary>
    /// Model for an article
    /// </summary>
    public class Article
    {
        public string Id { get; set; }
        
        public string Title { get; set; }
        
        public string Link { get; set; }

        public DateTime PublicationDate { get; set; }

        public Category[] Categories { get; set; }

        public Author[] Authors { get; set; }
    }
}
