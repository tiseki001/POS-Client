using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.XML
{
    public class ServiceModel
    {
        private string url;

        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        private List<EventModel> eventList = new List<EventModel>();

        public List<EventModel> EventList
        {
            get { return eventList; }
            set { eventList = value; }
        }
    }

    public class EventModel
    {
        private string className;

        public string ClassName
        {
            get { return className; }
            set { className = value; }
        }

        private string functionName;

        public string FunctionName
        {
            get { return functionName; }
            set { functionName = value; }
        }

        private string timerType;

        public string TimerType
        {
            get { return timerType; }
            set { timerType = value; }
        }

        private List<string> timerInfo = new List<string>();

        public List<string> TimerInfo
        {
            get { return timerInfo; }
            set { timerInfo = value; }
        }
    }

    public class TimerModel
    {
        private string url;

        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        private string className;

        public string ClassName
        {
            get { return className; }
            set { className = value; }
        }

        private string functionName;

        public string FunctionName
        {
            get { return functionName; }
            set { functionName = value; }
        }

        private string timerInfo;

        public string TimerInfo
        {
            get { return timerInfo; }
            set { timerInfo = value; }
        }

        private DateTime preExcTime;

        public DateTime PreExcTime
        {
            get { return preExcTime; }
            set { preExcTime = value; }
        }

        private bool isExcuted;

        public bool IsExcuted
        {
            get { return isExcuted; }
            set { isExcuted = value; }
        }

       
        private DateTime dateTimeInfo;

        public DateTime DateTimeInfo
        {
            get { return dateTimeInfo; }
            set { dateTimeInfo = value; }
        }


    }
}
