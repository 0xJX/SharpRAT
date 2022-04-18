namespace Server
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
            UI_ADD_USER = 0,
            UI_REMOVE_USER,
            UI_UPDATE_STATUS,
            UI_UPDATE_FILES,
            UI_UPDATE_SCREENSHOT
        }

        public List<RequestItem> GetRequests()
        {
            return requestArray;
        }

        public int RequestReceived()
        {
            int i = 0;
            foreach(RequestItem request in GetRequests())
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
            RequestItem newRequest = new();
            newRequest.requestType = requestType;
            newRequest.szText = text;
            newRequest.bRequest = true;
            GetRequests().Add(newRequest);
        }

        public void Request(RequestType requestType)
        {
            RequestItem newRequest = new();
            newRequest.requestType = requestType;
            newRequest.bRequest = true;
            GetRequests().Add(newRequest);
        }

        public string GetRequestText(int index)
        {
            return GetRequests()[index-1].szText;
        }

        public RequestType GetRequestType(int index)
        {
            return GetRequests()[index-1].requestType;
        }
    }
}
