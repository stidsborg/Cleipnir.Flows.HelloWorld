using Cleipnir.ResilientFunctions.Domain;
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

    [HttpPut]
    public async Task RestartFailed(string flowId)
    {
        var controlPanel = await _flows.ControlPanel(flowId);
        if (controlPanel == null) return;

        if (controlPanel.Status == Status.Failed)
        {
            controlPanel.Param = "do_not_throw";
            await controlPanel.ReInvoke();
        }
            
    }
}
