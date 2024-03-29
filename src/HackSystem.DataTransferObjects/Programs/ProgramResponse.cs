﻿namespace HackSystem.DataTransferObjects.Programs;

public class ProgramResponse
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string IconUri { get; set; }

    public bool Enabled { get; set; }

    public bool SingleInstance { get; set; }

    public string EntryAssemblyName { get; set; }

    public string EntryTypeName { get; set; }

    public string EntryParameter { get; set; }

    public bool Mandatory { get; set; }
}
