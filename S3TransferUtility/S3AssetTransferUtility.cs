using Amazon;
using Amazon.S3;
using Amazon.S3.IO;
using Amazon.S3.Transfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3TransferUtility
{
    public class S3AssetTransferUtility
    {
        private readonly TransferUtility _transferUtility;
        private readonly AmazonS3Client _client;
        public S3AssetTransferUtility()
        {
            _transferUtility = new TransferUtility("", "", RegionEndpoint.USWest2);
            _client = new AmazonS3Client("", "", RegionEndpoint.USWest2);
        }

        /// <summary>
        /// Upload specified Diretory to S3 bucket
        /// </summary>
        /// <param name="uploadDirectory"></param>
        /// <param name="bucket"></param>
        /// <returns></returns>
        public bool SaveAsset(string uploadDirectory, string bucket)
        {
            try
            {

                TransferUtilityUploadDirectoryRequest request = new TransferUtilityUploadDirectoryRequest
                {
                    BucketName = bucket,
                    Directory = uploadDirectory,
                    SearchOption = System.IO.SearchOption.AllDirectories,
                    CannedACL = S3CannedACL.PublicRead
                };
                _transferUtility.UploadDirectory(request);

                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.InnerException.Message);
                return false;
            }
        }

        /// <summary>
        /// Delete Directory from S3
        /// </summary>
        /// <param name="uploadDirectory"></param>
        /// <param name="bucket"></param>
        /// <returns></returns>
        public bool DeleteAsset(string bucket, string uploadDirectory)
        {
            try
            {
                S3DirectoryInfo directoryToDelete = new S3DirectoryInfo(_client, bucket, uploadDirectory);

                var directoryFiles = directoryToDelete.EnumerateFiles();
                foreach (S3FileInfo file in directoryFiles)
                {
                    S3FileInfo filetoDelete = new S3FileInfo(_client, bucket, file.FullName.Replace(bucket + ":\\", string.Empty));
                    if (filetoDelete.Exists)
                    {
                        filetoDelete.Delete();
                    }
                }


                if (directoryToDelete.Exists)
                {
                    directoryToDelete.Delete(false);
                    return true;
                }
                
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.InnerException.Message);

                return false;
            }
            return false;
        }

    }
}
