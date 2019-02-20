using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngryUsers.Services
{
    public class S3FileObject
    {
        public string SignedUrl { get; set; }
        public string Filename { get; set; }
        public string FileType { get; set; }
        public string Key { get; set; }
    }
}