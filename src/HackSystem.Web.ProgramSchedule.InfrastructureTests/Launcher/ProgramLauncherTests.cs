using Xunit;

namespace HackSystem.Web.ProgramSchedule.Infrastructure.Launcher.Tests;

public class ProgramLauncherTests
{
    class Launcher1 { Type Launch1() => default; static Type Launch2() => default; }
    class Launcher2 { void Launch1() { } static void Launch2() { } }
    class Launcher3 { public Type Launch1() => default; public void Launch2() { } }
    static class Launcher4 { public static Type Launch1() => default; public static void Launch2() { } }
    static class Launcher5 { public static void Launch() { } }
    static class Launcher6 { public static Type Launch() => default; }
    static class Launcher7 { public static Type Launch() => default; public static Type Launch1(string parameter) => default; }
    static class Launcher8 { public static Type Launch() => default; public static void Launch(string parameter) { } }
    static class Launcher9 { public static void Launch() { } public static Type Launch(string parameter) => default; }
    static class Launcher10 { public static Type Launch() => default; public static void Launch1() { } }
    static class Launcher11 { public static Type Launch(string parameter1, string parameter2) => default; public static void Launch() { } }

    [Fact()]
    public void GetProgramEntryMethodTest()
    {
        Assert.Throws<EntryPointNotFoundException>(() => ProgramLauncher.GetProgramEntryMethod(typeof(Launcher1)));
        Assert.Throws<EntryPointNotFoundException>(() => ProgramLauncher.GetProgramEntryMethod(typeof(Launcher2)));
        Assert.Throws<EntryPointNotFoundException>(() => ProgramLauncher.GetProgramEntryMethod(typeof(Launcher3)));
        Assert.Throws<EntryPointNotFoundException>(() => ProgramLauncher.GetProgramEntryMethod(typeof(Launcher4)));
        Assert.Equal(nameof(Launcher5.Launch), ProgramLauncher.GetProgramEntryMethod(typeof(Launcher5)).Name);
        Assert.Equal(nameof(Launcher6.Launch), ProgramLauncher.GetProgramEntryMethod(typeof(Launcher6)).Name);
        Assert.Equal(nameof(Launcher7.Launch), ProgramLauncher.GetProgramEntryMethod(typeof(Launcher7)).Name);
        Assert.Equal(nameof(Launcher8.Launch), ProgramLauncher.GetProgramEntryMethod(typeof(Launcher8)).Name);
        Assert.Equal(typeof(System.Type), ProgramLauncher.GetProgramEntryMethod(typeof(Launcher8)).ReturnType);
        Assert.Equal(nameof(Launcher9.Launch), ProgramLauncher.GetProgramEntryMethod(typeof(Launcher9)).Name);
        Assert.Equal(typeof(System.Type), ProgramLauncher.GetProgramEntryMethod(typeof(Launcher9)).ReturnType);
        Assert.Equal(typeof(System.Type), ProgramLauncher.GetProgramEntryMethod(typeof(Launcher9)).ReturnType);
        Assert.Equal(nameof(Launcher10.Launch), ProgramLauncher.GetProgramEntryMethod(typeof(Launcher10)).Name);
        Assert.Equal(nameof(Launcher11.Launch), ProgramLauncher.GetProgramEntryMethod(typeof(Launcher11)).Name);
        Assert.Equal(typeof(void), ProgramLauncher.GetProgramEntryMethod(typeof(Launcher11)).ReturnType);
    }
}
