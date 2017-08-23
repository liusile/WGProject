namespace Service.Model
{
    public class RolePermission
    {
        public bool Approve { get; internal set; } = false;
        public bool ApproveCheck { get; internal set; } = false;
        public bool RejectCheck { get; internal set; } = false;
        public bool Delete { get; internal set; } = false;
        public bool DeleteCheck { get; internal set; } = false;
        public bool Edit { get; internal set; } = false;
        public bool EditCheck { get; internal set; } = false;
        public int ModuleId { get; internal set; }
        public string ModuleName { get; internal set; }
        public bool Reject { get; internal set; } = false;
        public bool View { get; internal set; } = false;
        public bool ViewCheck { get; internal set; }
        public int ViewID { get; internal set; }
        public int DeleteID { get; internal set; }
        public int EditID { get; internal set; }
        public int ApproveID { get; internal set; }
        public int RejectID { get; internal set; }
    }
}