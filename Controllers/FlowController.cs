using Microsoft.AspNetCore.Mvc;
using Test.Flows;

namespace Test.Controllers;

[ApiController]
[Route("[controller]")]
public class FlowController : ControllerBase
{
    private readonly HelloWorldFlows _flows;

    public FlowController(HelloWorldFlows flows)
    {
        _flows = flows;
    }

    [HttpPost]
    public async Task Post(string flowId)
    {
        await _flows.Run(instanceId: flowId, param: flowId);
    }
}
