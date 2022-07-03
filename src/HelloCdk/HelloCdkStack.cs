using Amazon.CDK;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.APIGateway;
using Constructs;

namespace HelloCdk
{
    public class HelloCdkStack : Stack
    {
        internal HelloCdkStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var dateLambda = new Amazon.CDK.AWS.Lambda.Function(this, "dateLambda", new FunctionProps
            {
                Runtime = Runtime.DOTNET_6,
                Code = Code.FromAsset("src/DateFunction/bin/Release/net6.0/linux-x64/publish"),
                Handler = "DateFunction::DateFunction.Functions::Get"
            });

            new LambdaRestApi(this, "dateApiEndpoint", new LambdaRestApiProps
            {
                Handler = dateLambda
            });

            var timeLambda = new Amazon.CDK.AWS.Lambda.Function(this, "timeLambda", new FunctionProps
            {
                Runtime = Runtime.DOTNET_6,
                Code = Code.FromAsset("src/TimeFunction/bin/Release/net6.0/linux-x64/publish"),
                Handler = "TimeFunction::TimeFunction.Functions::Get"
            });

            new LambdaRestApi(this, "timeApiEndpoint", new LambdaRestApiProps
            {
                Handler = timeLambda
            });
        }
    }
}
