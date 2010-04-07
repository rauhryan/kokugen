<%@ Page Language="C#" Inherits="Kokugen.Web.Actions.Board.ViewBoard" AutoEventWireup="true" MasterPageFile="~/Shared/Site.Master" %>
<%@ Import Namespace="Kokugen.Web.Actions.Card"%>
<%@ Import Namespace="Kokugen.Web"%>
<%@ Import Namespace="Kokugen.Web.Actions.BoardColumn"%>
<%@ Import Namespace="FubuMVC.Core.Urls"%>
<%@ Import Namespace="Kokugen.Web.Actions.Company"%>
<%@ Import Namespace="Kokugen.Web.Actions.Board"%>
<asp:Content ID="columnconfigHead" ContentPlaceHolderID="head" runat="server">
<%= this.CSS("board.css")%>
<%= this.Script("board.js") %>
<script type="text/javascript">
    var cards = <%= Model.AllCards.ToJson() %>
    $(document).ready(function() {

        // bind resizing
        $(window).resize(setCardColumnHeight);

        // Set the initial height of the sortables based on window size
        setCardColumnHeight();

        $("#add-story-button").click(function() {
            $("#compact-card-container").slideToggle('slow');
        });

        $(".ui-sortable").sortable({ connectWith: '.ui-sortable', placeholder: 'phase-placeholder', forcePlaceholderSize: true,
            receive: cardMoved, over: cardOverColumn });
        $(".ui-sortable").disableSelection();
        for(var i = 0; i < cards.length; i++)
        {
            var newCard = new Card(cards[i]);
            _cards.push(newCard);
            
            var hcard = buildCardDisplay(newCard);
            
            $('#'+ newCard.ColumnId).append(hcard);
        }
        
        $("div.column").each(function() {
        var width = 100 / $("div.column").length;
            $(this).attr("style", "width: "+ width + "%");});
    });

    function setCardColumnHeight() {
        var newHeight = ($(window).height() - $(".card-list").position().top)-20;
        $(".card-list").attr("style", "min-height: " + newHeight + "px;");
    }

</script>
</asp:Content>
<asp:Content ID="BoardNav" ContentPlaceHolderID="extraNavigation" runat="server">
<li class="lbar"><a href="#" id="add-story-button">Add Story</a></li>
</asp:Content>
<asp:Content ID="THISCONTENTAREAID" ContentPlaceHolderID="mainContent" runat="server">
<% this.Partial(new CompactCardFormInput{ Id = Model.Id}); %>
<div class="board">

    <div id="backlog-container"  class="column">
        <div class="board-phase-header"><%= Model.BackLog.Name %></div>
        <ul class="card-list ui-sortable" id="<%= Model.BackLog.Id %>" limit="<%= Model.BackLog.Limit %>"></ul>
    </div>

    <%= this.PartialForEach(m => m.Columns).WithoutItemWrapper().WithoutListWrapper().Using<BoardPhase_Control>() %>

    <div id="archive-container" class="column">
        <div class="board-phase-header"><%= Model.Archive.Name %></div>
        <ul class="card-list ui-sortable" id="<%= Model.Archive.Id %>" limit="<%= Model.Archive.Limit %>"></ul>
    </div>
</div>
</asp:Content>