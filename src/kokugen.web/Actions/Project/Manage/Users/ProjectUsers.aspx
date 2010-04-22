﻿<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Project.Manage.Users.ProjectUsers"
    MasterPageFile="~/Shared/Project.Master" %>

<%@ Import Namespace="Kokugen.Web.Actions.Project.Manage.Users.Add" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=this.Script("projectusers.js") %>
    <script type="text/javascript">
      $(function () {

             // bind resizing
        $(window).resize(setUserColumnHeight);

        // Set the initial height of the sortables based on window size
        setUserColumnHeight();

        function setUserColumnHeight(){
            $('#project-users-list').attr("style","height:"+ $(window).height() + "px;");
        };

            var users = <%=Model.Users.ToJson() %>;
            
            for(var i in users){
                var user = new User(users[i]);

                _users.push(user);

                $('#project-users-list').append(buildUserWidget(user));
            }

            $('#add-user').ajaxDialog({onComplete:addUserToList,dataType:'json'});

            function addUserToList(data){
                var $data = JSON.parse(data);
                if($data.Success){
                    var user = new User($data.Item);
                    _users.push(user);
                    var widget = buildUserWidget(user);
                    makeDraggable(widget);
                    $('#project-users-list').append(widget);
                }else{
                    $.jGrowl('Error occured adding the user to the project!', {sticky:true,theme:'jgrowl-error'});
                }
            };

            var roles = <%=Model.Roles.ToJson() %>;

            for(var i in roles){
                var role = new Role(roles[i]);

                _roles.push(role);

                var widget = buildRoleWidget(role);
                $('#role-widget-list').append(widget);
            }


            $(".ui-draggable").each(function(){makeDraggable(this);});
            $(".ui-sortable").disableSelection();

            function makeDraggable(element){
                $(element).draggable({
                connectToSortable:".ui-sortable", 
                helper: function(element, ui){
                    return buildUserWidget($(element.currentTarget).data('User'));
                }, 
                revert:'invalid',
                start:function(event, ui){
                    $(ui.helper).find('.delete').remove();
                }
            });
            };
      });

    </script>
    <style type="text/css">
        .user {width:230px; font-size:1.1em;}
        .user > * {display:inline;}
        
        .user-left-side
        {
        	float: left;
            height: 100%;
            width: 250px;
            
        }
        .user-role-area
        {
           margin-left:-250px;
            height: 100%;
            overflow:hidden;
        }
        .user-role-area ul
        {
            height: 100%;
            overflow:hidden;
        }
        .user-left-side ul
        {
        	background-color:Red;
            height:100%;
        }
        div.role-body
        {
            height: 200px;
        }
        .role-body .ui-sortable
        {
            height: 100%;
        }
        .ui-sortable .user-placeholder
        {
            background-color: #CC4B3A;
            border-color: #903529 !important;
        }
        .ui-sortable li
        {
            padding: 5px;
        }

        .ui-draggable { padding:5px;}
        .ui-sortable > .user > .delete {display:none;}

       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
<div id="manage-users-wrapper">
    <div id="project-users-toolbar">
        <%=this.LinkTo(new AddUserToProjectRequest(){Id = Model.ProjectId}).Text("Add User").Id("add-user")%></div>
    
        <div class="user-left-side">
           <ul id="project-users-list">

            </ul>
        </div>
        <div class="user-role-area">
            <ul id="role-widget-list">
            </ul>
        </div>
</div>
</asp:Content>
