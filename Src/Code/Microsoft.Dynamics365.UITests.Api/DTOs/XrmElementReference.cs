using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dynamics365.UITests.Api
{
    public static class Elements
    {
        public static Dictionary<string, string> Xpath = new Dictionary<string, string>()
        {
            //Business Process Flow
            { "BPF_NextStage"       , "id(\"stageAdvanceActionContainer\")/div[1]"},
            { "BPF_PreviousStage"   , "id(\"stageBackActionContainer\")/div[1]/img[1]"},
            { "BPF_Hide"            , "id(\"processControlCollapseButton\")/img[1]" },
            { "BPF_SetActive"       , "id(\"stageSetActiveActionContainer\")/div[1]" },
            { "BPF_SelectStage"     , "id(\"stage_[STAGENUM]\")/div[2]/div[1]/div[1]/div[1]/span[1]" },

            //Dialogs
            { "Dialog_Header"       , "id(\"dialogHeaderTitle\")"},
            { "Dialog_CloseOpportunityOk"       , "id(\"ok_id\")"},

            //Frames
            { "Frame_ContentPanel"       , "id(\"crmContentPanel\")"},
            { "Frame_ContentFrame"       , "id(\"currentcontentid\")"},
            { "Frame_DialogFrame"       , "id(\"InlineDialog\")"},
            { "Frame_QuickFindFrame"       , "id(\"globalquickcreate_container_NavBarGloablQuickCreate\")"},

        };

        public static Dictionary<string, string> ElementId = new Dictionary<string, string>()
        {
            //Frames
            { "Frame_ContentFrameId"       , "currentcontentid"},
            { "Frame_DialogFrameId"       , "InlineDialog_Iframe"},
            { "Frame_QuickFindFrame"       , "InlineDialog_Iframe"},

            //Dialogs
            { "Dialog_ActualRevenue"       , "actualrevenue_id"},
            { "Dialog_CloseDate"       , "closedate_id"},
            { "Dialog_Description"       , "description_id"},
        };
    }

    public static class Reference
    {
        public static class BusinessProcessFlow
        {
            public static string NextStage = "BPF_NextStage";
            public static string PreviousStage = "BPF_PreviousStage";
            public static string Hide = "BPF_Hide";
            public static string SetActive = "BPF_SetActive";
            public static string SelectStage = "BPF_SelectStage";
        }

        public static class Dialogs
        {
            public static string Header = "Dialog_Header";

            public static class CloseOpportunity
            {
                public static string ActualRevenueId = "Dialog_ActualRevenue";
                public static string CloseDateId = "Dialog_CloseDate";
                public static string DescriptionId = "Dialog_Description";
                public static string Ok = "Dialog_CloseOpportunityOk";
            }
        }

        public static class Frames
        {
            public static string ContentPanel = "Frame_ContentPanel";
            public static string ContentFrameId = "Frame_ContentFrameId";
            public static string DialogFrame = "Frame_DialogFrame";
            public static string DialogFrameId = "Frame_DialogFrameId";
            public static string QuickFindFrame = "Frame_QuickFindFrame";
            public static string QuickFindFrameId = "Frame_QuickFindFrameId";

        }
    }
}
