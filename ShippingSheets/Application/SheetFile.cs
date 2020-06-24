using System;

namespace ShippingSheets.Application
{
    public class SheetFile
    {
        public static readonly SheetFile Empty = new SheetFile(null, null, Array.Empty<byte>());

        public SheetFile(string fileName, string contentType, byte[] content)
        {
            FileName = fileName;
            ContentType = contentType;
            Content = content;
        }

        public string FileName { get; }
        public string ContentType { get; }
        public byte[] Content { get; }
    }
}
