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
            { "BPF_Ok"     , "id(\"SwitchProcess-Select\")" },

            //Dialogs
            { "Dialog_Header"       , "id(\"dialogHeaderTitle\")"},
            { "Dialog_DeleteHeader"       , "id(\"tdDialogHeader\")"},            
            { "Dialog_WorkflowHeader", "id(\"DlgHdContainer\")" },
            { "Dialog_ProcessFlowHeader", "id(\"processSwitcherFlyout\")" },
            { "Dialog_CloseOpportunityOk"       , "id(\"ok_id\")"},
            { "Dialog_AssignOk"       , "id(\"ok_id\")"},
            { "Dialog_DeleteOk"       , "id(\"butBegin\")"},
            { "Dialog_DuplicateOk"       , "id(\"butBegin\")"},
            { "Dialog_DuplicateCancel"       , "id(\"cmdDialogCancel\")"},
            { "Dialog_ConfirmWorkflow"       , "id(\"butBegin\")"},

            //GuidedHelp
            { "GuidedHelp_MarsOverlay"       , "id(\"marsOverlay\")"},
            { "GuidedHelp_ButBegin"       , "id(\"butBegin\")"},
            { "GuidedHelp_ButtonClose", "id(\"buttonClose\")" },


            //Frames
            { "Frame_ContentPanel"       , "id(\"crmContentPanel\")"},
            { "Frame_ContentFrame"       , "id(\"currentcontentid\")"},
            { "Frame_DialogFrame"       , "id(\"InlineDialog\")"},
            { "Frame_QuickCreateFrame"       , "id(\"globalquickcreate_container_NavBarGloablQuickCreate\")"},

            //Navigation
            { "Nav_HomeTab"       , "id(\"HomeTabLink\")"},
            { "Nav_ActionGroup"       , "id(\"actionGroupControl\")"},
            { "Nav_SubActionGroup"       , "id(\"actionGroupControl\")"},
            { "Nav_SubActionGroupContainer"       , "id(\"detailActionGroupControl\")"},
            { "Nav_GuidedHelp"       , "id(\"TabButtonHelpId\")/a"},
            { "Nav_AdminPortal"       , "id(\"TabAppSwitcherNode\")/a"},
            { "Nav_Settings"       , "id(\"TabButtonSettingsId\")/a"},
            { "Nav_Options"       , "id(\"navTabButtonSettingsOptionsId\")"},
            { "Nav_PrintPreview"       , "id(\"navTabButtonSettingsPrintPreviewId\")"},
            { "Nav_AppsForCrm"       , "id(\"navTabButtonSettingsNavAppsForCrmId\")"},
            { "Nav_WelcomeScreen"       , "id(\"navTabButtonSettingsNavTourId\")"},
            { "Nav_About"       , "id(\"navTabButtonSettingsAboutId\")"},
            { "Nav_OptOutLP"       , "id(\"navTabButtonSettingsGuidedHelpId\")"},
            { "Nav_Privacy"       , "id(\"NodeSettingsPrivacyStatementId\")"},
            { "Nav_UserInfo"       , "id(\"navTabButtonUserInfoLinkId\")"},
            { "Nav_SignOut"       , "id(\"navTabButtonUserInfoSignOutId\")"},
            { "Nav_TabGlobalMruNode"       , "id(\"TabGlobalMruNode\")"},
            { "Nav_GlobalCreate"       , "id(\"navTabGlobalCreateImage\")"},
            { "Nav_AdvFindSearch"       , "id(\"AdvFindSearch\")"},
            { "Nav_FindCriteria"       , "id(\"findCriteriaButton\")"},
            { "Nav_Shuffle"       , "id(\"nav-shuffle\")"},
            { "Nav_TabNode"       , "id(\"TabNode_tab0Tab\")"},
                
            
            //Grid
            { "Grid_JumpBar"       , "id(\"crmGrid_JumpBar\")"},
            { "Grid_ShowAll"       , "id(\"crmGrid_JumpBar\")/tbody/tr/td[1]"},
            { "Grid_GridTable"       , "id(\"gridBodyTable\")"},
            { "Grid_RowSelect"       , "id(\"gridBodyTable\")/tbody/tr[[INDEX]]/td[1]"},
            { "Grid_Filter"       , "id(\"filterButtonLink\")"},
            { "Grid_ChartList"       , "id(\"visualizationListLink\")"},
            { "Grid_ChartDialog"       , "id(\"Dialog_1\")"},
            { "Grid_ToggleSelectAll"   , "id(\"crmGrid_gridBodyTable_checkBox_Image_All\")" },
            { "Grid_FirstPage"       , "id(\"fastRewind\")"},
            { "Grid_NextPage"       , "id(\"_nextPageImg\")"},
            { "Grid_PreviousPage"   , "id(\"_prevPageImg\")" },
            { "Grid_FindCriteriaImg"       , "id(\"crmGrid_findCriteriaImg\")"},
            { "Grid_FindCriteria"       , "id(\"crmGrid_findCriteria\")"},
            { "Grid_GridBodyTable"   , "id(\"gridBodyTable\")" },
            { "Grid_DefaultViewIcon"   , "id(\"defaultViewIcon\")" },
            { "Grid_ControlBar"   , "id(\"gridControlBar\")" },
            { "Grid_Refresh"   , "id(\"grid_refresh\")" },

             
            //Entity
            { "Entity_Close"       , "id(\"closeButton\")"},
            { "Entity_Save"       , "id(\"savefooter_statuscontrol\")"},
            { "Entity_ContentTable" , "id(\"flyoutFormSection_ContentTable\")" },
            { "Entity_SelectForm",  "id(\"FormSecNavigationControl-Icon\")"},

            //Global Search
            { "Search_Filter"       , "id(\"filterCombo\")"},
            { "Search_Text"       , "id(\"searchTextBox\")"},
            { "Search_Button"       , "id(\"SearchButton\")"},
            { "Search_Result"       , "id(\"contentResult\")"},
            { "Search_Container"       , "id(\"panoramaContainer\")"},

            //DashBoard
            { "DashBoard_Selector"       , "id(\"dashboardSelector\")"},

            //CommandBar
            { "CommandBar_RibbonManager"       , "id(\"crmRibbonManager\")"},
            { "CommandBar_List"       , "id(\"moreCommandsList\")"},
            { "CommandBar_MoreCommands"       , "id(\"moreCommands\")"},
            
            //ActivityFeed
            { "Notes_NotesControl"       , "id(\"notescontrol\")"},
            { "Notes_NotesWall"       , "id(\"notesWall\")"},
            { "Notes_NotesText"       , "id(\"createNote_notesTextBox\")"},
            { "Notes_NotesButton"       , "id(\"postButton\")"},
            { "Notes_PostWall"       , "id(\"activityFeedsWall\")"},
            { "Notes_PostButton"       , "id(\"postButton\")"},
            { "Notes_PostText"       , "id(\"postTextBox\")"},
            { "Notes_ActivityWall"       , "id(\"notescontrolactivityContainer_notescontrol\")"},
            { "Notes_ActivityStatusFilter"       , "id(\"activityWallFilterButton\")"},
            { "Notes_ActivityStatusFilterDialog"       , "id(\"moreActivitiesList\")"},
            { "Notes_ActivityStatusAll"       , "id(\"AllActivitiesButton\")"},
            { "Notes_ActivityStatusOpen"       , "id(\"OpenActivitiesButton\")"},
            { "Notes_ActivityStatusOverdue"       , "id(\"OverdueActivitiesButton\")"},
            { "Notes_ActivityAssociatedView"       , "id(\"OpenAssociatedGridView\")"},
            { "Notes_ActivityPhoneCallOk"       , "id(\"save4210QuickCreateButton\")"},
            { "Notes_ActivityTaskOk", "id(\"save4212QuickCreateButton\")"},
            { "Notes_ActivityMoreActivities", "id(\"moreActivitiesButton\")"},
            { "Notes_ActivityAddEmail", "id(\"AddemailButton\")"},
            { "Notes_ActivityAddAppointment", "id(\"AddappointmentButton\")"},
            { "Notes_ActivityAddPhoneCall", "id(\"activityLabelinlineactivitybar4210\")"},
            { "Notes_ActivityAddTask", "id(\"activityLabelinlineactivitybar4212\")"},

            //Login           
            { "Login_UserId", "id(\"cred_userid_inputtext\")"},
            { "Login_Password", "id(\"cred_password_inputtext\")"},
            { "Login_SignIn", "id(\"cred_sign_in_button\")"},

            //Notification           
            { "Notification_AppMessageBar", "id(\"crmAppMessageBar\")"},
            { "Notification_Close", "id(\"crmAppMessageBarCloseButton\")"},
    };

        public static Dictionary<string, string> ElementId = new Dictionary<string, string>()
        {
            //Frames
            { "Frame_ContentFrameId"       , "currentcontentid"},
            { "Frame_DialogFrameId"       , "InlineDialog[INDEX]_Iframe"},
            { "Frame_QuickCreateFrameId"       , "NavBarGloablQuickCreate"},

            //Dialogs
            { "Dialog_ActualRevenue"       , "actualrevenue_id"},
            { "Dialog_CloseDate"       , "closedate_id"},
            { "Dialog_Description"       , "description_id"},
            { "Dialog_UserOrTeamLookupId"       , "systemuserview_id"},

            //Entity
            { "Entity_TabId"       , "[NAME]_TAB_headerText_anchor"},

            //Global Search
            { "Search_EntityNameId"       , "entityName"},
            { "Search_RecordNameId"       , "attribone"},
            { "Search_EntityContainersId"       , "entitypic"},

            //ActivityFeed
            { "Notes_ActivityPhoneCallDescId"       , "quickCreateActivity4210controlId_description_i"},
            { "Notes_ActivityPhoneCallVoiceMailId"       , "PhoneCallQuickformleftvoiceCheckBoxContol"},
            { "Notes_ActivityPhoneCallDirectionId"       , "quickCreateActivity4210controlId_directioncode_i"},
            { "Notes_ActivityTaskSubjectId"      , "quickCreateActivity4212controlId_subject_i"},
            { "Notes_ActivityTaskDescriptionId"      , "quickCreateActivity4212controlId_description_i"},
            { "Notes_ActivityTaskScheduledEndId"      , "quickCreateActivity4212controlId_scheduledend_iDateInput"},
            { "Notes_ActivityTaskPriorityId"      , "quickCreateActivity4212controlId_prioritycode_i"},
    };

        public static Dictionary<string, string> CssClass = new Dictionary<string, string>()
        {
            //Navigation
            { "Nav_ActionGroupContainerClass"       , "navActionButtonContainer"},
            { "Nav_SubActionElementClass"       , "nav-rowBody"},
            
            //Dialogs
            { "Dialog_SwitchProcessTitleClass"       , "ms-crm-ProcessSwitcher-ProcessTitle"},

            //SetValue
            { "SetValue_LookupRenderClass"       , "Lookup_RenderButton_td"},

            //DashBoard
            { "DashBoard_ViewContainerClass"       , "ms-crm-VS-Menu"},

            //CommandBar
            { "CommandBar_FlyoutAnchorArrow"       , "flyoutAnchorArrow"},

            //Grid
            { "Grid_ViewContainerClass"     , "ms-crm-VS-Menu"},
            { "Grid_OpenChartClass"       , "ms-crm-ImageStrip-navLeft"},
            { "Grid_CloseChartClass"       , "ms-crm-PaneChevron"},
            { "Grid_SortColumnClass"       , "ms-crm-List-Sortable"},

            //Entity
            { "Entity_LookupRenderClass"       , "Lookup_RenderButton_td"},
            { "Entity_PopoutClass"       , "ms-crm-ImageStrip-popout"},

            //Notifications
            { "Notification_MessageBarRowClass"     , "crmAppMessageBarRow"},
            { "Notification_MessageBarButtonContainerClass"       , "crmAppMessageBarButtonContainer"},
            { "Notification_MessageBarMessageClass"       , "crmAppMessageBarMessage"},
            { "Notification_MessageBarTitleClass"       , "crmAppMessageBarTitle"},

            //SetValue
            { "SetValue_EditClass"     , "ms-crm-Inline-Edit"},
            { "SetValue_ValueClass"       , "ms-crm-Inline-Value"},     

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
            public static string Ok = "BPF_Ok";
        }

        public static class Dialogs
        {
            public static string Header = "Dialog_Header";
            public static string DeleteHeader = "Dialog_DeleteHeader";
            public static string WorkflowHeader = "Dialog_WorkflowHeader";
            public static string ProcessFlowHeader = "Dialog_ProcessFlowHeader";

            public static class CloseOpportunity
            {
                public static string ActualRevenueId = "Dialog_ActualRevenue";
                public static string CloseDateId = "Dialog_CloseDate";
                public static string DescriptionId = "Dialog_Description";
                public static string Ok = "Dialog_CloseOpportunityOk";
            }
            public static class Assign
            {
                public static string Ok = "Dialog_AssignOk";
                public static string UserOrTeamLookupId = "Dialog_UserOrTeamLookupId";
            }
            public static class Delete
            {
                public static string Ok = "Dialog_DeleteOk";
            }
            public static class SwitchProcess
            {
                public static string Process = "Dialog_SwitchProcessTitleClass";
            }
            public static class DuplicateDetection
            {
                public static string Save = "Dialog_DuplicateOk";
                public static string Cancel = "Dialog_DuplicateCancel";
            
            }

            public static class RunWorkflow
            {
                public static string Confirm = "Dialog_ConfirmWorkflow";
            }

        }
        public static class SetValue
        {
            public static string LookupRenderClass = "SetValue_LookupRenderClass";
            public static string EditClass = "SetValue_EditClass";
            public static string ValueClass = "SetValue_ValueClass";
 
        }

        public static class Frames
        {
            public static string ContentPanel = "Frame_ContentPanel";
            public static string ContentFrameId = "Frame_ContentFrameId";
            public static string DialogFrame = "Frame_DialogFrame";
            public static string DialogFrameId = "Frame_DialogFrameId";
            public static string QuickCreateFrame = "Frame_QuickCreateFrame";
            public static string QuickCreateFrameId = "Frame_QuickCreateFrameId";

        }

        public static class Navigation
        {
            public static string HomeTab = "Nav_HomeTab";
            public static string ActionGroup = "Nav_ActionGroup";
            public static string ActionGroupContainerClass = "Nav_ActionGroupContainerClass";
            public static string SubActionGroup = "Nav_SubActionGroup";
            public static string SubActionGroupContainer = "Nav_SubActionGroupContainer";
            public static string SubActionElementClass = "Nav_SubActionElementClass";
            public static string GuidedHelp = "Nav_GuidedHelp";
            public static string AdminPortal = "Nav_AdminPortal";
            public static string Settings = "Nav_Settings";
            public static string Options = "Nav_Options";
            public static string PrintPreview = "Nav_PrintPreview";
            public static string AppsForCRM = "Nav_AppsForCrm";
            public static string WelcomeScreen = "Nav_WelcomeScreen";
            public static string About = "Nav_About";
            public static string OptOutLP = "Nav_OptOutLP";
            public static string Privacy = "Nav_Privacy";
            public static string UserInfo = "Nav_UserInfo";
            public static string SignOut = "Nav_SignOut";
            public static string TabGlobalMruNode = "Nav_TabGlobalMruNode";
            public static string GlobalCreate = "Nav_GlobalCreate";
            public static string AdvFindSearch = "Nav_AdvFindSearch";
            public static string FindCriteria = "Nav_FindCriteria";
            public static string Shuffle = "Nav_Shuffle";
            public static string TabNode = "Nav_TabNode";
        }
        public static class Grid
        {
            public static string JumpBar = "Grid_JumpBar";
            public static string ShowAll = "Grid_ShowAll";
            public static string GridTable = "Grid_GridTable";
            public static string RowSelect = "Grid_RowSelect";
            public static string Filter = "Grid_Filter";
            public static string ChartList = "Grid_ChartList";
            public static string ChartDialog = "Grid_ChartDialog";
            public static string ToggleSelectAll = "Grid_ToggleSelectAll";
            public static string FirstPage = "Grid_FirstPage";
            public static string NextPage = "Grid_NextPage";
            public static string PreviousPage = "Grid_PreviousPage";
            public static string DefaultViewIcon = "Grid_DefaultViewIcon";
            public static string FindCriteriaImg = "Grid_FindCriteriaImg";
            public static string GridBodyTable = "Grid_GridBodyTable";
            public static string FindCriteria = "Grid_FindCriteria";
            public static string ControlBar = "Grid_ControlBar";
            public static string Refresh = "Grid_Refresh";
            public static string ViewContainer = "Grid_ViewContainerClass";
            public static string OpenChart = "Grid_OpenChartClass";
            public static string CloseChart = "Grid_CloseChartClass";
            public static string SortColumn = "Grid_SortColumnClass";

            
        }

        public static class Entity
        {
            public static string Close = "Entity_Close";
            public static string Tab = "Entity_TabId";
            public static string Save = "Entity_Save";
            public static string ContentTable = "Entity_ContentTable";
            public static string SelectForm = "Entity_SelectForm";
            public static string LookupRender = "Entity_LookupRenderClass";
            public static string Popout = "Entity_PopoutClass";
        }

        public static class GlobalSearch
        {
            public static string Filter = "Search_Filter";
            public static string SearchText = "Search_Text";
            public static string SearchButton = "Search_Button";
            public static string SearchResults = "Search_Result";
            public static string Container = "Search_Container";
            public static string EntityContainersId = "Search_EntityContainersId";
            public static string EntityNameId = "Search_EntityNameId";
            public static string RecordNameId = "Search_RecordNameId";
        }

        public static class ActivityFeed
        {
            public static string NotesControl = "Notes_NotesControl";
            public static string NotesWall = "Notes_NotesWall";
            public static string NotesText = "Notes_NotesText";
            public static string NotesButton = "Notes_NoteButton";
            public static string PostWall = "Notes_PostWall";
            public static string PostText = "Notes_PostText";
            public static string PostButton = "Notes_PostButton";
            public static string ActivityWall = "Notes_ActivityWall";
            public static string ActivityStatusFilter = "Notes_ActivityStatusFilter";
            public static string ActivityStatusFilterDialog = "Notes_ActivityStatusFilterDialog";
            public static string ActivityStatusAll = "Notes_ActivityStatusAll";
            public static string ActivityStatusOpen = "Notes_ActivityStatusOpen";
            public static string ActivitySTatusOverdue = "Notes_ActivityStatusOverdue";
            public static string ActivityAssociatedView = "Notes_ActivityAssociatedView";
            public static string ActivityPhoneCallDescriptionId = "Notes_ActivityPhoneCallDescId";
            public static string ActivityPhoneCallVoiceMailId = "Notes_ActivityPhoneCallVoiceMailId";
            public static string ActivityPhoneCallDirectionId = "Notes_ActivityPhoneCallDirectionId";
            public static string ActivityPhoneCallOk = "Notes_ActivityPhoneCallOk";
            public static string ActivityTaskOk = "Notes_ActivityTaskOk";
            public static string ActivityTaskSubjectId = "Notes_ActivityTaskSubjectId";
            public static string ActivityTaskDescriptionId = "Notes_ActivityTaskDescriptionId";
            public static string ActivityTaskScheduledEndId = "Notes_ActivityTaskScheduledEndId";
            public static string ActivityTaskPriorityId = "Notes_ActivityTaskPriorityId";
            public static string ActivityMoreActivities = "Notes_ActivityMoreActivities";
            public static string ActivityAddEmail = "Notes_ActivityAddEmail";
            public static string ActivityAddAppointment = "Notes_ActivityAddAppointment";
            public static string ActivityAddPhoneCall = "Notes_ActivityAddPhoneCall";
            public static string ActivityAddTask = "Notes_ActivityAddTask";

        }

        public static class DashBoard
        {
            public static string NotesControl = "Notes_NotesControl";
            public static string NotesWall = "Notes_NotesWall";
            public static string Selector = "DashBoard_Selector";
            public static string ViewContainerClass = "DashBoard_ViewContainerClass";
            
        }

        public static class CommandBar
        {
            public static string RibbonManager = "CommandBar_RibbonManager";
            public static string List = "CommandBar_List";
            public static string MoreCommands = "CommandBar_MoreCommands";
            public static string FlyoutAnchorArrow = "CommandBar_FlyoutAnchorArrow";

        }
        public static class Login
        {
            public static string UserId = "Login_UserId";
            public static string Password = "Login_Password";
            public static string SignIn = "Login_SignIn";

        }
        public static class GuidedHelp
        {
            public static string MarsOverlay = "GuidedHelp_MarsOverlay";
            public static string ButBegin = "GuidedHelp_ButBegin";
            public static string ButtonClose = "GuidedHelp_ButtonClose";

        }
        public static class Notification
        {
            public static string AppMessageBar = "Notification_AppMessageBar";
            public static string Close = "Notification_Close";
            public static string MessageBarRow = "Notification_MessageBarRowClass";
            public static string MessageBarButtonContainer = "Notification_MessageBarButtonContainerClass";
            public static string MessageBarMessage = "Notification_MessageBarMessageClass";
            public static string MessageBarTitle = "Notification_MessageBarTitleClass";

        }
        public static class Login
        {
            public static string UserId = "Login_UserId";
            public static string Password = "Login_Password";
            public static string SignIn = "Login_SignIn";
        }
    }
}
