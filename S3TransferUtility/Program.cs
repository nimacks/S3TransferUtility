using System;

namespace S3TransferUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            var directoryToUpload = @"c:\\Dev\\site";
            var bucketName = "s3mediatransfers/transfers/site";

            //Upload Directory
            S3AssetTransferUtility transferUtility = new S3AssetTransferUtility();
            var uploadStatus = transferUtility.SaveAsset(directoryToUpload, bucketName);

            Console.WriteLine(string.Format("Upload to S3 Succeded : {0}", uploadStatus));

            //Delete Directory
            var deleteStatus = transferUtility.DeleteAsset("s3mediatransfers", "transfers\\site");
            Console.WriteLine(string.Format("Directory Deletion from S3 Succeded : {0}",deleteStatus));
        
        }
    }
}
