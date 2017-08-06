using System.Collections.Generic;
using System.Linq;
using USQLINTERFACES = Microsoft.Analytics.Interfaces;

namespace MarkdownFormat
{
    [USQLINTERFACES.SqlUserDefinedOutputter(AtomicFileProcessing = true)]
    public class MarkdownOutputter : USQLINTERFACES.IOutputter
    {
        private int row_count;
        public bool OutputHeader;
        public bool OutputHeaderType;
        public bool EncodeHtml;
        public bool ComplexTypeParameters;
        private string _LineSeparator = "\r\n";
        private string _FieldSeparator = "|";
        private string _Space  = " ";
        private string _Header = "---";

        public MarkdownOutputter( bool outputHeader = false, bool outputHeaderType = false, bool encodeHtml = false, bool complexTypeParameters = false)
        {
            row_count = 0;
            this.OutputHeader = outputHeader;
            this.OutputHeaderType = outputHeaderType;
            this.EncodeHtml = encodeHtml;
            this.ComplexTypeParameters = complexTypeParameters;
        }

        public override void Close()
        {
        }

        public override void Output(USQLINTERFACES.IRow row, USQLINTERFACES.IUnstructuredWriter output)
        {
            var streamWriter = new System.IO.StreamWriter(output.BaseStream);

            // Metadata schema initialization to enumerate column names
            var schema = row.Schema;


            if (this.row_count == 0)
            {
                if (this.OutputHeader)
                {
                    streamWriter.Write(this._FieldSeparator);
                    for (int i = 0; i < schema.Count(); i++)
                    {
                        var col = schema[i];
                        streamWriter.Write(this._Space);
                        streamWriter.Write(col.Name);
                        streamWriter.Write(this._Space);
                        if (this.OutputHeaderType)
                        {
                            streamWriter.Write(UdoUtils.get_usql_type_name(col.Type));
                            streamWriter.Write(this._Space);
                        }
                        streamWriter.Write(this._FieldSeparator);
                    }
                    streamWriter.Write(_LineSeparator);
                    streamWriter.Flush();

                    streamWriter.Write(this._FieldSeparator);
                    for (int i = 0; i < schema.Count(); i++)
                    {
                        var col = schema[i];
                        streamWriter.Write(this._Space);
                        streamWriter.Write(this._Header);
                        streamWriter.Write(this._Space);
                        streamWriter.Write(this._FieldSeparator);
                    }
                    streamWriter.Write(_LineSeparator);
                    streamWriter.Flush();
                }
            }

            // Data row output
            streamWriter.Write(this._FieldSeparator);
            for (int i = 0; i < schema.Count(); i++)
            {
                var col = schema[i];
                string val = string.Empty;

                try
                {
                    var coltype = col.Type;
                    val = UdoUtils.GetValueDisplayString(row, coltype, val, col, this.ComplexTypeParameters);
                }
                catch (System.NullReferenceException exc)
                {
                    // Handling NULL values--keeping them empty
                    val = "NullReferenceException";
                }

                streamWriter.Write(this._Space);

                if (this.EncodeHtml)
                {
                    var encoded_val = System.Web.HttpUtility.HtmlEncode(val);
                    val = encoded_val;
                }

                streamWriter.Write(val);
                streamWriter.Write(this._Space);
                streamWriter.Write(this._FieldSeparator);
            }
            streamWriter.Write(this._LineSeparator);
            streamWriter.Flush();

            this.row_count++;
        }
    }
}