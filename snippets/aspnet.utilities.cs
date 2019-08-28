namespace softwareithink.tidbits
{
    public static class Utilities
    {
                public XmlDocument RetrieveSoapRequest()
        {
            // Initialize soap request XML
            XmlDocument xmlSoapRequest = new XmlDocument();

            // Get raw request body
            Stream receiveStream = HttpContext.Current.Request.InputStream;

            // Move to beginning of input stream and read
            receiveStream.Position = 0;
            using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
            {
                // Load into XML document
                xmlSoapRequest.Load(readStream);
            }

            // Return
            return xmlSoapRequest;
        }

        private string ToIndentedString(XmlDocument doc)
        {
            var stringWriter = new StringWriter(new StringBuilder());
            var xmlTextWriter = new XmlTextWriter(stringWriter) { Formatting = System.Xml.Formatting.Indented };
            doc.Save(xmlTextWriter);
            return stringWriter.ToString();
        }

   }
}