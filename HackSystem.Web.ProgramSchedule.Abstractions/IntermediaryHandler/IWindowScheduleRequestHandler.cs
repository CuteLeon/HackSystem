using HackSystem.Intermediary.Application;
using HackSystem.Web.ProgramSchedule.Entity;
using HackSystem.Web.ProgramSchedule.Intermediary;

namespace HackSystem.Web.ProgramSchedule.IntermediaryHandler;

public interface IWindowScheduleRequestHandler : IIntermediaryRequestHandler<WindowScheduleRequest, WindowScheduleResponse>
{
    delegate void WindowScheduleHandler(ProgramWindowDetail programWindowDetail);

    event WindowScheduleHandler? OnWindowSchedule;
}
