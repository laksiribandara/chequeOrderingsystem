using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Cheque_Ordering_system.Common
{
    public class Enums
    {

        public enum StaffApprover
        {
            Pending = 0,//To add more
            PendingHrModification = 1,
            ApprovedByMgr = 5,
            ApprovedByHRMgr = 6,
            RejectedByMgr = 10,
            RejectedByHRMgr = 11
        }
        public enum RecordStatus
        {
            Edited = 0,
            New_Recorde = 5
        }
        public enum AreaOffice
        {
            With = 0,
            Without = 1
        }

        public enum ShiftDetails
        {
            Overtime = 1,
            Standard = 0,

            NightShiftCalcByRate = 1,
            NightShiftCalcByPercentage =2,

            NotInUse = 0,
            FixedBreakTimes = 1,
            FixedBreakDurations = 2,
            DurationWithInTimeRange = 3,

            BreakDeductible = 1,
            BreakNotDeductible = 0,

            CalculateAbsence = 1,
            NoCalculateAbsence = 0,
            CalculateTardiness = 1,
            NoCalculateTardiness = 0,
            CalculateUndertime = 1,
            NoCalculateUndertime = 0,
            CalculateUnscheduledBreaks = 1,
            NoCalculateUnscheduledBreaks = 0,
            CalculateWorkBeforeShift = 1,
            NoCalculateWorkBeforeShift = 0,
            ComputeWBSAnomalies = 1,
            NoComputeWBSAnomalies = 0,
            CalculateWorkAfterShift = 1,
            NoCalculateWorkAfterShift = 0,
            ComputeWASAnomalies = 1,
            NoComputeWASAnomalies = 0,
            ComputeNigthShift = 1,
            NoComputeNigthShift = 0,
            ClacOTMethod1 = 1,
            NoClacOTMethod1 = 0,
            ClacOTMethod2 = 1,
            NoClacOTMethod2 = 0

        }

        public enum TowRowFlag
        {
            Enable = 1,
            Disable = 0
        }

        public enum OTApprovalStatus
        {
            New_Entry_Reject = 0,
            New_Entry_Approve = 1,
            New_Entry_Pending = 2,
            Modify_Entry_Approve = 3,
            Modify_Entry_Reject = 4,
            Modify_Entry_Pending = 5,
            Delete_Entry_Approve = 6,
            Delete_Entry_Reject = 7,
            Delete_Entry_Pending = 8,
            Delete_Tag = 9,
            New_Entry_Temp_Add = 10
        }

       
    }
}