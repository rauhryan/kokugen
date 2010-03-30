<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Board.EditBoardColumnForm"%>
<%@ Import Namespace="HtmlTags"%>
<%@ Import Namespace="Kokugen.Web.Actions.Board"%>
<%@ Import Namespace="Kokugen.Web.Conventions"%>

<div id="new-column-container" class="hidden">
<%= this.FormFor(new BoardColumnInputModel()).Id("column-form")%>
       
    <%= this.Edit(x => x.Column.Name) %>
    <%= this.Edit(x => x.Column.Description)%>
    <%= this.Edit(x => x.Column.Limit)%>
    <%= this.InputFor(x => x.Id).Hide() %>
    
     <input type="submit" name="Submit" value="Save" />
</form>
</div>

<script type="text/javascript">

    function closeColumnDialog(response) {

        $("#new-column-container").slideToggle('slow');
        // would want to update list here too
    }
    
    function validateAndSave() {
        var options = {
            success: closeColumnDialog,  // post-submit callback 
            type: 'post',        // 'get' or 'post', override for form's 'method' attribute 
            dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type) 
            clearForm: true        // clear all form fields after successful submit 
        };
        var isValid = $("#column-form").valid();

        if (isValid) {
            $("#column-form").ajaxSubmit(options);
        }
        
    }

    $(document).ready(function() {
    $("#column-form").validate({ errorClass: "error" });

    //$("#column-form-container").dialog({ title: "Add Column", autoOpen: false, width: 450, buttons: { "Cancel": function() { $("#company-form-container").dialog('close'); }, "Save": function() { validateAndSave(); } } });


        // bind form using 'ajaxForm'
    $('#column-form').ajaxForm(options);
    });
</script>