using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logger;

namespace SocketIOClient
{
	class EndPointClient : IEndPointClient
	{
		public IClient Client { get; private set; }
		public string EndPoint { get; private set; }

		public EndPointClient(IClient client, string endPoint)
		{
			this.validateNameSpace(endPoint);
			this.Client = client;
			this.EndPoint = endPoint;
		}

		private void validateNameSpace(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentNullException("nameSpace", "Parameter cannot be null");
			if (name.Contains(':'))
				throw new ArgumentException("Parameter cannot contain ':' characters", "nameSpace");
		}
			
		public void On(string eventName, Action<Messages.IMessage> action)
		{
			this.Client.On(eventName, this.EndPoint, action);
		}

		public void Emit(string eventName, dynamic payload, Action<dynamic> callBack = null)
		{
            try
            {
                this.Client.Emit(eventName, payload, this.EndPoint, callBack);
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
            }
		}

		public void Send(Messages.IMessage msg)
		{
            try
            {
                msg.Endpoint = this.EndPoint;
                this.Client.Send(msg);
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
            }
        }
	}
}
