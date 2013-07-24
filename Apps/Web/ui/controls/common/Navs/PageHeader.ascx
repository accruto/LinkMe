<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PageHeader.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.Navs.PageHeader" %>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>
<%@ Register TagPrefix="uc" TagName="MemberHeader" Src="~/ui/controls/common/navs/MemberHeader.ascx" %>
<%@ Register TagPrefix="uc" TagName="OtdMemberHeader" Src="~/ui/controls/common/navs/Verticals/OtdMemberHeader.ascx" %>
<%@ Register TagPrefix="uc" TagName="SharedMemberHeader" Src="~/ui/controls/common/navs/Verticals/SharedMemberHeader.ascx" %>
<%@ Register TagPrefix="uc" TagName="EmployerHeader" Src="~/ui/controls/common/navs/EmployerHeader.ascx" %>
<%@ Register TagPrefix="uc" TagName="AutopeopleEmployerHeader" Src="~/ui/controls/common/navs/Verticals/AutopeopleEmployerHeader.ascx" %>
<%@ Register TagPrefix="uc" TagName="OtdEmployerHeader" Src="~/ui/controls/common/navs/Verticals/OtdEmployerHeader.ascx" %>
<%@ Register TagPrefix="uc" TagName="VecciEmployerHeader" Src="~/ui/controls/common/navs/Verticals/VecciEmployerHeader.ascx" %>
<%@ Register TagPrefix="uc" TagName="SharedEmployerHeader" Src="~/ui/controls/common/navs/Verticals/SharedEmployerHeader.ascx" %>
<%@ Register TagPrefix="uc" TagName="AdministratorHeader" Src="~/ui/controls/common/navs/AdministratorHeader.ascx" %>
<%@ Register TagPrefix="uc" TagName="CustodianHeader" Src="~/ui/controls/common/navs/CustodianHeader.ascx" %>

<%  var activeUserType = ActiveUserType;
    if (activeUserType == UserType.Member)
    {
        if (ActiveVerticalHeader == "otd")
        { %>
            <uc:OtdMemberHeader ID="ucOtdMemberHeader" runat="server" />
<%      }
        else if (ActiveVerticalHeader == "shared")
        { %>
            <uc:SharedMemberHeader ID="ucSharedMemberHeader" runat="server" />
<%      }
        else
        { %>
            <uc:MemberHeader ID="ucMemberHeader" runat="server" />
<%      }
    }
    else if (activeUserType == UserType.Employer)
    {
        if (ActiveVerticalHeader == "autopeople")
        { %>
            <uc:AutopeopleEmployerHeader ID="ucAutopeopleEmployerHeader" runat="server" />
<%      }
        else if (ActiveVerticalHeader == "otd")
        { %>                    
            <uc:OtdEmployerHeader ID="ucOtdEmployerHeader" runat="server" />
<%      }
        else if (ActiveVerticalHeader == "vecci")
        { %>                    
            <uc:VecciEmployerHeader ID="ucVecciEmployerHeader" runat="server" />
<%      }
        else if (ActiveVerticalHeader == "shared")
        { %>                    
            <uc:SharedEmployerHeader ID="ucSharedEmployerHeader" runat="server" />
<%      }
        else
        { %>
            <uc:EmployerHeader ID="ucEmployerHeader" runat="server" />
<%      }
    }
    else if (activeUserType == UserType.Administrator)
    { %>
        <uc:AdministratorHeader ID="ucAdministratorHeader" runat="server" />
<%  }
    else if (activeUserType == UserType.Custodian)
    { %>    
        <uc:CustodianHeader ID="ucCustodianHeader" runat="server" />
<%  } %>
