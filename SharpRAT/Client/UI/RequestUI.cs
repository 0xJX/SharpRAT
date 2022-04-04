namespace Client
{
    public class RequestItem : RequestUI
    {
        public RequestType requestType = 0;
        public bool bRequest = false;
        public string szText = "";
    }

    public class RequestUI
    {
        private readonly List<RequestItem> requestArray = new List<RequestItem>();

        public enum RequestType
        {
            UI_SHOW_MESSAGEBOX = 0
        }

        public List<RequestItem> GetRequests()
        {
            return requestArray;
        }

        public int RequestReceived()
        {
            int i = 0;
            foreach (RequestItem request in GetRequests())
            {
                i++;
                if (request.bRequest)
                {
                    request.bRequest = false;
                    return i;
                }
            }
            return 0;
        }

        public void Request(string text, RequestType requestType)
        {
            RequestItem newRequest = new RequestItem();
            newRequest.requestType = requestType;
            newRequest.szText = text;
            newRequest.bRequest = true;
            GetRequests().Add(newRequest);
        }

        public string GetRequestText(int index)
        {
            return GetRequests()[index - 1].szText;
        }

        public RequestType GetRequestType(int index)
        {
            return GetRequests()[index - 1].requestType;
        }
    }
}
