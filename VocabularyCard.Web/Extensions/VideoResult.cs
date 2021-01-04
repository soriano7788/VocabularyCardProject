using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace VocabularyCard.Web.Extensions
{
    public class VideoResult : FileResult
    {
        public string FileName { get; private set; }
        private readonly string[] _videoTypes = { ".mp4", ".webm", ".ogg" };

        public VideoResult(string fileName, string contentType) : base(contentType)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("忘了檔案名稱?", "fileName");
            }
            // 附檔名確認
            // HostingEnvironment (System.Web.Hosting)
            string filePath = HostingEnvironment.MapPath("~/Videos/" + fileName);
            string fileExtension = Path.GetExtension(filePath);
            foreach (string videoType in _videoTypes)
            {
                if (string.Equals(videoType, fileExtension))
                {
                    FileName = fileName;
                }
            }
        }

        protected override void WriteFile(HttpResponseBase response)
        {
            string filePath = HostingEnvironment.MapPath("~/Videos/" + FileName);
            FileInfo file = new FileInfo(filePath);

            if (file.Exists)
            {
                FileStream stream = file.OpenRead();
                byte[] videoStream = new byte[stream.Length];
                stream.Read(videoStream, 0, (int)file.Length);

                // todo: 禁用瀏覽器的類型猜測行為，https://blog.csdn.net/zhuyiquan/article/details/52173735
                //response.AddHeader("X-Content-Type-Options", "nosniff");
                response.BinaryWrite(videoStream);
            }
            else
            {
                throw new ArgumentException("檔案不存在。", "fileName");
            }
        }
    }
}