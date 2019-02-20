using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Http;
using Amazon.S3;
using Amazon.S3.Model;
//using AngryUsers.Models;

namespace AngryUsers.Services
{
    public class AWSServices
    {
        public AWSServices()
        {

        }

        public List<S3FileObject> SignUrl(IEnumerable<S3FileObject> Files)
        {
            // Create a client
            AmazonS3Client client = new AmazonS3Client(Amazon.RegionEndpoint.USEast2);

            List<S3FileObject> fileObjs = new List<S3FileObject>();
            foreach (var file in Files)
            {
                string filename = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.Filename);

                GetPreSignedUrlRequest request = new GetPreSignedUrlRequest
                {
                    BucketName = "angryusers-complaint-files",
                    Key = filename,
                    Verb = HttpVerb.PUT,
                    ContentType = file.FileType,
                    Expires = DateTime.Now.AddMinutes(5)
                };
                
                fileObjs.Add(new S3FileObject()
                {
                    SignedUrl = client.GetPreSignedURL(request),
                    Filename = file.Filename,
                    FileType = file.FileType,
                    Key = filename
                });
            }
            return fileObjs;
        }
    }
}