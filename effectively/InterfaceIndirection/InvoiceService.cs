namespace effectively.ExtractInterface {
    using System.Net;
    using System.Web;

    public class InvoiceService
    {
        ISetResponseCode responseCodeSetter;

        public InvoiceService(ISetResponseCode responseCode)
        {
            this.responseCodeSetter = responseCode;
        }

        public InvoiceDto GetInvoice(int id)
        {
            this.responseCodeSetter.SetStatusCode((int)HttpStatusCode.OK);

            return new InvoiceDto { Id = id };
        }
    }

    public interface ISetResponseCode
    {
        void SetStatusCode(int statusCode);
    }

    public class HttpResponseCode : ISetResponseCode
    {
        public void SetStatusCode(int statusCode)
        {
            HttpContext.Current.Response.StatusCode = statusCode;
        }
    }
}