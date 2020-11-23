using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oyang.Scmc.BlazorServerApp.EntityFrameworkCore;
using System.Text.Json;
using QRCoder;
using System.Drawing;
using System.IO;
using System.IO.Compression;

namespace Oyang.Scmc.BlazorServerApp.Data
{
    public class QRCodeService
    {
        private readonly ScmcDbContext _dbContext;
        private readonly MyConfiguration _myConfiguration;
        public QRCodeService(
            ScmcDbContext dbContext,
            MyConfiguration myConfiguration
            )
        {
            _dbContext = dbContext;
            _myConfiguration = myConfiguration;
        }

        public void CreateQRCode(Guid id)
        {
            var entity = new QRCodeEntity();
            entity.Id = id;
            entity.CreationTime = DateTime.Now;
            _dbContext.Set<QRCodeEntity>().Add(entity);
            WriteLog(QRCodeActionType.保存二维码, entity);
            _dbContext.SaveChanges();
        }

        public MemoryStream CreateQRCodes()
        {
            var dict = new Dictionary<Guid, Bitmap>();
            for (int i = 0; i < 10; i++)
            {
                var guid = Guid.NewGuid();
                var url = _myConfiguration.QRCodeUrlPrefix + guid;
                var bitmap = GenerateImage(url, 10);
                dict.Add(guid, bitmap);
            }

            MemoryStream archiveMemoryStream;
            using (archiveMemoryStream = new MemoryStream())
            {
                using (ZipArchive archive = new ZipArchive(archiveMemoryStream, ZipArchiveMode.Update))
                {
                    foreach (var item in dict.Keys)
                    {
                        ZipArchiveEntry entry = archive.CreateEntry(item + ".jpg");
                        var stream = entry.Open();
                        dict[item].Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                }
            }

            var now = DateTime.Now;
            var listEntity =  dict.Keys.Select(t => new QRCodeEntity()
            {
                Id = t,
                CreationTime = now,
            });
            _dbContext.Set<QRCodeEntity>().AddRange(listEntity);
            foreach (var item in listEntity)
            {
                WriteLog(QRCodeActionType.保存二维码, item);
            }
            _dbContext.SaveChanges();
            return archiveMemoryStream;
        }


        public QRCodeEntity GetQRCode(Guid id)
        {
            var entity = _dbContext.Set<QRCodeEntity>().Find(id);
            return entity;
        }

        public void BindQRCode(Guid id, string name, string mobileNumber)
        {
            var entity = _dbContext.Set<QRCodeEntity>().Find(id);
            entity.BindTime = DateTime.Now;
            entity.Name = name;
            entity.MobileNumber = mobileNumber;
            WriteLog(QRCodeActionType.绑定二维码, entity);
            _dbContext.SaveChanges();
        }

        private void WriteLog(QRCodeActionType type, QRCodeEntity qrCodeEntity)
        {
            var entity = new LogEntity();
            entity.Id = Guid.NewGuid();
            entity.ActionId = (int)type;
            entity.QRCodeId = qrCodeEntity.Id;
            entity.CreationTime = DateTime.Now;
            entity.Json = JsonSerializer.Serialize(qrCodeEntity);
            _dbContext.Set<LogEntity>().Add(entity);
        }

        public Bitmap GenerateImage(string url, int pixel)
        {

            QRCodeGenerator generator = new QRCodeGenerator();
            QRCodeData codeData = generator.CreateQrCode(url, QRCodeGenerator.ECCLevel.M, true);
            QRCoder.QRCode qrcode = new QRCoder.QRCode(codeData);

            Bitmap qrImage = qrcode.GetGraphic(pixel, Color.Black, Color.White, true);
            return qrImage;
        }


        public Bitmap GenerateImageTest(string url,int requestedVersion)
        {
            QRCodeGenerator qrCodeGenerator = new QRCodeGenerator();

            //下边第二个参数代表二维码质量，最后那个参数6代表生成二维码密度
            QRCodeData qrCodeData = qrCodeGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.M, true, true, QRCodeGenerator.EciMode.Utf8, requestedVersion);
            QRCode qrCode = new QRCode(qrCodeData);

            Bitmap qrImage = qrCode.GetGraphic(10, Color.Black, Color.White, true);
            return qrImage;
        }

    }
}
