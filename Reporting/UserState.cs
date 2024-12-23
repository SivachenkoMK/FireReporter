﻿namespace FireReporter.Reporting;

public class UserState
{
    private readonly TimeSpan _sessionActiveSpan = TimeSpan.FromHours(1);
    public Guid IncidentId { get; private set; }
    public DateTime IncidentTimestamp { get; private set; }

    public UserState()
    {
        // Initialize with a new incident ID and timestamp
        TryNewSessionCreation();
    }

    public bool TryNewSessionCreation()
    {
        var newSessionCreationRequired = DateTime.UtcNow - IncidentTimestamp > _sessionActiveSpan;
        if (newSessionCreationRequired)
        {
            IncidentId = Guid.NewGuid();
            IncidentTimestamp = DateTime.UtcNow;
        }

        return newSessionCreationRequired;
    }
}