using System.Linq;
using System.Text;

namespace Билдер_отчетов
{
    // інтерфейс будівельника звіту
    interface IReportBuilder
    {
        void SetHeader(string header);
        void SetBody(string body);
        void SetFooter(string footer);
        string GetReport();
    }

    // конкретний будівельник звіту у вигляді текстового документу
    class TextReportBuilder : IReportBuilder
    {
        private string _report;

        public void SetHeader(string header)
        {
            _report += "Header: " + header + "\n";
        }

        public void SetBody(string body)
        {
            _report += "Body: " + body + "\n";
        }

        public void SetFooter(string footer)
        {
            _report += "Footer: " + footer + "\n";
        }

        public string GetReport()
        {
            return _report;
        }
    }

    class HTMLReportBuilder : IReportBuilder
    {
        private string _report;

        public HTMLReportBuilder()
        {
            _report += "<html><style>.box {\r\n  display: flex;\r\n  flex-direction: column;\r\n align-items: center;\r\n  justify-content: space-between;\r\n  height: 500px;\r\n}</style> <body><div class=\"box\">";
        }

        public void SetHeader(string header)
        {
            _report += "<header>\n<h1>\n" + header + "</h1>\n</header>\n";
        }

        public void SetBody(string body)
        {
            _report += "<main>\n<h5>\n" + body + "</h5>\n</main>\n";
        }

        public void SetFooter(string footer)
        {
            _report += "<footer>\n<h1>\n" + footer + "</h1>\n</footer>\n";
        }

        public string GetReport()
        {
            _report += "</div></body></html>";
            return _report;
        }
    }

    class ReportDirector
    {
        private IReportBuilder _builder;

        public ReportDirector(IReportBuilder builder)
        {
            _builder = builder;
        }

        public void ConstructReport(string header, string body, string footer)
        {
            _builder.SetHeader(header);
            _builder.SetBody(body);
            _builder.SetFooter(footer);
        }

        public string GetReport()
        {
            return _builder.GetReport();
        }
    }
}
