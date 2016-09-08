using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public enum BurnDuration
    {
        PreviousDay,
        Last3Days,
        Last7Days,
        Last15Days,
        Last30Days,
        Duration
    };
    public enum ResourceLevel
    {
        Trainee,
        Engineer,
        Senior,
        Lead,
        Manager
    };

    public enum QueryInterest
    {
        Product,
        Site,
        Team,
        Resource,
    };

    public enum ActivityType
    {
        None,
        Development,
        Testing,
        Documentation,
    };

    public enum ItemType
    {
        ALL = 0,
        Task = 1,
        Bug = 2,
        UserStory = 3,
        OutOfScope = 4,
        TechnicalDebt = 5,
        Feature = 6,
        EnhancementRequest = 7,
        Item =8
    };

    public enum ItemBroadType
    {
        None,
        UserStory,
        Bug
    }

    public enum StatusType
    {
        Any = 0,
        Active = 1,
        Closed = 2,
        Impeded = 3,
        New = 4,      
        Open = 5,
        Resolved = 6,
    }

    public enum BugStatusType
    {
        None,
        New,
        Open,
        Resolved,
        Closed
    }

    public enum StoryStatusType
    {
        None,
        New,
        Active,
        Impeded,
        Resolved,
        Closed
    };
    public enum ReferenceType
    {
        Child,
        Parent,
    }

    public enum ReportType
    {
        Burn,
    }

    public enum ViewType
    {
        Story,
        Resource,
        Team,
        Site,
    }
}
