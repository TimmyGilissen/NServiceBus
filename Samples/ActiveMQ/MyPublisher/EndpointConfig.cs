﻿namespace MyPublisher
{
    using NServiceBus;
    using NServiceBus.ActiveMQ.Config;

    class EndpointConfig :  IConfigureThisEndpoint, AsA_Publisher,IWantCustomInitialization
    {
        public void Init()
        {
            var config = Configure.With()
                //this overrides the NServiceBus default convention of IEvent
                .DefaultBuilder()
                .ActiveMqTransport("A", "activemq:tcp://localhost:61616")
                .XmlSerializer(dontWrapSingleMessages: true);
        }
    }
}
