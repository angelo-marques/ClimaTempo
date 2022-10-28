using Amazon.SQS;
using Amazon.SQS.Model;
using System;
using System.Threading.Tasks;

namespace MensageriaAWS
{
    public async Task Class1()
    {
        var cliente = new AmazonSQSClient(Amazon.RegionEndpoint.AFSouth1);
        var request = new ReceiveMessageRequest {
            GueueUrl = "";
        }

    }
}
