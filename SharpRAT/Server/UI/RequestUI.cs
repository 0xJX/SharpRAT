namespace Server
{
    public class RequestItem : RequestUI
    {
        public RequestType requestType = 0;
        public string szText = "";
    }

    public class RequestUI
    {
        private readonly List<RequestItem> requestArray = new();

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
            if (GetRequests().Count != 0)
            {
                // Return first request index.
                // The request shall be removed by the responding function
                return 0;
            }
            return -1;
        }

        public void Request(string text, RequestType requestType)
        {
            RequestItem newRequest = new();
            newRequest.requestType = requestType;
            newRequest.szText = text;
            GetRequests().Add(newRequest);
        }

        public void Request(RequestType requestType)
        {
            RequestItem newRequest = new();
            newRequest.requestType = requestType;
            GetRequests().Add(newRequest);
        }

        public string GetRequestText(int index)
        {
            return GetRequests()[index].szText;
        }

        public RequestType GetRequestType(int index)
        {
            return GetRequests()[index].requestType;
        }

        public void ClearRequestOfType(RequestType requestType)
        {
            for(int i = 0; i < GetRequests().Count; i++)
            {
                if (GetRequests()[i].requestType == requestType)
                    GetRequests().RemoveAt(i);
            }
        }
    }
}
