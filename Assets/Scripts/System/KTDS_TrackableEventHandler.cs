using System;
using UnityEngine;

public class KTDS_TrackableEventHandler : DefaultTrackableEventHandler
{
    [SerializeField] private string m_ID;

    private Action<string, bool> m_actTrack;
    public Action<string, bool> ACT_TRACK { set { m_actTrack = value; } }

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        if (m_actTrack != null) m_actTrack(m_ID, true);
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        if (m_actTrack != null) m_actTrack(m_ID, false);
    }
}
